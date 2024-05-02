using System;

namespace DataObjects
{
    public struct HistoryRecord
    {
        public DateTime TriggerTime { get; set; }
        public string DirectoryName { get; set; }
        public bool IsDCEnable { get; set; }
    }
}
