﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Keithley
{
    [Serializable]
    public sealed class KEDevices
    {
        public List<KEDevice> Devices { get; set; } = new();
    }
}
