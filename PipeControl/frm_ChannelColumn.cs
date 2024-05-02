using System;
using System.Windows.Forms;

namespace PipeControl
{
    public partial class frm_ChannelColumn : Form
    {
        ChannelDataGridView channelDataGridView;
        public frm_ChannelColumn(ChannelDataGridView cdgv)
        {
            InitializeComponent();
            channelDataGridView = cdgv;
            Load += Frm_ChannelColumn_Load;
            lvw.ItemChecked += lvw_ItemChecked;
            btn_Close.Click += Btn_Close_Click;
        }
        private void Frm_ChannelColumn_Load(object sender, EventArgs e)
        {
            lvw.Columns[0].Width = lvw.Width - 27;
            foreach (DataGridViewColumn column in channelDataGridView.Columns)
            {
                ListViewItem itemX = new ListViewItem(column.HeaderText);
                itemX.Checked = channelDataGridView.ShownInColumns(column.HeaderText);
                lvw.Items.Add(itemX);
            }
        }
        private void lvw_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Text == "序号")
            {
                e.Item.Checked = true;
            }
            if (e.Item.Text == "测点名称")
            {
                e.Item.Checked = true;
            }
            if (e.Item.Checked)
            {
                channelDataGridView.ShowColumn(e.Item.Text);
            }
            else
            {
                channelDataGridView.HideColumn(e.Item.Text);
            }
            channelDataGridView.SaveColumn();
        }
        private void Btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
