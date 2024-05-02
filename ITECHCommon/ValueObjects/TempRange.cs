using ITECHCommon;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITECHCommon
{
    [JsonObject(MemberSerialization.OptOut)]
    [Serializable]
    public class TempRange
    {
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 相关测点
        /// </summary>
        public List<Channel> Channels { get; set; } = new();
        /// <summary>
        /// 算法
        /// </summary>
        public Algorithm Algorithm { get; set; }
        /// <summary>
        /// 升降温速率(度/秒)
        /// </summary>
        public double R { get; set; }
        /// <summary>
        /// 控制周期(秒)
        /// </summary>
        public int ControlCycle { get; set; } = 6;
        /// <summary>
        /// 目标温度
        /// </summary>
        public double TargetTemp { get; set; }
        /// <summary>
        /// 跟随温度
        /// </summary>
        public double FollowTemp { get; set; }
        /// <summary>
        /// 最大输出电压
        /// </summary>
        public double MaxCurr { get; set; }
        /// <summary>
        /// 最小输出电压
        /// </summary>
        public double MinCurr { get; set; }
        /// <summary>
        /// 按照事先确定的时间间隔和输出电流的输出列表
        /// </summary>
        public List<OutputData> OutputList { get; set; } = new();
        /// <summary>
        /// 电源使能
        /// </summary>
        [JsonIgnore]
        public int En { get; set; } = 1;
        /// <summary>
        /// 当前输出电流
        /// </summary>
        [JsonIgnore]
        public double LastCurrentOut { get; private set; }
        /// <summary>
        /// 所在试验
        /// </summary>
        [JsonIgnore]
        public Experiment Parent { get; set; }
        private double r1 = 0;
        private int blmt = 0;
        private double uout = 0;
        public Channel GetTempControl()
        {
            return Channels.Single(x => x.Function == ChannelFunction.控温点);
        }
        public Channel GetFollowTemp()
        {
            return Channels.Single(x => x.Function == ChannelFunction.跟随温度);
        }
        public List<Channel> GetHeatSources()
        {
            return Channels.Where(x => x.Function == ChannelFunction.热源).ToList();
        }
        public void CtrlDC()
        {
            if (GetTempControl().DataCount < 3)
                return;
            if (GetHeatSources().Count == 0)
                return;
            Utilities.GetTarget(r1, GetTempControl().CurrentValue[0], R, Algorithm.Tr, blmt, ControlCycle, TargetTemp, out double rr);
            double[] y = new double[3];
            y[0] = GetTempControl().CurrentValue[2];
            y[1] = GetTempControl().CurrentValue[1];
            y[2] = GetTempControl().CurrentValue[0];
            double[] r = new double[] { rr };
            double[] u = new double[] { uout };
            double rh = GetHeatSources()[0].RH;
            Utilities.ComputeData(MaxCurr, MinCurr, En, r, y, u, Algorithm.Kp, Algorithm.Ti, Algorithm.Td, rh, ControlCycle, out blmt, out uout, out double iout);
            r1 = rr;
            Log.WriteLog(string.Format("当前温度 = {0}, blmt = {1}, 当前电压= {2}, r1 = {3}, uout = {4}, iout = {5}",
                                        GetTempControl().LastValue, blmt, GetHeatSources()[0].CurrentV, r1, uout, iout));
            LastCurrentOut = iout;
        }
    }
}
