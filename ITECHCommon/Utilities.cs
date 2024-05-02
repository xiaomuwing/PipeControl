using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PipeControl.Common
{
    public static class Utilities
    {
        public static SystemConfig SystemConfig { get; private set; }
        static Utilities()
        {
            if (File.Exists("SystemConfig.json"))
            {
                SystemConfig = JsonConvert.DeserializeObject<SystemConfig>(File.ReadAllText("SystemConfig.json"));
            }
            else
            {
                SystemConfig = new();
                File.WriteAllText("SystemConfig.json", JsonConvert.SerializeObject(SystemConfig, Formatting.Indented));
            }
        }
        //rh = 26, rl = 1.2
        /// </summary>
        /// <param name="Imax"></param>
        /// <param name="Imin"></param>
        /// <param name="en">电源使能</param>
        /// <param name="r">控温目标值</param>
        /// <param name="y">控温测点测量值</param>
        /// <param name="u">热源当前输出</param>
        /// <param name="Kp"></param>
        /// <param name="Ti"></param>
        /// <param name="Td"></param>
        /// <param name="R">热源阻值</param>
        /// <param name="ts">控温周期</param>
        /// <param name="lmt"></param>
        /// <param name="uout"></param>
        /// <param name="Iout"></param>
        public static void ComputeData(double Imax, double Imin, int en, double[] r, double[] y, double[] u, double Kp, double Ti, double Td, double R, double ts,
                                       out int lmt, out double uout, out double Iout)
        {
            double ut;
            lmt = 0;
            double ki = Kp * ts / Ti; //0.006 
            double kd = Kp * Td / ts; //0.16
            double dupi = Kp * (y[1] - y[0]) + ki * (r[0] - y[0]);
            //Log.WriteLog(string.Format("Kp={0},y[1]={1},y[0]={2},r[0]={3},ki={4},dupi={5}", Kp, y[1], y[0], r[0], ki, dupi), "ComputeData", "TempRange");
            double dud = -kd * (y[0] + y[2] - y[1] * 2);
            //Log.WriteLog(string.Format("Kd={0},y[0]={1},y[2]={2},y[1]={3},dud={4}", kd, y[0], y[2], y[1], dud), "ComputeData", "TempRange");
            double drate;
            if (Math.Abs(dupi) < 1E-4)
            {
                //Log.WriteLog("因为dupi < 1E-4, drate = 0", "ComputeData", "TempRange");
                drate = 0;
            }
            else
            {
                //Log.WriteLog(string.Format("因为dupi >= 1E-4, drate正常计算=abs(dud/dupi)={0}", Math.Abs(dud) / Math.Abs(dupi)), "ComputeData", "TempRange");
                drate = Math.Abs(dud) / Math.Abs(dupi);
            }
            if (drate > SystemConfig.DRATE_MAX)
            {
                //Log.WriteLog("因为DRATE大于最大值, drate取最大值" + SystemConfig.DRATE_MAX.ToString(), "ComputeData", "TempRange");
                drate = SystemConfig.DRATE_MAX;
            }
            dud *= drate;
            ut = u[0] + dupi + dud;
            //Log.WriteLog(string.Format("dud={0},u[0]={1},ut={2}", dud, u[0], ut), "ComputeData", "TempRange");
            if (ut < 0)
            {
                Log.WriteLog("因为ut < 0, ut取0", "ComputeData", "TempRange");
                ut = 0;
                lmt = -1;
            }
            Iout = (double)Math.Pow(Math.Pow(ut, 4) / R, 0.5);
            //Log.WriteLog(string.Format("Iout={0},R={1},ut={2}", Iout, R, ut), "ComputeData", "TempRange");
            if (Iout >= Imax)
            {
                //Log.WriteLog("因为Iout 大于最大值, Iout取最大值，同时设lmt=1", "ComputeData", "TempRange");
                Iout = Imax;
                lmt = 1;
            }
            if (Iout <= Imin)
            {
                //Log.WriteLog("因为Iout 小于于最小值, Iout取最小值，同时设lmt=-1", "ComputeData", "TempRange");

                Iout = Imin;
                lmt = -1;
            }
            Iout *= en;
            if (en < 0.5)
            {
                lmt = 1;
            }
            uout = (double)Math.Pow(Iout * Iout * R, 0.25);
            //Log.WriteLog(string.Format("uout={0}", uout), "ComputeData", "TempRange");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="r1">上一控制周期的设定值</param>
        /// <param name="y">控温测点测量值</param>
        /// <param name="rate">升降温速率</param>
        /// <param name="Tr">Algorithm.Tr</param>
        /// <param name="blmt">上一周期LMT</param>
        /// <param name="Ts">控温周期</param>
        /// <param name="rin">温区设定值</param>
        /// <param name="rr">输出值</param>
        public static void GetTarget(double r1, double y, double rate, double Tr, int blmt, double Ts, double rin, out double rr)
        {
            double alp = (double)Math.Exp(-Ts / Tr); //0.98
            double drm = rate * Ts; //0.6
            //Log.WriteLog(string.Format("drm={0}", drm), "GetTarget", "TempRange");
            double tr = r1 * alp + rin * (1 - alp);
            //Log.WriteLog(string.Format("tr={0},r1={1},alp={2},rin={3}", tr, r1, alp, rin), "GetTarget", "TempRange");
            if ((blmt == 1) && (tr > r1))
            {
                //Log.WriteLog("因为blmt=1并且tr>r1，设tr=r1", "GetTarget", "TempRange");
                tr = r1;
            }
            else if ((blmt == -1) && (tr < r1))
            {
                //Log.WriteLog("因为blmt=-1并且tr<r1，设tr=r1", "GetTarget", "TempRange");
                tr = r1;
            }
            if ((tr - r1) > drm)
            {
                //Log.WriteLog(string.Format("因为tr-r1={0}>drm，设tr=r1+drm={1}", tr - r1, r1 + drm), "GetTarget", "TempRange");
                tr = r1 + drm;
            }
            if ((tr - r1) < -drm)
            {
                //Log.WriteLog(string.Format("因为tr-r1<={0}-drm，设tr=r1-drm={1}", tr - r1, r1 - drm), "GetTarget", "TempRange");
                tr = r1 - drm;
            }
            if ((tr - y) * (rin - y) < 0)
            {
                //Log.WriteLog(string.Format("tr-y={0},rin-y={1},设tr=y={2}", tr - y, rin - y, y), "GetTarget", "TempRange");
                tr = y;
            }
            rr = tr;
            //Log.WriteLog(string.Format("rr={0}", rr), "GetTarget", "TempRange");
        }
        /// <summary>
        /// 求若干INT数值的最大公约数
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int MaxGYS(List<int> numbers)
        {
            int minNumber = numbers.Min();
            int gys = 1;
            for (int i = 1; i <= minNumber; i++)
            {
                for (int j = 0; j < numbers.Count; j++)
                {
                    if (numbers[j] % i != 0)
                    {
                        break;
                    }
                    else
                    {
                        if (numbers.Count == j + 1)
                        {
                            gys = i;
                        }
                    }
                }
            }
            return gys;
        }
        public static uint ParseRGB(Color color)
        {
            return ((uint)color.B << 16) | (ushort)((color.G << 8) | color.R);
        }
        public static Color ToColor(this uint value)
        {
            int v = (int)value;
            return Color.FromArgb(v & 0x0000ff, (v & 0x00ff00) >> 8, (v & 0xff0000) >> 16);
        }
        public static bool ValidName(string str, out string invalidString)
        {
            invalidString = string.Empty;
            char[] invalidchars = { ':', '+', '-', '*', '/', '=', '<', '>', '|', '\\', '"', '\'', '[', ']', '@', '^', '.' };
            if (str.IndexOfAny(invalidchars) >= 0)
            {
                invalidString = "输入的项目中有非法字符，如下字符为非法字符： \r\n" + "                  : + - * / = < > | \\ \" ' [ ] @ ^ . ";
                return false;
            }
            return true;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetEnumDisplayName(Enum value)
        {
            Type enumType = value.GetType();
            string name = Enum.GetName(enumType, value);
            MemberInfo member = enumType.GetMember(name)[0];
            DisplayAttribute attribute = member.GetCustomAttribute<DisplayAttribute>();
            return attribute?.GetName() ?? name;
        }
        public static List<string> GetEnumDisplayNames(Type enumType)
        {
            List<string> displayNames = new List<string>();
            foreach (Enum value in Enum.GetValues(enumType))
            {
                var displayName = GetEnumDisplayName(value);
                displayNames.Add(displayName);
            }
            return displayNames;
        }
        public static T GetEnumByDisplayName<T>(string displayName) where T : Enum
        {
            foreach (T t in Enum.GetValues(typeof(T)))
            {
                if (GetEnumDisplayName(t) == displayName)
                {
                    return t;
                }
            }
            return default;
        }
        public static string GetDurationString(TimeSpan ts)
        {
            string str = "";
            if (ts.Days != 0)
            {
                str += ts.Days.ToString() + "天";
            }
            if (ts.Hours != 0)
            {
                str += ts.Hours.ToString() + "小时";
            }
            if (ts.Minutes != 0)
            {
                str += ts.Minutes.ToString() + "分";
            }
            if (ts.Seconds != 0)
            {
                str += ts.Seconds.ToString() + "秒";
            }
            return str;
        }
    }
}
