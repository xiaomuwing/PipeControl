using DataObjects;
using PipeControl.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PipeControl
{
    public partial class frm_ControlCircle : Form
    {
        readonly ControlCircleDataGridView controlCircleDataGridView;
        readonly Experiment myExperiment;
        private void EnableDoubleBuffering()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
        public frm_ControlCircle(Experiment exp)
        {
            EnableDoubleBuffering();
            InitializeComponent();
            myExperiment = exp;



            Load += Frm_ControlCircle_Load;
            FormClosing += Frm_ControlCircle_FormClosing;

            controlCircleDataGridView = new(CreateChannelDeviceColumnCollection(), Color.FromArgb(48, 71, 81), SystemColors.Highlight);
            spc_Main.Panel2.Controls.Add(controlCircleDataGridView);
            controlCircleDataGridView.Dock = DockStyle.Fill;
        }

        private void Frm_ControlCircle_FormClosing(object sender, FormClosingEventArgs e)
        {
            myExperiment.OnDataRefresh -= MyExperiment_OnDataRefresh;
        }

        private void MyExperiment_OnReloadEnd(object sender, EventArgs e)
        {
            List<ControlLine> lines = new();
            foreach (var control in myExperiment.ControlCircles)
            {
                foreach (var line in control.Lines)
                {
                    lines.Add(line);
                }
            }
            controlCircleDataGridView.ShowRows(lines);
        }

        private void Frm_ControlCircle_Load(object sender, EventArgs e)
        {
            controlCircleDataGridView.OrignalColumnWidth();
            controlCircleDataGridView.ReadColumn();
            foreach (DataGridViewColumn column in controlCircleDataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Programmatic;
            }
            List<ControlLine> lines = new();
            foreach(var control in myExperiment.ControlCircles)
            {
                foreach(var line in control.Lines)
                {
                    lines.Add(line);
                }
            }
            controlCircleDataGridView.ShowRows(lines);
            myExperiment.OnDataRefresh += MyExperiment_OnDataRefresh;
            myExperiment.OnReloadEnd += MyExperiment_OnReloadEnd;
        }
        private void MyExperiment_OnDataRefresh(object sender, EventArgs e)
        {
            BeginInvoke(new MethodInvoker(delegate
            {
                if (myExperiment.IsControl)
                {
                    lbl_ControlInfo.Visible = true;
                    string info = $"当前执行过程编号:{myExperiment.CurrentCircle.ID}; 已用时{Utilities.GetDurationString(myExperiment.ControlLast)}, 设定时间{myExperiment.CurrentCircle.LastTime.TotalMinutes}分";
                    lbl_ControlInfo.Text = info;

                    foreach(DataGridViewRow row in controlCircleDataGridView.Rows)
                    {
                        if(row.Cells[0].Value.ToString() == myExperiment.CurrentCircle.ID.ToString())
                        {
                            row.Selected = true;
                        }
                        else
                        {
                            row.Selected = false;
                        }
                    }
                }
                else
                {
                    lbl_ControlInfo.Visible = false;
                }
            }));
        }
        private List<ColumnStruct> CreateChannelDeviceColumnCollection()
        {
            List<ColumnStruct> columnCollection = new();

            ColumnStruct tmp;

            tmp.columnName = "序号";//---------------------------------------------------------------0
            tmp.columnType = Type.GetType("System.Int32");
            columnCollection.Add(tmp);

            tmp.columnName = "温控点";//--------------------------------------------------------------1
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "控温类型";//--------------------------------------------------------------2
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "电压输出";//---------------------------------------------------------------3
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "电流输出";//--------------------------------------------------------------4
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "温度下限";//-------------------------------------------------------------5
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "温度上限";//-------------------------------------------------------------6
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "热源";//------------------------------------------------------------7
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "目标温度";//-------------------------------------------------------------8
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            tmp.columnName = "持续时间(分)";//--------------------------------------------------------------9
            tmp.columnType = Type.GetType("System.String");
            columnCollection.Add(tmp);

            return columnCollection;
        }

        private void chk_TopMost_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = chk_TopMost.Checked;
        }
    }
}
