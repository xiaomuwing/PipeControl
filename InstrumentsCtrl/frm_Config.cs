using PipeControl.Common;
using System;
using System.Windows.Forms;

namespace InstrumentsCtrl
{
    public partial class frm_Config : Form
    {
        public float OutCurrent { get; set; }
        public float OutVolt { get; set; }
        readonly IPower power;
        public frm_Config(IPower p)
        {
            InitializeComponent();
            power = p;

            Load += Frm_Config_Load;

        }
        private async void Frm_Config_Load(object sender, EventArgs e)
        {
            lbl_Info.Text = "电源类型：" + power.DeviceModel.ToString();
            if(power.Opened)
            {
                txt_CurrentValue.Text = power.CurrentI.ToString();
                txt_VoltValue.Text = power.CurrentV.ToString();
                chk_OpenOrClose.Checked = true;

            }
            else
            {
                try
                {
                    await power.Open();
                    await power.SetOutputOn();
                    chk_OpenOrClose.Checked = false;
                }
                catch
                {
                    chk_OpenOrClose.Enabled = false;
                }
            }
            

        }
        private void chk_TopMost_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = chk_TopMost.Checked;
        }
        private async void chk_OpenOrClose_CheckedChanged(object sender, EventArgs e)
        {
            if (!chk_OpenOrClose.Checked)
            {
                await power.SetOutputOFF();
                await power.Close();
                groupBox1.Enabled = false;
            }
            else
            {
                await power.Open();
                await power.SetOutputOn();
                groupBox1.Enabled = true;
            }
        }
        private void btn_Output_Click(object sender, EventArgs e)
        {
            if (!float.TryParse(txt_VoltValue.Text, out float v))
            {
                txt_VoltValue.Focus();
                return;
            }
            if (!float.TryParse(txt_CurrentValue.Text, out float i))
            {
                txt_CurrentValue.Focus();
                return;
            }
            if(v > power.MaxVolt)
            {
                MessageBox.Show("输入的电压值超过设备最大电压" + power.MaxVolt.ToString() + "V");
                txt_VoltValue.Focus();
                return;
            }
            if (i > power.MaxCurrent)
            {
                MessageBox.Show("输入的电流值超过设备最大电流" + power.MaxCurrent.ToString() + "A");
                txt_CurrentValue.Focus();
                return;
            }
            power.SetVoltOutput(v);
            power.SetCurrentOutput(i);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //txt_CurrentValue.Text = power.CurrentI.ToString();
            //txt_VoltValue.Text = power.CurrentV.ToString();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //var oldOnline = power.Opened;
            //var online = await power.IsOnline();
            //label3.Text = online.ToString();
        }
    }
}
