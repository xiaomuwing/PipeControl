using Keithley;
using System.Collections.Generic;
using System;
using PipeControl.Common;
using System.Threading.Tasks;

namespace InstrumentsCtrl
{
    public class Instruments
    {
        public event EventHandler<ChannelsEventArgs> OnKEChangeConfig;
        public List<IPower> Powers { get; set; } = new();
        public KECtrl KECtrl { get; set; } = new(false);
        readonly MMTimer statusTimer = new();
        public Instruments()
        {
            KECtrl.OnChangeConfig += KECtrl_OnChangeConfig;
            statusTimer.Mode = TimerMode.Periodic;
            statusTimer.Period = 1000;
            statusTimer.Tick += StatusTimer_Tick;
        }
        public async Task Open()
        {
            try
            {
                Parallel.For(0, Powers.Count, async i =>
                {
                    await Powers[i].Open();
                });
                await Task.Delay(100);
                statusTimer.Start();
            }
            catch { }
        }
        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            Parallel.For(0, Powers.Count, async i =>
            {
                await Powers[i].IsOnline();
            });
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
        public async Task Close()
        {
            statusTimer.Stop();
            statusTimer.Dispose();
            KECtrl.Dispose();
            Parallel.For(0, Powers.Count, async i => {
                await Powers[i].Close();
            });
            await Task.Delay(1);
        }
    }
}
