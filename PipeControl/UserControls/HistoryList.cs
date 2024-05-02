using ImcCoreLib;
using ImcFamosLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace PipeControl
{
    public class HistoryList : DataGridView
    {
        private int length;
        private double xdelta;
        private readonly Famos myfamos;
        public DTime BeginTime { get; set; }
        public DTime EndTime { get; set; }
        private int first;
        private readonly List<DChannel> rChannels;
        public void ShowData(IList<DChannel> Channels)
        {
            if (Channels.Count == 0)
                return;
            xdelta = Channels[0].xDelta;
            length = (int)((EndTime.Value - BeginTime.Value) / xdelta);
            if (Channels[0].Length < length)
            {
                length = Channels[0].Length;
            }
            first = (int)BeginTime.Value - (int)Channels[0].TriggerTime.Value + 1;
            if (first < 1)
                first = 1;
            base.Columns.Clear();
            base.ColumnCount = Channels.Count + 2;
            base.Columns[0].HeaderText = "编号";
            base.Columns[0].Name = "Sn";
            base.Columns[0].Width = 80;
            base.Columns[0].Frozen = true;
            base.Columns[1].HeaderText = "日期";
            base.Columns[1].Width = 180;
            base.Columns[1].Frozen = true;
            for (int i = 2; i < Channels.Count + 2; i++)
            {
                base.Columns[i].HeaderText = Channels[i - 2].Name;
            }
            rChannels.Clear();
            int indexEnd = first + length;
            if (indexEnd > Channels[0].Length)
                indexEnd = Channels[0].Length;
            foreach (DChannel channel in Channels)
            {
                DChannel tmp = myfamos.Edit.CopyPartI(channel, first, indexEnd);
                tmp.TriggerTime.Value = BeginTime.Value + 1;
                tmp.xDelta = channel.xDelta;
                tmp.Name = channel.Name;
                tmp.Comment = channel.Comment;
                tmp.zUnit = channel.zUnit;
                rChannels.Add(tmp);
            }
            base.RowCount = length + 3;

        }
        public HistoryList()
        {
            base.VirtualMode = true;
            base.AllowUserToAddRows = false;
            base.AllowUserToDeleteRows = false;
            base.AllowUserToOrderColumns = true;
            base.AllowUserToResizeColumns = true;
            base.AllowUserToResizeRows = false;
            base.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.Fill);
            base.BackgroundColor = Color.White;
            base.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            base.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            base.EditMode = DataGridViewEditMode.EditProgrammatically;
            base.EnableHeadersVisualStyles = false;
            base.GridColor = Color.DarkGray;
            base.ReadOnly = true;
            base.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            base.RowHeadersVisible = false;
            base.RowTemplate.Height = 23;
            base.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            base.BackgroundColor = Color.Gray;
            base.BorderStyle = BorderStyle.None;
            base.DoubleBuffered = true;
            base.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            DataGridViewCellStyle DataGridViewCellStyle1;
            DataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle DataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewCellStyle1.BackColor = Color.Gray;
            DataGridViewCellStyle1.Font = new Font("微软雅黑", 10.5F);
            DataGridViewCellStyle1.ForeColor = Color.White;
            DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            DataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            base.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1;
            base.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            DataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            DataGridViewCellStyle2.Font = new Font("微软雅黑", 10.5F);
            DataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            base.Font = DataGridViewCellStyle2.Font;
            base.DefaultCellStyle = DataGridViewCellStyle2;

            base.CellValueNeeded += HistoryList_CellValueNeeded;
            CellFormatting += HistoryList_CellFormatting;
            myfamos = new ImcFamosLib.Famos();
            rChannels = new List<DChannel>();
        }

        private void HistoryList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //int cnt = 2;
            //foreach (DChannel channel in rChannels)
            //{
            //    string comment = channel.Comment;
            //    if (e.ColumnIndex == cnt && e.Value.ToString() == myfamos.Stat.Max(channel).Value.ToString(comment))
            //    {
            //        e.CellStyle.BackColor = Color.Red;
            //    }
            //    if (e.ColumnIndex == cnt && e.Value.ToString() == myfamos.Stat.Min(channel).Value.ToString(comment))
            //    {
            //        e.CellStyle.BackColor = Color.Green;
            //    }
            //    cnt += 1;
            //}
        }

        void HistoryList_CellValueNeeded(object sender, DataGridViewCellValueEventArgs e)
        {
            //try
            //{
            #region 平均值
            if (e.RowIndex == length)
            {
                if (e.ColumnIndex == 0)
                {
                    e.Value = "平均值：";
                }
                if (e.ColumnIndex >= 2)
                {
                    double result = 0;

                    result = myfamos.Stat.Mean(rChannels[e.ColumnIndex - 2]).get_Value(1);


                    e.Value = result.ToString("0.000");

                }
            }
            #endregion
            #region 标准差
            if (e.RowIndex == length + 1)
            {
                if (e.ColumnIndex == 0)
                {
                    e.Value = "标准差：";
                }
                if (e.ColumnIndex >= 2)
                {

                    e.Value = myfamos.Stat.StDev(rChannels[e.ColumnIndex - 2]).get_Value(1).ToString("0.000");

                }
            }
            #endregion
            #region 变异系数
            if (e.RowIndex == length + 2)
            {
                if (e.ColumnIndex == 0)
                {
                    e.Value = "变异系数：";
                }
                if (e.ColumnIndex >= 2)
                {

                    e.Value = (myfamos.Stat.StDev(rChannels[e.ColumnIndex - 2]).get_Value(1) / myfamos.Stat.Mean(rChannels[e.ColumnIndex - 2]).get_Value(1) * 100).ToString("0.000") + "%";
                }
            }
            #endregion
            #region 显示数值
            if (e.RowIndex < length)
            {
                if (e.ColumnIndex == 0)
                {
                    e.Value = e.RowIndex + 1;
                }
                if (e.ColumnIndex == 1)
                {
                    DTime tmp = new DTime();
                    tmp.Value = BeginTime.Value + xdelta * e.RowIndex;
                    DateTime date = BeginTime.AsDATE.AddMilliseconds(xdelta * 1000 * e.RowIndex);
                    e.Value = date.ToString("yyyy年MM月dd日 HH:mm:ss");
                }
                if (e.ColumnIndex >= 2)
                {
                    string comment = "0.000";
                    try
                    {
                        if (e.RowIndex + 1 <= rChannels[e.ColumnIndex - 2].Length)
                        { e.Value = rChannels[e.ColumnIndex - 2].get_Value(e.RowIndex + 1).ToString(comment); }
                        else
                        { e.Value = ""; }
                    }
                    catch
                    { }
                }
            }
            #endregion
            //}
            //catch { }
        }
    }
}
