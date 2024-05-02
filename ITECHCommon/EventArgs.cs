using System;
using System.Collections.Generic;

namespace ITECHCommon
{
    public class BoolValueEventArgs : EventArgs
    {
        public bool Value { get; }
        public BoolValueEventArgs(bool t)
        {
            Value = t;
        }
    }
    public class StringEventArgs : EventArgs
    {
        public string Value { get; set; }
        public StringEventArgs(string str)
        {
            Value = str;
        }
    }
}
