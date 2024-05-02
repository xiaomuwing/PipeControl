using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Keithley
{
    [JsonObject(MemberSerialization.OptOut)]
    [Serializable]
    public sealed class KEChannel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string DeviceAddress { get; set; }
        public string Address { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ChannelType ChannelType { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TransducerType TransducerType { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ThermocoupleType ThermocoupleType { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public FourWireRTDType FourWireRTDType { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public ThermistorType ThermistorType { get; set; }
        public bool ColdJunc { get; set; } = false;
        [JsonIgnore]
        public bool IsConfiged { get; set; }
        [JsonIgnore]
        public double LastData { get; set; } = double.MinValue;
        public KEChannel(int id, string address, string ip)
        {
            ID = id;
            Address = address;
            ChannelType = ChannelType.无;
            TransducerType = TransducerType.无;
            ThermocoupleType = ThermocoupleType.无;
            FourWireRTDType = FourWireRTDType.无;
            ThermistorType = ThermistorType.无;
            IsConfiged = false;
            DeviceAddress = ip;
            Name = "KE_" + DeviceAddress + "_" + address;
        }
        public string GetRangeDescription()
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
                            }
                            break;
                    }
                    break;
            }
            return result;
        }
    }
}
