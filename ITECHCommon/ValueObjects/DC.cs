using Newtonsoft.Json;
using System;

namespace PipeControl.Common
{
    [JsonObject(MemberSerialization.OptOut)]
    public class DC : IEquatable<DC>
    {
        public int ID { get; set; }
        /// <summary>
        /// 热源名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 热源设备IP地址
        /// </summary>
        public string IPAddress { get; set; }
        /// <summary>
        /// 最大工作电流
        /// </summary>
        public double Imax { get; set; } = 12;
        /// <summary>
        /// 最大工作电压
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
        public bool Enabled { get; set; } = false;
        public DCPowerType DCType { get; set; } = DCPowerType.ITech;
        [JsonIgnore]
        public bool Selected { get; set; }
        [JsonIgnore]
        public double CurrentC { get; set; }
        [JsonIgnore]
        public double CurrentV { get; set; }
        public DC(string ip)
        {
            IPAddress = ip;
        }
        public bool Equals(DC other)
        {
            if (other.IPAddress == IPAddress)
            {
                return true;
            }
            return false;
        }
    }
}
