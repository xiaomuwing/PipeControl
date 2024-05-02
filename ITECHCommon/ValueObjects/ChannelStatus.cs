using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITECHCommon
{
    public enum ChannelStatus : byte
    {
        未知 = 0,
        正在连接 = 1,
        正常 = 2,
        断开连接 = 3,
        报警
    }
}
