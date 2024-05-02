using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HPSocket;
using HPSocket.Tcp;
using Keithley;
namespace PipeControl
{
    public partial class Form1 : Form
    {
        //private TcpClient client;
        private Keithley.KECtrl ke = new(false);
        public Form1()
        {
            InitializeComponent();

            Load += Form1_Load;
            FormClosing += Form1_FormClosing;

            //client = new();
            //client.OnClose += Client_OnClose;
            //client.OnConnect += Client_OnConnect;
            //client.OnReceive += Client_OnReceive;
            //client.Connect("192.168.0.7", 58);
            ke.ReadDevices("KEDevices.txt");
            ke.OnConnected += Ke_OnConnected;
            ke.Connect();

        }

        private void Ke_OnConnected(object sender, EventArgs e)
        {
            foreach(var device in ke.Devices.Devices)
            {
                if(device != null)
                {
                    device.OnReceived += Device_OnReceived;
                }
            }
        }

        private void Device_OnReceived(object sender, MsgEventArgs e)
        {
            KEDevice device = (KEDevice)sender;
            BeginInvoke(new MethodInvoker(delegate { 

                string line = device.IPAddress.ToString() + " ";
                var channels = device.Channels.Where(x => x.Address == "102" || x.Address == "103" || x.Address == "104" || x.Address == "105");
                foreach( var channel in channels)
                {
                    line += channel.Address + ":" + channel.LastData.ToString("0.000") + ";  ";
                }
                lb_Data.Items.Add(line);
            }));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ke.Dispose();
        }

        private HandleResult Client_OnReceive(IClient sender, byte[] data)
        {
            string str = Encoding.Default.GetString(data);
            lb.Items.Add(str);
            return HandleResult.Ok;
        }

        private HandleResult Client_OnConnect(IClient sender)
        {
            return HandleResult.Ok;
        }

        private HandleResult Client_OnClose(IClient sender, SocketOperation socketOperation, int errorCode)
        {
            return HandleResult.Ok;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //lb.Items.Add(txt_Send.Text);
            //SendCommand(txt_Send.Text);

            ke.ShowConfig();
        }

        public void SendCommand(string str)
        {
            byte[] b = GetCommand(str);
            try
            {
                //if (client.State == ServiceState.Started)
                //{
                //    client.Send(b, b.Length);
                //}
            }
            catch { }
        }
        private static byte[] GetCommand(string command)
        {
            byte[] b = Encoding.Default.GetBytes(command);
            byte[] b2 = new byte[b.Length + 1];
            Array.Copy(b, b2, b.Length);
            b2[b.Length] = 10;
            return b2;
        }
    }
}
