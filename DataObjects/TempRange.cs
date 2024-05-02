using PipeControl.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataObjects
{
    [JsonObject(MemberSerialization.OptOut)]
    [Serializable]
    public class TempRange
    {
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 算法
        /// </summary>
        public Algorithm Algorithm { get; set; }
        /// <summary>
        /// 升降温速率(度/秒)
        /// </summary>
        public double R { get; set; } = 0.1;
        /// <summary>
        /// 控制周期(秒)
        /// </summary>
        public int ControlCycle { get; set; } = 6;
        [JsonConverter(typeof(StringEnumConverter))]
        /// <summary>
        /// 温区控温方式
        /// </summary>
        public TempCtrlType TempCtrlType { get; set; } = TempCtrlType.默认;
        /// <summary>
        /// 最大输出电流
        /// </summary>
        public double MaxCurr { get; set; } = 4;
        /// <summary>
        /// 最小输出电流
        /// </summary>
        public double MinCurr { get; set; } = 0;
        /// <summary>
        /// 按照事先确定的相对时间和输出电流的输出列表
        /// </summary>
        public List<OutputDataRelative> OutputRelativeList { get; set; } = new();
        /// <summary>
        /// 按照事先确定的绝对时间和输出电流的输出列表
        /// </summary>
        public List<OutputDataAbsolute> OutputAbsoluteList { get; set; } = new();
        /// <summary>
        /// 目标温度
        /// </summary>
        public double TargetTemp { get; set; }
        /// <summary>
        /// 电源使能
        /// </summary>
        [JsonIgnore]
        public int En { get; set; } = 0;
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
        /// <summary>
        /// 当前阻值
        /// </summary>
        [JsonIgnore]
        public double Resistance { get; set; }
        /// <summary>
        /// 当前输出电压
        /// </summary>
        [JsonIgnore]
        public double VoltNow { get; set; }
        /// <summary>
        /// 添加测点到此温区
        /// </summary>
        /// <param name="channel"></param>
        public void AddChannel(Channel channel)
        {
            if (channel.Function == ChannelFunction.控温点)
            {
                if (!channel.Equals(GetTempControl()) && GetTempControl() != null)
                {
                    GetTempControl().Function = ChannelFunction.无;
                }
            }
            if (channel.Function == ChannelFunction.跟随温度)
            {
                if (!channel.Equals(GetFollowTemp()) && GetFollowTemp() != null)
                {
                    GetFollowTemp().Function = ChannelFunction.无;
                }
            }
        }
        /// <summary>
        /// 获取此温区所有测点
        /// </summary>
        /// <returns></returns>
        public List<Channel> GetChannels()
        {
            return Parent.Channels.Where(x => x.ParentID == ID).ToList();
        }
        /// <summary>
        /// 获取此温区的控温点
        /// </summary>
        /// <returns></returns>
        public Channel GetTempControl()
        {
            foreach (Channel channel in GetChannels())
            {
                if (channel.Function == ChannelFunction.控温点)
                {
                    return channel;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取此温区的温度跟随点
        /// </summary>
        /// <returns></returns>
        public Channel GetFollowTemp()
        {
            foreach (Channel channel in GetChannels())
            {
                if (channel.Function == ChannelFunction.跟随温度)
                {
                    return channel;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取此温区的热源
        /// </summary>
        /// <returns></returns>
        public List<Channel> GetHeatSources()
        {
            List<Channel> tmps = new();
            var channels = GetChannels();

            foreach (Channel channel in GetChannels())
            {
                if (channel.Function == ChannelFunction.热源)
                {
                    tmps.Add(channel);
                }
            }
            return tmps;
        }
        private double r1 = 0;
        private int blmt = 0;
        private double uout = 0;
        /// <summary>
        /// 控制热源
        /// </summary>
        public void CtrlDC()
        {
            switch (TempCtrlType)
            {
                case TempCtrlType.默认:
                    if (GetTempControl().DataCount < 3)
                        return;
                    if (GetHeatSources().Count == 0)
                        return;
                    Utilities.GetTarget(r1, GetTempControl().CurrentValues[0], R, Algorithm.Tr, blmt, ControlCycle, TargetTemp, out double rr);
                    double[] y = new double[3];
                    y[0] = GetTempControl().CurrentValues[2];
                    y[1] = GetTempControl().CurrentValues[1];
                    y[2] = GetTempControl().CurrentValues[0];
                    Log.WriteLog(string.Format("y[0]={0},y[1]={1},y[2]={2}",y[0],y[1],y[2]), "CtrlDC", "TempRange");          
                    double 倒数和 = 0;
                    foreach (Channel channel in GetHeatSources())
                    {
                        倒数和 += 1 / (channel.RH + channel.RL);
                    }
                    double rh = 1 / 倒数和;
                    double[] uoutNow = new double[] { uout };
                    double[] r = new double[] { rr };
                    //if (rh < Resistance)
                    //{
                    //    rh = Resistance;
                    //}
                    Utilities.ComputeData(MaxCurr, MinCurr, En, r, y, uoutNow, Algorithm.Kp, Algorithm.Ti, Algorithm.Td, rh, ControlCycle, out blmt, out uout, out double iout);
                    r1 = rr;
                    //Log.WriteLog(string.Format("当前温度={0:0.000}, blmt={1:0}, 电阻={2:0.000}, 下轮目标温度={3:0.0}, uout={4:0.000000}, iout={5:0.000000}, 控温周期={6}",
                    //                            GetTempControl().CurrentValues[0], blmt, rh, r1, uout, iout, ControlCycle), "CtrlDC", "TempRange");
                    LastCurrentOut = iout;
                    break;
                case TempCtrlType.跟随测点:
                    if (GetTempControl() == null)
                        return;
                    if(GetFollowTemp() == null)
                        return;
                    if (GetTempControl().DataCount < 3)
                        return;
                    if (GetHeatSources().Count == 0)
                        return;
                    Utilities.GetTarget(r1, GetTempControl().CurrentValues[0], R, Algorithm.Tr, blmt, ControlCycle, GetFollowTemp().LastValue, out double followrr);
                    double[] followy = new double[3];
                    followy[0] = GetTempControl().CurrentValues[2];
                    followy[1] = GetTempControl().CurrentValues[1];
                    followy[2] = GetTempControl().CurrentValues[0];
                    double followrh = GetHeatSources()[0].RH;
                    double[] u = new double[] { uout };
                    double[] rfollow = new double[] { followrr };
                    Utilities.ComputeData(MaxCurr, MinCurr, En, rfollow, followy, u, Algorithm.Kp, Algorithm.Ti, Algorithm.Td, followrh, ControlCycle, out blmt, out uout, out double followiout);
                    r1 = followrr;
                    Log.WriteLog(string.Format("当前温度 = {0}, blmt = {1}, 当前电压= {2}, r1 = {3}, uout = {4}, iout = {5}",
                                                GetTempControl().LastValue, blmt, GetHeatSources()[0].CurrentV, r1, uout, followiout), "CtrlDC", "TempRange");
                    LastCurrentOut = followiout;
                    break;
                case TempCtrlType.导入列表_相对时间:
                    if (GetHeatSources().Count == 0)
                        return;
                    if (OutputRelativeList.Count == 0)
                        return;

                    break;
                case TempCtrlType.导入列表_绝对时间:
                    if (GetHeatSources().Count == 0)
                        return;
                    if (OutputAbsoluteList.Count == 0)
                        return;

                    break;
            }

        }
    }
}
