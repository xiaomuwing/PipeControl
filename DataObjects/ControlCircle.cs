using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PipeControl.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataObjects
{
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    /// <summary>
    /// 一个温控执行过程
    /// </summary>
    public sealed class ControlCircle
    {
        /// <summary>
        /// 执行过程编号
        /// </summary>
        public int ID { get; set; }
        public List<ControlLine> Lines { get; set; } = new();
        /// <summary>
        /// 持续时间
        /// </summary>
        public TimeSpan LastTime { get; set; }
        /// <summary>
        /// 所在试验
        /// </summary>
        [JsonIgnore]
        public Experiment Parent { get; set; }
        /// <summary>
        /// 使能标识
        /// </summary>
        [JsonIgnore]
        public int En { get; set; } = 0;
        /// <summary>
        /// 当前目标温度，下一轮执行过程需要将上一轮执行过程的目标温度传递下来
        /// </summary>
        public double r1 = 0;
        private int blmt = 0;
        public double uout = 0;
        public async Task ControlDC()
        {
            Log.WriteLog(string.Format("-----ID={0}  开始控制---------------------", ID), "CtrlDC", "Control");
            foreach (ControlLine line in Lines)
            {
                Channel tempChannel = Parent.GetChannelBySN(line.TempChannelSN);
                List<Channel> dcChannels = new();
                foreach (var sn in line.DCChannelSNs)
                {
                    var channel = Parent.GetChannelBySN(sn);
                    channel.TempMax = line.TempMax;
                    channel.TempMin = line.TempMin;
                    dcChannels.Add(Parent.GetChannelBySN(sn));
                }
                switch (line.ControlType)
                {
                    case ControlType.PID:
                        if (tempChannel.ControlDataCount < 3)
                        {
                            continue;
                        }
                        if (dcChannels.Count == 0)
                        {
                            continue;
                        }
                        Utilities.GetTarget(r1, tempChannel.CurrentValues[0], SystemConfig.R, SystemConfig.Algorithm.Tr, blmt, SystemConfig.ControlCycle, line.TargetTemp, out double rr);
                        double[] y = new double[3] { tempChannel.CurrentValues[2], tempChannel.CurrentValues[1], tempChannel.CurrentValues[0] };
                        Log.WriteLog(string.Format("y[0]={0},y[1]={1},y[2]={2}, r1={3}, rr={4}", y[0], y[1], y[2], r1, rr), "CtrlDC", "Control");
                        double 倒数和 = 0;
                        foreach (Channel channel in dcChannels)
                        {
                            倒数和 += 1 / (channel.RH + channel.RL);
                        }
                        double rh = 1 / 倒数和;
                        double[] uoutNow = new double[1] { uout };
                        double[] r = new double[1] { rr };
                        Utilities.ComputeData(SystemConfig.MaxCurrent, SystemConfig.MinCurrent, En, r, y, uoutNow, SystemConfig.Algorithm.Kp, SystemConfig.Algorithm.Ti, SystemConfig.Algorithm.Td, rh, SystemConfig.ControlCycle, out blmt, out uout, out double iout);
                        r1 = rr;
                        Log.WriteLog(string.Format("当前温度={0:0.000}, blmt={1:0}, 电阻={2:0.000}, 下轮目标温度={3:0.0}, uout={4:0.000000}, iout={5:0.000000}, 控温周期={6}",
                                                    tempChannel.CurrentValues[0], blmt, rh, r1, uout, iout, SystemConfig.ControlCycle), "CtrlDC", "Control");
                        foreach (var dc in dcChannels)
                        {
                            var power = Parent.Instruments.GetPower(dc.HardwareDeviceAddress, dc.HardwareChannelAddress);
                            if (power.Opened)
                            {
                                await power.SetCurrentOutput((float)iout);
                            }
                        }
                        break;
                    case ControlType.开关量:
                        Log.WriteLog(string.Format("ControlDataCount = {0}", tempChannel.ControlDataCount), "CtrlDC", "Control");
                        if (tempChannel.ControlDataCount == 1)
                        {
                            if (tempChannel.LastValue < line.TempMax)
                            {
                                Log.WriteLog(string.Format("第一次判断，当前值{0}小于当前执行过程设定的最大温度{1}，指定电源输出", tempChannel.LastValue, line.TempMax), "CtrlDC", "Control");
                                await DCSetOutputON(dcChannels, line, Parent.Instruments);
                            }
                        }
                        else
                        {
                            var power = Parent.Instruments.GetPower(dcChannels[0].HardwareDeviceAddress, dcChannels[0].HardwareChannelAddress);
                            if (power.IsOutput)
                            {
                                if (tempChannel.LastValue >= line.TempMax)
                                {
                                    Log.WriteLog(string.Format("当前值{0}大于当前执行过程设定的最大温度{1}，指定电源停止输出", tempChannel.LastValue, line.TempMax), "CtrlDC", "Control");
                                    await DCSetOutputOFF(dcChannels, Parent.Instruments);
                                }
                                else
                                {
                                    
                                    await DCSetOutputON(dcChannels, line, Parent.Instruments);
                                }
                            }
                            else
                            {
                                if (tempChannel.LastValue <= line.TempMin)
                                {
                                    Log.WriteLog(string.Format("当前值{0}小于当前执行过程设定的最小温度{1}，指定电源输出功率", tempChannel.LastValue, line.TempMin), "CtrlDC", "Control");
                                    await DCSetOutputON(dcChannels, line, Parent.Instruments);
                                }
                            }
                        }
                        break;
                }
            }
        }
        public async static Task DCSetOutputOFF(List<Channel> dcChannels, InstrumentsCtrl.Instruments instruments)
        {
            foreach (var dc in dcChannels)
            {
                var power = instruments.GetPower(dc.HardwareDeviceAddress, dc.HardwareChannelAddress);
                if (!power.Opened)
                {
                    continue;
                }
                await power.SetVoltOutput(0);
                await power.SetCurrentOutput(0);
                await power.SetOutputOFF();
            }
        }
        public async static Task DCSetOutputON(List<Channel> dcChannels, ControlLine line, InstrumentsCtrl.Instruments instruments)
        {
            foreach (var dc in dcChannels)
            {
                var power = instruments.GetPower(dc.HardwareDeviceAddress, dc.HardwareChannelAddress);
                if (!power.Opened)
                {
                    continue;
                }
                if (!power.IsOutput)
                {
                    await power.SetOutputOn();
                }
                if (power.CurrentV != line.VOut)
                {
                    await power.SetVoltOutput((float)line.VOut);
                }
                if (power.CurrentI != line.IOut)
                {
                    await power.SetCurrentOutput((float)line.IOut);
                }
            }
        }
    }
    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public sealed class ControlLine
    {

        [JsonConverter(typeof(StringEnumConverter))]
        public ControlType ControlType { get; set; }
        public string TempChannelSN { get; set; }
        public List<string> DCChannelSNs { get; set; }
        public double VOut { get; set; }
        public double IOut { get; set; }
        public double TempMax { get; set; }
        public double TempMin { get; set; }
        public double TargetTemp { get; set; }
        [JsonIgnore]
        public ControlCircle Parent { get; set; }
    }
}
