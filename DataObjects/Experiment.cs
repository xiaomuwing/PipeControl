using InstrumentsCtrl;
using Keithley;
using Newtonsoft.Json;
using PipeControl.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;

namespace DataObjects
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public sealed class Experiment
    {
        public event EventHandler OnDataRefresh;
        public event EventHandler OnReloadEnd;
        public event EventHandler OnChangeChannelConfig;
        public event EventHandler OnControlEnd;
        /// <summary>
        /// 试验编号
        /// </summary>
        public long ID { get; set; } = 0;
        /// <summary>
        /// 试验名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 试验备注
        /// </summary>
        public string Commet { get; set; } = "";
        /// <summary>
        /// 试验创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 试验最后运行时间
        /// </summary>
        public DateTime LastRunDate { get; set; }
        /// <summary>
        /// 采样频率
        /// </summary>
        public int XDelta { get; set; } = 1000;
        /// <summary>
        /// 执行过程列表
        /// </summary>
        public List<ControlCircle> ControlCircles { get; set; } = new();
        /// <summary>
        /// 所有测点列表
        /// </summary>
        public List<Channel> Channels { get; set; } = new();
        [JsonIgnore]
        /// <summary>
        /// 试验保存数据和配置文件的根目录
        /// </summary>
        public string MainDirectory { get; set; } = "";
        /// <summary>
        /// 试验配置文件，保存在历史数据文件夹内
        /// </summary>
        [JsonIgnore]
        public string ConfigFile { get; set; }
        /// <summary>
        /// 试验主路径
        /// </summary>
        [JsonIgnore]
        public DirectoryInfo MainDirInfo { get; set; }
        /// <summary>
        /// 设备管理对象
        /// </summary>
        [JsonIgnore]
        public Instruments Instruments { get; set; }
        /// <summary>
        /// 标识是否在控温状态
        /// </summary>
        [JsonIgnore]
        public bool IsControl { get; set; }
        /// <summary>
        /// 标识是否在保存数据
        /// </summary>
        [JsonIgnore]
        public bool IsSavingData { get; set; }
        [JsonIgnore]
        public bool IsDemo { get; set; }
        /// <summary>
        /// 当前数据保存路径
        /// </summary>
        [JsonIgnore]
        public string SavingDataPath { get; private set; }
        [JsonIgnore]
        public ControlCircle CurrentCircle { get; private set; }
        [JsonIgnore]
        public TimeSpan ControlLast { get; private set; }
        private readonly MMTimer freshDataTimer = new();
        private int currentCircleID = 0;
        private DateTime controlBegin;

        public Experiment(string name, long id, bool createDir = true)
        {
            Name = name;
            ID = id;
            MainDirectory = AppDomain.CurrentDomain.BaseDirectory + "experiments\\" + name + "\\";
            MainDirInfo = new(MainDirectory);
            if (createDir)
            {
                MainDirInfo.Create();
            }
            CreateDate = DateTime.Now;
            ConfigFile = MainDirectory + name + ".json";
        }
        public void InitTimer()
        {
            if (freshDataTimer.IsRunning)
            {
                freshDataTimer.Stop();
            }
            freshDataTimer.Mode = TimerMode.Periodic;
            freshDataTimer.Period = XDelta;
            freshDataTimer.Tick += FreshDataTimer_Tick;
            freshDataTimer.Start();
        }
        public List<Channel> GetChannels(DeviceType deviceType)
        {
            List<Channel> result = new();
            foreach (Channel channel in Channels)
            {
                if (channel.DeviceType == deviceType)
                {
                    result.Add(channel);
                }
            }
            return result;
        }
        public Channel GetChannelByName(string name)
        {
            foreach (Channel ch in Channels)
            {
                if (ch.Name == name)
                {
                    return ch;
                }
            }
            return null;
        }
        public Channel GetChannelBySN(string sn)
        {
            foreach (Channel ch in Channels)
            {
                if (ch.SN == sn)
                {
                    return ch;
                }
            }
            return null;
        }
        public Channel GetChannel(string deviceAddress, string channelAddress)
        {
            foreach (var channel in Channels)
            {
                if (channel.HardwareDeviceAddress == deviceAddress && channel.HardwareChannelAddress == channelAddress)
                {
                    return channel;
                }
            }
            return null;
        }
        public List<Channel> GetTempChannels()
        {
            List<Channel> channels = new();
            foreach (Channel c in Channels)
            {
                if (c.Function != ChannelFunction.热源)
                {
                    channels.Add(c);
                }
            }
            return channels;
        }
        public List<Channel> GetDCChannels()
        {
            List<Channel> channels = new();
            foreach (Channel c in Channels)
            {
                if (c.Function == ChannelFunction.热源)
                {
                    channels.Add(c);
                }
            }
            return channels;
        }
        public static List<IPower> GetPowers(List<Channel> channels)
        {
            var powers = new List<IPower>();
            foreach (var channel in channels)
            {
                switch (channel.DeviceModel)
                {
                    case DeviceModel.N6700B:
                        N6700 n6700b = new(channel.HardwareDeviceAddress, channel.HardwareDevicePort);
                        n6700b.SN = channel.SN;
                        n6700b.DeviceModel = channel.DeviceModel;
                        n6700b.ChannelNo = "(@" + channel.HardwareChannelAddress + ")";
                        n6700b.MaxCurrent = (float)channel.Imax;
                        n6700b.MaxVolt = (float)channel.Vmax;
                        powers.Add(n6700b);
                        break;
                    case DeviceModel.N6702A:
                        N6700 n6702a = new(channel.HardwareDeviceAddress, channel.HardwareDevicePort);
                        n6702a.SN = channel.SN;
                        n6702a.DeviceModel = channel.DeviceModel;
                        n6702a.ChannelNo = "(@" + channel.HardwareChannelAddress + ")";
                        n6702a.MaxCurrent = (float)channel.Imax;
                        n6702a.MaxVolt = (float)channel.Vmax;
                        powers.Add(n6702a);
                        break;
                    case DeviceModel.SP_1U:
                        SP_1U sp_1u = new(channel.HardwareDeviceAddress, channel.HardwareDevicePort);
                        sp_1u.SN = channel.SN;
                        sp_1u.DeviceModel = channel.DeviceModel;
                        sp_1u.MaxCurrent = (float)channel.Imax;
                        sp_1u.MaxVolt = (float)channel.Vmax;
                        powers.Add(sp_1u);
                        break;
                    case DeviceModel.IT6726G:
                        IT6700 it6726G = new(channel.HardwareDeviceAddress, 9600, StopBits.One, Parity.None, 8);
                        it6726G.SN = channel.SN;
                        it6726G.DeviceModel = channel.DeviceModel;
                        it6726G.MaxCurrent = (float)channel.Imax;
                        it6726G.MaxVolt = (float)channel.Vmax;
                        powers.Add(it6726G);
                        break;
                    case DeviceModel.IT6722A:
                        IT6700 it6722a = new(channel.HardwareDeviceAddress, 9600, StopBits.One, Parity.None, 8);
                        it6722a.SN = channel.SN;
                        it6722a.DeviceModel = channel.DeviceModel;
                        it6722a.MaxCurrent = (float)channel.Imax;
                        it6722a.MaxVolt = (float)channel.Vmax;
                        powers.Add(it6722a);
                        break;
                    case DeviceModel.IT6832:
                        IT6832 it6832 = new(channel.HardwareDeviceAddress, 9600, StopBits.One, Parity.None, 8, 1);
                        it6832.SN = channel.SN;
                        it6832.DeviceModel = channel.DeviceModel;
                        it6832.MaxCurrent = (float)channel.Imax;
                        it6832.MaxVolt = (float)channel.Vmax;
                        powers.Add(it6832);
                        break;
                    case DeviceModel.IT_M3123:
                        IT_M3100 m3100 = new(channel.HardwareDeviceAddress, channel.HardwareDevicePort);
                        m3100.SN = channel.SN;
                        m3100.DeviceModel = channel.DeviceModel;
                        m3100.MaxCurrent = (float)channel.Imax;
                        m3100.MaxVolt = (float)channel.Vmax;
                        powers.Add(m3100);
                        break;
                }
            }
            return powers;
        }
        public static List<KEDevice> GetKEDevices(List<Channel> channels)
        {
            var devices = new List<KEDevice>();
            foreach (var channel in channels)
            {
                if (Exists(devices, channel.HardwareDeviceAddress))
                {
                    continue;
                }
                KEDevice device = new(channel.HardwareDeviceAddress, false, channel.HardwareDevicePort);
                if (device is null)
                {
                    continue;
                }
                device.DeviceModule = channel.DeviceModel.ToString();
                devices.Add(device);
            }
            return devices;
        }
        private static bool Exists(List<KEDevice> devices, string address)
        {
            foreach (KEDevice device in devices)
            {
                if (device.IPAddress == address)
                {
                    return true;
                }
            }
            return false;
        }
        private async void FreshDataTimer_Tick(object sender, EventArgs e)
        {
            GetCurrentTemp();

            if (!IsControl)
            {
                GetCurrentDC();
                OnDataRefresh?.Invoke(this, new EventArgs());
                return;
            }

            ControlLast += TimeSpan.FromSeconds(XDelta / 1000);
            //var tmp = GetTempChannels()[0];
            //var cnt = tmp.ControlDataCount;
            //if (cnt < 3)
            //{
            //    OnDataRefresh?.Invoke(this, new EventArgs());
            //    return;
            //}
            if (((int)ControlLast.TotalSeconds) % SystemConfig.ControlCycle == 0)
            {
                await CurrentCircle.ControlDC();
                GetCurrentDC();
            }
            else
            {
                GetCurrentDC();
            }
            if (ControlLast >= CurrentCircle.LastTime)
            {
                ControlLast = TimeSpan.Zero;
                currentCircleID += 1;
                Log.WriteLog(string.Format("========当前执行过程ID  {0}", currentCircleID), "Exp-FreshTimer", "Control");
                if (currentCircleID == ControlCircles.Count)
                {
                    IsControl = false;
                    IsSavingData = false;
                    await DisableCircle();
                    OnDataRefresh?.Invoke(this, EventArgs.Empty);
                    OnControlEnd?.Invoke(this, EventArgs.Empty);
                    Log.WriteLog(string.Format("全部执行过程执行完毕"), "Exp-FreshTimer", "Control");
                    return;
                }
                CurrentCircle = ControlCircles[currentCircleID];
                CurrentCircle.En = 1;
                CurrentCircle.r1 = ControlCircles[currentCircleID - 1].r1;
                CurrentCircle.uout = ControlCircles[currentCircleID - 1].uout;
                foreach (var line in CurrentCircle.Lines)
                {
                    var channel = GetChannelBySN(line.TempChannelSN);
                    channel.TargetTemp = line.TargetTemp;
                    channel.ControlType = line.ControlType;
                    foreach (var dcsn in line.DCChannelSNs)
                    {
                        var dcchannel = GetChannelBySN(dcsn);
                        dcchannel.IOut = line.IOut;
                        dcchannel.VOut = line.VOut;
                        dcchannel.TempMax = line.TempMax;
                        dcchannel.TempMin = line.TempMin;
                        dcchannel.ControlType = line.ControlType;
                    }
                }
                OnChangeChannelConfig?.Invoke(this, EventArgs.Empty);
            }
            OnDataRefresh?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// 读取当前各温度测点的温度值
        /// </summary>
        private void GetCurrentTemp()
        {
            var channels = GetTempChannels();
            foreach (Channel channel in channels)
            {
                if (channel == null)
                {
                    continue;
                }
                if (Instruments.KECtrl == null)
                {
                    channel.AddData(0, ChannelStatus.无设备, IsSavingData);
                    continue;
                }
                KEDevice device = Instruments.KECtrl.Devices.Devices.Single(x => x.IPAddress == channel.HardwareDeviceAddress);
                if (device is null)
                {
                    channel.AddData(0, ChannelStatus.无设备, IsSavingData);
                    continue;
                }
                if (!device.IsConnected)
                {
                    channel.AddData(0, ChannelStatus.断开连接, IsSavingData);
                    continue;
                }
                KEChannel kEChannel = Instruments.KECtrl.GetChannel(channel.HardwareDeviceAddress, channel.HardwareChannelAddress);
                if (kEChannel == null)
                {
                    channel.AddData(0, ChannelStatus.无设备, IsSavingData);
                    continue;
                }

                channel.AddData((double)kEChannel.LastData, ChannelStatus.正常, IsSavingData);
            }
        }
        private void GetCurrentDC()
        {
            var channels = GetDCChannels();
            foreach (Channel channel in channels)
            {
                if (channel == null)
                {
                    continue;
                }
                var power = Instruments.GetPower(channel.HardwareDeviceAddress, channel.HardwareChannelAddress);
                if (power is null)
                {
                    channel.AddData(0, 0, ChannelStatus.无设备, IsSavingData);
                    continue;
                }
                if (!power.Opened)
                {
                    channel.AddData(0, 0, ChannelStatus.断开连接, IsSavingData);
                    continue;
                }
                channel.AddData(power.CurrentV, power.CurrentI, ChannelStatus.正常, IsSavingData);
            }
        }
        public async Task Reload(string file)
        {
            var cnt = Channels.Count;
            ControlCircles = await EXCELCtrl.ReloadEXCEL(file);
            foreach (var channel in GetTempChannels())
            {
                channel.Function = ChannelFunction.监测温度点;
                channel.ControlType = ControlType.无;
            }
            foreach (var circle in ControlCircles)
            {
                circle.Parent = this;

            }
            var c = ControlCircles[0];
            foreach (var line in c.Lines)
            {
                var tempChannel = GetChannelBySN(line.TempChannelSN);
                tempChannel.Function = ChannelFunction.控温点;
                tempChannel.ControlType = line.ControlType;
                tempChannel.TargetTemp = line.TargetTemp;
                foreach (var dcsn in line.DCChannelSNs)
                {
                    var dcchannel = GetChannelBySN(dcsn);
                    dcchannel.IOut = line.IOut;
                    dcchannel.VOut = line.VOut;
                    dcchannel.TempMax = line.TempMax;
                    dcchannel.TempMin = line.TempMin;
                    dcchannel.ControlType = line.ControlType;
                }
            }
            cnt = Channels.Count;
            OnReloadEnd?.Invoke(this, EventArgs.Empty);
        }
        public void Save()
        {
            File.WriteAllText(ConfigFile, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
        public void Save(string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(this, Formatting.Indented));
        }
        public void BeginSaveData()
        {
            SavingDataPath = MainDirectory + "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\\";
            Directory.CreateDirectory(SavingDataPath);
            foreach (Channel channel in Channels)
            {
                channel.DChannel.TriggerTime.AsDATE = DateTime.Now;
                channel.SaveDataPath = SavingDataPath;
            }
            string file = SavingDataPath + "config.json";
            Save(file);
            IsSavingData = true;
            LastRunDate = DateTime.Now;
        }
        public async Task BeginControl()
        {
            currentCircleID = 0;
            CurrentCircle = ControlCircles[currentCircleID];
            controlBegin = DateTime.Now;
            ControlLast = TimeSpan.Zero;
            SavingDataPath = MainDirectory + "\\" + controlBegin.ToString("yyyyMMddHHmmss") + "\\";
            Directory.CreateDirectory(SavingDataPath);
            await EnableCircle();
            foreach (Channel channel in Channels)
            {
                channel.DChannel.TriggerTime.AsDATE = DateTime.Now;
                channel.SetAvgDataZero();
                channel.SaveDataPath = SavingDataPath;
            }
            foreach (var line in CurrentCircle.Lines)
            {
                var channel = GetChannelBySN(line.TempChannelSN);
                channel.TargetTemp = line.TargetTemp;
                channel.ControlType = line.ControlType;
                foreach (var dcsn in line.DCChannelSNs)
                {
                    var dcchannel = GetChannelBySN(dcsn);
                    dcchannel.IOut = line.IOut;
                    dcchannel.VOut = line.VOut;
                    dcchannel.TempMax = line.TempMax;
                    dcchannel.TempMin = line.TempMin;
                    dcchannel.ControlType = line.ControlType;
                }
            }
            string file = SavingDataPath + "config.json";
            IsSavingData = true;
            LastRunDate = DateTime.Now;
            IsControl = true;
            Save(file);
            Log.WriteLog(string.Format("======================当前执行过程ID  {0}", currentCircleID + 1), "Exp-BeginControl", "Control");
        }
        public async Task StopControl()
        {
            IsControl = false;
            IsSavingData = false;
            await DisableCircle();
        }
        private async Task EnableCircle()
        {
            CurrentCircle.En = 1;
            foreach (var line in CurrentCircle.Lines)
            {
                foreach (var sn in line.DCChannelSNs)
                {
                    var dc = GetChannelBySN(sn);
                    var power = Instruments.GetPower(dc.HardwareDeviceAddress, dc.HardwareChannelAddress);
                    if (!power.Opened)
                    {
                        continue;
                    }
                    await power.SetOutputOn();
                    if (line.ControlType == ControlType.PID)
                    {
                        await power.SetVoltOutput(power.MaxVolt);
                    }
                    dc.Enabled = true;
                }
            }
        }
        private async Task DisableCircle()
        {
            CurrentCircle.En = 0;
            foreach (var line in CurrentCircle.Lines)
            {
                foreach (var sn in line.DCChannelSNs)
                {
                    var dc = GetChannelBySN(sn);
                    var power = Instruments.GetPower(dc.HardwareDeviceAddress, dc.HardwareChannelAddress);
                    if (!power.Opened)
                    {
                        dc.AddData(0, 0, ChannelStatus.断开连接, IsSavingData);
                        continue;
                    }
                    await power.SetOutputOFF();
                    dc.AddData(0, 0, ChannelStatus.正常, IsSavingData);
                    dc.Enabled = false;
                }
            }
        }
        public async Task Close()
        {
            freshDataTimer?.Stop();
            foreach (Channel channel in Channels)
            {
                channel.DisposeDChannel();
            }
            Channels.Clear();
            ControlCircles.Clear();
            await Instruments?.Close();
        }
    }
}
