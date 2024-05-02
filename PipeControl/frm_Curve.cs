using DataObjects;
using ImcCurvesLib;
using PipeControl.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PipeControl
{
    public partial class frm_Curve : Form
    {
        readonly List<Channel> ChannelList = new();
        private readonly CurveGlobalsClass cgc = new CurveGlobalsClass();
        private readonly Experiment myExperiment;
        private bool showLegend, showLastValue;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014)
                return;
            base.WndProc(ref m);
        }
        private void EnableDoubleBuffering()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
        public frm_Curve(List<Channel> channels, Experiment experiment)
        {
            EnableDoubleBuffering();
            InitializeComponent();

            ChannelList = channels;
            myExperiment = experiment;
            myExperiment.OnDataRefresh += MyExperiment_OnDataRefresh;
            Load += Frm_Curve_Load;
            FormClosing += Frm_Curve_FormClosing;
            pnl_right.Resize += Pnl_right_Resize;
        }
        private void MyExperiment_OnDataRefresh(object sender, EventArgs e)
        {
            foreach (Control control in pnl_right.Controls)
            {
                ChannelMonitor chm = (ChannelMonitor)control;
                chm.RefreshData();
            }
        }
        private void Frm_Curve_Load(object sender, EventArgs e)
        {
            spc_Top.SplitterDistance = (int)(spc_Top.Width * 0.8);
            spc_Main.SplitterWidth = 1;
            btn_LastValue.Top = 0 ;
            btn_ShowLegend.Top = 0;
            foreach (Channel channel in ChannelList)
            {
                if (channel != null)
                {
                    channel.ShowInCurve += 1;
                    curve.AppendChannel(channel.DChannel);
                    ChannelMonitor chMonitor = new ChannelMonitor();
                    chMonitor.OnSmall += chMonitor_OnSmall;
                    chMonitor.SetChannel(channel);
                    int i = curve.Lines.Count;
                    Color c = cgc.CfgScreen.Colors.get_LineColor((short)i).ToColor();
                    chMonitor.Color = c;
                    pnl_right.Controls.Add(chMonitor);
                }
            }
            ResizeMonitor();
        }
        private void Frm_Curve_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Channel channel in ChannelList)
            {
                channel.ShowInCurve -= 1;
            }
        }
        private void Pnl_right_Resize(object sender, EventArgs e)
        {
            ResizeMonitor();
        }
        private void chMonitor_OnSmall(object sender, EventArgs e)
        {
            spc_Main.SplitterDistance = spc_Main.Width;
        }
        private void btn_ShowLegend_Click(object sender, EventArgs e)
        {
            if (!showLegend)
            {
                curve.LegendDisplay = CwLegendDisplayConstants.ccwLgDisplayAlways;
                btn_ShowLegend.Text = "关闭图例";
            }
            else
            {
                curve.LegendDisplay = CwLegendDisplayConstants.ccwLgDisplayNever;
                btn_ShowLegend.Text = "显示图例";
            }
            showLegend = !showLegend;
        }
        private void btn_LastValue_Click(object sender, EventArgs e)
        {
            if (!showLastValue)
            {
                curve.DisplayMode = CwDisplayModeConstants.ccwLastValue;
                btn_LastValue.Text = "显示曲线";
            }
            else
            {
                curve.DisplayMode = CwDisplayModeConstants.ccwDefault;
                btn_LastValue.Text = "显示最后值";
            }
            showLastValue = !showLastValue;
        }
        private void ResizeMonitor()
        {
            int i = 0;
            foreach (Control control in pnl_right.Controls)
            {
                control.Width = pnl_right.Width;
                control.Height = pnl_right.Height / pnl_right.Controls.Count;
                control.Left = 0;
                control.Top = control.Height * i;
                i += 1;
            }
        }
    }
}
