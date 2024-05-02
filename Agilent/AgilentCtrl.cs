using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Agilent
{
    public class AgilentCtrl : IDisposable
    {
        public event EventHandler OnConnected;
        public event EventHandler OnDisconnected;
        public bool IsDemo { get; private set; }
        public List<AgilentDevice> Devices { get; private set; } = new();
        private int connectedDevices = 0;
        public AgilentCtrl(bool demo)
        {
            IsDemo = demo;
        }
        public void ReadDevices()
        {
            Devices.Clear();
            connectedDevices = 0;
            if (!File.Exists("AgilentDevice.txt"))
                return;
            using FileStream fs = new("AgilentDevice.txt", FileMode.Open);
            StreamReader sr = new(fs);
            while (!sr.EndOfStream)
            {
                string str = sr.ReadLine();
                if (string.IsNullOrEmpty(str))
                {
                    continue;
                }
                string[] strs = str.Split(':');
                AgilentDevice device = new(strs[0], ushort.Parse(strs[1]), IsDemo);
                device.OnConnected += Device_OnConnected;
                device.OnDisconnected += Device_OnDisconnected;
                Devices.Add(device);
            }
            int cnt = 0;
            foreach (var device in Devices.OrderBy(x => x.IPAddress))
            {
                cnt += 1;
                device.ID = cnt;
                device.Name = "DC" + cnt.ToString("00");
            }
            sr.Close();
            sr.Dispose();
            fs.Close();
        }

        private void Device_OnDisconnected(object sender, EventArgs e)
        {
            OnDisconnected?.Invoke(sender, e);
            AgilentDevice device = (AgilentDevice)sender;
            device.Connect();
        }

        public void Connect()
        {
            foreach (AgilentDevice device in Devices)
            {
                device.Connect();
            }
        }
        private void Device_OnConnected(object sender, EventArgs e)
        {
            OnConnected?.Invoke(sender, new EventArgs());
        }
        public AgilentDevice GetAgilentDeviceByIP(string ip)
        {
            foreach (var device in Devices)
            {
                if (device.IPAddress == ip)
                {
                    return device;
                }
            }
            return null;
        }
        public void Dispose()
        {
            foreach (AgilentDevice device in Devices)
            {
                device.Dispose();
            }
        }
    }
}
