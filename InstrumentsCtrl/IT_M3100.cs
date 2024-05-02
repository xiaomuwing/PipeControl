using HPSocket.Tcp;
using System;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentsCtrl
{
    /// <summary>
    /// ITECH IT_M3123
    /// </summary>
    public sealed class IT_M3100 : IPower, IDisposable
    {
        private readonly TcpClient client = new();
        public string SN { get; set; }
        public DeviceType DeviceType { get; set; } = DeviceType.ITECH;
        public DeviceModel DeviceModel { get; set; } = DeviceModel.IT_M3123;
        public float CurrentV { get; set; }
        public float CurrentI { get; set; }
        public float MaxVolt { get; set; }
        public float MaxCurrent { get; set; }
        public bool Opened { get; set; }
        public string Address { get; set; }
        public ushort Port { get; set; }
        public string ChannelNo { get; set; }
        public bool IsOutput { get; set; }
        public IT_M3100(string addr, ushort p)
        {
            Address = addr;
            Port = p;
        }
        public async Task Open()
        {
            if (!Opened)
            {
                client.Connect(Address, Port);
                await Task.Delay(50);
                await SetRemote(true);
                Opened = true;
            }
        }
        private async Task SetRemote(bool remote)
        {
            if (remote)
            {
                SendCommand("SYST:REM");
            }
            else
            {
                SendCommand("SYST:LOC");
            }
            await Task.Delay(50);
        }
        public async Task SetOutputOn()
        {
            await Task.Delay(10);
            IsOutput = true;
        }
        public async Task SetCurrentOutput(float current)
        {
            CurrentI = current;
            SendCommand("CURR " + current.ToString("0.000"));
            await Task.Delay(50);
        }
        public async Task SetVoltOutput(float volt)
        {
            CurrentV = volt;
            SendCommand("VOLT " + volt.ToString("0.000"));
            await Task.Delay(50);
        }
        public async Task SetOutputOFF()
        {
            await Task.Delay(10);
            IsOutput = false;
        }
        public async Task Close()
        {
            if (Opened)
            {
                await SetCurrentOutput(0);
                await SetVoltOutput(0);
                await SetOutputOFF();
                await SetRemote(false);
            }
        }
        public async void Dispose()
        {
            await Close();
            client.Dispose();
        }

        private void SendCommand(string str)
        {
            byte[] b = GetCommand(str);
            client.Send(b, b.Length);
        }
        private static byte[] GetCommand(string command)
        {
            byte[] b = Encoding.Default.GetBytes(command);
            byte[] b2 = new byte[b.Length + 1];
            Array.Copy(b, b2, b.Length);
            b2[b.Length] = 10;
            return b2;
        }

        public async Task GetVolt()
        {
            await Task.Delay(1);
            return;
        }
    }
}
