using System.Drawing;
using System.Windows.Forms;

namespace PipeControl
{
    public class CustomTree : TreeView
    {
        private void EnableDoubleBuffering()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
        public CustomTree()
        {
            EnableDoubleBuffering();
            base.BackColor = Color.FromArgb(24, 37, 45);
            base.ForeColor = Color.White;
            base.Font = new Font("微软雅黑", 10.5F);
            base.AllowDrop = true;
            base.DoubleBuffered = true;
            base.FullRowSelect = true;
            base.HideSelection = false;
            base.LineColor = Color.White;
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) // 禁掉清除背景消息
                return;
            base.WndProc(ref m);
        }
    }
}
