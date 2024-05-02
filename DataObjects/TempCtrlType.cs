using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    /// <summary>
    /// 温区控温方式
    /// </summary>
    public enum TempCtrlType : byte
    {
        默认 = 0,
        跟随测点 = 1,
        导入列表_相对时间 = 2,
        导入列表_绝对时间 = 3,
    }
}
