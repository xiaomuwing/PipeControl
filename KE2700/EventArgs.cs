using System;
using System.Collections.Generic;

namespace Keithley
{
    public class MsgEventArgs : EventArgs
    {
        public string Msg { get; }
        public MsgEventArgs(string str)
        {
            Msg = str;
        }
    }
    public class ChannelsEventArgs : EventArgs
    {
        public List<KEChannel> Channels { get; }
        public ChannelsEventArgs(List<KEChannel> channels)
        {
            Channels = channels;
        }
    }
}
