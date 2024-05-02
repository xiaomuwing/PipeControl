using Keithley;
using System.Collections.Generic;
using System;
namespace InstrumentsCtrl
{
    public class Instruments : IDisposable
    {
        public event EventHandler<ChannelsEventArgs> OnKEChangeConfig;
        public List<IPower> Powers { get; set; } = new();
        public KECtrl KECtrl { get; set; } = new(false);
        public Instruments()
        {
            KECtrl.OnChangeConfig += KECtrl_OnChangeConfig;
        }

        private void KECtrl_OnChangeConfig(object sender, ChannelsEventArgs e)
        {
            OnKEChangeConfig?.Invoke(sender, e);
        }

        public IPower GetPower(string address, string channelNO)
        {
            foreach (var power in Powers)
            {
                if(power.Address == address && power.ChannelNo == channelNO)
                {
                    return power;
                }
            }
            return null;
        }
        public KEChannel GetKEChannel(string address, string channelNO)
        {
            foreach(var channel in KECtrl.Channels)
            {
                if(channel.DeviceAddress == address && channel.Address == channelNO)
                {
                    return channel;
                }
            }
            return null;
        }

        public async void Dispose()
        {
            KECtrl.Dispose();
            foreach(var power in Powers)
            {
                await power.Close();
            }

        }
    }
}
