using DataObjects;
using ImcCoreLib;
using ImcCurvesLib;
using ImcFamosLib;
using Newtonsoft.Json;
using PipeControl.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace PipeControl
{
    public partial class frm_History : Form
    {
        private readonly DirectoryInfo mainDirectory;
        private Experiment myExperiment;
        private readonly List<DChannel> selectedDChannels = new(5);
        private readonly List<HistoryDataDir> historyDataDirs = new(5);
        private void EnableDoubleBuffering()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
        public frm_History(DirectoryInfo dir)
        {
            EnableDoubleBuffering();
            InitializeComponent();
            mainDirectory = dir;
            spc_Right.Enabled = true;
            pnl_Out.Visible = false;
            pnl_CurveMode.Visible = false;
            pnl_CutCurve.Visible = true;
            AddHandles();
        }
        private void AddHandles()
        {
            Load += Frm_History_Load;
            lb_Channels.SelectedIndexChanged += Lb_Channels_SelectedIndexChanged;
            cmb_CurveMode.SelectedIndexChanged += Cmb_CurveMode_SelectedIndexChanged;
            curve.OnCursorMove += Curve_OnCursorMove;
            mtxt_Left.KeyPress += Mtxt_Left_KeyPress;
            mtxt_Right.KeyPress += Mtxt_Right_KeyPress;
            cmb_CursorLeft.SelectedIndexChanged += Cmb_CursorLeft_SelectedIndexChanged;
            btn_CutCurve.Click += Btn_CutCurve_Click;
            btn_ShowAll.Click += Btn_ShowAll_Click;
            btn_ShowChannels.Click += Btn_ShowChannels_Click;
            txt_Inteval.TextChanged += Txt_Inteval_TextChanged;
            btn_CurveMode.Click += Btn_CurveMode_Click;
            btn_Export.Click += Btn_Export_Click;
            btn_CreateOutFile.Click += Btn_CreateOutFile_Click;
            btn_Out.Click += Btn_Out_Click;

            btn_ShowList.Click += Btn_ShowList_Click;
            btn_Legend.Click += Btn_Legend_Click;

            btn_Cut.Click += Btn_Cut_Click;

        }
        private void Frm_History_Load(object sender, EventArgs e)
        {
            AddHistoryDirectories();
            spc_Right.SplitterDistance = spc_Right.Height - 145;
            pnl_CurveMode.Top = 25;
            pnl_Out.Top = 25;
            pnl_CutCurve.Top = 25;
            btn_ShowChannels.Width = spc_Main.Panel1.Width;
        }
        private void AddHistoryDirectories()
        {
            foreach (DirectoryInfo dir in mainDirectory.EnumerateDirectories())
            {
                string str = GetDataInfo(dir);
                if (!string.IsNullOrEmpty(str))
                    lb_HistoryDirectory.Items.Add(str);
            }
        }
        private string GetDataInfo(DirectoryInfo directory)
        {
            try
            {
                string ss = directory.Name;
                DateTime begin;
                long lastTime = long.MaxValue;
                begin = DateTime.Parse(ss.Substring(0, 4) + "-" + ss.Substring(4, 2) + "-" + ss.Substring(6, 2) + " " +
                                       ss.Substring(8, 2) + ":" + ss.Substring(10, 2) + ":" + ss.Substring(12, 2));
                TimeSpan ts = TimeSpan.MinValue;
                var config = directory.EnumerateFiles("*.json").ToList()[0];
                myExperiment = new("tmp", 0, false);
                string str = File.ReadAllText(config.FullName);
                myExperiment = JsonConvert.DeserializeObject<Experiment>(str);
                //foreach (var zone in myExperiment.ControlZones)
                //{
                //    foreach (var channel in zone.HeatChannels)
                //    {
                //        channel.Parent = zone;
                //        channel.XDelta = myExperiment.XDelta;
                //        myExperiment.Channels.Add(channel);
                //    }
                //    zone.TempChannel.Parent = zone;
                //    myExperiment.Channels.Add(zone.TempChannel);
                //}
                var channels = myExperiment.Channels.Where(x => x.Function != ChannelFunction.热源);
                int xdelta = 1;
                foreach (Channel channel in channels)
                {
                    xdelta = channel.XDelta / 1000;
                    var file = directory.EnumerateFiles(channel.Name).ToList()[0];
                    if (file.Length < lastTime)
                    {
                        lastTime = file.Length;
                    }
                }
                ts = TimeSpan.FromSeconds(lastTime / 4 * xdelta);
                HistoryDataDir hdata = new()
                {
                    BeginTime = begin,
                    Directory = directory.FullName,
                    Duration = ts,
                    ShowString = begin.ToString("yyyy-MM-dd HH:mm:ss") + "[" + Utilities.GetDurationString(ts) + "]"
                };
                historyDataDirs.Add(hdata);
                return hdata.ShowString;
            }
            catch
            {
                return string.Empty;
            }
        }
        private void Btn_ShowChannels_Click(object sender, EventArgs e)
        {
            if (lb_HistoryDirectory.SelectedItems.Count == 0)
            {
                return;
            }
            lb_Channels.Items.Clear();
            string select = (string)lb_HistoryDirectory.SelectedItems[0];
            HistoryDataDir historyDataDir = new();
            foreach (HistoryDataDir hdd in historyDataDirs)
            {
                if (hdd.ShowString == select)
                {
                    historyDataDir = hdd;
                    break;
                }
            }
            string config = historyDataDir.Directory + "\\config.json";
            myExperiment = JsonConvert.DeserializeObject<Experiment>(File.ReadAllText(config));
            List<DChannel> dChannels = new();
            DirectoryInfo dir = new(historyDataDir.Directory);
            foreach (FileInfo file in dir.EnumerateFiles())
            {
                if (file.Extension == ".json")
                    continue;
                string name1 = file.Name;
                if (file.Name.Contains("_curr"))
                {
                    name1 = file.Name.Replace("_curr", "");
                    var channel = myExperiment.GetChannelByName(name1);
                    var dchannel = GetDChannel(channel, file, historyDataDir.BeginTime, "电流");
                    dChannels.Add(dchannel);
                }
                else if (file.Name.Contains("_volt"))
                {
                    name1 = file.Name.Replace("_volt", "");
                    var channel = myExperiment.GetChannelByName(name1);
                    var dchannel = GetDChannel(channel, file, historyDataDir.BeginTime, "电压");
                    dChannels.Add(dchannel);
                }
                else
                {
                    var channel = myExperiment.GetChannelByName(name1);
                    var dchannel = GetDChannel(channel, file, historyDataDir.BeginTime, "");
                    dChannels.Add(dchannel);
                }
            }
            foreach (var dchannel in dChannels.OrderBy(x => x.Comment))
            {
                lb_Channels.Items.Add(dchannel);
                lb_Channels.DisplayMember = "Name";
            }
        }
        private DChannel GetDChannel(Channel channel, FileInfo file, DateTime trigger, string suffix)
        {
            DChannel dchannel = new();
            if (channel != null)
            {
                byte[] b = new byte[file.Length];
                using (FileStream s = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    s.Read(b, 0, b.Length);
                }
                float[] f = new float[b.Length / 4];
                int fi = 0;
                for (int i = 0; i < b.Length; i += 4)
                {
                    f[fi] = BitConverter.ToSingle(b, i);
                    fi += 1;
                }
                dchannel.TriggerTime.AsDATE = trigger;
                dchannel.xDelta = channel.XDelta / 1000;
                dchannel.Name = channel.UserName + suffix;
                dchannel.zUnit = channel.Function.ToString();
                dchannel.set_Flags(DmChannelFlagConstants.cdmFlagPrivate, true);
                dchannel.Data = f;
            }
            else
            {
                string name = file.Name;
                byte[] b = new byte[file.Length];
                using (FileStream s = File.Open(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    s.Read(b, 0, b.Length);
                }
                float[] f = new float[b.Length / 4];
                int fi = 0;
                for (int i = 0; i < b.Length; i += 4)
                {
                    f[fi] = BitConverter.ToSingle(b, i);
                    fi += 1;
                }
                dchannel.TriggerTime.AsDATE = trigger;
                dchannel.xDelta = myExperiment.XDelta / 1000d;
                dchannel.Name = file.Name;
                dchannel.Comment = "";
                dchannel.zUnit = "";
                dchannel.set_Flags(DmChannelFlagConstants.cdmFlagPrivate, true);
                dchannel.Data = f;
            }
            return dchannel;
        }
        private void Lb_Channels_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lb_Channels.SelectedItems.Count == 0)
            {
                spc_Right.Enabled = false;
                curve.Clear();
                return;
            }
            spc_Right.Enabled = true;
            curve.Clear();
            selectedDChannels.Clear();
            cmb_CursorLeft.Items.Clear();
            foreach (var item in lb_Channels.SelectedItems)
            {
                DChannel dchannel = (DChannel)item;
                selectedDChannels.Add(dchannel);
                curve.AppendChannel(dchannel, CwAppendConstants.ccwAppendNewLine, 0);
                cmb_CursorLeft.Items.Add(dchannel.Name);
            }
            cmb_CursorLeft.Text = cmb_CursorLeft.Items[0].ToString();
            curve.xAxes[1].MinMaxType = CwAxisMinMaxConstants.ccwAuto;
            curve.xAxes[1].ScaleType = CwAxisScaleConstants.ccwAbsTime;
            curve.CursorLeft.Visible = true;
            curve.CursorRight.Visible = true;
            curve.xAxes[1].MinMaxType = CwAxisMinMaxConstants.ccwAuto;
            historyList.Visible = false;
            curve.Visible = true;
            btn_Legend.Enabled = true;
            btn_ShowList.Text = "显示表格";
            if (lb_Channels.SelectedItems.Count > 1)
            {
                cmb_CurveMode.Enabled = true;
            }
            else
            {
                cmb_CurveMode.Enabled = false;
            }


        }
        private void Btn_ShowList_Click(object sender, EventArgs e)
        {
            if (btn_ShowList.Text == "显示表格")
            {
                if (curve.Lines.Count == 0)
                {
                    return;
                }
                List<DChannel> tmp = new();
                foreach (CwLine line in curve.Lines)
                {
                    tmp.Add(line.Data.Channel);
                }
                DTime begin = new();
                DTime end = new();
                begin.Value = curve.xAxes[1].CurrentMin;
                historyList.BeginTime = begin;
                end.Value = curve.xAxes[1].CurrentMax;
                historyList.EndTime = end;
                historyList.ShowData(tmp);

                historyList.Dock = DockStyle.Fill;
                historyList.Visible = true;
                btn_ShowList.Text = "显示曲线";
                btn_Legend.Enabled = false;

                pnl_CurveMode.Visible = false;
                pnl_Out.Visible = true;
                pnl_CutCurve.Visible = false;
                btn_Cut.BackColor = Color.Gainsboro;
                btn_CurveMode.BackColor = Color.Gainsboro;
                btn_Export.BackColor = Color.Silver;
            }
            else
            {
                historyList.Visible = false;
                btn_ShowList.Text = "显示表格";
                btn_Legend.Enabled = true;

                pnl_Out.Visible = false;
                pnl_CurveMode.Visible = true;
                pnl_CutCurve.Visible = false;
                btn_Cut.BackColor = Color.Gainsboro;
                btn_CurveMode.BackColor = Color.Silver;
                btn_Export.BackColor = Color.Gainsboro;
            }
        }
        private void Btn_Legend_Click(object sender, EventArgs e)
        {
            if (curve.LegendDisplay == CwLegendDisplayConstants.ccwLgDisplayNever)
            {
                btn_Legend.Text = "关闭图例";
                curve.LegendDisplay = CwLegendDisplayConstants.ccwLgDisplayAlways;
            }
            else
            {
                btn_Legend.Text = "显示图例";
                curve.LegendDisplay = CwLegendDisplayConstants.ccwLgDisplayNever;
            }
        }
        private void Curve_OnCursorMove(object sender, EventArgs e)
        {
            ShowCurveValues();
        }
        private void ShowCurveValues()
        {
            if (curve.Lines.Count == 0)
            {
                return;
            }

            if (curve.xAxes[1].ScaleType == CwAxisScaleConstants.ccwAbsTime)
            {
                DTime t = new DTime
                {
                    Value = curve.CursorLeft.GetValue(CwCursorValueConstants.ccwCursorX)
                };
                lbl_LeftValue.Text = "鼠标左键测量值：" + curve.CursorLeft.GetValue(CwCursorValueConstants.ccwCursorY).ToString("#0.00000") + curve.CursorLeft.AssocLine.Data.Channel.yUnit;
                mtxt_Left.Text = t.AsDATE.ToString("dd日HH时mm分ss秒");
                mtxt_Left.Visible = true;
                lbl1.Text = t.Value.ToString();

                t.Value = curve.CursorRight.GetValue(CwCursorValueConstants.ccwCursorX);
                lbl_RightValue.Text = "鼠标右键测量值：" + curve.CursorRight.GetValue(CwCursorValueConstants.ccwCursorY).ToString("#0.00000") + curve.CursorRight.AssocLine.Data.Channel.yUnit;
                mtxt_Right.Text = t.AsDATE.ToString("dd日HH时mm分ss秒");
                mtxt_Right.Visible = true;
                lbl2.Text = t.Value.ToString();
            }
            else
            {
                lbl_Left.Text = "鼠标左键X值：" + curve.CursorLeft.GetValue(CwCursorValueConstants.ccwCursorX).ToString("#0.00000") + curve.CursorLeft.AssocLine.Data.Channel.yUnit;
                lbl_LeftValue.Text = "鼠标左键Y值：" + curve.CursorLeft.GetValue(CwCursorValueConstants.ccwCursorY).ToString("#0.00000") + curve.CursorLeft.AssocLine.Data.Channel.yUnit;
                mtxt_Left.Visible = false;

                lbl_Right.Text = "鼠标左键X值：" + curve.CursorRight.GetValue(CwCursorValueConstants.ccwCursorX).ToString("#0.00000") + curve.CursorRight.AssocLine.Data.Channel.yUnit;
                lbl_RightValue.Text = "鼠标右键Y值：" + curve.CursorRight.GetValue(CwCursorValueConstants.ccwCursorY).ToString("#0.00000") + curve.CursorRight.AssocLine.Data.Channel.yUnit;
                mtxt_Right.Visible = false;
            }

        }
        #region 曲线方式
        private void Btn_CurveMode_Click(object sender, EventArgs e)
        {
            btn_Cut.BackColor = Color.Gainsboro;
            btn_CurveMode.BackColor = Color.Silver;
            btn_Export.BackColor = Color.Gainsboro;
            pnl_CutCurve.Visible = false;
            pnl_CurveMode.Visible = true;
            pnl_Out.Visible = false;
        }
        private void Cmb_CurveMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cmb_CurveMode.SelectedItem.ToString())
            {
                case "相同的X轴和Y轴":
                    curve.Clear();
                    foreach (DChannel channel in selectedDChannels)
                    {
                        curve.AppendChannel(channel, CwAppendConstants.ccwAppendNewLine, 0);
                    }
                    break;
                case "相同的X轴，不同的Y轴":
                    curve.Clear();
                    foreach (DChannel channel in selectedDChannels)
                    {
                        curve.AppendChannel(channel, CwAppendConstants.ccwAppendNewAxis, 0);
                    }
                    break;
                case "不同的X轴，不同的Y轴":
                    curve.Clear();
                    foreach (DChannel channel in selectedDChannels)
                    {
                        curve.AppendChannel(channel, CwAppendConstants.ccwAppendNewCosy, 0);
                    }
                    break;
            }
        }
        #endregion
        #region 导出数据
        private void Btn_Export_Click(object sender, EventArgs e)
        {
            btn_Cut.BackColor = Color.Gainsboro;
            btn_CurveMode.BackColor = Color.Gainsboro;
            btn_Export.BackColor = Color.Silver;
            pnl_CutCurve.Visible = false;
            pnl_CurveMode.Visible = false;
            pnl_Out.Visible = true;
        }
        private void Txt_Inteval_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    int i = int.Parse(txt_Inteval.Text);
            //    if (i > 1)
            //    {
            //        chk.Visible = true;
            //    }
            //    else
            //    {
            //        chk.Visible = false;
            //    }
            //}
            //catch
            //{
            //    chk.Visible = false;
            //}
        }
        private void Btn_CreateOutFile_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "EXCEL文件|*.csv"
            };
            string fileName = "";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                fileName = dlg.FileName;
                if (fileName.Substring(fileName.Length - 4, 4) != ".csv")
                {
                    fileName += ".csv";
                }

                txt_OutFileLocation.Text = fileName;
            }
        }
        private void Btn_Out_Click(object sender, EventArgs e)
        {
            if (curve.Lines.Count == 0)
            {
                return;
            }

            if (txt_OutFileLocation.Text == "")
            {
                txt_OutFileLocation.Focus();
                return;
            }
            if (!int.TryParse(txt_Inteval.Text, out int inteval))
            {
                txt_Inteval.Focus();
                return;
            }
            Cursor = Cursors.WaitCursor;
            btn_Out.Enabled = false;
            DTime begin = new DTime();
            DTime end = new DTime();
            begin.Value = curve.xAxes[1].CurrentMin;
            end.Value = curve.xAxes[1].CurrentMax;
            pb.Maximum = (int)((end.Value - begin.Value)) + 3;
            pb.Value = 0;
            pb.Visible = true;
            ThrOutCSV(txt_OutFileLocation.Text, inteval);
            Cursor = Cursors.Default;
        }
        private IEnumerable<CwLine> GetLineOrder()
        {
            IEnumerable<CwLine> order = from CwLine line in curve.Lines select line;
            return order;
        }
        private readonly Famos famos = new();
        private void ThrOutCSV(string fileName, int inteval)
        {
            int allLines = curve.Lines.Count;
            int lineNum = 0;
            string[] title = new string[allLines];
            string[] comments = new string[allLines];
            string avg = "平均值, ,";
            string stdev = "标准差, ,";
            string vc = "变异系数, ,";
            DTime beginTime = new();
            beginTime.Value = curve.xAxes[1].CurrentMin;
            int xdelta = 1;
            List<DChannel> channels = new();
            foreach (CwLine line in GetLineOrder())
            {
                DChannel channel = line.Data.Channel;
                xdelta = (int)channel.xDelta;
                DChannel tmpChannel = famos.Misc.ResampleRed(channel, inteval);
                tmpChannel.Name = channel.Name;
                tmpChannel.xDelta = channel.xDelta;
                comments[lineNum] = "0.000";
                if (!string.IsNullOrEmpty(line.Data.Channel.yUnit))
                {
                    title[lineNum] = line.Data.Channel.Name + "(" + line.Data.Channel.yUnit + ")";
                }
                else
                {
                    title[lineNum] = line.Data.Channel.Name;
                }
                avg += famos.Stat.Mean(tmpChannel).get_Value(1).ToString(comments[lineNum]) + ",";
                stdev += famos.Stat.StDev(tmpChannel).get_Value(1).ToString(comments[lineNum]) + ",";
                vc += (famos.Stat.StDev(tmpChannel).get_Value(1) / famos.Stat.Mean(tmpChannel).get_Value(1) * 100) + "%,";
                lineNum += 1;
                channels.Add(tmpChannel);
            }
            try
            {
                using (FileStream fs = new(fileName, FileMode.Create, FileAccess.Write))
                {
                    using StreamWriter sw = new(fs, System.Text.Encoding.GetEncoding("gb2312"));
                    string str = "编号" + ",时间,";
                    for (int i = 0; i < allLines; i++)
                    {
                        str += title[i] + ",";
                    }
                    str = str.Substring(0, str.Length - 1);
                    sw.WriteLine(str);
                    avg = avg.Substring(0, avg.Length - 1);
                    stdev = stdev.Substring(0, stdev.Length - 1);
                    vc = vc.Substring(0, vc.Length - 1);
                    DateTime date = new();
                    date = beginTime.AsDATE;
                    for (int i = 1; i <= channels[0].Length; i++)
                    {
                        string line = string.Empty;
                        line = i.ToString() + ",";
                        line += date.ToString("yyyy年MM月dd日 HH:mm:ss") + ",";
                        foreach (var channel in channels)
                        {
                            double d = channel.get_Value(i);
                            line += d.ToString("0.000") + ",";
                        }
                        line = line.Substring(0, line.Length - 1);
                        sw.WriteLine(line);
                        date = date.AddSeconds(inteval * xdelta);
                        Invoke(new MethodInvoker(delegate () { pb.Value += 1; }));
                    }
                    sw.WriteLine(avg);
                    sw.WriteLine(stdev);
                    sw.WriteLine(vc);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                }
                curve.yAxes[1].MinMaxType = CwAxisMinMaxConstants.ccwAuto;
                Invoke(new MethodInvoker(delegate () { pb.Visible = false; }));
                MessageBox.Show("导出成功！", "导出数据", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (IOException)
            {
                MessageBox.Show("当前文件已打开，请新建一个文件名称", "导出数据", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            BeginInvoke(new MethodInvoker(delegate
            {
                txt_OutFileLocation.Text = "";
                btn_Out.Enabled = true;
            }));
        }
        #endregion 
        #region 剪切数据
        private void Btn_Cut_Click(object sender, EventArgs e)
        {
            btn_Cut.BackColor = Color.Silver;
            btn_CurveMode.BackColor = Color.Gainsboro;
            btn_Export.BackColor = Color.Gainsboro;
            pnl_CutCurve.Visible = true;
            pnl_CurveMode.Visible = false;
            pnl_Out.Visible = false;
        }
        private void Cmb_CursorLeft_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (curve.Lines.Count == 0)
            { return; }
            foreach (CwLine line in curve.Lines)
            {
                if (line.Data.Channel.Name == cmb_CursorLeft.SelectedItem.ToString())
                {
                    try
                    {
                        curve.CursorLeft.AssocLine = line;
                        curve.CursorRight.AssocLine = line;
                    }
                    catch { }
                    break;
                }
            }
        }
        private void Btn_ShowAll_Click(object sender, EventArgs e)
        {
            if (curve.Lines.Count == 0)
            {
                return;
            }

            curve.xAxes[1].MinMaxType = CwAxisMinMaxConstants.ccwAuto;
            curve.yAxes[1].MinMaxType = CwAxisMinMaxConstants.ccwAuto;
            curve.DoUpdate();
            ShowCurveValues();
        }
        private void Btn_CutCurve_Click(object sender, EventArgs e)
        {
            if (curve.Lines.Count == 0)
            { return; }
            double min, max;
            min = curve.xAxes[1].CurrentMin;
            max = curve.xAxes[1].CurrentMax;

            if (curve.xAxes[1].ScaleType == CwAxisScaleConstants.ccwAbsTime)
            {
                double left = double.Parse(lbl1.Text);// curve.CursorLeft.GetValue(CwCursorValueConstants.ccwCursorX);
                if (left > max)
                { left = max; }
                if (left < min)
                { left = min; }

                double right = double.Parse(lbl2.Text);// curve.CursorRight.GetValue(CwCursorValueConstants.ccwCursorX);
                if (right > max)
                { right = max; }
                if (right < min)
                { right = min; }
                if (Math.Abs(right - left) < 1)
                {
                    return;
                }

                DTime dleft = new DTime();
                DTime dright = new DTime();
                dleft.Value = left;
                dright.Value = right;
                dleft.Second = Math.Floor(dleft.Second);
                dright.Second = Math.Floor(dright.Second);
                if (left > right)
                {
                    curve.xAxes[1].SetMinMax(dright.Value, dleft.Value);
                }
                else
                {
                    curve.xAxes[1].SetMinMax(dleft.Value, dright.Value);
                }
                curve.yAxes[1].MinMaxType = CwAxisMinMaxConstants.ccwAuto;
            }
            else
            {
                double left = curve.CursorLeft.GetValue(CwCursorValueConstants.ccwCursorX);
                double right = curve.CursorRight.GetValue(CwCursorValueConstants.ccwCursorX);
                if (left >= right)
                {
                    curve.xAxes[1].SetMinMax(right, left);
                }
                else
                {
                    curve.xAxes[1].SetMinMax(left, right);
                }
                curve.yAxes[1].MinMaxType = CwAxisMinMaxConstants.ccwAuto;
            }
            curve.DoUpdate();
            ShowCurveValues();
        }
        private void Mtxt_Right_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (curve.Lines.Count == 0)
            {
                return;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                DTime t = new DTime
                {
                    Value = curve.CursorRight.GetValue(CwCursorValueConstants.ccwCursorX)
                };
                DateTime temp = DateTime.MaxValue;
                if (DateTime.TryParse(t.AsDATE.Year.ToString() + "年" + t.AsDATE.Month.ToString() + "月" + mtxt_Right.Text, out temp))
                {
                    t.AsDATE = temp;
                    if (t.Value > curve.xAxes[1].CurrentMax)
                    {
                        t.Value = curve.xAxes[1].CurrentMax;
                    }
                    if (t.Value < curve.xAxes[1].CurrentMin)
                    {
                        t.Value = curve.xAxes[1].CurrentMin;
                    }
                    mtxt_Right.Text = t.AsDATE.ToString("dd日HH时mm分ss秒");
                    curve.CursorRight.SetPos1(t.Value, CwCursorValueConstants.ccwCursorX);
                    lbl2.Text = t.Value.ToString();
                    lbl_RightValue.Text = "鼠标右键测量值：" + curve.CursorRight.GetValue(CwCursorValueConstants.ccwCursorY).ToString("#0.00000") + curve.CursorRight.AssocLine.Data.Channel.yUnit;
                }
            }
        }
        private void Mtxt_Left_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (curve.Lines.Count == 0)
            {
                return;
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                DTime t = new DTime
                {
                    Value = curve.CursorLeft.GetValue(CwCursorValueConstants.ccwCursorX)
                };
                DateTime temp = DateTime.MaxValue;
                if (DateTime.TryParse(t.AsDATE.Year.ToString() + "年" + t.AsDATE.Month.ToString() + "月" + mtxt_Left.Text, out temp))
                {
                    t.AsDATE = temp;
                    t.Second = Math.Floor(t.Second);
                    if (t.Value > curve.xAxes[1].CurrentMax)
                    {
                        t.Value = curve.xAxes[1].CurrentMax;
                    }
                    if (t.Value < curve.xAxes[1].CurrentMin)
                    {
                        t.Value = curve.xAxes[1].CurrentMin;
                    }
                    mtxt_Left.Text = t.AsDATE.ToString("dd日HH时mm分ss秒");
                    curve.CursorLeft.SetPos1(t.Value, CwCursorValueConstants.ccwCursorX);
                    lbl1.Text = t.Value.ToString();
                    lbl_LeftValue.Text = "鼠标左键测量值：" + curve.CursorLeft.GetValue(CwCursorValueConstants.ccwCursorY).ToString("#0.000000") + curve.CursorLeft.AssocLine.Data.Channel.yUnit;
                }
            }
        }
        #endregion
    }
}
