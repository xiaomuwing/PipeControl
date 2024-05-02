using HPSocket;
using HPSocket.Tcp;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ITECH
{
    public class ITECHInstrument : IDisposable, IEquatable<ITECHInstrument>
    {
        public event EventHandler OnConnectStatusChange;
        public string IPAddress { get; set; }
        public ushort Port { get; set; } = 30000;
        public double LastValueCurrent { get; private set; }
        public double LastValueVolt { get; private set; }
        public bool IsInit { get; private set; }
        public bool Enabled { get; private set; }
        public bool Connected { get; private set; }
        public bool IsDemo { get; private set; }
        private TcpClient client;
        public ITECHInstrument(string ip, bool demo)
        {
            IPAddress = ip;
            IsDemo = demo;
        }
        public void Init()
        {
            if(IsDemo)
            {
                IsInit = true;
                Connected = true;
            }
            client = new();
            client.OnConnect += Client_OnConnect;
            client.OnClose += Client_OnClose;
            client.OnReceive += Client_OnReceive;
            client.Connect(IPAddress, Port);
            IsInit = true;
        }
        public void Enable(bool enable)
        {
            if(IsInit && Connected)
            {
                Enabled = enable;
                string command;
                if (Enabled)
                {
                    command = "OUTP ON";
                }
                else
                {
                    command = "OUTP OFF";
                }
                SendCommand(command);
            }
        }
        private HandleResult Client_OnReceive(IClient sender, byte[] data)
        {
            string str = Encoding.Default.GetString(data);
            double.TryParse(str, out double d);
            if (d != 0)
            {
                LastValueVolt = d;
            }
            return HandleResult.Ok;
        }
        private HandleResult Client_OnClose(IClient sender, SocketOperation socketOperation, int errorCode)
        {
            Connected = false;
            OnConnectStatusChange?.Invoke(this, new EventArgs());
            return HandleResult.Ok;
        }
        private HandleResult Client_OnConnect(IClient sender)
        {
            Connected = true;
            Enable(false);
            SendCommand("SYST:REM");
            SendCommand("VOLT  150.00");
            GetVoltValue();
            OnConnectStatusChange?.Invoke(this, new EventArgs());
            return HandleResult.Ok;
        }
        public void GetVoltValue()
        {
            Task.Run(async () => { 
                while (Connected)
                {
                    try
                    {
                        SendCommand("MEAS:VOLT?");
                    }
                    catch
                    {

                    }
                    finally
                    {
                        await Task.Delay(2000);
                    }
                }
            });
        }
        public void SetCurrentValue(double value)
        {
            LastValueCurrent = value;
            if (Connected)
            {
                string command = "CURR " + value.ToString("0.000");
                SendCommand(command);
            }
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
        public void ZeroOutput()
        {
            SendCommand("CURR 0");
            SendCommand("OUTP 0");
        }
        public void Dispose()
        {
            Connected = false;
            SendCommand("CURR 0");
            SendCommand("OUTP 0");
            SendCommand("SYST:LOC");
            client.Dispose();
        }
        public bool Equals(ITECHInstrument other)
        {
            if (other.IPAddress == IPAddress)
            {
                return true;
            }
            return false;
        }
    }
}
