using System.Windows.Forms;

namespace PipeControl
{
    public class DataList : DataGridView
    {
        public DataList()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new ();
            DoubleBuffered = true;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BackgroundColor = System.Drawing.Color.White;
            BorderStyle = BorderStyle.None;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            DefaultCellStyle = dataGridViewCellStyle1;
            EditMode = DataGridViewEditMode.EditOnEnter;
            GridColor = System.Drawing.Color.Gray;
            ImeMode = ImeMode.Alpha;
            Location = new System.Drawing.Point(0, 0);
            Margin = new Padding(0);
            RowHeadersVisible = false;
            RowTemplate.Height = 23;
            ScrollBars = ScrollBars.Vertical;
        }
    }
}
