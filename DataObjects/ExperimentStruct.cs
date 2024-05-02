using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public struct ExperimentStruct
    {
        /// <summary>
        /// 试验编号
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 试验名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 试验备注
        /// </summary>
        public string Commet { get; set; }
        /// <summary>
        /// 读取测点信息的采样时间(ms)
        /// </summary>
        public int Xdelta { get; set; } 
        /// <summary>
        /// 控制周期(ms)
        /// </summary>
        public int ControlCycle { get; set; } 
        /// <summary>
        /// 试验创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 试验最后运行时间
        /// </summary>
        public DateTime LastRunDate { get; set; }
        /// <summary>
        /// 试验保存数据和配置文件的根目录
        /// </summary>
        public string MainDirectory { get; set; }
        /// <summary>
        /// 温区列表
        /// </summary>
        public List<TempRange> Ranges { get; set; }
        /// <summary>
        /// 所有测点列表
        /// </summary>
        public List<Channel> Channels { get; set; } 
        public ExperimentStruct(int i)
        {
            ID = 0;
            Name = "";
            Commet = "";
            Xdelta = 1000;
            ControlCycle = 6000;
            CreateDate = DateTime.Now;
            LastRunDate = DateTime.Now;
            MainDirectory = "";
            Ranges = new();
            Channels = new();
        }
    }
}
