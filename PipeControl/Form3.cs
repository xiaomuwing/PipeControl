using DataObjects;
using InstrumentsCtrl;
using System;
using System.IO.Ports;
using System.Windows.Forms;
namespace PipeControl
{

    public partial class Form3 : Form
    {
        IT6700 iT6722A;
        IT6832 it6832;
        public Form3()
        {
            InitializeComponent();
            //iT6722A = new("COM1", 9600, StopBits.One, Parity.None, 8);
            it6832 = new("COM4", 9600, StopBits.One, Parity.None, 8, 1);
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            //iT6722A.Open();
            await it6832.Open();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            //iT6722A.SetOutputOn();
            await it6832.SetOutputOn();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            //iT6722A.SetOutput(1.5, 80);
            await it6832.SetCurrentOutput(0.5f);
            await it6832.SetVoltOutput(33f);
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await it6832.SetOutputOFF();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            await it6832.Close();
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            await EXCELCtrl.ReadEXCEL("D:\\repos\\PipeControl\\DOC\\工况配置.xlsx");
            var circles = EXCELCtrl.ControlCircles;
            var channels = EXCELCtrl.Channels;

            Instruments instruments = new();

            instruments.Powers = Experiment.GetPowers(channels);

            foreach (var device in Experiment.GetKEDevices(channels))
            {
                instruments.KECtrl.AddDevice(device);
            }
        }
    }
}
