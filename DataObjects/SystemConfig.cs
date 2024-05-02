using System;
using System.IO;

namespace DataObjects
{
    public static class SystemConfig
    {
        static readonly string configFile = AppDomain.CurrentDomain.BaseDirectory + "\\SystemConfig.txt";
        /// <summary>
        /// 最大输出电流
        /// </summary>
        public static double MaxCurrent { get; set; }
        /// <summary>
        /// 最小输出电流
        /// </summary>
        public static double MinCurrent { get; set; }
        /// <summary>
        /// 升降温速率
        /// </summary>
        public static double R { get; set; }
        /// <summary>
        /// 控温周期，单位秒
        /// </summary>
        public static int ControlCycle { get; set; }
        /// <summary>
        /// 算法
        /// </summary>
        public static Algorithm Algorithm { get; set; } = new();
        public static void ReadConfig()
        {
            Algorithms algorithms = new();
            using FileStream fs = new(configFile, FileMode.Open, FileAccess.Read);
            using StreamReader sr = new(fs);
            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split('=');
                if (line.Length <= 1)
                {
                    continue;
                }
                if (line[0] == "最大输出电流(A)")
                {
                    if (double.TryParse(line[1].Trim(), out double max))
                    {
                        MaxCurrent = max;
                    }
                }
                if (line[0] == "最小输出电流(A)")
                {
                    if (double.TryParse(line[1].Trim(), out double min))
                    {
                        MinCurrent = min;
                    }
                }
                if (line[0] == "升降温速率(℃/s)")
                {
                    if (double.TryParse(line[1].Trim(), out double r))
                    {
                        R = r;
                    }
                }
                if (line[0] == "控温周期(秒)")
                {
                    if (int.TryParse(line[1].Trim(), out int cc))
                    {
                        ControlCycle = cc;
                    }
                }
                if (line[0] == "控温算法")
                {
                    var al = algorithms.GetObject(line[1].Trim());
                    if (al != null)
                    {
                        Algorithm = al;
                    }
                }
            }
        }
    }
}
