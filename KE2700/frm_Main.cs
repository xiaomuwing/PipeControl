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

namespace Keithley
{
    public partial class frm_Main : Form
    {
        readonly KECtrl ke;
        readonly TreeNode root;
        readonly List<KEChannel> selectedChannels = new();
        private void EnableDoubleBuffering()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
        public frm_Main(KECtrl ctrl)
        {
            EnableDoubleBuffering();
            InitializeComponent();
            ke = ctrl;
            Load += Frm_Main_Load;
            FormClosing += Frm_Main_FormClosing;
            root = new("所有设备");
            root.Name = "Root";
            tv.Nodes.Add(root);
            dgv_Channel.SelectionChanged += Dgv_Channel_SelectionChanged;
            cmb_ChannelType.SelectedIndexChanged += Cmb_ChannelType_SelectedIndexChanged;
            cmb_Transducer.SelectedIndexChanged += Cmb_Transducer_SelectedIndexChanged;
            foreach (string ct in Enum.GetNames(typeof(ChannelType)))
            {
                cmb_ChannelType.Items.Add(ct);
            }
            foreach (string ct in Enum.GetNames(typeof(TransducerType)))
            {
                cmb_Transducer.Items.Add(ct);
            }
            cmb_ChannelType.Items.Add("");
            cmb_Transducer.Items.Add("");
            cmb_Thermocouple.Items.Add("");
        }
        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {

            source.Cancel();
        }
        private void Frm_Main_Load(object sender, EventArgs e)
        {
            root.Nodes.Clear();
            dgv_Channel.Rows.Clear();
            foreach(KEDevice inst in ke.Devices.Devices)
            {
                TreeNode keiNode = new(inst.IPAddress);
                keiNode.Name = "Root_" + inst.IPAddress;
                foreach (KEChannel channel in inst.Channels)
                {
                    TreeNode channelNode = new(channel.Address);
                    channelNode.Name = keiNode.Name + "_" + channel.Address;
                    keiNode.Nodes.Add(channelNode);
                    dgv_Channel.Rows.Add(new object[] { inst.IPAddress, channel.Address, channel.ChannelType.ToString(), channel.TransducerType.ToString(), channel.ThermocoupleType.ToString(), channel.ThermistorType.ToString(), channel.FourWireRTDType.ToString(), channel.ColdJunc ? "是" : "否", channel.LastData });
                }
                root.Nodes.Add(keiNode);
                root.Expand();
            }
            token = source.Token;
            ShowChannelData();
        }
        private void Cmb_ChannelType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_ChannelType.Text)
            {
                case "温度":
                    cmb_Transducer.Enabled = true;
                    cmb_Thermocouple.Enabled = false;
                    break;
                case "":
                    cmb_Transducer.Enabled = false;
                    cmb_Thermocouple.Enabled = false;
                    break;
                default:
                    cmb_Transducer.Enabled = false;
                    cmb_Thermocouple.Enabled = false;
                    break;
            }
        }
        private void Cmb_Transducer_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmb_Thermocouple.Items.Clear();
            switch (cmb_Transducer.Text)
            {
                case "热电偶":
                    foreach (string ct in Enum.GetNames(typeof(ThermocoupleType)))
                    {
                        cmb_Thermocouple.Items.Add(ct);
                    }
                    cmb_Thermocouple.Enabled = true;
                    label4.Text = cmb_Transducer.Text;
                    chk_ColdJunc.Checked = false;
                    chk_ColdJunc.Visible = true;
                    break;
                case "热敏电阻":
                    foreach (string ct in Enum.GetNames(typeof(ThermistorType)))
                    {
                        cmb_Thermocouple.Items.Add(ct);
                    }
                    cmb_Thermocouple.Enabled = true;
                    label4.Text = cmb_Transducer.Text;
                    chk_ColdJunc.Visible = false;
                    break;
                case "RTD":
                    foreach (string ct in Enum.GetNames(typeof(FourWireRTDType)))
                    {
                        cmb_Thermocouple.Items.Add(ct);
                    }
                    cmb_Thermocouple.Enabled = true;
                    label4.Text = cmb_Transducer.Text;
                    chk_ColdJunc.Visible = false;
                    break;
                default:
                    cmb_Thermocouple.Enabled = false;
                    break;
            }
        }
        private void Dgv_Channel_SelectionChanged(object sender, EventArgs e)
        {
            selectedChannels.Clear();
            if (dgv_Channel.SelectedRows.Count == 1)
            {
                string ip = dgv_Channel.SelectedRows[0].Cells[0].Value.ToString();
                string address = dgv_Channel.SelectedRows[0].Cells[1].Value.ToString();
                KEChannel channel = ke.GetChannel(ip, address);
                selectedChannels.Add(channel);
                txt_InstrumentAddress.Text = channel.DeviceAddress;
                txt_ChannelAddress.Text = channel.Address;
                cmb_ChannelType.Text = channel.ChannelType.ToString();
                cmb_Transducer.Text = channel.TransducerType.ToString();
                cmb_Thermocouple.Text = channel.ThermocoupleType.ToString();
            }
            else
            {
                foreach (DataGridViewRow row in dgv_Channel.SelectedRows)
                {
                    string ip = row.Cells[0].Value.ToString();
                    string address = row.Cells[1].Value.ToString();
                    KEChannel channel = ke.GetChannel(ip, address);
                    selectedChannels.Add(channel);
                }
                var c1 = selectedChannels.GroupBy(x => x.DeviceAddress);
                if (c1.Count() == 1)
                {
                    txt_InstrumentAddress.Text = c1.First().Key;
                }
                txt_ChannelAddress.Text = "多值…";
                var c2 = selectedChannels.GroupBy(x => x.ChannelType);
                if (c2.Count() == 1)
                {
                    cmb_ChannelType.Text = c2.First().Key.ToString();
                }
                else
                {
                    cmb_ChannelType.Text = "";
                }
                var c3 = selectedChannels.GroupBy(x => x.TransducerType);
                if (c3.Count() == 1)
                {
                    cmb_Transducer.Text = c3.First().Key.ToString();
                }
                else
                {
                    cmb_Transducer.Text = "";
                }
                var c4 = selectedChannels.GroupBy(x => x.ThermocoupleType);
                if (c4.Count() == 1)
                {
                    cmb_Thermocouple.Text = c4.First().Key.ToString();
                }
                else
                {
                    cmb_Thermocouple.Text = "";
                }
            }
        }
        private void btn_SaveConfig_Click(object sender, EventArgs e)
        {
            FourWireRTDType fourWireRTDType = FourWireRTDType.无;
            TransducerType transducerType = TransducerType.无;
            ThermocoupleType thermocoupleType = ThermocoupleType.无;
            ThermistorType thermistorType = ThermistorType.无;
            ChannelType channelType = (ChannelType)Enum.Parse(typeof(ChannelType), cmb_ChannelType.Text);

            bool cold = false;
            if (!string.IsNullOrEmpty(cmb_Transducer.Text))
            {
                transducerType = (TransducerType)Enum.Parse(typeof(TransducerType), cmb_Transducer.Text);
            }

            switch (transducerType)
            {
                case TransducerType.热电偶:
                    thermocoupleType =(ThermocoupleType)Enum.Parse(typeof(ThermocoupleType), cmb_Thermocouple.Text);
                    cold = chk_ColdJunc.Checked;
                    break;
                case TransducerType.热敏电阻:
                    thermistorType = (ThermistorType)Enum.Parse(typeof(ThermistorType), cmb_Thermocouple.Text);
                    break;
                case TransducerType.RTD:
                    fourWireRTDType = (FourWireRTDType)Enum.Parse(typeof(FourWireRTDType), cmb_Thermocouple.Text);
                    break;
                case TransducerType.无:
                    break;
            }
            foreach (KEChannel channel in selectedChannels)
            {
                if (channelType == ChannelType.无)
                {
                    channel.IsConfiged = false;
                }
                channel.IsConfiged = true;
                channel.ChannelType = channelType;
                channel.TransducerType = transducerType;
                channel.ThermocoupleType = thermocoupleType;
                channel.ThermistorType = thermistorType;
                channel.FourWireRTDType = fourWireRTDType;
                channel.ColdJunc = cold;
            }
        }
        private void btn_Download_Click(object sender, EventArgs e)
        {
            selectedChannels.Clear();
            foreach (KEDevice device in ke.Devices.Devices)
            {
                foreach(var channel in device.Channels)
                {
                    if(channel.IsConfiged)
                    {
                        selectedChannels.Add(channel);
                    }
                }
            }
            ke.SendConfig(selectedChannels);
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "配置文件|*.json",
                InitialDirectory = Application.StartupPath
            };
            string fileName = "";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fileName = dlg.FileName;
                if (fileName.Substring(fileName.Length - 5, 5) != ".json")
                {
                    fileName += ".json";
                }
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                ke.SaveConfig(fileName);
            }
        }
        private void btn_ReadConfig_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new();
            dlg.Filter = "配置文件|*.json";
            dlg.InitialDirectory = Application.StartupPath;
            string fileName = "";

            if(dlg.ShowDialog() == DialogResult.OK)
            {
                fileName = dlg.FileName;
            }
            else
            {
                return;
            }
            ke.LoadConfig(fileName);
            root.Nodes.Clear();
            dgv_Channel.Rows.Clear();
            foreach (KEDevice inst in ke.Devices.Devices)
            {
                TreeNode keiNode = new(inst.IPAddress);
                keiNode.Name = "Root_" + inst.IPAddress;
                foreach (KEChannel channel in inst.Channels)
                {
                    TreeNode channelNode = new(channel.Address);
                    channelNode.Name = keiNode.Name + "_" + channel.Address;
                    keiNode.Nodes.Add(channelNode);
                    dgv_Channel.Rows.Add(new object[] { inst.IPAddress, channel.Address, channel.ChannelType.ToString(), channel.TransducerType.ToString(), channel.ThermocoupleType.ToString(), channel.ThermistorType.ToString(), channel.FourWireRTDType.ToString(), channel.LastData.ToString("0.00") });
                }
                root.Nodes.Add(keiNode);
                root.Expand();
            }
        }
        readonly CancellationTokenSource source = new();
        CancellationToken token;
        private Task readDataTask;
        private void ShowChannelData()
        {
            readDataTask = new Task(async (object obj) =>
            {
                while (true)
                {
                    if(token.IsCancellationRequested)
                    {
                        return;
                    }
                    BeginInvoke(new MethodInvoker(delegate {
                        foreach (DataGridViewRow row in dgv_Channel.Rows)
                        {
                            string ip = row.Cells[0].Value.ToString();
                            string address = row.Cells[1].Value.ToString();
                            KEChannel channel = ke.GetChannel(ip, address);
                            row.Cells[2].Value = channel.ChannelType.ToString(); ;
                            row.Cells[3].Value = channel.TransducerType.ToString();
                            row.Cells[4].Value = channel.ThermocoupleType.ToString();
                            row.Cells[5].Value = channel.ThermistorType.ToString();
                            row.Cells[6].Value = channel.FourWireRTDType.ToString();
                            row.Cells[7].Value = channel.ColdJunc ? "是" : "否";
                            row.Cells[8].Value = channel.LastData.ToString("0.00000");
                        }
                    }));
                    await Task.Delay(1000);
                }
            }, token);
            readDataTask.Start();
        }

    }
}
