using DataObjects;
using InstrumentsCtrl;
using Keithley;
using Newtonsoft.Json;
using PipeControl.Common;
using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PipeControl
{
    public partial class frm_Main : Form
    {
        private Experiment myExperiment;
        private TreeNode root;
        private ImageList myImgIcon;
        readonly ChannelDataGridView channelDataGridView;
        readonly IdWorker idWorker = new(1, 1);
        private readonly List<string> existsExperimentNames = new();
        private string windowsTitle;
        private void EnableDoubleBuffering()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
        public frm_Main()
        {
            EnableDoubleBuffering();
            InitializeComponent();

            Load += Frm_Main_Load;
            FormClosing += Frm_Main_FormClosing;
            HandleDestroyed += Frm_Main_HandleDestroyed;
            lbl_CollaspDevice.Click += Lbl_CollaspDevice_Click;

            InitDeviceTree();

            channelDataGridView = new(CreateChannelDeviceColumnCollection(), Color.FromArgb(48, 71, 81), SystemColors.Highlight);
            spc_Channel.Panel1.Controls.Add(channelDataGridView);
            channelDataGridView.Dock = DockStyle.Fill;

        }
        private void AddHandles()
        {
            deviceTree.DoubleClick += DeviceTree_DoubleClick;
            deviceTree.AfterSelect += DeviceTree_AfterSelect;
            deviceTree.NodeMouseClick += DeviceTree_NodeMouseClick;

            channelDataGridView.MouseClick += ChannelDataGridView_MouseClick;
            channelDataGridView.SelectionChanged += ChannelDataGridView_SelectionChanged;


            btn_New.Click += Btn_New_Click;
            lbl_CloseExperiment.Click += Lbl_CloseExperiment_Click;
            btn_CreateExperiment.Click += Btn_CreateExperiment_Click;

            btn_OpenExperiment.Click += Btn_OpenExperiment_Click;
            lbl_CloseOpenExperiment.Click += Lbl_CloseOpenExperiment_Click;
            lb_Experiments.SelectedIndexChanged += Lb_Experiments_SelectedIndexChanged;
            btn_OpenSelectedExperiment.Click += Btn_OpenSelectedExperiment_Click;
            btn_DelExperment.Click += Btn_DelExperment_Click;
            btn_OpenEXCEL.Click += Btn_OpenEXCEL_Click;

            btn_SaveExperiment.Click += Btn_SaveExperiment_Click;
            btn_CloseExperiment.Click += Btn_CloseExperiment_Click;
            btn_DCEnable.Click += Btn_DCEnable_Click;
            btn_DCDisable.Click += Btn_DCDisable_Click;
            btn_HistoryData.Click += Btn_HistoryData_Click;

            btn_ChannelManage.Click += Btn_ChannelManage_Click;
            btn_DCManage.Click += Btn_DCManage_Click;
            btn_SensorManage.Click += Btn_SensorManage_Click;
            btn_Procedue.Click += Btn_Procedue_Click;
            btn_ReloadExcel.Click += Btn_ReloadExcel_Click;


            全部ToolStripMenuItem.Click += 全部ToolStripMenuItem_Click;
            配置信息ToolStripMenuItem.Click += 配置信息ToolStripMenuItem_Click;
            测量信息ToolStripMenuItem.Click += 测量信息ToolStripMenuItem_Click;
            最简信息ToolStripMenuItem.Click += 最简信息ToolStripMenuItem_Click;
            自定义ToolStripMenuItem.Click += 自定义ToolStripMenuItem_Click;
            最大值复位ToolStripMenuItem.Click += 最大值复位ToolStripMenuItem_Click;
            最小值复位ToolStripMenuItem.Click += 最小值复位ToolStripMenuItem_Click;
            平均值复位ToolStripMenuItem.Click += 平均值复位ToolStripMenuItem_Click;
            显示实时曲线ToolStripMenuItem.Click += 显示实时曲线ToolStripMenuItem_Click;
        }
        private void Frm_Main_Load(object sender, EventArgs e)
        {
            RegCheck();
#if !DEBUG
                AddExclusionPathWin10();
#endif
            spc_Right.SplitterWidth = 1;

            if (File.Exists(Application.StartupPath + "\\demo.txt"))
            {
                windowsTitle = "温控系统(演示版)";
            }
            else
            {
                windowsTitle = "温控系统";
            }

            Text = windowsTitle;
            channelDataGridView.OrignalColumnWidth();
            channelDataGridView.ReadColumn();
            channelDataGridView.ContextMenuStrip = cms_ChannelList;
            AddHandles();
            foreach (DataGridViewColumn column in channelDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
        }
        private async void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myExperiment == null)
                return;
            if (!string.IsNullOrEmpty(myExperiment.Name))
            {
                if (myExperiment.IsSavingData)
                {
                    MessageBox.Show("正在保存试验数据，无法退出程序。请首先停止保存数据", "退出程序", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    e.Cancel = true;
                    return;
                }
            }
            if (MessageBox.Show("确定要退出程序吗？", "退出程序", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            if (myExperiment != null)
            {
                myExperiment.OnDataRefresh -= MyExperiment_OnDataRefresh;
                await myExperiment.Close();
                await Task.Delay(1000);
            }
        }
        private void Frm_Main_HandleDestroyed(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private List<ColumnStruct> CreateChannelDeviceColumnCollection()
        {
            List<ColumnStruct> columnCollection = new();

            ColumnStruct tmp;

            tmp.columnName = "序号";//---------------------------------------------------------------0
            tmp.columnType = Type.GetType("System.Int32");
            columnCollection.Add(tmp);

            tmp.columnName = "测点名称";//--------------------------------------------------------------1
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "测点功能";//--------------------------------------------------------------2
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "设备类型";//---------------------------------------------------------------3
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "设备地址";//--------------------------------------------------------------4
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "通道地址";//-------------------------------------------------------------5
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "设备型号";//-------------------------------------------------------------6
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "测量方式";//------------------------------------------------------------7
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "传感器类型";//-------------------------------------------------------------8
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "冷端补偿";//--------------------------------------------------------------9
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "报警上限";//--------------------------------------------------------------10
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "报警下限";//--------------------------------------------------------------11
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "单位";//--------------------------------------------------------------12
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "小数显示位数";//--------------------------------------------------------------13
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "最大电流";//--------------------------------------------------------------14
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "最大电压";//--------------------------------------------------------------15
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "热源电阻";//--------------------------------------------------------------16
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "导线电阻";//--------------------------------------------------------------17
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "控温方式";//--------------------------------------------------------------18
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "温度下限";//--------------------------------------------------------------19
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "温度上限";//--------------------------------------------------------------20
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "输出电压";//--------------------------------------------------------------21
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "输出电流";//---------------------------------------------------------------22
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "目标温度";//---------------------------------------------------------------23
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "采样频率";//---------------------------------------------------------------24
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "热源使能";//---------------------------------------------------------------25
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "测点状态";//---------------------------------------------------------------26
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "当前值";//----------------------------------------------------------------27
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "当前电压值";//-------------------------------------------------------------28
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "当前电流值";//-------------------------------------------------------------29
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "最大值";//----------------------------------------------------------------30
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "最小值";//----------------------------------------------------------------31
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            tmp.columnName = "平均值";//----------------------------------------------------------------32
            tmp.columnType = Type.GetType("System.Single");
            columnCollection.Add(tmp);

            return columnCollection;
        }
        private void RegCheck()
        {
            Register reg = new Register();
            if (!reg.IsReg())
            {
                frm_About f = new frm_About();
                f.ShowDialog();
            }
        }
        private void Lbl_CollaspDevice_Click(object sender, EventArgs e)
        {
            spc_Main.Panel1Collapsed = !spc_Main.Panel1Collapsed;
            if (spc_Main.Panel1Collapsed)
            {
                lbl_CollaspDevice.Text = ">";
                toolTip1.SetToolTip(lbl_CollaspDevice, "打开设备列表");
            }
            else
            {
                lbl_CollaspDevice.Text = "<";
                toolTip1.SetToolTip(lbl_CollaspDevice, "关闭设备列表");
            }
        }
        #region device tree
        private void InitDeviceTree()
        {
            myImgIcon = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit
            };
            myImgIcon.Images.Add(Properties.Resources.AllDevice);//--------------------------------0
            myImgIcon.Images.Add(Properties.Resources.Imp);//--------------------------------------1
            myImgIcon.Images.Add(Properties.Resources.ImpError);//---------------------------------2
            myImgIcon.Images.Add(Properties.Resources.ITECH);//------------------------------------3
            myImgIcon.Images.Add(Properties.Resources.ITECHError);//-------------------------------4
            myImgIcon.Images.Add(Properties.Resources.Keysight);//---------------------------------5
            myImgIcon.Images.Add(Properties.Resources.KEComm);//-----------------------------------6
            myImgIcon.Images.Add(Properties.Resources.KECommError);//------------------------------7
            myImgIcon.Images.Add(Properties.Resources.Keysight_bad);//-----------------------------8
            myImgIcon.Images.Add(Properties.Resources.EIC_nomal);//--------------------------------9
            deviceTree.ImageList = myImgIcon;
            deviceTree.ShowNodeToolTips = true;
        }
        private void ShowDeviceTree()
        {
            deviceTree.Nodes.Clear();

            root = new("所有设备");
            root.Name = "All_Device";
            root.ImageIndex = 0;
            root.SelectedImageIndex = 0;

            TreeNode dcRoot = new("热源(" + myExperiment.Instruments.Powers.Count.ToString() + ")");
            dcRoot.Name = "DC_Root";
            dcRoot.ImageIndex = 9;
            dcRoot.SelectedImageIndex = 9;
            foreach (var power in myExperiment.Instruments.Powers.OrderBy(x => x.Address))
            {
                TreeNode dcNode = new();
                if (string.IsNullOrEmpty(power.ChannelNo))
                {
                    dcNode.Name = dcRoot.Name + "_" + power.Address;
                    dcNode.Text = power.DeviceModel + "(" + power.Address + ")";
                }
                else
                {
                    dcNode.Name = dcRoot.Name + "_" + power.Address + power.ChannelNo;
                    dcNode.Text = power.DeviceModel + "(" + power.Address + power.ChannelNo + ")";
                }
                switch (power.DeviceType)
                {
                    case DeviceType.ITECH:
                        dcNode.ImageIndex = 3;
                        dcNode.SelectedImageIndex = 3;
                        break;
                    case DeviceType.Keysight:
                        dcNode.ImageIndex = 5;
                        dcNode.SelectedImageIndex = 5;
                        break;
                    case DeviceType.AMP:
                        dcNode.ImageIndex = 1;
                        dcNode.SelectedImageIndex = 1;
                        break;
                }
                dcRoot.Nodes.Add(dcNode);
            }
            root.Nodes.Add(dcRoot);

            TreeNode tempRoot = new("温度采集设备(" + myExperiment.Instruments.KECtrl.Devices.Devices.Count.ToString() + ")");
            tempRoot.Name = "TEMP_Root";
            tempRoot.ImageIndex = 9;
            tempRoot.SelectedImageIndex = 9;
            foreach (var device in myExperiment.Instruments.KECtrl.Devices.Devices)
            {
                TreeNode deviceNode = new();
                deviceNode.ImageIndex = 6;
                deviceNode.SelectedImageIndex = 6;
                deviceNode.Name = tempRoot.Name + "_" + device.IPAddress;
                deviceNode.Text = device.IPAddress;
                tempRoot.Nodes.Add(deviceNode);
            }
            root.Nodes.Add(tempRoot);

            deviceTree.Nodes.Add(root);
            root.Expand();
        }
        private void DeviceTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            deviceTree.SelectedNode = e.Node;
        }
        private void DeviceTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ;
        }
        private void DeviceTree_DoubleClick(object sender, EventArgs e)
        {
            string str = deviceTree.SelectedNode.Name;
            if (str == "TEMP_Root")
            {
                myExperiment.Instruments.KECtrl.ShowConfig();
            }
            var strs = str.Split('_');
            if (str.Contains("DC_Root") && strs.Length == 3)
            {
                string port = strs[2];
                var power = myExperiment.Instruments.GetPower(port, "");
                frm_Config f = new(power);
                f.Show();
            }
        }
        #endregion
        #region channel data grid view
        private void ChannelDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (channelDataGridView.SelectedChannels.Count == 0)
                return;
        }
        private void ChannelDataGridView_MouseClick(object sender, MouseEventArgs e)
        {
            ;
        }
        #endregion
        #region win10添加病毒文件夹例外
        private enum OSVersion
        {
            Windows2000 = 0,
            WindowsXP = 1,
            Windows2003 = 2,
            Windows2008 = 3,
            Windows7 = 4,
            Windows8 = 5,
            Windows10 = 6,
            Unknown = 7
        }
        private static OSVersion GetOSVersion()
        {
            const string Windows2000 = "5.0";
            const string WindowsXP = "5.1";
            const string Windows2003 = "5.2";
            const string Windows2008 = "6.0";
            const string Windows7 = "6.1";
            const string Windows8 = "6.2";
            const string Windows10 = "10.0";

            switch (Environment.OSVersion.Version.Major + "." + Environment.OSVersion.Version.Minor)
            {
                case Windows2000:
                    return OSVersion.Windows2000;
                case WindowsXP:
                    return OSVersion.WindowsXP;
                case Windows2003:
                    return OSVersion.Windows2003;
                case Windows2008:
                    return OSVersion.Windows2008;
                case Windows7:
                    return OSVersion.Windows7;
                case Windows8:
                    return OSVersion.Windows8;
                case Windows10:
                    return OSVersion.Windows10;
            }
            return OSVersion.Unknown;
        }
        private void AddExclusionPathWin10()
        {
            if (GetOSVersion() == OSVersion.Windows10 | GetOSVersion() == OSVersion.Windows8)
            {
                Task.Run(() =>
                {
                    AddExclusionPath.AddPath(Application.StartupPath);
                });
            }
        }
        #endregion
        #region 测点列表弹出菜单
        private void 全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColumnShowAll();
            最简信息ToolStripMenuItem.Checked = false;
            测量信息ToolStripMenuItem.Checked = false;
            配置信息ToolStripMenuItem.Checked = false;
            全部ToolStripMenuItem.Checked = true;
        }
        private void 配置信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColumnShowConfigChannel();
            最简信息ToolStripMenuItem.Checked = false;
            测量信息ToolStripMenuItem.Checked = false;
            配置信息ToolStripMenuItem.Checked = true;
            全部ToolStripMenuItem.Checked = false;
        }
        private void 测量信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColumnShowMeasure();
            最简信息ToolStripMenuItem.Checked = false;
            测量信息ToolStripMenuItem.Checked = true;
            配置信息ToolStripMenuItem.Checked = false;
            全部ToolStripMenuItem.Checked = false;
        }
        private void 最简信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColumnShowMinimum();
            最简信息ToolStripMenuItem.Checked = true;
            测量信息ToolStripMenuItem.Checked = false;
            配置信息ToolStripMenuItem.Checked = false;
            全部ToolStripMenuItem.Checked = false;
        }
        private void 自定义ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ChannelColumn f = new(channelDataGridView);
            f.Show();
        }
        private void 最大值复位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Channel channel in channelDataGridView.SelectedChannels)
            {
                channel.SetMaxDataZero();
            }
        }
        private void 最小值复位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Channel channel in channelDataGridView.SelectedChannels)
            {
                channel.SetMinDataZero();
            }
        }
        private void 平均值复位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myExperiment.IsSavingData)
            {
                MessageBox.Show("保存试验数据期间不能修改试验配置", "编辑测点", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (Channel channel in channelDataGridView.SelectedChannels)
            {
                channel.SetAvgDataZero();
            }
        }
        private void 显示实时曲线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (channelDataGridView.Rows.Count == 0)
            {
                return;
            }
            if (channelDataGridView.SelectedChannels.Count > 20)
            {
                MessageBox.Show("所选测点个数不能多于20个，请重新选择", "显示曲线", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Thread thr = new(new ThreadStart(OpenCurveForm));
            thr.SetApartmentState(ApartmentState.STA);
            thr.Start();
        }
        private void OpenCurveForm()
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                frm_Curve f = new(channelDataGridView.SelectedChannels, myExperiment);
                f.Show();
            }));
        }
        #region 调整表头
        /// <summary>
        /// 显示测点全部信息
        /// </summary>
        private void ColumnShowAll()
        {
            全部ToolStripMenuItem.Checked = true;
            测量信息ToolStripMenuItem.Checked = false;
            配置信息ToolStripMenuItem.Checked = false;
            最简信息ToolStripMenuItem.Checked = false;
            foreach (DataGridViewColumn C in channelDataGridView.Columns)
            {
                C.Visible = true;
            }
            channelDataGridView.SaveColumn();
        }
        /// <summary>
        /// 显示测点测量信息
        /// </summary>
        private void ColumnShowMeasure()
        {
            全部ToolStripMenuItem.Checked = false;
            测量信息ToolStripMenuItem.Checked = true;
            配置信息ToolStripMenuItem.Checked = false;
            最简信息ToolStripMenuItem.Checked = false;
            channelDataGridView.Columns[0].Visible = true;
            channelDataGridView.Columns[1].Visible = true;
            channelDataGridView.Columns[2].Visible = true;
            channelDataGridView.Columns[3].Visible = false;
            channelDataGridView.Columns[4].Visible = false;
            channelDataGridView.Columns[5].Visible = false;
            channelDataGridView.Columns[6].Visible = false;
            channelDataGridView.Columns[7].Visible = false;
            channelDataGridView.Columns[8].Visible = false;
            channelDataGridView.Columns[9].Visible = false;
            channelDataGridView.Columns[10].Visible = false;
            channelDataGridView.Columns[11].Visible = false;
            channelDataGridView.Columns[12].Visible = false;
            channelDataGridView.Columns[13].Visible = false;
            channelDataGridView.Columns[14].Visible = false;
            channelDataGridView.Columns[15].Visible = false;
            channelDataGridView.Columns[16].Visible = true;
            channelDataGridView.Columns[17].Visible = true;
            channelDataGridView.Columns[18].Visible = true;
            channelDataGridView.Columns[19].Visible = true;
            channelDataGridView.Columns[20].Visible = true;
            channelDataGridView.Columns[21].Visible = true;
            channelDataGridView.Columns[22].Visible = true;
            channelDataGridView.Columns[23].Visible = true;
            channelDataGridView.Columns[24].Visible = true;
            channelDataGridView.SaveColumn();
        }
        /// <summary>
        /// 显示测点配置信息
        /// </summary>
        private void ColumnShowConfigChannel()
        {
            try
            {
                配置信息ToolStripMenuItem.Checked = true;
                全部ToolStripMenuItem.Checked = false;
                测量信息ToolStripMenuItem.Checked = false;
                最简信息ToolStripMenuItem.Checked = false;

                channelDataGridView.Columns[0].Visible = true;
                channelDataGridView.Columns[1].Visible = true;
                channelDataGridView.Columns[2].Visible = true;
                channelDataGridView.Columns[3].Visible = true;
                channelDataGridView.Columns[4].Visible = true;
                channelDataGridView.Columns[5].Visible = true;
                channelDataGridView.Columns[6].Visible = true;
                channelDataGridView.Columns[7].Visible = true;
                channelDataGridView.Columns[8].Visible = true;
                channelDataGridView.Columns[9].Visible = true;
                channelDataGridView.Columns[10].Visible = true;
                channelDataGridView.Columns[11].Visible = true;
                channelDataGridView.Columns[12].Visible = true;
                channelDataGridView.Columns[13].Visible = true;
                channelDataGridView.Columns[14].Visible = true;
                channelDataGridView.Columns[15].Visible = true;
                channelDataGridView.Columns[16].Visible = true;
                channelDataGridView.Columns[17].Visible = false;
                channelDataGridView.Columns[18].Visible = false;
                channelDataGridView.Columns[19].Visible = false;
                channelDataGridView.Columns[20].Visible = false;
                channelDataGridView.Columns[21].Visible = false;
                channelDataGridView.Columns[22].Visible = false;
                channelDataGridView.Columns[23].Visible = false;
                channelDataGridView.Columns[24].Visible = false;
                channelDataGridView.SaveColumn();
            }
            catch { }
        }
        /// <summary>
        /// 显示测点最简信息
        /// </summary>
        private void ColumnShowMinimum()
        {
            全部ToolStripMenuItem.Checked = false;
            测量信息ToolStripMenuItem.Checked = false;
            配置信息ToolStripMenuItem.Checked = false;
            最简信息ToolStripMenuItem.Checked = true;

            channelDataGridView.Columns[0].Visible = true;
            channelDataGridView.Columns[1].Visible = true;
            channelDataGridView.Columns[2].Visible = false;
            channelDataGridView.Columns[3].Visible = false;
            channelDataGridView.Columns[4].Visible = false;
            channelDataGridView.Columns[5].Visible = false;
            channelDataGridView.Columns[6].Visible = false;
            channelDataGridView.Columns[7].Visible = false;
            channelDataGridView.Columns[8].Visible = false;
            channelDataGridView.Columns[9].Visible = false;
            channelDataGridView.Columns[10].Visible = false;
            channelDataGridView.Columns[11].Visible = false;
            channelDataGridView.Columns[12].Visible = false;
            channelDataGridView.Columns[13].Visible = false;
            channelDataGridView.Columns[14].Visible = false;
            channelDataGridView.Columns[15].Visible = false;
            channelDataGridView.Columns[16].Visible = true;
            channelDataGridView.Columns[17].Visible = true;
            channelDataGridView.Columns[18].Visible = true;
            channelDataGridView.Columns[19].Visible = true;
            channelDataGridView.Columns[20].Visible = true;
            channelDataGridView.Columns[21].Visible = true;
            channelDataGridView.Columns[22].Visible = false;
            channelDataGridView.Columns[23].Visible = false;
            channelDataGridView.Columns[24].Visible = false;
            channelDataGridView.SaveColumn();
        }
        #endregion
        #endregion
        #region 新建试验
        private void Btn_New_Click(object sender, EventArgs e)
        {
            pnl_OpenExperiment.Visible = false;
            pnl_NewExperiment.BringToFront();
            pnl_NewExperiment.Left = (pnl_NewExperiment.Parent.Width - pnl_NewExperiment.Width) / 2;
            pnl_NewExperiment.Top = (pnl_NewExperiment.Parent.Height - pnl_NewExperiment.Height) / 2;
            txt_ExperimentName.Focus();
            pnl_NewExperiment.Visible = true;
            pnl_Experiment.Visible = false;
            txt_ExperimentName.Focus();
            txt_ExperimentName.Text = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            txt_ExperimentCommet.Text = "";
            txt_XDelta.Text = "1000";
            txt_EXCELLocation.Text = "";
        }
        private void Lbl_CloseExperiment_Click(object sender, EventArgs e)
        {
            pnl_NewExperiment.Visible = false;
        }
        private void Btn_OpenEXCEL_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new();
            dialog.Filter = "Excel文件(*.xlsx)|*.xlsx";
            dialog.Title = "打开Excel配置文件";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                txt_EXCELLocation.Text = dialog.FileName;
            }
        }
        private async void Btn_CreateExperiment_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_ExperimentName.Text))
            {
                txt_ExperimentName.Focus();
                toolTip1.Show("请输入试验名称", txt_ExperimentName, 3000);
                return;
            }
            if (!int.TryParse(txt_XDelta.Text, out int xdelta))
            {
                txt_XDelta.Focus();
                toolTip1.Show("请输入采样频率", txt_XDelta, 3000);
                return;
            }
            if (string.IsNullOrEmpty(txt_EXCELLocation.Text))
            {
                txt_EXCELLocation.Focus();
                toolTip1.Show("请选择EXCEL配置文件位置", txt_EXCELLocation, 3000);
                return;
            }
            await EXCELCtrl.ReadEXCEL(txt_EXCELLocation.Text);
            await PrepareExperiment(txt_ExperimentName.Text, idWorker.NextId(), xdelta, txt_ExperimentCommet.Text, EXCELCtrl.ControlCircles, EXCELCtrl.Channels, DateTime.Now, DateTime.MinValue);
            myExperiment.Save();

            AfterPrepareExperiment();
        }
        #endregion
        #region 打开试验
        private void Btn_OpenExperiment_Click(object sender, EventArgs e)
        {
            pnl_OpenExperiment.Visible = true;
            pnl_OpenExperiment.BringToFront();
            pnl_NewExperiment.Visible = false;
            pnl_Experiment.Visible = false;
            pnl_OpenExperiment.Left = (pnl_OpenExperiment.Parent.Width - pnl_OpenExperiment.Width) / 2;
            pnl_OpenExperiment.Top = (pnl_OpenExperiment.Parent.Height - pnl_OpenExperiment.Height) / 2;
            ShowExperiments();
        }
        private void ShowExperiments()
        {
            ReadExistsExperimentNames();
            lb_Experiments.Items.Clear();
            foreach (string exp in existsExperimentNames)
            {
                lb_Experiments.Items.Add(exp);
            }
            lb_Experiments.DisplayMember = "Name";
        }
        private void Lbl_CloseOpenExperiment_Click(object sender, EventArgs e)
        {
            pnl_OpenExperiment.Visible = false;
        }
        private void ReadExistsExperimentNames()
        {
            existsExperimentNames.Clear();
            DirectoryInfo dirs = new(Path.Combine(Application.StartupPath, "experiments"));
            foreach (var dir in dirs.EnumerateDirectories())
            {
                var configFile = dir.FullName + "\\" + dir.Name + ".json";
                if (!File.Exists(configFile))
                {
                    continue;
                }
                existsExperimentNames.Add(dir.Name);
            }
        }
        private void Lb_Experiments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_Experiments.SelectedItems.Count == 0)
                return;
            string name = lb_Experiments.SelectedItem.ToString();
            DirectoryInfo directory = new(Path.Combine(Application.StartupPath, "experiments", name));
            string json = File.ReadAllText(directory.FullName + "\\" + name + ".json");
            Experiment exp = JsonConvert.DeserializeObject<Experiment>(json);
            StringBuilder info = new(256);
            info.Append(string.Format("试验ID：{0}\r\n", exp.ID));
            info.Append(string.Format("试验名称：{0}\r\n", exp.Name));
            info.Append(string.Format("备注：{0}\r\n", exp.Commet));
            info.Append(string.Format("执行过程数量：{0}\r\n", exp.ControlCircles.Count));
            info.Append(string.Format("创建时间：{0:yyyy-MM-dd HH:mm:ss}\r\n", exp.CreateDate));
            if (exp.LastRunDate < exp.CreateDate)
            {
                info.Append("最后运行时间：无\r\n");
            }
            else
            {
                info.Append(string.Format("最后运行时间：{0:yyyy-MM-dd HH:mm:ss}\r\n", exp.LastRunDate));
            }
            info.Append(string.Format("运行试验次数：{0}\r\n", exp.MainDirInfo.EnumerateDirectories().Count()));
            info.Append("\r\n");
            txt_ExperimentInfo.Text = info.ToString();
        }
        private async void Btn_OpenSelectedExperiment_Click(object sender, EventArgs e)
        {
            if (lb_Experiments.SelectedItem == null)
            {
                return;
            }
            Cursor = Cursors.WaitCursor;
            string name = lb_Experiments.SelectedItem.ToString();
            DirectoryInfo directory = new(Path.Combine(Application.StartupPath, "experiments", name));
            string json = File.ReadAllText(directory.FullName + "\\" + name + ".json");
            Experiment tmp = JsonConvert.DeserializeObject<Experiment>(json);
            await PrepareExperiment(tmp.Name, tmp.ID, tmp.XDelta, tmp.Commet, tmp.ControlCircles, tmp.Channels, tmp.CreateDate, tmp.LastRunDate);
            SendKEConfig();
            AfterPrepareExperiment();
            Cursor = Cursors.Default;
        }
        private void Btn_DelExperment_Click(object sender, EventArgs e)
        {
            if (lb_Experiments.SelectedItem == null)
            {
                return;
            }
            if (MessageBox.Show("是否删除此试验？选择删除试验将同时删除此试验的所有历史数据。", "删除试验", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            string folder = Path.Combine(Application.StartupPath, "experiments", lb_Experiments.SelectedItem.ToString());
            FileOperateProxy.DeleteFolder(folder);
            ShowExperiments();
            txt_ExperimentInfo.Text = "";
        }
        #endregion
        #region 主体功能按钮
        private void Btn_SaveExperiment_Click(object sender, EventArgs e)
        {
            myExperiment.Save();
            MessageBox.Show("试验保存成功！", "保存试验", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private async void Btn_CloseExperiment_Click(object sender, EventArgs e)
        {
            await myExperiment?.Close();
            pnl_Experiment.Visible = false;
            btn_New.Enabled = true;
            btn_OpenExperiment.Enabled = true;
            btn_DCEnable.Enabled = false;
            btn_DCDisable.Enabled = false;
            btn_HistoryData.Enabled = false;
            btn_SaveExperiment.Enabled = false;
            btn_CloseExperiment.Enabled = false;
            deviceTree.Nodes.Clear();
        }
        private async void Btn_DCEnable_Click(object sender, EventArgs e)
        {
            await myExperiment.BeginControl();
            btn_CloseExperiment.Enabled = false;
            btn_DCEnable.Enabled = false;
            btn_DCDisable.Enabled = true;
            btn_ReloadExcel.Enabled = false;
            channelDataGridView.ChangeConfigs(myExperiment.Channels);
        }
        private async void Btn_DCDisable_Click(object sender, EventArgs e)
        {
            await myExperiment.StopControl();
            btn_DCEnable.Enabled = true;
            btn_DCDisable.Enabled = false;
            btn_CloseExperiment.Enabled = true;
            btn_ReloadExcel.Enabled = true;
            channelDataGridView.ChangeConfigs(myExperiment.Channels);
        }
        private void Btn_HistoryData_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new(AppDomain.CurrentDomain.BaseDirectory + "experiments\\" + myExperiment.Name.ToString() + "\\");
            frm_History f = new(dir);
            f.Show();
        }
        #endregion 
        #region 切换测点列表方式
        private void Btn_ChannelManage_Click(object sender, EventArgs e)
        {
            spc_Channel.Left = 0;
            spc_Channel.Top = 32;
            spc_Channel.Width = pnl_Experiment.Width;
            spc_Channel.Height = pnl_Experiment.Height - 32;
            spc_Channel.Visible = true;
            btn_ChannelManage.BackColor = Color.FromArgb(48, 71, 81);
            btn_DCManage.BackColor = Color.Black;
            btn_SensorManage.BackColor = Color.Black;
            channelDataGridView.SetShowMode(0);
        }
        private void Btn_DCManage_Click(object sender, EventArgs e)
        {
            btn_ChannelManage.BackColor = Color.Black;
            btn_DCManage.BackColor = Color.FromArgb(48, 71, 81);
            btn_SensorManage.BackColor = Color.Black;
            channelDataGridView.SetShowMode(1);
        }
        private void Btn_SensorManage_Click(object sender, EventArgs e)
        {
            btn_ChannelManage.BackColor = Color.Black;
            btn_DCManage.BackColor = Color.Black;
            btn_SensorManage.BackColor = Color.FromArgb(48, 71, 81);
            channelDataGridView.SetShowMode(2);
        }
        private void Btn_Procedue_Click(object sender, EventArgs e)
        {
            foreach(Form form in Application.OpenForms)
            {
                if(form.Text == "执行过程列表")
                {
                    return;
                }
            }
            frm_ControlCircle f = new(myExperiment);
            f.Show();
        }
        private async void Btn_ReloadExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new();
            dialog.Filter = "Excel文件(*.xlsx)|*.xlsx";
            dialog.Title = "打开Excel配置文件";
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if(string.IsNullOrEmpty(dialog.FileName))
                {
                    return;
                }
                myExperiment.OnDataRefresh -= MyExperiment_OnDataRefresh;
                await myExperiment.Reload(dialog.FileName);
                myExperiment.OnDataRefresh += MyExperiment_OnDataRefresh;
                myExperiment.Save();
                channelDataGridView.ShowRows(myExperiment.Channels);
                channelDataGridView.SetShowMode(channelDataGridView.ShowMode);
            }
        }

        #endregion
        private void MyExperiment_OnDataRefresh(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                channelDataGridView.ChangeValues(myExperiment.Channels);
                if (myExperiment.IsControl)
                {
                    lbl_ControlInfo.Visible = true;
                    string info = $"当前执行过程编号:{myExperiment.CurrentCircle.ID}; 已用时{Utilities.GetDurationString(myExperiment.ControlLast)}, 设定时间{myExperiment.CurrentCircle.LastTime.TotalMinutes}分";
                    lbl_ControlInfo.Text = info;
                }
                else
                {
                    lbl_ControlInfo.Visible = false;
                }
            }));
        }
        private void Instruments_OnKEChangeConfig(object sender, Keithley.ChannelsEventArgs e)
        {
            var channels = new List<Channel>();
            foreach (var kechannel in e.Channels)
            {
                var ch = myExperiment.GetChannel(kechannel.DeviceAddress, kechannel.Address);
                if (ch is not null)
                {
                    ch.ChannelType = kechannel.ChannelType;
                    ch.TransducerType = kechannel.TransducerType;
                    ch.ThermocoupleType = kechannel.ThermocoupleType;
                    ch.ThermistorType = kechannel.ThermistorType;
                    ch.FourWireRTDType = kechannel.FourWireRTDType;
                    ch.ColdJunc = kechannel.ColdJunc;
                    channels.Add(ch);
                }
            }
            channelDataGridView.ChangeConfigs(channels);
            myExperiment.Save();
        }
        private async Task PrepareExperiment(string name, long id, int xdelta, string comment, List<ControlCircle> circles, List<Channel> channels, DateTime create, DateTime lastrun)
        {
            myExperiment = new(name, id);
            myExperiment.XDelta = xdelta;
            myExperiment.Commet = comment;
            myExperiment.ControlCircles = circles;
            myExperiment.CreateDate = create;
            myExperiment.LastRunDate = lastrun;
            foreach (var circle in myExperiment.ControlCircles)
            {
                circle.Parent = myExperiment;
                foreach (var line in circle.Lines)
                {
                    line.Parent = circle;
                }
            }
            myExperiment.Channels = channels;
            foreach (var channel in myExperiment.Channels)
            {
                channel.InitDChannel();
            }
            myExperiment.Instruments = new();
            myExperiment.Instruments.Powers = Experiment.GetPowers(myExperiment.GetDCChannels());
            await myExperiment.Instruments.Open();
            foreach (var device in Experiment.GetKEDevices(myExperiment.GetTempChannels()))
            {
                myExperiment.Instruments.KECtrl.AddDevice(device);
                device.Connect();
            }
            foreach (var channel in myExperiment.Channels)
            {
                channel.XDelta = myExperiment.XDelta;
            }
            myExperiment.Instruments.OnKEChangeConfig += Instruments_OnKEChangeConfig;
            myExperiment.InitTimer();
            myExperiment.OnDataRefresh += MyExperiment_OnDataRefresh;
            myExperiment.OnChangeChannelConfig += MyExperiment_OnChangeChannelConfig;
            myExperiment.OnControlEnd += MyExperiment_OnControlEnd;
        }
        private void MyExperiment_OnControlEnd(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                btn_DCEnable.Enabled = true;
                btn_DCDisable.Enabled = false;
                btn_CloseExperiment.Enabled = true;
                btn_ReloadExcel.Enabled = true;
            }));
        }
        private void MyExperiment_OnChangeChannelConfig(object sender, EventArgs e)
        {
            channelDataGridView.ChangeConfigs(myExperiment.Channels);
        }
        private void SendKEConfig()
        {
            if (myExperiment.GetTempChannels().Count > 0)
            {
                bool hasKeithley = false;
                foreach (Channel channel in myExperiment.GetTempChannels())
                {
                    switch (channel.DeviceType)
                    {
                        case DeviceType.Keithley:
                            KEChannel kEChannel = myExperiment.Instruments.GetKEChannel(channel.HardwareDeviceAddress, channel.HardwareChannelAddress);
                            if (kEChannel != null)
                            {
                                kEChannel.ChannelType = channel.ChannelType;
                                kEChannel.ThermistorType = channel.ThermistorType;
                                kEChannel.ThermocoupleType = channel.ThermocoupleType;
                                kEChannel.FourWireRTDType = channel.FourWireRTDType;
                                kEChannel.TransducerType = channel.TransducerType;
                                if (kEChannel.ChannelType != ChannelType.无)
                                {
                                    kEChannel.IsConfiged = true;
                                }
                            }
                            hasKeithley = true;
                            break;
                    }

                }
                if (hasKeithley)
                {
                    foreach (KEDevice device in myExperiment.Instruments.KECtrl.Devices.Devices)
                    {
                        device.PrepareCommands();
                        device.BeginTrigger();
                    }
                }
            }
        }
        private void AfterPrepareExperiment()
        {
            ShowDeviceTree();
            pnl_NewExperiment.Visible = false;
            pnl_OpenExperiment.Visible = false;
            pnl_Experiment.Visible = true;
            pnl_Experiment.Dock = DockStyle.Fill;
            btn_New.Enabled = false;
            btn_OpenExperiment.Enabled = false;
            btn_SaveExperiment.Enabled = true;
            btn_CloseExperiment.Enabled = true;
            btn_DCEnable.Enabled = true;
            btn_DCDisable.Enabled = false;
            btn_HistoryData.Enabled = true;
            btn_ChannelManage.BackColor = Color.FromArgb(48, 71, 81);
            btn_DCManage.BackColor = Color.Black;
            btn_SensorManage.BackColor = Color.Black;
            channelDataGridView.ShowRows(myExperiment.Channels);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using FileStream fs = new("R2T2.txt", FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new(fs);
            using FileStream fsWrite = new("TTT.TXT", FileMode.Create, FileAccess.ReadWrite);
            StreamWriter sw = new(fsWrite);
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine().Split('\t');
                int begin = int.Parse(line.First());
                int end = int.Parse(line.Last());
                if(begin == end)
                {
                    string writeLine = line[0] + "," + line[1];
                    sw.WriteLine(writeLine);
                }
                else if(end < 0)
                {
                    for(int i = 10; i >= 1; i--)
                    {
                        string writeLine = (begin - i + 1).ToString() + "," + line[i] + ",0,0";
                        sw.WriteLine(writeLine);
                    }
                }
                else if(end > 0)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        string writeLine = (begin + i - 1).ToString() + "," + line[i] + ",0,0";
                        sw.WriteLine(writeLine);
                    }
                }
            }
            sw.Flush();
            sw.Close();
            sw.Dispose();
            streamReader.Close();
            streamReader.Dispose();
            
        }
    }
}
