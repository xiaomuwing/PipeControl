using HPSocket;
using HPSocket.Tcp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keithley
{
    [JsonObject(MemberSerialization.OptOut)]
    [Serializable]
    public sealed class KEDevice : IDisposable
    {
        public event EventHandler OnConnected;
        public event EventHandler<MsgEventArgs> OnSendCommand;
        public event EventHandler<MsgEventArgs> OnReceived;
        public event EventHandler OnTriggerPrepareEnd;
        public event EventHandler OnDisconnected;
        public string IPAddress { get; set; }
        public ushort Port { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string DeviceModule { get; set; }
        public List<KEChannel> Channels { get; set; }
        public int MaxTrace { get; set; } = 45;
        [JsonIgnore]
        public bool IsDemo { get; private set; }
        [JsonIgnore]
        public bool IsConnected { get; private set; } = false;
        [JsonIgnore]
        public bool IsReadData { get; private set; } = false;
        private TcpClient client;
        private readonly List<string> commands = new();
        private int currentCommandNumber = 0;
        private int traceCount = 0;
        private int trace = 0;
        private int dev = 0;
        private bool getData = false;
        private readonly MMTimer demoTimer;
        private readonly Random random;
        private static readonly R2TCtrl rt = new();
        public KEDevice(string ip, bool demo, ushort port)
        {
            IPAddress = ip;
            IsDemo = demo;
            Port = port;
            Channels = new();
            if (demo)
            {
                random = new Random(Guid.NewGuid().GetHashCode());
                demoTimer = new();
                demoTimer.Mode = TimerMode.Periodic;
                demoTimer.Period = 1000;
                demoTimer.Tick += DemoTimer_Tick;
            }
        }
        private void DemoTimer_Tick(object sender, EventArgs e)
        {
            UpdateRandomData();
        }
        private double RandomData(KEChannel channel)
        {
            double result = int.Parse(channel.Address) / 2D + random.NextDouble();
            return result;
        }
        public void CreateChannels()
        {
            for (int i = 1; i <= 40; i++)
            {
                KEChannel channel = new(i, "1" + i.ToString("00"), IPAddress);
                Channels.Add(channel);
            }
            for (int i = 1; i <= 40; i++)
            {
                KEChannel channel = new(i, "2" + i.ToString("00"), IPAddress);
                Channels.Add(channel);
            }
        }
        private void ReadPreparedCommands()
        {
            using FileStream fs = new("commands\\PrepareCommands.txt", FileMode.Open);
            StreamReader sr = new(fs);
            while (!sr.EndOfStream)
            {
                string str = sr.ReadLine();
                commands.Add(str);
            }
            sr.Close();
            sr.Dispose();
            fs.Close();
        }
        public async void Connect()
        {
            if (IsDemo)
            {
                await Task.Delay(200);
                IsConnected = true;
                OnConnected?.Invoke(this, new EventArgs());
            }
            else
            {
                client = new();
                client.OnClose += Client_OnClose;
                client.OnConnect += Client_OnConnect;
                client.OnReceive += Client_OnReceive;
                client.Connect(IPAddress, Port);
            }

        }
        public void PrepareCommands()
        {
            if (IsDemo)
            {
                foreach (KEChannel channel in Channels)
                {
                    channel.IsConfiged = true;
                }
                return;
            }

            commands.Clear();
            currentCommandNumber = 0;
            trace = 0;
            traceCount = 0;
            dev = 0;
            getData = false;
            IsReadData = false;
            int configedChannelCount = Channels.Where(x => x.IsConfiged == true).Count();
            if (configedChannelCount == 0)
            {
                return;
            }
            ReadPreparedCommands();

            string command = string.Empty;
            List<string> strs = new();
            string channelList = string.Empty;
            int channelCount = 0;
            foreach (KEChannel channel in Channels.Where(x => x.IsConfiged == true))
            {
                command = string.Empty;
                switch (channel.ChannelType)
                {
                    case ChannelType.交流电压:
                        command = "sens:func 'volt:ac', (@" + channel.Address + ")";
                        break;
                    case ChannelType.温度:
                        if (channel.FourWireRTDType == FourWireRTDType.PT1000)
                        {
                            command = "sens:func 'fres', (@" + channel.Address + ")";
                        }
                        else
                        {
                            command = "sens:func 'temp', (@" + channel.Address + ")";
                        }
                        break;
                    case ChannelType.直流电压:
                        command = "sens:func 'volt:dc', (@" + channel.Address + ")";
                        break;
                    case ChannelType.两线电阻:
                        command = "sens:func 'res', (@" + channel.Address + ")";
                        break;
                    case ChannelType.四线电阻:
                        command = "sens:func 'fres', (@" + channel.Address + ")";
                        break;
                }
                if (!string.IsNullOrEmpty(command))
                    commands.Add(command);
                channelList += channel.Address + ",";
                channelCount += 1;
            }
            foreach (KEChannel channel in Channels.Where(x => x.IsConfiged == true))
            {
                switch (channel.ChannelType)
                {
                    case ChannelType.交流电压:
                        strs = ReadVoltCommands("commands\\accommands.txt", channel.Address);
                        foreach (string str in strs)
                        {
                            commands.Add(str);
                        }
                        break;
                    case ChannelType.温度:
                        switch (channel.TransducerType)
                        {
                            case TransducerType.热电偶:
                                if (channel.ColdJunc)
                                {
                                    strs = ReadTempThermocoupleCommands("commands\\tempThermocoupleWithCold.txt", channel.Address, channel.ThermocoupleType);
                                }
                                else
                                {
                                    strs = ReadTempThermocoupleCommands("commands\\tempThermocouple.txt", channel.Address, channel.ThermocoupleType);
                                }
                                foreach (string str in strs)
                                {
                                    commands.Add(str);
                                }
                                break;
                            case TransducerType.热敏电阻:
                                strs = ReadTempThermistorCommands("commands\\TempThermistor.txt", channel.Address, channel.ThermistorType);
                                foreach (string str in strs)
                                {
                                    commands.Add(str);
                                }
                                break;
                            case TransducerType.RTD:
                                if (channel.FourWireRTDType == FourWireRTDType.PT1000)
                                {
                                    strs = ReadFresCommands("commands\\fres.txt", channel.Address);
                                    foreach (string str in strs)
                                    {
                                        commands.Add(str);
                                    }
                                }
                                else
                                {
                                    strs = ReadTempRTDCommands("commands\\TempRTD.txt", channel.Address, channel.FourWireRTDType);
                                    foreach (string str in strs)
                                    {
                                        commands.Add(str);
                                    }
                                }

                                break;
                        }
                        break;
                    case ChannelType.直流电压:
                        strs = ReadVoltCommands("commands\\dccommands.txt", channel.Address);
                        foreach (string str in strs)
                        {
                            commands.Add(str);
                        }
                        break;
                    case ChannelType.两线电阻:
                        strs = ReadResCommands("commands\\res.txt", channel.Address);
                        foreach (string str in strs)
                        {
                            commands.Add(str);
                        }
                        break;
                    case ChannelType.四线电阻:
                        strs = ReadFresCommands("commands\\fres.txt", channel.Address);
                        foreach (string str in strs)
                        {
                            commands.Add(str);
                        }
                        break;
                }
            }
            if (!string.IsNullOrEmpty(channelList))
            {
                channelList = channelList.Substring(0, channelList.Length - 1);
            }
            strs = ReadAfterAllCommands("commands\\afterall.txt", channelList, channelCount.ToString());
            foreach (string str in strs)
            {
                commands.Add(str);
            }
        }
        public void BeginTrigger()
        {
            if (IsDemo)
            {
                UpdateRandomData();
                demoTimer.Start();
                return;
            }
            if (commands.Count == 0)
            {
                return;
            }
            SendCommand(commands[currentCommandNumber]);
            SendCommand(":syst:err?");
        }
        private void UpdateRandomData()
        {
            foreach (KEChannel channel in Channels)
            {
                channel.LastData = RandomData(channel);
            }
        }
        public void BeginReadData()
        {
            IsReadData = true;
            SendCommand(":trac:next?");
        }
        public async void StopReadData()
        {
            if (!IsReadData)
            {
                return;
            }
            currentCommandNumber = -1;
            IsReadData = false;
            SendCommand("abort");
            await Task.Delay(50);
            SendCommand(":rout:scan:lsel none");
            await Task.Delay(50);
            SendCommand("SYST:LOC");
        }
        private KEChannel GetChannel(string address)
        {
            foreach (var channel in Channels)
            {
                if (channel.Address == address)
                {
                    return channel;
                }
            }
            return null;
        }
        private HandleResult Client_OnReceive(IClient sender, byte[] data)
        {

            string str = Encoding.Default.GetString(data);

            if (!IsReadData)
            {
                MsgEventArgs mea = new(str);
                OnReceived?.Invoke(this, mea);
                if (currentCommandNumber < 0)
                {
                    return HandleResult.Ok;
                }
                currentCommandNumber += 1;
                if (currentCommandNumber == commands.Count)
                {
                    OnTriggerPrepareEnd?.Invoke(this, new EventArgs());
                    BeginReadData();
                    return HandleResult.Ok;
                }
                if (commands.Count >= 1)
                {
                    if (!string.IsNullOrEmpty(commands[currentCommandNumber]))
                    {
                        SendCommand(commands[currentCommandNumber]);
                        SendCommand(":syst:err?");
                    }
                }
            }
            else
            {
                if (str.Contains("\n"))
                {
                    str = str.Substring(0, str.Length - 1);
                }
                if (getData)
                {
                    //Log.WriteLog(str, "RecvCommand", IPAddress);
                    string[] strs = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 2; i < strs.Length; i += 3)
                    {
                        KEChannel channel = GetChannel(strs[i]);
                        if (channel != null)
                        {
                            if (channel.ChannelType == ChannelType.直流电压)
                            {
                                channel.LastData = double.Parse(strs[i - 2]) * 1000;
                            }
                            else if (channel.ChannelType == ChannelType.温度 && channel.FourWireRTDType == FourWireRTDType.PT1000)
                            {
                                double om = double.Parse(strs[i - 2]);
                                channel.LastData = GetFourWireRTDByOm(om);
                            }
                            else
                            {
                                channel.LastData = double.Parse(strs[i - 2]);
                            }
                        }
                    }
                    MsgEventArgs mea = new(string.Format("receivecount={0}", strs.Length));
                    OnReceived?.Invoke(this, mea);
                    getData = false;
                    SendCommand(":trac:next?");
                }
                else
                {
                    if (int.TryParse(str, out trace))
                    {
                        if (trace == 0 && traceCount > 0)
                        {
                            dev = MaxTrace - traceCount;
                            string command = string.Format(":trac:data:sel? {0}, {1}", traceCount, dev);
                            MsgEventArgs mea = new(string.Format("trace={0};traceCount={1};dev={2}", trace, traceCount, dev));
                            OnReceived?.Invoke(this, mea);
                            traceCount = trace;
                            getData = true;
                            SendCommand(command);
                            return HandleResult.Ok;
                        }
                        if (trace > traceCount)
                        {
                            dev = trace - traceCount;
                            string command = string.Format(":trac:data:sel? {0}, {1}", traceCount, dev);
                            MsgEventArgs mea = new(string.Format("trace={0};traceCount={1};dev={2}", trace, traceCount, dev));
                            OnReceived?.Invoke(this, mea);
                            traceCount = trace;
                            getData = true;
                            SendCommand(command);
                        }
                        else
                        {
                            //Thread.Sleep(1000);
                            SendCommand(":trac:next?");
                        }
                    }
                    else
                    {
                        string[] strs = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (strs.Length % 3 != 0)
                        {
                            getData = false;
                            string command = string.Format(":trac:data:sel? {0}, {1}", traceCount, dev);
                            getData = true;
                            SendCommand(command);
                            return HandleResult.Ok;
                        }
                        for (int i = 2; i < strs.Length; i += 3)
                        {
                            KEChannel channel = Channels.Single(x => x.Address == strs[i]);
                            channel.LastData = double.Parse(strs[i - 2]);
                        }
                        getData = false;
                        SendCommand(":trac:next?");
                    }
                }
            }
            return HandleResult.Ok;
        }
        private HandleResult Client_OnConnect(IClient sender)
        {
            IsConnected = true;
            OnConnected?.Invoke(this, new EventArgs());
            return HandleResult.Ok;
        }
        private HandleResult Client_OnClose(IClient sender, SocketOperation socketOperation, int errorCode)
        {
            OnDisconnected?.Invoke(this, new EventArgs());
            Log.WriteLog("Keithley device disconnected!", "KEDevice", IPAddress);
            return HandleResult.Ok;
        }
        public void SendCommand(string str)
        {
            if (IsDemo)
            {
                return;
            }
            byte[] b = GetCommand(str);
            try
            {
                if (client.State == ServiceState.Started)
                {
                    client.Send(b, b.Length);
                    MsgEventArgs mea = new(str);
                    OnSendCommand?.Invoke(this, mea);
                    if (!str.Contains("trac"))
                    {
                        Log.WriteLog(str, "SendCommand", IPAddress);
                    }
                }
            }
            catch { }
        }
        private static byte[] GetCommand(string command)
        {
            byte[] b = Encoding.Default.GetBytes(command);
            byte[] b2 = new byte[b.Length + 1];
            Array.Copy(b, b2, b.Length);
            b2[b.Length] = 10;
            return b2;
        }
        private static List<string> ReadVoltCommands(string file, string address)
        {
            List<string> strs = new();
            using FileStream fs = new(file, FileMode.Open);
            StreamReader sr = new(fs);
            while (!sr.EndOfStream)
            {
                string str = sr.ReadLine();
                str = str.Replace("101", address);
                strs.Add(str);
            }
            sr.Close();
            sr.Dispose();
            fs.Close();
            return strs;
        }
        private static List<string> ReadTempThermocoupleCommands(string file, string address, ThermocoupleType thermocoupleType)
        {
            List<string> strs = new();
            using FileStream fs = new(file, FileMode.Open);
            StreamReader sr = new(fs);
            while (!sr.EndOfStream)
            {
                string str = sr.ReadLine();
                str = str.Replace("101", address);
                if (str.Contains("type T"))
                {
                    str = str.Replace("type T", "type " + Enum.GetName(typeof(ThermocoupleType), thermocoupleType));
                }
                strs.Add(str);
            }
            sr.Close();
            sr.Dispose();
            fs.Close();
            return strs;
        }
        private static List<string> ReadTempThermistorCommands(string file, string address, ThermistorType thermistorType)
        {
            List<string> strs = new();
            using FileStream fs = new(file, FileMode.Open);
            StreamReader sr = new(fs);
            while (!sr.EndOfStream)
            {
                string str = sr.ReadLine();
                str = str.Replace("101", address);
                if (str.Contains("thermistorValue"))
                {
                    str = str.Replace("thermistorValue", ((int)thermistorType).ToString());
                }
                strs.Add(str);
            }
            sr.Close();
            sr.Dispose();
            fs.Close();
            return strs;
        }
        private static List<string> ReadTempRTDCommands(string file, string address, FourWireRTDType rtdType)
        {
            List<string> strs = new();
            using FileStream fs = new(file, FileMode.Open);
            StreamReader sr = new(fs);
            while (!sr.EndOfStream)
            {
                string str = sr.ReadLine();
                str = str.Replace("101", address);
                if (str.Contains("RTDType"))
                {
                    str = str.Replace("RTDType", Enum.GetName(typeof(FourWireRTDType), rtdType));
                }
                strs.Add(str);
            }
            sr.Close();
            sr.Dispose();
            fs.Close();
            return strs;
        }
        private static List<string> ReadResCommands(string file, string address)
        {
            List<string> strs = new();
            using FileStream fs = new(file, FileMode.Open);
            StreamReader sr = new(fs);
            while (!sr.EndOfStream)
            {
                string str = sr.ReadLine();
                str = str.Replace("101", address);
                strs.Add(str);
            }
            sr.Close();
            sr.Dispose();
            fs.Close();
            return strs;
        }
        private static List<string> ReadFresCommands(string file, string address)
        {
            List<string> strs = new();
            using FileStream fs = new(file, FileMode.Open);
            StreamReader sr = new(fs);
            while (!sr.EndOfStream)
            {
                string str = sr.ReadLine();
                str = str.Replace("101", address);
                strs.Add(str);
            }
            sr.Close();
            sr.Dispose();
            fs.Close();
            return strs;
        }
        private static List<string> ReadAfterAllCommands(string file, string channelList, string channelCount)
        {
            List<string> strs = new();
            using FileStream fs = new(file, FileMode.Open);
            StreamReader sr = new(fs);
            while (!sr.EndOfStream)
            {
                string str = sr.ReadLine();
                if (str.Contains("channelList"))
                {
                    str = str.Replace("channelList", channelList);
                }
                if (str.Contains("channelCount"))
                {
                    str = str.Replace("channelCount", channelCount);
                }
                strs.Add(str);
            }
            sr.Close();
            sr.Dispose();
            fs.Close();
            return strs;
        }
        private sealed class R2TCtrl
        {
            public List<R2T> R2TList = new();
            public R2TCtrl()
            {
                ReadData();
            }
            private void ReadData()
            {
                using FileStream fs = new("R2T.txt", FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new(fs);
                int id = 0;
                while (!streamReader.EndOfStream)
                {
                    string[] line = streamReader.ReadLine().Split(',');
                    R2T rt = new();
                    rt.ID = id;
                    rt.T = double.Parse(line[0]);
                    rt.R = double.Parse(line[1]);
                    rt.Rd = double.Parse(line[2]);
                    rt.Td = double.Parse(line[3]);
                    R2TList.Add(rt);
                    id++;
                }
                ;
            }
        }
        private sealed class R2T
        {
            public int ID { get; set; }
            /// <summary>
            /// 电阻
            /// </summary>
            public double R { get; set; }
            /// <summary>
            /// 温度
            /// </summary>
            public double T { get; set; }
            /// <summary>
            /// 电阻偏差
            /// </summary>
            public double Rd { get; set; }
            /// <summary>
            /// 温度偏差
            /// </summary>
            public double Td { get; set; }
        }
        private static double GetFourWireRTDByOm(double om)
        {
            if (om > rt.R2TList.Last().R || om < rt.R2TList.First().R)
            {
                return 0;
            }
            for (int i = 0; i < rt.R2TList.Count; i++)
            {
                if (rt.R2TList[i].R >= om && rt.R2TList[i - 1].R < om)
                {
                    double r1 = rt.R2TList[i - 1].R;
                    double r2 = rt.R2TList[i].R;
                    double t1 = rt.R2TList[i - 1].T;
                    double t2 = rt.R2TList[i].T;
                    double t = ((om - r1) / (r2 - r1)) * (t2 - t1) + t1;
                    return t;
                }
            }

            return 0;
        }
        public async void Dispose()
        {
            if (IsDemo)
            {
                demoTimer.Dispose();
                return;
            }
            StopReadData();
            await Task.Delay(200);
            client.Dispose();
        }
    }
}
