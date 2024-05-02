using InstrumentsCtrl;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DataObjects
{
    public partial class ChannelProperty : UserControl
    {
        public event EventHandler OnChannelsConfigChange;
        public event EventHandler OnClose;
        public Experiment Experiment { get; set; }
        public List<Channel> ConfigChangedChannels { get; private set; } = new();
        private List<Channel> myChannels = new();
        public ChannelProperty()
        {
            DoubleBuffered = true;
            Enabled = false;
            InitializeComponent();
            InitDataGrid();
            base.DoubleBuffered = true;
        }
        private void InitDataGrid()
        {
            dgv_ChannelProperty.CellClick += dgv_ChannelProperty_CellClick;
            dgv_ChannelProperty.Click += dgv_ChannelProperty_Click;
            lb_ChannelConfigCellEdit.DoubleClick += lb_ChannelConfigCellEdit_DoubleClick;

            DataGridViewCellStyle DataGridViewCellStyle2 = new();
            dgv_ChannelProperty.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle2.BackColor = Color.White;
            DataGridViewCellStyle2.Font = new Font("微软雅黑", 10.5F);
            DataGridViewCellStyle2.ForeColor = Color.Black;
            DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgv_ChannelProperty.Font = DataGridViewCellStyle2.Font;
            dgv_ChannelProperty.DefaultCellStyle = DataGridViewCellStyle2;
            dgv_ChannelProperty.Columns.Add("column1", "column1");
            dgv_ChannelProperty.Columns.Add("column2", "column2");
            dgv_ChannelProperty.Columns[0].Frozen = true;
            dgv_ChannelProperty.Columns[0].ReadOnly = true;
            base.DoubleBuffered = true;
            ShowDataInit();
        }
        private void ShowDataInit()
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            #region 序号
            tmp = new DataGridViewRow();

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "序号";
            tmp.Cells.Add(textCell);

            textCell = new DataGridViewTextBoxCell();
            tmp.Cells.Add(textCell);

            dgv_ChannelProperty.Rows.Add(tmp);
            #endregion
            #region 测点名称
            tmp = new DataGridViewRow();

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "测点名称";
            tmp.Cells.Add(textCell);

            textCell = new DataGridViewTextBoxCell();
            tmp.Cells.Add(textCell);

            dgv_ChannelProperty.Rows.Add(tmp);
            #endregion
            #region 测点功能
            tmp = new DataGridViewRow();

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "测点功能";
            tmp.Cells.Add(textCell);

            textCell = new DataGridViewTextBoxCell();
            tmp.Cells.Add(textCell);

            dgv_ChannelProperty.Rows.Add(tmp);
            #endregion
            #region 设备类型
            tmp = new DataGridViewRow();

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "设备类型";
            tmp.Cells.Add(textCell);

            textCell = new DataGridViewTextBoxCell();
            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;

            dgv_ChannelProperty.Rows.Add(tmp);
            #endregion
            #region 设备地址
            tmp = new DataGridViewRow();

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "设备地址";
            tmp.Cells.Add(textCell);

            textCell = new DataGridViewTextBoxCell();
            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;

            dgv_ChannelProperty.Rows.Add(tmp);
            #endregion
            #region 通道地址
            tmp = new DataGridViewRow();

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "通道地址";
            tmp.Cells.Add(textCell);

            textCell = new DataGridViewTextBoxCell();
            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;

            dgv_ChannelProperty.Rows.Add(tmp);
            #endregion
            #region 设备型号
            tmp = new DataGridViewRow();

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "设备型号";
            tmp.Cells.Add(textCell);

            textCell = new DataGridViewTextBoxCell();
            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;

            dgv_ChannelProperty.Rows.Add(tmp);
            #endregion
            #region 测量方式
            tmp = new DataGridViewRow();

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "测量方式";
            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;

            textCell = new DataGridViewTextBoxCell();
            tmp.Cells.Add(textCell);

            dgv_ChannelProperty.Rows.Add(tmp);
            #endregion
            #region 传感器类型
            tmp = new DataGridViewRow();

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "传感器类型";
            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;

            textCell = new DataGridViewTextBoxCell();
            tmp.Cells.Add(textCell);

            dgv_ChannelProperty.Rows.Add(tmp);
            #endregion
            #region 测量单位
            tmp = new DataGridViewRow();

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "测量单位";
            tmp.Cells.Add(textCell);

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "V";
            tmp.Cells.Add(textCell);

            dgv_ChannelProperty.Rows.Add(tmp);
            #endregion
            #region 小数位数
            tmp = new DataGridViewRow();

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "小数位数";
            tmp.Cells.Add(textCell);

            textCell = new DataGridViewTextBoxCell();
            tmp.Cells.Add(textCell);

            dgv_ChannelProperty.Rows.Add(tmp);
            #endregion
            #region 报警值
            tmp = new DataGridViewRow();

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "报警值";
            tmp.Cells.Add(textCell);

            textCell = new DataGridViewTextBoxCell();
            tmp.Cells.Add(textCell);

            dgv_ChannelProperty.Rows.Add(tmp);
            #endregion
            #region 所在温区
            tmp = new DataGridViewRow();

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = "所在温区";
            tmp.Cells.Add(textCell);

            textCell = new DataGridViewTextBoxCell();
            tmp.Cells.Add(textCell);

            dgv_ChannelProperty.Rows.Add(tmp);
            #endregion
        }
        public void ShowChannelProperty(List<Channel> channels)
        {
            Enabled = true;
            lb_ChannelConfigCellEdit.Visible = false;
            myChannels = channels;
            dgv_ChannelProperty.Rows.Clear();
            switch (channels.Count)
            {
                case 1:
                    Channel channel = channels[0];
                    switch (channel.DeviceType)
                    {
                        case DeviceType.IMP:
                            AddRow序号(myChannels);
                            AddRow测点(myChannels);
                            AddRow测点名称(myChannels);
                            AddRow设备类型(myChannels);
                            AddRow设备地址(myChannels);
                            AddRow通道地址(myChannels);
                            AddRow所在温区(myChannels);
                            AddRow测点功能(myChannels);
                            AddRow小数位数(myChannels);
                            AddRow测量方式(myChannels);
                            AddRow单位(myChannels);
                            AddRow报警值Max(myChannels);
                            AddRow报警值Min(myChannels);
                            break;
                        case DeviceType.Agilent:
                            AddRow序号(myChannels);
                            AddRow测点(myChannels);
                            AddRow测点名称(myChannels);
                            AddRow设备类型(myChannels);
                            AddRow设备地址(myChannels);
                            AddRow所在温区(myChannels);
                            AddRow测点功能(myChannels);
                            AddRow小数位数(myChannels);
                            AddRow单位(myChannels);
                            AddRow最大电流(myChannels);
                            AddRow最大电压(myChannels);
                            AddRow热源阻值(myChannels);
                            AddRow导线阻值(myChannels);
                            AddRow报警值Max(myChannels);
                            AddRow报警值Min(myChannels);
                            break;
                        case DeviceType.ITECH:
                            AddRow序号(myChannels);
                            AddRow测点(myChannels);
                            AddRow测点名称(myChannels);
                            AddRow设备类型(myChannels);
                            AddRow设备地址(myChannels);
                            AddRow所在温区(myChannels);
                            AddRow测点功能(myChannels);
                            AddRow小数位数(myChannels);
                            AddRow单位(myChannels);
                            AddRow最大电流(myChannels);
                            AddRow最大电压(myChannels);
                            AddRow热源阻值(myChannels);
                            AddRow导线阻值(myChannels);
                            AddRow报警值Max(myChannels);
                            AddRow报警值Min(myChannels);
                            break;
                        case DeviceType.Keithley:
                            AddRow序号(myChannels);
                            AddRow测点(myChannels);
                            AddRow测点名称(myChannels);
                            AddRow设备类型(myChannels);
                            AddRow设备地址(myChannels);
                            AddRow通道地址(myChannels);
                            AddRow所在温区(myChannels);
                            AddRow测点功能(myChannels);
                            AddRow小数位数(myChannels);
                            AddRow测量方式(myChannels);
                            AddRow冷端补偿(myChannels);
                            AddRow单位(myChannels);
                            AddRow报警值Max(myChannels);
                            AddRow报警值Min(myChannels);
                            break;
                    }
                    break;
                default:
                    var order = from Channel ch in channels group ch by ch.DeviceType into deviceType select deviceType.Key;
                    if (order.Count() == 1)
                    {
                        DeviceType deviceType = (DeviceType)Enum.Parse(typeof(DeviceType), order.First().ToString());
                        switch (deviceType)
                        {
                            case DeviceType.IMP:
                                AddRow序号(myChannels);
                                AddRow测点(myChannels);
                                AddRow测点名称(myChannels);
                                AddRow设备类型(myChannels);
                                AddRow设备地址(myChannels);
                                AddRow通道地址(myChannels);
                                AddRow所在温区(myChannels);
                                AddRow测点功能(myChannels);
                                AddRow小数位数(myChannels);
                                AddRow测量方式(myChannels);
                                AddRow单位(myChannels);
                                AddRow报警值Max(myChannels);
                                AddRow报警值Min(myChannels);
                                break;
                            case DeviceType.Agilent:
                                AddRow序号(myChannels);
                                AddRow测点(myChannels);
                                AddRow测点名称(myChannels);
                                AddRow设备类型(myChannels);
                                AddRow设备地址(myChannels);
                                AddRow所在温区(myChannels);
                                AddRow测点功能(myChannels);
                                AddRow小数位数(myChannels);
                                AddRow单位(myChannels);
                                AddRow最大电流(myChannels);
                                AddRow最大电压(myChannels);
                                AddRow热源阻值(myChannels);
                                AddRow导线阻值(myChannels);
                                AddRow报警值Max(myChannels);
                                AddRow报警值Min(myChannels);
                                break;
                            case DeviceType.ITECH:
                                AddRow序号(myChannels);
                                AddRow测点(myChannels);
                                AddRow测点名称(myChannels);
                                AddRow设备类型(myChannels);
                                AddRow设备地址(myChannels);
                                AddRow所在温区(myChannels);
                                AddRow测点功能(myChannels);
                                AddRow小数位数(myChannels);
                                AddRow单位(myChannels);
                                AddRow最大电流(myChannels);
                                AddRow最大电压(myChannels);
                                AddRow热源阻值(myChannels);
                                AddRow导线阻值(myChannels);
                                AddRow报警值Max(myChannels);
                                AddRow报警值Min(myChannels);
                                break;
                            case DeviceType.Keithley:
                                AddRow序号(myChannels);
                                AddRow测点(myChannels);
                                AddRow测点名称(myChannels);
                                AddRow设备类型(myChannels);
                                AddRow设备地址(myChannels);
                                AddRow通道地址(myChannels);
                                AddRow所在温区(myChannels);
                                AddRow测点功能(myChannels);
                                AddRow小数位数(myChannels);
                                AddRow测量方式(myChannels);
                                AddRow冷端补偿(myChannels);
                                AddRow单位(myChannels);
                                AddRow报警值Max(myChannels);
                                AddRow报警值Min(myChannels);
                                break;
                        }
                    }
                    else
                    {
                        AddRow序号(myChannels);
                        AddRow测点(myChannels);
                        AddRow测点名称(myChannels);
                        AddRow设备类型(myChannels);
                        AddRow所在温区(myChannels);
                        AddRow小数位数(myChannels);
                        AddRow单位(myChannels);
                        AddRow报警值Max(myChannels);
                        AddRow报警值Min(myChannels);
                    }
                    break;
            }
        }
        private void lb_ChannelConfigCellEdit_DoubleClick(object sender, EventArgs e)
        {
            if (lb_ChannelConfigCellEdit.SelectedItems.Count == 0)
            { return; }
            if (lb_ChannelConfigCellEdit.Items.Count == 0)
            {
                lb_ChannelConfigCellEdit.Visible = false;
                return;
            }
            dgv_ChannelProperty.SelectedCells[0].Value = lb_ChannelConfigCellEdit.SelectedItem;
            lb_ChannelConfigCellEdit.Visible = false;
        }
        private void dgv_ChannelProperty_Click(object sender, EventArgs e)
        {
            ;
        }
        private void dgv_ChannelProperty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (myChannels == null)
            { return; }
            if (lb_ChannelConfigCellEdit.Visible == true)
            {
                lb_ChannelConfigCellEdit.Visible = false;
                return;
            }
            Rectangle rect;
            lb_ChannelConfigCellEdit.Items.Clear();
            if (e.ColumnIndex == 1)
            {
                switch (dgv_ChannelProperty.Rows[e.RowIndex].Cells[0].Value.ToString())
                {
                    case "测量方式":
                        var deviceType = from Channel channel in myChannels group channel by channel.DeviceType into dt1 select dt1;
                        DeviceType dt = deviceType.First().Key;
                        if (dt == DeviceType.Keithley)
                        {
                            foreach (string str in Keithley.KECtrl.SensorDescription())
                            {
                                lb_ChannelConfigCellEdit.Items.Add(str);
                            }
                        }
                        rect = dgv_ChannelProperty.GetCellDisplayRectangle(1, e.RowIndex, false);
                        lb_ChannelConfigCellEdit.Top = rect.Y + dgv_ChannelProperty.Top + rect.Height;
                        lb_ChannelConfigCellEdit.Width = dgv_ChannelProperty.GetRowDisplayRectangle(e.RowIndex, false).Width;
                        lb_ChannelConfigCellEdit.Height = 280;
                        lb_ChannelConfigCellEdit.Left = 1;
                        lb_ChannelConfigCellEdit.BringToFront();
                        lb_ChannelConfigCellEdit.Visible = true;
                        break;
                    case "测点功能":
                        foreach (string name in Enum.GetNames(typeof(ChannelFunction)))
                        {
                            lb_ChannelConfigCellEdit.Items.Add(name);
                        }
                        rect = dgv_ChannelProperty.GetCellDisplayRectangle(1, e.RowIndex, false);
                        lb_ChannelConfigCellEdit.Top = rect.Y + dgv_ChannelProperty.Top + rect.Height;
                        lb_ChannelConfigCellEdit.Width = dgv_ChannelProperty.GetRowDisplayRectangle(e.RowIndex, false).Width;
                        lb_ChannelConfigCellEdit.Height = 280;
                        lb_ChannelConfigCellEdit.Left = 1;
                        lb_ChannelConfigCellEdit.BringToFront();
                        lb_ChannelConfigCellEdit.Visible = true;
                        break;
                    case "所在温区":
                        foreach (TempRange range in Experiment.Ranges)
                        {
                            lb_ChannelConfigCellEdit.Items.Add(range.Name);
                        }
                        rect = dgv_ChannelProperty.GetCellDisplayRectangle(1, e.RowIndex, false);
                        lb_ChannelConfigCellEdit.Top = rect.Y + dgv_ChannelProperty.Top + rect.Height;
                        lb_ChannelConfigCellEdit.Width = dgv_ChannelProperty.GetRowDisplayRectangle(e.RowIndex, false).Width;
                        lb_ChannelConfigCellEdit.Height = 280;
                        lb_ChannelConfigCellEdit.Left = 1;
                        lb_ChannelConfigCellEdit.BringToFront();
                        lb_ChannelConfigCellEdit.Visible = true;
                        break;
                    case "冷端补偿":
                        lb_ChannelConfigCellEdit.Items.Add("False");
                        lb_ChannelConfigCellEdit.Items.Add("True");
                        rect = dgv_ChannelProperty.GetCellDisplayRectangle(1, e.RowIndex, false);
                        lb_ChannelConfigCellEdit.Top = rect.Y + dgv_ChannelProperty.Top + rect.Height;
                        lb_ChannelConfigCellEdit.Width = dgv_ChannelProperty.GetRowDisplayRectangle(e.RowIndex, false).Width;
                        lb_ChannelConfigCellEdit.Height = 280;
                        lb_ChannelConfigCellEdit.Left = 1;
                        lb_ChannelConfigCellEdit.BringToFront();
                        lb_ChannelConfigCellEdit.Visible = true;
                        break;
                    case "小数位数":
                        for(int i = 1; i <= 6; i++)
                        {
                            lb_ChannelConfigCellEdit.Items.Add(i.ToString());
                        }
                        rect = dgv_ChannelProperty.GetCellDisplayRectangle(1, e.RowIndex, false);
                        lb_ChannelConfigCellEdit.Top = rect.Y + dgv_ChannelProperty.Top + rect.Height;
                        lb_ChannelConfigCellEdit.Width = dgv_ChannelProperty.GetRowDisplayRectangle(e.RowIndex, false).Width;
                        lb_ChannelConfigCellEdit.Height = 280;
                        lb_ChannelConfigCellEdit.Left = 1;
                        lb_ChannelConfigCellEdit.BringToFront();
                        lb_ChannelConfigCellEdit.Visible = true;
                        break;
                }
            }
        }
        private void Btn_ChannelConfigOK_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgv_ChannelProperty.Rows)
            {
                string value;
                if (row.Cells[1].Value == null)
                {
                    value = "";
                }
                else
                {
                    value = row.Cells[1].Value.ToString();
                }
                switch (row.Cells[0].Value.ToString())
                {
                    case "测点名称":
                        string t;
                        if (row.Cells[1].Value == null)
                        {
                            MessageBox.Show("必须输入测点名称", "修改测点配置", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            row.Selected = true;
                            return;
                        }
                        else
                        { 
                            t = row.Cells[1].Value.ToString(); 
                        }
                        if (string.IsNullOrWhiteSpace(t))
                        {
                            MessageBox.Show("必须输入测点名称", "修改测点配置", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            row.Selected = true;
                            return;
                        }
                        if (t.Contains(","))
                        {
                            break;
                        }
                        string invalidString;
                        if (PipeControl.Common.Utilities.ValidName(t, out invalidString) == false)
                        {
                            MessageBox.Show(invalidString, "修改测点配置");
                            row.Selected = true;
                            return;
                        }
                        if (myChannels.Count == 1)
                        {
                            foreach (Channel ch in Experiment.Channels)
                            {
                                if (ch.UserName == t)
                                {
                                    if (ch != myChannels[0])
                                    {
                                        MessageBox.Show("与已有测点名称重复，请重新输入");
                                        row.Selected = true;
                                        return;
                                    }
                                }
                            }
                            if (myChannels[0].UserName != t)
                            {
                                myChannels[0].UserName = t;
                            }
                        }
                        else
                        {
                            string[] str = t.Split(';');
                            try
                            {
                                int nameNum = int.Parse(str[1]);
                                string addZero = "000";
                                int add = 0;
                                foreach (Channel channel in myChannels)
                                {
                                    string name = addZero + (nameNum + add).ToString();
                                    name = str[0] + name.Substring(name.Length - 3, 3);
                                    foreach (Channel ch in Experiment.Channels)
                                    {
                                        if (ch.UserName == name)
                                        {
                                            MessageBox.Show("与已有测点名称重复，请重新输入");
                                            row.Selected = true;
                                            return;
                                        }
                                    }
                                    if (channel.UserName != name)
                                    {
                                        channel.UserName = name;
                                    }
                                    add += 1;
                                }
                            }
                            catch
                            {
                                break;
                            }
                        }
                        break;
                    case "测点功能":
                        ChannelFunction function = (ChannelFunction)Enum.Parse(typeof(ChannelFunction), value);
                        foreach (Channel channel in myChannels)
                        {
                            if (function == ChannelFunction.控温点)
                            {
                                if (channel.Parent != null)
                                {
                                    if (!channel.Equals(channel.Parent.GetTempControl()) && channel.Parent.GetTempControl() != null)
                                    {
                                        channel.Parent.GetTempControl().Function = ChannelFunction.无;
                                    }
                                }
                            }
                            channel.Function = function;
                        }
                        break;
                    case "测量方式":
                        if (value.Contains(","))
                            continue;
                        var deviceType = from Channel channel in myChannels group channel by channel.DeviceType into dt1 select dt1;
                        DeviceType dt = deviceType.First().Key;
                        if (dt == DeviceType.Keithley)
                        {
                            string[] strs = value.Split('_');
                            Keithley.ChannelType channelType = Keithley.ChannelType.无;
                            Keithley.TransducerType transducerType = Keithley.TransducerType.无;
                            Keithley.ThermocoupleType thermocoupleType = Keithley.ThermocoupleType.无;
                            Keithley.ThermistorType thermistorType = Keithley.ThermistorType.无;
                            Keithley.FourWireRTDType fourWireRTDType = Keithley.FourWireRTDType.无;
                            switch (strs.Length)
                            {
                                case 1:
                                    if (strs[0] == "未配置")
                                    {

                                    }
                                    if (strs[0] == "交流电压")
                                    {
                                        channelType = Keithley.ChannelType.交流电压;
                                    }
                                    if (strs[0] == "直流电压")
                                    {
                                        channelType = Keithley.ChannelType.直流电压;
                                    }
                                    break;
                                case 3:
                                    channelType = Keithley.ChannelType.温度;
                                    if (strs[1] == "热电偶")
                                    {
                                        transducerType = Keithley.TransducerType.热电偶;
                                        if (strs[2] == "B型")
                                        {
                                            thermocoupleType = Keithley.ThermocoupleType.B;
                                        }
                                        if (strs[2] == "J型")
                                        {
                                            thermocoupleType = Keithley.ThermocoupleType.J;
                                        }
                                        if (strs[2] == "K型")
                                        {
                                            thermocoupleType = Keithley.ThermocoupleType.K;
                                        }
                                        if (strs[2] == "N型")
                                        {
                                            thermocoupleType = Keithley.ThermocoupleType.N;
                                        }
                                        if (strs[2] == "R型")
                                        {
                                            thermocoupleType = Keithley.ThermocoupleType.R;
                                        }
                                        if (strs[2] == "E型")
                                        {
                                            thermocoupleType = Keithley.ThermocoupleType.E;
                                        }
                                        if (strs[2] == "S型")
                                        {
                                            thermocoupleType = Keithley.ThermocoupleType.S;
                                        }
                                        if (strs[2] == "T型")
                                        {
                                            thermocoupleType = Keithley.ThermocoupleType.T;
                                        }
                                    }
                                    if (strs[1] == "热敏电阻")
                                    {
                                        transducerType = Keithley.TransducerType.热敏电阻;
                                        if (strs[2] == "2200Ω")
                                        {
                                            thermistorType = Keithley.ThermistorType.两千两百欧姆;
                                        }
                                        if (strs[2] == "5000Ω")
                                        {
                                            thermistorType = Keithley.ThermistorType.五千欧姆;
                                        }
                                        if (strs[2] == "10000Ω")
                                        {
                                            thermistorType = Keithley.ThermistorType.一万欧姆;
                                        }
                                    }
                                    if (strs[1] == "RTD")
                                    {
                                        transducerType = Keithley.TransducerType.RTD;
                                        if (strs[2] == "PT100")
                                        {
                                            fourWireRTDType = Keithley.FourWireRTDType.PT100;
                                        }
                                        if (strs[2] == "D100")
                                        {
                                            fourWireRTDType = Keithley.FourWireRTDType.D100;
                                        }
                                        if (strs[2] == "F100")
                                        {
                                            fourWireRTDType = Keithley.FourWireRTDType.F100;
                                        }
                                        if (strs[2] == "PT385")
                                        {
                                            fourWireRTDType = Keithley.FourWireRTDType.PT385;
                                        }
                                        if (strs[2] == "PT3916")
                                        {
                                            fourWireRTDType = Keithley.FourWireRTDType.PT3916;
                                        }
                                    }

                                    break;
                            }
                            foreach (Channel channel in myChannels)
                            {
                                if (channel.ChannelType != channelType ||
                                   channel.TransducerType != transducerType ||
                                   channel.ThermocoupleType != thermocoupleType ||
                                   channel.ThermistorType != thermistorType ||
                                   channel.FourWireRTDType != fourWireRTDType)
                                {
                                    channel.ChannelType = channelType;
                                    channel.TransducerType = transducerType;
                                    channel.ThermocoupleType = thermocoupleType;
                                    channel.ThermistorType = thermistorType;
                                    channel.FourWireRTDType = fourWireRTDType;
                                    ConfigChangedChannels.Add(channel);
                                }
                            }
                        }
                        break;
                    case "冷端补偿":
                        foreach (Channel channel in myChannels)
                        {
                            if (bool.TryParse(value, out bool cold))
                            {
                                if (channel.ColdJunc != cold)
                                {
                                    channel.ColdJunc = bool.Parse(value);
                                    ConfigChangedChannels.Add(channel);
                                }
                            }
                        }
                        break;
                    case "报警上限":
                        foreach (Channel channel in myChannels)
                        {
                            if (double.TryParse(value, out double alert))
                            {
                                channel.AlertMax = alert;
                            }
                            else
                            {
                                channel.AlertMax = null;
                            }
                        }
                        break;
                    case "报警下限":
                        foreach (Channel channel in myChannels)
                        {
                            if (double.TryParse(value, out double alert))
                            {
                                channel.AlertMin = alert;
                            }
                            else
                            {
                                channel.AlertMin = null;
                            }
                        }
                        break;
                    case "单位":
                        if(value.Contains(","))
                        {
                            continue;
                        }
                        foreach (Channel channel in myChannels)
                        {
                            channel.YUnit = value;
                        }
                        break;
                    case "小数位数":
                        foreach (Channel channel in myChannels)
                        {
                            if (int.TryParse(value, out int showDecimal))
                            {
                                channel.ShowDecimal = showDecimal;
                            }
                        }
                        break;
                    case "最大电流":
                        foreach (Channel channel in myChannels)
                        {
                            if (double.TryParse(value, out double maxI))
                            {
                                channel.Imax = maxI;
                            }
                        }
                        break;
                    case "最大电压":
                        foreach (Channel channel in myChannels)
                        {
                            if (double.TryParse(value, out double maxV))
                            {
                                channel.Vmax = maxV;
                            }
                        }
                        break;
                    case "热源阻值":
                        foreach (Channel channel in myChannels)
                        {
                            if (double.TryParse(value, out double rh))
                            {
                                channel.RH = rh;
                            }
                        }
                        break;
                    case "导线阻值":
                        foreach (Channel channel in myChannels)
                        {
                            if (double.TryParse(value, out double rl))
                            {
                                channel.RL = rl;
                            }
                        }
                        break;
                }
            }
            foreach (DataGridViewRow row in dgv_ChannelProperty.Rows)
            {
                string value;
                if (row.Cells[1].Value == null)
                {
                    value = "";
                }
                else
                {
                    value = row.Cells[1].Value.ToString();
                }
                switch (row.Cells[0].Value.ToString())
                {
                    case "所在温区":
                        if (string.IsNullOrEmpty(value))
                            break;
                        if (value.Contains(","))
                            break;
                        var range = Experiment.Ranges.Single(x => x.Name == value);
                        foreach (Channel channel in myChannels)
                        {
                            bool eq = channel.Equals(range.GetTempControl());
                            if (channel.Function == ChannelFunction.控温点 && !channel.Equals(range.GetTempControl()) && range.GetTempControl() != null)
                            {
                                range.GetTempControl().Function = ChannelFunction.无;
                            }
                            channel.Parent = range;
                            channel.ParentID = range.ID;
                            range.AddChannel(channel);
                        }
                        break;
                }
            }
            OnChannelsConfigChange?.Invoke(this, new EventArgs());
        }
        private void btn_Close_Click(object sender, EventArgs e)
        {
            OnClose?.Invoke(this, new EventArgs());
        }
        #region 处理测点属性，增加每个属性为一行
        private void AddRow序号(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "序号";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            foreach (Channel channel in channels)
            {
                str += channel.ID.ToString() + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);

            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow测点(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "测点";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            foreach (Channel channel in channels)
            {
                str += channel.Name.ToString() + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);
            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow测点名称(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "测点名称";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            foreach (Channel channel in channels)
            {
                str += channel.UserName.ToString() + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);
            tmp.Cells.Add(textCell);
            //if (channels.Count > 1)
            //{
            //    textCell.ReadOnly = true;
            //}
            //else
            //{
            //    textCell.ReadOnly = false;
            //}
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow测点功能(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "测点功能";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            var order = from Channel channel in channels group channel by channel.Function into deviceAddress select deviceAddress;
            foreach (var deviceAddress in order)
            {
                str += deviceAddress.Key + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);
            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow设备类型(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "设备类型";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            var order = from Channel channel in channels group channel by channel.DeviceType into deviceAddress select deviceAddress;
            foreach (var deviceAddress in order)
            {
                str += deviceAddress.Key + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);
            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow设备地址(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "设备地址";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            var order = from Channel channel in channels group channel by channel.HardwareDeviceAddress into deviceAddress select deviceAddress;
            foreach (var deviceAddress in order)
            {
                str += deviceAddress.Key + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);
            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow通道地址(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "通道地址";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            foreach (Channel channel in channels)
            {
                str += channel.HardwareChannelAddress.ToString() + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);
            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;

            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow测量方式(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "测量方式";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            var order = from Channel channel in channels group channel by channel.GetRangeDescription() into deviceAddress select deviceAddress;
            foreach (var deviceAddress in order)
            {
                str += deviceAddress.Key + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);

            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;
            dgv_ChannelProperty.Rows.Add(tmp);

        }
        private void AddRow冷端补偿(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "冷端补偿";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            var order = from Channel channel in channels group channel by channel.ColdJunc into deviceAddress select deviceAddress;
            foreach (var deviceAddress in order)
            {
                str += deviceAddress.Key + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);

            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow报警值Max(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "报警上限";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            var order = from Channel channel in channels group channel by channel.AlertMax into deviceAddress select deviceAddress;
            foreach (var deviceAddress in order)
            {
                str += deviceAddress.Key + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);

            tmp.Cells.Add(textCell);
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow报警值Min(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "报警下限";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            var order = from Channel channel in channels group channel by channel.AlertMin into deviceAddress select deviceAddress;
            foreach (var deviceAddress in order)
            {
                str += deviceAddress.Key + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);

            tmp.Cells.Add(textCell);
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow单位(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "单位";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";
            var order = from Channel channel in channels group channel by channel.YUnit into deviceAddress select deviceAddress;
            foreach (var deviceAddress in order)
            {
                str += deviceAddress.Key + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);

            tmp.Cells.Add(textCell);
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow小数位数(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;

            tmp = new();
            textCell = new();
            textCell.Value = "小数位数";
            tmp.Cells.Add(textCell);

            textCell = new DataGridViewTextBoxCell();
            string str = "";
            var order = from Channel channel in channels group channel by channel.ShowDecimal into deviceAddress select deviceAddress;
            foreach (var deviceAddress in order)
            {
                str += deviceAddress.Key + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);
            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow最大电流(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "最大电流";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            var order = from Channel channel in channels group channel by channel.Imax into deviceAddress select deviceAddress;
            foreach (var deviceAddress in order)
            {
                str += deviceAddress.Key + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);

            tmp.Cells.Add(textCell);
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow最大电压(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "最大电压";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            var order = from Channel channel in channels group channel by channel.Vmax into deviceAddress select deviceAddress;
            foreach (var deviceAddress in order)
            {
                str += deviceAddress.Key + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);

            tmp.Cells.Add(textCell);
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow热源阻值(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "热源阻值";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            var order = from Channel channel in channels group channel by channel.RH into deviceAddress select deviceAddress;
            foreach (var deviceAddress in order)
            {
                str += deviceAddress.Key + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);

            tmp.Cells.Add(textCell);
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow导线阻值(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "导线阻值";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            var order = from Channel channel in channels group channel by channel.RL into deviceAddress select deviceAddress;
            foreach (var deviceAddress in order)
            {
                str += deviceAddress.Key + ", ";
            }
            textCell.Value = str.Substring(0, str.Length - 2);

            tmp.Cells.Add(textCell);
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        private void AddRow所在温区(List<Channel> channels)
        {
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;
            tmp = new();
            textCell = new();
            textCell.Value = "所在温区";
            tmp.Cells.Add(textCell);

            textCell = new();
            string str = "";

            var order = from Channel channel in channels group channel by channel.Parent into deviceAddress select deviceAddress;
            if (order.First().Key == null)
            {
                textCell.Value = "";

                tmp.Cells.Add(textCell);
                textCell.ReadOnly = true;
                dgv_ChannelProperty.Rows.Add(tmp);
                return;
            }
            foreach (var deviceAddress in order)
            {
                if(deviceAddress.Key == null)
                {
                    str += ", ";
                }
                else
                {
                    str +=  deviceAddress.Key.Name + ", " ;
                }
                
            }
            textCell.Value = str.Substring(0, str.Length - 2);

            tmp.Cells.Add(textCell);
            textCell.ReadOnly = true;
            dgv_ChannelProperty.Rows.Add(tmp);
        }
        #endregion
        /// <summary>
        /// 通过指定属性名称，删除属性显示的行
        /// </summary>
        /// <param name="cell"></param>
        private void RemoveRow(string cell)
        {
            foreach (DataGridViewRow row in dgv_ChannelProperty.Rows)
            {
                if (row.Cells[0].Value.ToString() == cell)
                {
                    dgv_ChannelProperty.Rows.Remove(row);
                    break;
                }
            }
        }
        /// <summary>
        /// 指定属性名称和属性初始值，将此属性插入到指定位置
        /// </summary>
        /// <param name="property">属性名称</param>
        /// <param name="value">属性初始值</param>
        /// <param name="rowIndex">指定位置</param>
        private void InsertRow(string property, string value, int rowIndex, bool isReadOnly = false)
        {
            foreach (DataGridViewRow row in dgv_ChannelProperty.Rows)
            {
                if (row.Cells[0].Value.ToString() == property)
                {
                    return;
                }
            }
            DataGridViewRow tmp;
            DataGridViewTextBoxCell textCell;

            tmp = new DataGridViewRow();
            textCell = new DataGridViewTextBoxCell();
            textCell.Value = property;
            tmp.Cells.Add(textCell);

            textCell = new DataGridViewTextBoxCell();
            textCell.Value = value;
            tmp.Cells.Add(textCell);
            textCell.ReadOnly = isReadOnly;

            dgv_ChannelProperty.Rows.Insert(rowIndex, tmp);
        }
        /// <summary>
        /// 通过指定属性名称获得属性值
        /// </summary>
        /// <param name="cell1">指定的属性名称</param>
        /// <returns></returns>
        private string GetCellValue(string cell1)
        {
            foreach (DataGridViewRow row in dgv_ChannelProperty.Rows)
            {
                if (row.Cells[0].Value.ToString() == cell1)
                {
                    return (row.Cells[1].Value.ToString());
                }
            }
            return null;
        }
        /// <summary>
        /// 设置指定属性的值
        /// </summary>
        /// <param name="cell1">指定的属性名称</param>
        /// <param name="cell2">属性要设置的值</param>
        private void SetCellValue(string cell1, string cell2)
        {
            foreach (DataGridViewRow row in dgv_ChannelProperty.Rows)
            {
                if (row.Cells[0].Value.ToString() == cell1)
                {
                    row.Cells[1].Value = cell2;
                }
            }
        }
    }
}
