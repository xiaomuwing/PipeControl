using PipeControl.Common;
using System;
using System.IO.Ports;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InstrumentsCtrl
{
    /// <summary>
    /// include ITECH 6722A, 6726G
    /// </summary>
    public sealed class IT6700 : IPower, IDisposable
    {
        private readonly SerialPort MyCOM = new();
        public string SN { get;set; }
        public float CurrentV { get; set; }
        public float CurrentI { get; set; }
        public float MaxVolt { get; set; }
        public float MaxCurrent { get; set; }
        public bool Opened { get; set; }
        public string Address { get; set; }
        public ushort Port { get; set; } = 0;
        public string ChannelNo { get; set; } = "";
        public DeviceType DeviceType { get; set; } = DeviceType.ITECH;
        public DeviceModel DeviceModel { get; set; }
        public bool IsOutput { get; set; }
        public IT6700(string port, int baudRate, StopBits stopBits, Parity parity, int dataBits)
        {
            Address = port;
            MyCOM.PortName = port;
            MyCOM.BaudRate = baudRate;
            MyCOM.Parity = parity;
            MyCOM.DataBits = dataBits;
            MyCOM.StopBits = stopBits;
        }
        public async Task Open()
        {
            if(!Opened)
            {
                MyCOM.Open();
                await Task.Delay(100);
                MyCOM.DiscardInBuffer();
                SendCommand("SYST:REM");
                Opened = true;
            }
        }
        public async Task SetOutputOn()
        {
            await SetCurrentOutput(0);
            await SetVoltOutput(0);
            SendCommand("OUTP ON");
            Log.WriteLog(string.Format("IT6722A打开输出"), "CtrlDC", "Control");
            IsOutput = true;
        }
        public async Task SetCurrentOutput(float current)
        {
            CurrentI = current;
            SendCommand("CURR " + CurrentI.ToString("0.000"));
            Log.WriteLog(string.Format("IT6722A输出电流{0}", current), "CtrlDC", "Control");
            await Task.Delay(100);
        }
        public async Task SetVoltOutput(float volt)
        {
            CurrentV = volt;
            if (volt != 0)
            {
                SendCommand("VOLT:LIMIT " + volt.ToString("0.000"));
            }
            SendCommand("VOLT " + volt.ToString("0.000"));
            Log.WriteLog(string.Format("IT6722A输出电压{0}", volt), "CtrlDC", "Control");
            await Task.Delay(100);
        }
        public async Task SetOutputOFF()
        {
            SendCommand("OUTP OFF");
            Log.WriteLog(string.Format("IT6722A关闭输出"), "CtrlDC", "Control");
            await Task.Delay(100);
            IsOutput = false;
        }
        public async Task GetVolt()
        {
            SendCommand("MEAS:VOLT?");
            await Task.Delay(100);
            byte[] bs = new byte[MyCOM.BytesToRead];
            MyCOM.Read(bs, 0, bs.Length);
            var volt = Encoding.UTF8.GetString(bs).Split('\n')[0];
            CurrentV = float.Parse(volt);
        }
        public async Task Close()
        {
            if(Opened)
            {
                await SetCurrentOutput(0);
                await SetVoltOutput(0);
                await SetOutputOFF();
                SendCommand("SYST:LOC");
                MyCOM.Close();
                Opened = false;
            }
        }
        private void SendCommand(string str)
        {
            byte[] b = GetCommand(str);
            MyCOM.Write(b, 0, b.Length);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] GetCommand(string command)
        {
            byte[] b = Encoding.Default.GetBytes(command);
            byte[] b2 = new byte[b.Length + 1];
            Array.Copy(b, b2, b.Length);
            b2[b.Length] = 10;
            return b2;
        }
        public void Dispose()
        {
            MyCOM.Dispose();
        }

        public async Task<bool> IsOnline()
        {
            SendCommand("MEAS:VOLT?");
            await Task.Delay(100);
            byte[] bs = new byte[MyCOM.BytesToRead];
            MyCOM.Read(bs, 0, bs.Length);
            var volt = Encoding.UTF8.GetString(bs).Split('\n')[0];
            if(float.TryParse(volt, out _))
            {
                if(!Opened)
                {
                    MyCOM.Close();
                    await Open();
                    //await SetOutputOn();
                }
                Opened = true;
                return true;
            }
            Opened = false;
            return false;
        }
    }
}
