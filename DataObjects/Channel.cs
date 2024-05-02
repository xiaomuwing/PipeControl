using ImcCoreLib;
using InstrumentsCtrl;
using Keithley;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace DataObjects
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Channel : IEqualityComparer<Channel>
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string SN {  get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 按照硬件属性自动命名的测点名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户命名的测点名称
        /// </summary>
        public string UserName { get; set; } = "";
        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// 测点功能
        /// </summary>
        public ChannelFunction Function { get; set; } = ChannelFunction.无;
        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// 硬件设备类型
        /// </summary>
        public DeviceType DeviceType { get; set; } = DeviceType.无;
        /// <summary>
        /// 设备型号，有些型号据此推断挂载驱动
        /// </summary>
        public DeviceModel DeviceModel { get; set; } = DeviceModel.无;
        /// <summary>
        /// 硬件设备地址
        /// </summary>
        public string HardwareDeviceAddress { get; set; } = "";
        /// <summary>
        /// 硬件设备端口号
        /// </summary>
        public ushort HardwareDevicePort { get; set; } = 0;
        /// <summary>
        /// 硬件设备通道地址
        /// </summary>
        public string HardwareChannelAddress { get; set; } = "";
        /// <summary>
        /// IMP设备测点测量方式
        /// </summary>
        public int Mode { get; set; } = 100;
        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// Keithley设备测点的测量方式
        /// <summary>
        public ChannelType ChannelType { get; set; } = ChannelType.无;
        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// Keithley设备测点的传感器类型
        /// <summary>
        public TransducerType TransducerType { get; set; } = TransducerType.无;
        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// Keithley设备测点的热电偶类型
        /// <summary>
        public ThermocoupleType ThermocoupleType { get; set; } = ThermocoupleType.无;
        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// Keithley设备测点的四线制铂电阻类型
        /// <summary>
        public FourWireRTDType FourWireRTDType { get; set; } = FourWireRTDType.无;
        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// Keithley设备测点的热敏电阻类型
        /// <summary>
        public ThermistorType ThermistorType { get; set; } = ThermistorType.无;
        /// <summary>
        /// Keithley设备测点是否冷端补偿
        /// <summary>
        public bool ColdJunc { get; set; } = false;
        /// <summary>
        /// 报警值上限
        /// </summary>
        public double? AlertMax { get; set; } = null;
        /// <summary>
        /// 报警值下限
        /// </summary>
        public double? AlertMin { get; set; } = null;
        /// <summary>
        /// 单位
        /// </summary>
        public string YUnit { get; set; } = "";
        /// <summary>
        /// 小数位数显示个数
        /// </summary>
        public int ShowDecimal { get; set; } = 3;

        /// <summary>
        /// 热源最大电流
        /// </summary>
        public double Imax { get; set; }
        /// <summary>
        /// 热源最大电压
        /// </summary>
        public double Vmax { get; set; } 
        /// <summary>
        /// 采样频率
        /// <summary>
        public int XDelta { get; set; }
        /// <summary>
        /// 热源阻值
        /// </summary>
        public double RH { get; set; }
        /// <summary>
        /// 导线阻值
        /// </summary>
        public double RL { get; set; }
        [JsonIgnore]
        /// <summary>
        /// 当前测点状态
        /// </summary>
        public ChannelStatus Status { get; set; } = ChannelStatus.未知;
        [JsonIgnore]
        /// <summary>
        /// 控温点最后三个测量值
        /// </summary>
        public double[] CurrentValues { get; set; } = new double[3];
        [JsonIgnore]
        /// <summary>
        /// 控温点当前测量值
        /// </summary>
        public double LastValue { get; set; }
        [JsonIgnore]
        /// <summary>
        /// 测点当前采集的数据个数
        /// </summary>
        public int DataCount { get; set; }
        [JsonIgnore]
        /// <summary>
        /// 热源是否使能
        /// </summary>
        public bool Enabled { get; set; }
        [JsonIgnore]
        /// <summary>
        /// 当前电流值
        /// </summary>
        public double CurrentI { get; set; }
        [JsonIgnore]
        /// <summary>
        /// 当前电压值
        /// </summary>
        public double CurrentV { get; set; }
        [JsonIgnore]
        /// <summary>
        /// 当前保存数据路径
        /// </summary>
        public string SaveDataPath { get; set; }
        [JsonIgnore]
        /// <summary>
        /// DChannel对象
        /// </summary>
        public DChannel DChannel { get; set; }
        [JsonIgnore]
        /// <summary>
        /// 是否报警
        /// </summary>
        public bool Alert { get; private set; }
        [JsonIgnore]
        /// <summary>
        /// 获得测量值最大值
        /// </summary>
        public double MaxValue { get; private set; } = 0;
        [JsonIgnore]
        /// <summary>
        /// 获得测量值最小值
        /// </summary>
        public double MinValue { get; private set; } = 0;
        [JsonIgnore]
        /// <summary>
        /// 获得计算值平均值
        /// </summary>
        public double AverageValue { get; private set; }
        [JsonIgnore]
        /// <summary>
        /// 是否正在保存数据
        /// </summary>
        public bool SavingData { get; private set; }
        [JsonIgnore]
        /// <summary>
        /// 在曲线窗体中显示
        /// </summary>
        public ushort ShowInCurve
        {
            get
            {
                return myShowInCurve;
            }
            set
            {
                myShowInCurve = value;
                if (DChannel == null)
                    return;
                if (myShowInCurve == 0)
                {
                    DChannel.Length = 0;
                }
                else
                {
                    if (SavingData)
                    {
                        string name = "";
                        if (Function == ChannelFunction.热源)
                        {
                            name = Name + "_curr";
                        }
                        else
                        {
                            name = Name;
                        }
                        FileInfo file = new(SaveDataPath + "\\" + name);
                        if (!file.Exists)
                        {
                            DTime t = new();
                            t.SetNow();
                            DChannel.TriggerTime = t;
                        }
                        else
                        {
                            byte[] b = new byte[file.Length];
                            float[] f = new float[file.Length / 4];
                            using (FileStream s = File.Open(SaveDataPath + "\\" + name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                s.Read(b, 0, (int)file.Length);
                            }
                            int fi = 0;
                            for (int i = 0; i < b.Length; i += 4)
                            {
                                f[fi] = BitConverter.ToSingle(b, i);
                                fi += 1;
                            }
                            DChannel.Data = f;
                        }
                    }
                    else
                    {
                        DTime t = new();
                        t.SetNow();
                        DChannel.TriggerTime = t;
                    }
                }
            }
        }
        [JsonIgnore]
        /// <summary>
        /// 测量方式描述
        /// <summary>
        public string RangeDescription { get; set; }
        [JsonIgnore]
        /// <summary>
        /// 温控点控制对应热源的方式，PID或者开关量
        /// </summary>
        public ControlType ControlType { get; set; } = ControlType.无;
        [JsonIgnore]
        /// <summary>
        /// 开关量控制方式下，热源应输出的电压值
        /// </summary>
        public double VOut { get; set; }
        [JsonIgnore]
        /// <summary>
        /// 开关量控制方式下，热源应输出的电流值
        /// </summary>
        public double IOut { get; set; }
        [JsonIgnore]
        /// <summary>
        /// 开关量控制方式下，关闭热源的温控点测量温度上限
        /// </summary>
        public double TempMax { get; set; }
        [JsonIgnore]
        /// <summary>
        /// 开关量控制方式下，启动热源的温控点测量温度下限
        /// </summary>
        public double TempMin { get; set; }
        [JsonIgnore]
        /// <summary>
        /// PID控制方式下，温控点的目标温度
        /// </summary>
        public double TargetTemp { get; set; }
        /// <summary>
        /// PID控制中使用控温周期采样的数据个数
        /// </summary>
        [JsonIgnore]
        public int ControlDataCount { get; set; } = 0;
        //[JsonIgnore]
        ///// <summary>
        ///// 控制测量值数量
        ///// <summary>
        //public int ControlDataCount { get; set; }
        private ushort myShowInCurve;
        private double sumValue;
        public void InitDChannel()
        {
            DChannel = new();
            DChannel.set_Flags(DmChannelFlagConstants.cdmFlagPrivate, true);
            DChannel.Name = UserName;
            DChannel.xDelta = XDelta / 1000;
            DChannel.yUnit = YUnit;
            DChannel.Length = 0;
        }
        public void DisposeDChannel()
        {
            if (DChannel == null)
                return;
            DChannel.Length = 0;
            DChannel = null;
        }
        public string GetFormatString()
        {
            string formatString = "";
            formatString = "0.";
            if (ShowDecimal != 0)
            {
                for (int i = 1; i <= ShowDecimal; i++)
                {
                    formatString += "0";
                }
            }
            else
            {
                formatString = "0";
            }
            return formatString;
        }
        public string GetRangeDescription()
        {
            if (DeviceType == DeviceType.Keithley)
            {
                string result = "未配置";
                switch (ChannelType)
                {
                    case ChannelType.无:

                        break;
                    case ChannelType.交流电压:
                        result = "交流电压";
                        break;
                    case ChannelType.直流电压:
                        result = "直流电压";
                        break;
                    case ChannelType.温度:
                        switch (TransducerType)
                        {
                            case TransducerType.热电偶:
                                switch (ThermocoupleType)
                                {
                                    case ThermocoupleType.B:
                                        result = "温度_热电偶_B型";
                                        break;
                                    case ThermocoupleType.E:
                                        result = "温度_热电偶_E型";
                                        break;
                                    case ThermocoupleType.J:
                                        result = "温度_热电偶_J型";
                                        break;
                                    case ThermocoupleType.K:
                                        result = "温度_热电偶_K型";
                                        break;
                                    case ThermocoupleType.N:
                                        result = "温度_热电偶_N型";
                                        break;
                                    case ThermocoupleType.R:
                                        result = "温度_热电偶_R型";
                                        break;
                                    case ThermocoupleType.S:
                                        result = "温度_热电偶_S型";
                                        break;
                                    case ThermocoupleType.T:
                                        result = "温度_热电偶_T型";
                                        break;
                                }
                                break;
                            case TransducerType.热敏电阻:
                                switch (ThermistorType)
                                {
                                    case ThermistorType.两千两百欧姆:
                                        result = "温度_热敏电阻_2200Ω";
                                        break;
                                    case ThermistorType.五千欧姆:
                                        result = "温度_热敏电阻_5000Ω";
                                        break;
                                    case ThermistorType.一万欧姆:
                                        result = "温度_热敏电阻_10000Ω";
                                        break;
                                }
                                break;
                            case TransducerType.RTD:
                                switch (FourWireRTDType)
                                {
                                    case FourWireRTDType.PT100:
                                        result = "温度_RTD_PT100";
                                        break;
                                    case FourWireRTDType.D100:
                                        result = "温度_RTD_D100";
                                        break;
                                    case FourWireRTDType.F100:
                                        result = "温度_RTD_F100";
                                        break;
                                    case FourWireRTDType.PT385:
                                        result = "温度_RTD_PT385";
                                        break;
                                    case FourWireRTDType.PT3916:
                                        result = "温度_RTD_PT3916";
                                        break;
                                    case FourWireRTDType.PT1000:
                                        result = "温度_RTD_PT1000";
                                        break;
                                }
                                break;
                        }
                        break;
                }
                return result;
            }
            return "";
        }
        public void AddData(double data, ChannelStatus status, bool saveData)
        {
            if (data > float.MaxValue)
            {
                data = 0;
            }
            if (data < float.MinValue)
            {
                data = 0;
            }
            DataCount += 1;

            LastValue = data;
            Status = status;
            SavingData = saveData;
            if (DataCount % (SystemConfig.ControlCycle / (XDelta / 1000)) == 0)
            {
                CurrentValues[0] = CurrentValues[1];
                CurrentValues[1] = CurrentValues[2];
                CurrentValues[2] = data;
                ControlDataCount += 1;
            }

            if (MaxValue == 0)
            {
                MaxValue = LastValue;
            }
            if (MinValue == 0)
            {
                MinValue = LastValue;
            }

            if (LastValue > MaxValue)
            { MaxValue = LastValue; }
            if (LastValue < MinValue)
            { MinValue = LastValue; }

            sumValue += LastValue;
            AverageValue = sumValue / DataCount;

            if (myShowInCurve > 0)
            {
                DChannel.AppendData(LastValue, null);
            }

            Alert = GetAlert(LastValue, AlertMax, AlertMin);
            if (SavingData)
            {
                Task.Run(() =>
                {
                    FlushData(LastValue, SaveDataPath + "\\" + Name);
                });
            }
        }
        public void AddData(double v, double i, ChannelStatus status, bool saveData)
        {
            if (v > float.MaxValue)
            {
                v = 0;
            }
            if (v < float.MinValue)
            {
                v = 0;
            }
            if (i > float.MaxValue)
            {
                i = 0;
            }
            if (i < float.MinValue)
            {
                i = 0;
            }
            CurrentI = i;
            CurrentV = v;
            Status = status;
            SavingData = saveData;
            DataCount += 1;

            if (myShowInCurve > 0)
            {
                DChannel.AppendData(CurrentI, null);
            }

            Alert = GetAlert(CurrentI, AlertMax, AlertMin);
            if (SavingData)
            {
                Task.Run(() =>
                {
                    FlushData(CurrentI, SaveDataPath + "\\" + Name + "_curr");
                    FlushData(CurrentV, SaveDataPath + "\\" + Name + "_volt");
                });
            }
        }
        static void FlushData(double data, string file)
        {
            using FileStream fs = new(file, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            using BinaryWriter bw = new(fs);
            bw.Write(BitConverter.GetBytes((float)data));
            bw.Flush();
            bw.Close();
            bw.Dispose();
            fs.Close();
        }
        /// <summary>
        /// 将测点历史数据中的最大值设置为当前值
        /// </summary>
        public void SetMaxDataZero()
        {
            MaxValue = LastValue;
        }
        /// <summary>
        /// 将测点历史数据中的最小值设置为当前值
        /// </summary>
        public void SetMinDataZero()
        {
            MinValue = LastValue;
        }
        /// <summary>
        /// 删除以前的数据，将数据个数和各平均值置零
        /// </summary>
        public void SetAvgDataZero(bool create = false)
        {
            try
            {
                DataCount = 0;
                sumValue = 0;
                AverageValue = 0;
                ControlDataCount = 0;
                CurrentValues[0] = 0;
                CurrentValues[1] = 0;
                CurrentValues[2] = 0;
                DChannel.Length = 0;
                if (create)
                {
                    SaveData s = new((decimal)LastValue, SaveDataPath + "\\" + Name);
                    System.Threading.ThreadPool.UnsafeQueueUserWorkItem(new System.Threading.WaitCallback(s.CreateFile), null);
                }
            }
            catch { }
        }
        private static bool GetAlert(double value, double? max, double? min)
        {
            if (max == null && min == null)
            {
                return false;
            }
            if (max == null && min != null)
            {
                if (value < min)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (max != null && min == null)
            {
                if (value > max)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (max != null && min != null)
            {
                if (value > max || value < min)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        public bool Equals(Channel x, Channel y)
        {
            if (x == null && y == null)
            {
                return true;
            }
            if (x == null || y == null)
            {
                return false;
            }
            if (x.Name == y.Name)
            {
                return true;
            }
            return false;
        }
        public int GetHashCode(Channel obj)
        {
            return ID.GetHashCode();
        }
        private class SaveData
        {
            private readonly byte[] data;
            private readonly string mySaveFile;
            public SaveData(decimal d, string saveFile)
            {
                data = BitConverter.GetBytes((float)d);
                mySaveFile = saveFile;
            }
            public void SaveDataThread(object sender)
            {
                try
                {
                    using (FileStream fs = new FileStream(mySaveFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            bw.Write(data);
                            bw.Flush();
                        }
                    }
                }
                catch { }
            }
            public void CreateFile(object sender)
            {
                try
                {
                    using (FileStream fs = new FileStream(mySaveFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {

                    }
                }
                catch { }
            }
        }
    }
}
