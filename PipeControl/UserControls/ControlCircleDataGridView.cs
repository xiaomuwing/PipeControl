using DataObjects;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PipeControl
{
    public class ControlCircleDataGridView : CustomDataGridView<ControlLine>
    {
        public List<ControlLine> Channels { get; private set; } = new();
        private int firstRowIndex;
        public ControlCircleDataGridView(List<ColumnStruct> columns, Color back, Color selectBack) : base(columns, back, selectBack)
        {
            ColumnWidthFile = Application.StartupPath + "\\ControlCircleDataGridViewColumnWidth.json";
            ColumnShownMenuFile = Application.StartupPath + "\\ControlCircleDataGridViewMenuFile.dat";
            Scroll += ChannelDataGridView_Scroll;
        }
        private void ChannelDataGridView_Scroll(object sender, ScrollEventArgs e)
        {
            ChangeRowHeight();
            firstRowIndex = e.NewValue - 1;
        }
        public override void ShowRows(List<ControlLine> objList)
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
            foreach (var t in objList)
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
        public override void ShowRow(ControlLine k)
        {
            DataRow tmpRow = myDataTable.NewRow();
            tmpRow[0] = k.Parent.ID.ToString();
            tmpRow[1] = k.TempChannelSN;
            tmpRow[2] = k.ControlType.ToString();
            switch (k.ControlType)
            {
                case ControlType.PID:
                    tmpRow[3] = "";
                    tmpRow[4] = "";
                    tmpRow[5] = "";
                    tmpRow[6] = "";
                    break;
                case ControlType.开关量:
                    tmpRow[3] = k.VOut.ToString();
                    tmpRow[4] = k.IOut.ToString();
                    tmpRow[5] = k.TempMin.ToString();
                    tmpRow[6] = k.TempMax.ToString();
                    break;
            }

            string dcs = string.Empty;
            foreach (var dc in k.DCChannelSNs)
            {
                dcs += dc + ", ";
            }
            dcs = dcs.Substring(0, dcs.Length - 2);
            tmpRow[7] = dcs;
            switch (k.ControlType)
            {
                case ControlType.PID:
                    tmpRow[8] = k.TargetTemp.ToString();
                    break;
                case ControlType.开关量:
                    tmpRow[8] = "";
                    break;
            }
            
            tmpRow[9] = k.Parent.LastTime.TotalMinutes.ToString();
            myDataTable.Rows.Add(tmpRow);
        }
        public void ChangeConfigs(List<ControlLine> channels)
        {
            Invoke(new MethodInvoker(delegate
            {
                foreach (var channel in channels)
                {
                    ChangeConfig(channel);
                }
            }));
        }
        public void ChangeConfig(ControlLine k)
        {
            //foreach (DataRow tmpRow in myDataTable.Rows)
            //{
            //    if (tmpRow[0].ToString() == k.ID.ToString())
            //    {
            //        tmpRow[2] = k.Function.ToString();
            //        tmpRow[3] = k.DeviceType.ToString();
            //        tmpRow[4] = k.HardwareDeviceAddress.ToString();
            //        tmpRow[5] = k.HardwareChannelAddress.ToString();
            //        tmpRow[6] = k.DeviceModel.ToString();
            //        tmpRow[7] = k.GetRangeDescription();
            //        tmpRow[8] = k.TransducerType.ToString();
            //        tmpRow[9] = k.ColdJunc.ToString();
            //        tmpRow[10] = k.AlertMax.ToString();
            //        tmpRow[11] = k.AlertMin.ToString();
            //        tmpRow[12] = k.YUnit.ToString();
            //        tmpRow[13] = k.ShowDecimal.ToString();
            //        tmpRow[14] = k.Imax.ToString();
            //        tmpRow[15] = k.Vmax.ToString();
            //        tmpRow[16] = k.RH.ToString();
            //        tmpRow[17] = k.RL.ToString();
            //        tmpRow[18] = k.ControlType.ToString();
            //        tmpRow[19] = k.TempMin.ToString();
            //        tmpRow[20] = k.TempMax.ToString();
            //        tmpRow[21] = k.VOut.ToString();
            //        tmpRow[22] = k.IOut.ToString();
            //        tmpRow[23] = k.TargetTemp.ToString();
            //        tmpRow[24] = k.XDelta.ToString();
            //        break;
            //    }
            //}
        }
        public override void ChangeValues(List<ControlLine> obj)
        {
            //if (obj.Count == 0)
            //    return;
            //Invoke(new MethodInvoker(delegate
            //{
            //    for (int i = 0; i <= Rows.Count - 1; i++)
            //    {
            //        foreach (var channel in obj)
            //        {
            //            if (Rows[i].Cells[0].Value.ToString() == channel.ID.ToString())
            //            {
            //                ChangeValue(channel);
            //            }
            //        }
            //    }
            //}));
        }
        public override void ChangeValue(ControlLine k)
        {
            //foreach (DataRow tmpRow in myDataTable.Rows)
            //{
            //    if (tmpRow[0].ToString() == k.ID.ToString())
            //    {
            //        tmpRow[25] = k.Enabled.ToString();
            //        tmpRow[26] = k.Status.ToString();
            //        tmpRow[27] = k.LastValue.ToString("0.000");
            //        tmpRow[28] = k.CurrentV.ToString("0.000");
            //        tmpRow[29] = k.CurrentI.ToString("0.000");
            //        tmpRow[30] = k.MaxValue.ToString("0.000");
            //        tmpRow[31] = k.MinValue.ToString("0.000");
            //        tmpRow[32] = k.AverageValue.ToString("0.000");
            //        break;
            //    }
            //}
        }
    }
}
