using System.Threading.Tasks;

namespace InstrumentsCtrl
{
    /// <summary>
    /// 可控电源设备
    /// </summary>
    public interface IPower
    {
        public string SN { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public DeviceType DeviceType { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public DeviceModel DeviceModel { get; set; }
        /// <summary>
        /// 设备地址，包括IP地址和串口地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 设备端口号，串口设备保持为零
        /// </summary>
        public ushort Port { get; set; }
        /// <summary>
        /// 设备通道号
        /// </summary>
        public string ChannelNo { get; set; }
        /// <summary>
        /// 当前电压值
        /// </summary>
        public float CurrentV { get; set; }
        /// <summary>
        /// 当前电流值
        /// </summary>
        public float CurrentI { get; set; }
        public float MaxVolt { get; set; }
        public float MaxCurrent { get; set; }
        public bool Opened { get; set; }
        public bool IsOutput { get; set; }
        /// <summary>
        /// 连接设备
        /// </summary>
        Task Open();
        /// <summary>
        /// 打开输出
        /// </summary>
        Task SetOutputOn();
        /// <summary>
        /// 输出电流指定值
        /// </summary>
        /// <param name="output"></param>
        Task SetCurrentOutput(float current);
        /// <summary>
        /// 输出电压指定值
        /// </summary>
        Task SetVoltOutput(float volt);
        /// <summary>
        /// 关闭输出
        /// </summary>
        Task SetOutputOFF();
        Task GetVolt();
        Task<bool> IsOnline();
        /// <summary>
        /// 断开连接设备
        /// </summary>
        Task Close();
    }
}
