using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace ITECHCommon
{
    [JsonObject(MemberSerialization.OptOut)]
    public class Channel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int ID { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// 测点功能
        /// </summary>
        public ChannelFunction Function { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// 硬件设备类型
        /// </summary>
        public DeviceType DeviceType { get; set; }
        /// <summary>
        /// 硬件设备地址
        /// </summary>
        public string HardwareDeviceAddress { get; set; }
        /// <summary>
        /// 硬件设备通道地址
        /// </summary>
        public string HardwareChannelAddress { get; set; }
        /// <summary>
        /// 测点名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 温度报警值
        /// </summary>
        public double? AlertValue { get; set; } = null;
        /// <summary>
        /// 热源最大电流
        /// </summary>
        public double Imax { get; set; } = 12;
        /// <summary>
        /// 热源最大电压
        /// </summary>
        public double Vmax { get; set; } = 150;
        /// <summary>
        /// 热源阻值
        /// </summary>
        public double RH { get; set; } = 25;
        /// <summary>
        /// 导线阻值
        /// </summary>
        public double RL { get; set; } = 1.2;

        /// <summary>
        /// 所在温区的编号
        /// </summary>
        public int ParentID { get; set; }
        [JsonIgnore]
        public TempRange Parent { get; set; }
        [JsonIgnore]
        /// <summary>
        /// 当前测点状态
        /// </summary>
        public ChannelStatus Status { get; set; }
        [JsonIgnore]
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; }
        [JsonIgnore]
        /// <summary>
        /// 控温点最后三个测量值
        /// </summary>
        public double[] CurrentValue { get; set; } = new double[3];
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
        public void AddData(double data)
        {
            LastValue = data;
            if (DataCount == 0)
            {
                CurrentValue[2] = data;
            }
            if (DataCount == 1)
            {
                CurrentValue[1] = CurrentValue[2];
                CurrentValue[2] = data;
            }
            if (DataCount >= 2)
            {
                CurrentValue[0] = CurrentValue[1];
                CurrentValue[1] = CurrentValue[2];
                CurrentValue[2] = data;
            }
            DataCount += 1;
        }
    }
}
