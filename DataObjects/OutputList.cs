using System;

namespace DataObjects
{
    /// <summary>
    /// 用相对时间标注的输出列表
    /// </summary>
    public struct OutputDataRelative
    {
        public int ID { get; set; }
        public TimeSpan TimeSpan { get; set; }
        public double OutputValue { get; set; }
    }
    /// <summary>
    /// 用绝对时间标注的输出列表
    /// </summary>
    public struct OutputDataAbsolute
    {
        public int ID { get; set; }
        public DateTime DateTime { get; set; }
        public double OutputValue { get; set; }
    }
}
