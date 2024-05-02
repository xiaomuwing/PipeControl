using InstrumentsCtrl;
using System;
using System.Collections.Generic;

namespace DataObjects
{
    public class DeviceTypeEventArgs : EventArgs
    {
        public DeviceType DeviceType { get; private set; }
        public DeviceTypeEventArgs(DeviceType device)
        {
            DeviceType = device;
        }
    }
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
    public class DoubleEventArgs : EventArgs
    {
        public double Value { get; set; }
        public DoubleEventArgs(double d)
        {
            Value = d;
        }
    }
    public class ChannelsEventArgs : EventArgs
    {
        public List<Channel> Value { get; set; }
        public ChannelsEventArgs(List<Channel> value)
        {
            Value = value;
        }
    }
}
