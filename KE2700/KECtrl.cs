using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Keithley
{
    public sealed class KECtrl : IDisposable
    {
        public event EventHandler<ChannelsEventArgs> OnChangeConfig;
        public event EventHandler OnConnected;
        public bool IsDemo { get; private set; }
        public KEDevices Devices { get; private set; } = new();
        public List<KEChannel> Channels { get; private set; } = new();
        public KECtrl(bool demo)
        {
            IsDemo = demo;
        }
        public void ReadDevices(string file)
        {
            if (!File.Exists(file))
                return;
            Devices.Devices.Clear();
            Channels.Clear();
            using (FileStream fs = new(file, FileMode.Open))
            {
                StreamReader sr = new(fs);
                while (!sr.EndOfStream)
                {
                    try
                    {
                        string[] strs = sr.ReadLine().Split(':');
                        KEDevice device = new(strs[0], IsDemo, ushort.Parse(strs[1]));
                        device.CreateChannels();
                        device.OnConnected += Device_OnConnected;
                        Devices.Devices.Add(device);
                    }
                    catch (Exception ex)
                    {
                        string str = ex.Message;
                    }
                }
                sr.Close();
                sr.Dispose();
                fs.Close();
            }
            int id = 1;
            foreach (KEDevice inst in Devices.Devices.OrderBy(x => x.IPAddress))
            {
                foreach (KEChannel channel in inst.Channels.OrderBy(x => x.Address))
                {
                    channel.ID = id;
                    Channels.Add(channel);
                    id += 1;
                }
            }
        }
        public void AddDevice(string ip, bool demo, ushort port)
        {
            KEDevice device = new(ip, demo, port);
            device.CreateChannels();
            device.OnConnected += Device_OnConnected;
            Devices.Devices.Add(device);
            Channels.Clear();
            int id = 1;
            foreach (KEDevice inst in Devices.Devices.OrderBy(x => x.IPAddress))
            {
                foreach (KEChannel channel in inst.Channels.OrderBy(x => x.Address))
                {
                    channel.ID = id;
                    Channels.Add(channel);
                    id += 1;
                }
            }
        }
        public void AddDevice(KEDevice device)
        {
            device.CreateChannels();
            device.OnConnected += Device_OnConnected;
            Devices.Devices.Add(device);
            Channels.Clear();
            int id = 1;
            foreach (KEDevice inst in Devices.Devices.OrderBy(x => x.IPAddress))
            {
                foreach (KEChannel channel in inst.Channels.OrderBy(x => x.Address))
                {
                    channel.ID = id;
                    Channels.Add(channel);
                    id += 1;
                }
            }
        }
        public void Connect()
        {
            foreach (KEDevice device in Devices.Devices)
            {
                device.Connect();
            }
        }
        public KEChannel GetChannel(string ip, string address)
        {
            foreach (var channel in Channels)
            {
                if (channel.DeviceAddress == ip && channel.Address == address)
                {
                    return channel;
                }
            }
            return null;
        }
        public void SaveConfig(string file)
        {
            File.WriteAllText(file, JsonConvert.SerializeObject(Devices, Formatting.Indented));
        }
        public void LoadConfig(string file)
        {
            string json = File.ReadAllText(file);
            KEDevices tmpDevices = JsonConvert.DeserializeObject<KEDevices>(json);
            foreach (var device in tmpDevices.Devices)
            {
                foreach (var tmpchannel in device.Channels)
                {
                    var channel = GetChannel(tmpchannel.DeviceAddress, tmpchannel.Address);
                    if (channel != null)
                    {
                        channel.ChannelType = tmpchannel.ChannelType;
                        channel.TransducerType = tmpchannel.TransducerType;
                        channel.ThermocoupleType = tmpchannel.ThermocoupleType;
                        channel.ThermistorType = tmpchannel.ThermistorType;
                        channel.FourWireRTDType = tmpchannel.FourWireRTDType;
                        channel.ColdJunc = tmpchannel.ColdJunc;
                        if (channel.ChannelType != ChannelType.无)
                        {
                            channel.IsConfiged = true;
                        }
                    }
                }
            }
        }
        public void SendConfig(List<KEChannel> channels)
        {
            var deviceAddresses = channels.GroupBy(x => x.DeviceAddress).Select(x => x.Key).ToList();
            foreach (KEDevice device in Devices.Devices)
            {
                if (deviceAddresses.Contains(device.IPAddress))
                {
                    device.PrepareCommands();
                    device.BeginTrigger();
                }
            }
            ChannelsEventArgs cea = new(channels);
            OnChangeConfig?.Invoke(this, cea);
        }
        public void ShowConfig()
        {
            frm_Main f = new(this);
            f.Show();
        }
        private void Device_OnConnected(object sender, EventArgs e)
        {
            OnConnected?.Invoke(sender, new EventArgs());
            //if (connectedDevices == Devices.Devices.Count)
            //{
            //    OnConnected?.Invoke(this, new EventArgs());
            //}
        }
        public void Dispose()
        {
            foreach (KEDevice device in Devices.Devices)
            {
                device.Dispose();
                //Log.WriteLog(device.IPAddress + " disposed. ", "Dispose", "Keithley");
            }
        }
    }
}
