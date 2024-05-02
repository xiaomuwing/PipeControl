using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace InstrumentsCtrl
{
    public sealed class IT6832 : IPower, IDisposable
    {
        private readonly SerialPort MyCOM = new();
        public string SN { get; set; }
        public float CurrentV { get; set; }
        public float CurrentI { get; set; }
        public float MaxVolt { get; set; }
        public float MaxCurrent { get; set; }
        public bool Opened { get; set; }
        public string Address { get; set; }
        public ushort Port { get; set; } = 0;
        public string ChannelNo { get; set; } = "";
        public byte EquipmentID { get; set; }
        public DeviceType DeviceType { get; set; } = DeviceType.ITECH;
        public DeviceModel DeviceModel { get; set; } = DeviceModel.IT6832;
        public bool IsOutput { get; set; }
        public IT6832(string port, int baudRate, StopBits stopBits, Parity parity, int dataBits, byte address)
        {
            Address = port;
            MyCOM.PortName = Address;
            MyCOM.BaudRate = baudRate;
            MyCOM.Parity = parity;
            MyCOM.DataBits = dataBits;
            MyCOM.StopBits = stopBits;
            EquipmentID = address;
        }
        public async Task Open()
        {
            if (!Opened)
            {
                MyCOM.Open();
                await Task.Delay(50);
                MyCOM.DiscardInBuffer();
                SetRemote(true);
                Opened = true;
            }
        }
        public async Task SetOutputOn()
        {
            await SetCurrentOutput(0);
            await SetVoltOutput(0);
            List<byte> bs = new() { 0xAA, EquipmentID, 0x21, 0x01, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            SendCommand(bs);
            IsOutput = true;
        }
        public async Task SetCurrentOutput(float current)
        {
            MyCOM.DiscardInBuffer();
            CurrentI = current;
            var bi = BitConverter.GetBytes((int)(CurrentI * 1000f));
            List<byte> bs = new() { 0xAA, EquipmentID, 0x24, bi[0], bi[1], 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            SendCommand(bs);
            await Task.Delay(150);
        }
        public async Task SetVoltOutput(float volt)
        {
            MyCOM.DiscardInBuffer();
            CurrentV = volt;

            var bv = BitConverter.GetBytes((int)(CurrentV * 1000f));
            List<byte> bs = new() { 0xAA, EquipmentID, 0x23, bv[0], bv[1], bv[2], bv[3], 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            SendCommand(bs);
            await Task.Delay(150);
        }
        public async Task SetOutputOFF()
        {
            await SetCurrentOutput(0);
            await SetVoltOutput(0);
            //await Task.Delay(150);
            List<byte> bs = new() { 0xAA, EquipmentID, 0x21, 0x00, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            SendCommand(bs);
            IsOutput = false;
        }
        public async Task Close()
        {
            if (Opened)
            {
                await SetCurrentOutput(0);
                await SetVoltOutput(0);
                SetRemote(false);
                await Task.Delay(80);
                await SetOutputOFF();
                MyCOM.Close();
                Opened = false;
            }

        }
        private void SetRemote(bool remote)
        {
            List<byte> bs = new() { 0xAA, EquipmentID, 0x20, 0x01, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            if (remote)
            {
                bs[3] = 1;
            }
            else
            {
                bs[3] = 0;
            }
            SendCommand(bs);
        }
        private void SendCommand(List<byte> bs)
        {
            bs.Add(CalculateChecksum(bs.ToArray()));
            MyCOM.Write(bs.ToArray(), 0, bs.Count);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte CalculateChecksum(byte[] data)
        {
            int sum = data.Sum(b => b);
            byte b2 = (byte)(sum & 0xFF);
            return b2;
        }
        public void Dispose()
        {
            MyCOM.Dispose();
        }

        public async Task GetVolt()
        {
            await Task.Delay(1);
            return;
        }

        public async Task<bool> IsOnline()
        {
            MyCOM.DiscardInBuffer();
            List<byte> bs = new() { 0xAA, EquipmentID, 0x31, 0x01, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            SendCommand(bs);
            await Task.Delay(80);
            if (MyCOM.BytesToRead == 26)
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
