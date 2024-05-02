using HPSocket;
using HPSocket.Tcp;
using System;
using System.Text;
using System.Threading.Tasks;
namespace Agilent
{
    public class AgilentDevice : IDisposable
    {
        public event EventHandler OnConnected;
        public event EventHandler OnDisconnected;
        public int ID { get; set; }
        public string Name { get; set; }
        public string IPAddress { get; set; } = "192.1.2.136";
        public ushort Port { get; set; } = 5025;
        public bool IsConnected { get; private set; }
        public double LastCurr { get; private set; }
        public double Current { get; set; }
        public double Volt { get; set; }
        public double Resistance { get; set; }
        public bool IsDemo { get; set; }
        public bool IsOutput { get; private set; }
        private readonly TcpClient client;
        private readonly System.Timers.Timer mmTimer;
        private readonly Random random;
        public AgilentDevice(string ip, ushort port, bool demo = false)
        {

            IPAddress = ip;
            Port = port;
            IsDemo = demo;
            if(IsDemo)
            {
                random = new Random(Guid.NewGuid().GetHashCode());
            }
            client = new();
            client.OnClose += Client_OnClose;
            client.OnConnect += Client_OnConnect;
            client.OnReceive += Client_OnReceive;
            client.Connect(IPAddress, Port);

            mmTimer = new();
            mmTimer.Interval = 1000;
            mmTimer.Elapsed += MmTimer_Elapsed;
            mmTimer.AutoReset = true;
        }
        public async void Connect()
        {
            if (IsConnected)
                return;
            if (IsDemo)
            {
                IsConnected = true;
                await Task.Delay(300);
                OnConnected?.Invoke(this, new EventArgs());
                //mmTimer.Start();
                return;
            }
            client.Connect();
        }
        public void Enable(bool enable)
        {
            if (IsConnected)
            {
                IsOutput = enable;
                string command;
                if (IsOutput)
                {
                    command = "OUTP ON";
                }
                else
                {
                    Output(0, 0);
                    command = "OUTP OFF";
                }
                SendCommand(command);
            }
        }
        public void StopReadVolt()
        {
            mmTimer.Stop();
        }
        public void Output(double curr, double volt)
        {
            Current = curr;
            LastCurr = curr;
            string str = "VOLT " + volt.ToString("0.000") + ";CURR " + Current.ToString("0.000");
            byte[] b = GetKeySightCommand(str);
            client.Send(b, b.Length);
        }
        public void Output(double volt)
        {
            //Volt = volt;
            string str = "VOLT " + volt.ToString("0.000");
            byte[] b = GetKeySightCommand(str);
            client.Send(b, b.Length);
        }
        public void OutputOff()
        {
            string str = "OUTP OFF";
            byte[] b = GetKeySightCommand(str);
            client.Send(b, b.Length);
        }
        private void MmTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if(IsDemo)
            {
                UpdateRandomData();
                return;
            }
            string str = "Meas:Volt?";
            byte[] b = GetKeySightCommand(str);
            client.Send(b, b.Length);
        }
        private void UpdateRandomData()
        {
            Volt = RandomData();
        }
        private double RandomData()
        {
            double result = 10 / 2D + random.NextDouble();
            return result;
        }
        private HandleResult Client_OnConnect(IClient sender)
        {
            IsConnected = true;
            mmTimer.Start();
            OnConnected?.Invoke(this, new EventArgs());
            return HandleResult.Ok;
        }
        private HandleResult Client_OnReceive(IClient sender, byte[] data)
        {
            string str = Encoding.Default.GetString(data);
            try
            {
                Volt = ChangeToDecimal(str);
                if (Current > 0)
                {
                    Resistance = Volt / Current;
                }
            }
            catch { }
            return HandleResult.Ok;
        }
        private HandleResult Client_OnClose(IClient sender, SocketOperation socketOperation, int errorCode)
        {
            if (IsDemo)
            {
                return HandleResult.Ok;
            }
            OnDisconnected?.Invoke(this, new EventArgs());
            IsConnected = false;
            return HandleResult.Ok;
        }
        private void SendCommand(string str)
        {
            byte[] b = GetKeySightCommand(str);
            client.Send(b, b.Length);
        }
        static byte[] GetKeySightCommand(string command)
        {
            byte[] b = Encoding.Default.GetBytes(command);
            byte[] b2 = new byte[b.Length + 1];
            Array.Copy(b, b2, b.Length);
            b2[b.Length] = 10;
            return b2;
        }
        static double ChangeToDecimal(string strData)
        {
            double dData;
            if (strData.Contains("E"))
            {
                dData = Convert.ToDouble(decimal.Parse(strData.ToString(), System.Globalization.NumberStyles.Float));
            }
            else
            {
                dData = Convert.ToDouble(strData);
            }
            return dData;
        }
        public void Dispose()
        {
            string str = "OUTP OFF";
            byte[] b = GetKeySightCommand(str);
            client.Send(b, b.Length);
            client.Dispose();
            mmTimer.Stop();
            mmTimer.Dispose();
        }
    }
}
