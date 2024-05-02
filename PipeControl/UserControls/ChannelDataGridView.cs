using DataObjects;
using PipeControl.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PipeControl
{
    public class ChannelDataGridView : CustomDataGridView<Channel>
    {
        /// <summary>
        /// 显示模式。正常：0；只显示热源测点：1；显示除热源外的测点：2
        /// </summary>
        public ushort ShowMode { get; set; } = 0;
        public List<Channel> Channels { get; private set; } = new();
        public List<Channel> SelectedChannels { get; private set; } = new(20);
        private int firstRowIndex;
        public ChannelDataGridView(List<ColumnStruct> columns, Color back, Color selectBack) : base(columns, back, selectBack)
        {
            ColumnWidthFile = Application.StartupPath + "\\ChannelDataGridViewColumnWidth.json";
            ColumnShownMenuFile = Application.StartupPath + "\\ChannelDataGridViewMenuFile.dat";
            SelectionChanged += ChannelDataGridView_SelectionChanged;
            Scroll += ChannelDataGridView_Scroll;
        }
        private void ChannelDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            ChangeRowHeight();
            firstRowIndex = e.NewValue - 1;
        }
        private void ChannelDataGridView_SelectionChanged(object sender, EventArgs e)
        {

            SelectedChannels.Clear();
            if (myDataTable.Rows.Count == 0)
                return;
            List<Channel> tmp = new(20);
            foreach (DataGridViewRow row in SelectedRows)
            {
                int id = int.Parse(row.Cells[0].Value.ToString());
                foreach (Channel channel in Channels)
                {
                    if (channel.ID == id)
                    {
                        tmp.Add(channel);
                        break;
                    }
                }
            }
            if (tmp.Count == 0)
            {
                return;
            }
            SelectedChannels = tmp.OrderBy(x => x.ID).ToList();
        }
        public override void ShowRows(List<Channel> objList)
        {
            if (objList.Count == 0)
            {
                myDataTable.BeginInit();
                myDataTable.Rows.Clear();
                myDataTable.EndInit();
                return;
            }
            Channels = objList;
            int oldFirst = firstRowIndex;

            myDataTable.Rows.Clear();
            myDataTable.BeginLoadData();
            foreach (Channel t in objList)
            {
                ShowRow(t);
            }
            myDataTable.EndLoadData();
            ChangeRowHeight();
            if (Rows.Count < oldFirst)
            {
                FirstDisplayedScrollingRowIndex = 0;
                firstRowIndex = 0;
            }
            else
            {
                if (oldFirst <= 1)
                {
                    FirstDisplayedScrollingRowIndex = 0;
                }
                else
                {
                    try
                    {
                        FirstDisplayedScrollingRowIndex = oldFirst;
                    }
                    catch
                    {
                        FirstDisplayedScrollingRowIndex = 0;
                    }
                }
                firstRowIndex = oldFirst;
            }
            BeginInvoke(new MethodInvoker(delegate ()
            {
                Height += 1;
                Height -= 1;
            }));
        }
        public override void ShowRow(Channel k)
        {
            DataRow tmpRow = myDataTable.NewRow();
            tmpRow[0] = k.ID.ToString();
            tmpRow[1] = k.UserName.ToString();
            tmpRow[2] = k.Function.ToString();
            tmpRow[3] = k.DeviceType.ToString();
            tmpRow[4] = k.HardwareDeviceAddress.ToString();
            tmpRow[5] = k.HardwareChannelAddress.ToString();
            tmpRow[6] = k.DeviceModel.ToString();
            tmpRow[7] = k.GetRangeDescription();
            tmpRow[8] = k.TransducerType.ToString();
            tmpRow[9] = k.ColdJunc.ToString();
            tmpRow[10] = k.AlertMax.ToString();
            tmpRow[11] = k.AlertMin.ToString();
            tmpRow[12] = k.YUnit.ToString();
            tmpRow[13] = k.ShowDecimal.ToString();
            tmpRow[14] = k.Imax.ToString();
            tmpRow[15] = k.Vmax.ToString();
            tmpRow[16] = k.RH.ToString();
            tmpRow[17] = k.RL.ToString();
            tmpRow[18] = k.ControlType.ToString();
            tmpRow[19] = k.TempMin.ToString();
            tmpRow[20] = k.TempMax.ToString();
            tmpRow[21] = k.VOut.ToString();
            tmpRow[22] = k.IOut.ToString();
            tmpRow[23] = k.TargetTemp.ToString();
            tmpRow[24] = k.XDelta.ToString();
            tmpRow[25] = k.Enabled.ToString();
            tmpRow[26] = k.Status.ToString();
            tmpRow[27] = k.LastValue.ToString("0.000");
            tmpRow[28] = k.CurrentV.ToString("0.000");
            tmpRow[29] = k.CurrentI.ToString("0.000");
            tmpRow[30] = k.MaxValue.ToString("0.000");
            tmpRow[31] = k.MinValue.ToString("0.000");
            tmpRow[32] = k.AverageValue.ToString("0.000");
            myDataTable.Rows.Add(tmpRow);
        }
        public void ChangeConfigs(List<Channel> channels)
        {
            Invoke(new MethodInvoker(delegate
            {
                foreach (Channel channel in channels)
                {
                    ChangeConfig(channel);
                }
            }));
        }
        public void ChangeConfig(Channel k)
        {
            foreach (DataRow tmpRow in myDataTable.Rows)
            {
                if (tmpRow[0].ToString() == k.ID.ToString())
                {
                    tmpRow[2] = k.Function.ToString();
                    tmpRow[3] = k.DeviceType.ToString();
                    tmpRow[4] = k.HardwareDeviceAddress.ToString();
                    tmpRow[5] = k.HardwareChannelAddress.ToString();
                    tmpRow[6] = k.DeviceModel.ToString();
                    tmpRow[7] = k.GetRangeDescription();
                    tmpRow[8] = k.TransducerType.ToString();
                    tmpRow[9] = k.ColdJunc.ToString();
                    tmpRow[10] = k.AlertMax.ToString();
                    tmpRow[11] = k.AlertMin.ToString();
                    tmpRow[12] = k.YUnit.ToString();
                    tmpRow[13] = k.ShowDecimal.ToString();
                    tmpRow[14] = k.Imax.ToString();
                    tmpRow[15] = k.Vmax.ToString();
                    tmpRow[16] = k.RH.ToString();
                    tmpRow[17] = k.RL.ToString();
                    tmpRow[18] = k.ControlType.ToString();
                    tmpRow[19] = k.TempMin.ToString();
                    tmpRow[20] = k.TempMax.ToString();
                    tmpRow[21] = k.VOut.ToString();
                    tmpRow[22] = k.IOut.ToString();
                    tmpRow[23] = k.TargetTemp.ToString();
                    tmpRow[24] = k.XDelta.ToString();
                    break;
                }
            }
        }
        public override void ChangeValues(List<Channel> obj)
        {
            if (obj.Count == 0)
                return;
            Invoke(new MethodInvoker(delegate
            {
                for (int i = 0; i <= Rows.Count - 1; i++)
                {
                    foreach (Channel channel in obj)
                    {
                        if (Rows[i].Cells[0].Value.ToString() == channel.ID.ToString())
                        {
                            ChangeValue(channel);
                        }
                    }
                }
            }));
        }
        public override void ChangeValue(Channel k)
        {
            foreach (DataRow tmpRow in myDataTable.Rows)
            {
                if (tmpRow[0].ToString() == k.ID.ToString())
                {
                    tmpRow[25] = k.Enabled.ToString();
                    tmpRow[26] = k.Status.ToString();
                    tmpRow[27] = k.LastValue.ToString("0.000");
                    tmpRow[28] = k.CurrentV.ToString("0.000");
                    tmpRow[29] = k.CurrentI.ToString("0.000");
                    tmpRow[30] = k.MaxValue.ToString("0.000");
                    tmpRow[31] = k.MinValue.ToString("0.000");
                    tmpRow[32] = k.AverageValue.ToString("0.000");
                    break;
                }
            }
        }
        public void SetShowMode(ushort mode)
        {
            ShowMode = mode;
            if (Channels.Count == 0)
                return;
            List<Channel> channels = new();
            switch (ShowMode)
            {
                case 0:
                    ShowRowsPrivate(Channels);
                    break;
                case 1:
                    channels = Channels.Where(x => x.Function == ChannelFunction.热源).ToList();
                    ShowRowsPrivate(channels);
                    break;
                case 2:
                    channels = Channels.Where(x => x.Function != ChannelFunction.热源).ToList();
                    ShowRowsPrivate(channels);
                    break;
            }
        }
        private void ShowRowsPrivate(List<Channel> objList)
        {
            if (objList.Count == 0)
            {
                myDataTable.Rows.Clear();
                return;
            }
            int oldFirst = firstRowIndex;
            myDataTable.BeginLoadData();
            myDataTable.Rows.Clear();
            foreach (Channel t in objList)
            {
                ShowRow(t);
            }
            myDataTable.EndLoadData();
            ChangeRowHeight();
            if (Rows.Count < oldFirst)
            {
                FirstDisplayedScrollingRowIndex = 0;
                firstRowIndex = 0;
            }
            else
            {
                if (oldFirst <= 1)
                {
                    FirstDisplayedScrollingRowIndex = 0;
                }
                else
                {
                    try
                    {
                        FirstDisplayedScrollingRowIndex = oldFirst;
                    }
                    catch
                    {
                        FirstDisplayedScrollingRowIndex = 0;
                    }
                }
                firstRowIndex = oldFirst;
            }
            BeginInvoke(new MethodInvoker(delegate ()
            {
                Height += 1;
                Height -= 1;
            }));
        }
    }
}
