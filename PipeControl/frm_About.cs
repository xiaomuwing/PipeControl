using PipeControl.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace PipeControl
{
    public partial class frm_About : Form
    {
        public frm_About()
        {
            InitializeComponent();
            txt_RCode1.KeyDown += Txt_RCode1_KeyDown;
            Load += Frm_About_Load;
            btn_Cancel.Click += Btn_ChannelConfigOK_Click;
            btn_Reg.Click += Btn_Reg_Click;
        }
        private void Txt_RCode1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true && e.KeyCode == Keys.V)
            {
                Thread td = new Thread(() => {
                    GetClipboard();
                });
                td.TrySetApartmentState(ApartmentState.STA);
                td.IsBackground = true;
                td.Start();
            }
        }
        private void GetClipboard()
        {

            string str = Clipboard.GetText();
            string[] strs = str.Split('-');
            if (strs.Length == 6)
            {
                BeginInvoke(new MethodInvoker(delegate {
                    txt_RCode1.Text = strs[0];
                    txt_RCode2.Text = strs[1];
                    txt_RCode3.Text = strs[2];
                    txt_RCode4.Text = strs[3];
                    txt_RCode5.Text = strs[4];
                    txt_RCode6.Text = strs[5];
                }));
            }
        }
        private Register reg;
        private void Frm_About_Load(object sender, EventArgs e)
        {
            Label3.Text = "版本号：" + Application.ProductVersion;
            reg = new Register();
            if (!reg.IsReg())
            {
                gb.Visible = true;
                string mNum = reg.MNum;
                string result = "";
                for (int i = 0; i < mNum.Length; i += 4)
                {
                    result += mNum.Substring(i, 4) + "-";
                }
                txt_MCode.Text = result.Substring(0, result.Length - 1);
                txt_RCode1.Focus();
            }
            else
            {
                gb.Visible = false;
            }

        }
        private void Btn_ChannelConfigOK_Click(object sender, EventArgs e)
        {
            if (!reg.IsReg())
            {

                MessageBox.Show("程序没有正确注册", "真空温控系统", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
            else
            {
                Close();
            }
        }
        private void Btn_Reg_Click(object sender, EventArgs e)
        {
            string rNum = txt_RCode1.Text + txt_RCode2.Text + txt_RCode3.Text + txt_RCode4.Text + txt_RCode5.Text + txt_RCode6.Text;
            if (rNum == reg.RNum)
            {
                MessageBox.Show("注册成功！", "真空温控系统", MessageBoxButtons.OK, MessageBoxIcon.Information);
                reg.WriteReg();
                Close();
            }
            else
            {
                txt_RCode1.Text = "";
                txt_RCode2.Text = "";
                txt_RCode3.Text = "";
                txt_RCode4.Text = "";
                txt_RCode5.Text = "";
                txt_RCode6.Text = "";
                txt_RCode1.Focus();
            }
        }
    }
}
