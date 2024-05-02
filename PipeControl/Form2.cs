using System;
using System.IO.Ports;
using System.Text;
using System.Windows.Forms;
namespace PipeControl
{
    public partial class Form2 : Form
    {
        private SerialPort MyCOM;
        public Form2()
        {
            InitializeComponent();
            FormClosed += Form2_FormClosed;

            MyCOM = new SerialPort();
            MyCOM.PortName = "COM1";
            MyCOM.BaudRate = 9600;
            MyCOM.StopBits = StopBits.One;
            MyCOM.Parity = Parity.None;
            MyCOM.DataBits = 8;
            MyCOM.Open();
            MyCOM.DiscardInBuffer();
            MyCOM.DataReceived += MyCOM_DataReceived;

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            SendCommand("OUTP OFF");
            MyCOM.Close();
            MyCOM.Dispose();
        }

        private void MyCOM_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = MyCOM.BytesToRead;
            byte[] buf = new byte[n];
            try
            {
                MyCOM.Read(buf, 0, n);
            }
            catch
            {

            }
            finally
            {
                MyCOM.DiscardInBuffer();
            }
            string receive = Encoding.UTF8.GetString(buf);
            ;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            SendCommand("OUTP?");
        }
        private static byte[] GetCommand(string command)
        {
            byte[] b = Encoding.Default.GetBytes(command);
            byte[] b2 = new byte[b.Length + 1];
            Array.Copy(b, b2, b.Length);
            b2[b.Length] = 10;
            return b2;
        }
        private void SendCommand(string str)
        {
            byte[] b = GetCommand(str);
            MyCOM.Write(b, 0, b.Length);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SendCommand("SYST:REM");
             SendCommand("OUTP ON");
            SendCommand("CURR 1.50");
            SendCommand("VOLT  30.00");
            SendCommand("CURR?");
            double.TryParse(textBox1.Text, out double i);
        }
    }
}
