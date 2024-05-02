using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITECHCommon
{
    public enum ChannelFunction : byte
    {
        热源 = 0,
        控温点 = 1,
        跟随温度 = 2,
        无 = 3
    }
}
