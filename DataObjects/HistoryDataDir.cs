using System;

namespace DataObjects
{
    public class HistoryDataDir
    {
        public string Directory { get; set; }
        public DateTime BeginTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string ShowString { get; set; }
    }
}
