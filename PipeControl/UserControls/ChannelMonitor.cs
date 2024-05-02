using DataObjects;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PipeControl
{
    public partial class ChannelMonitor : UserControl
    {
        private Channel myChannel;
        internal event EventHandler OnSmall;
        internal string ChannelName { get; set; }
        internal bool InCurve { get; set; }
        private bool IsMouseHover;
        public ChannelMonitor()
        {
            InitializeComponent();
            DoubleBuffered = true;
            Resize += ChannelMonitor_Resize;

            lbl_ChannelValue.MouseHover += lbl_ChannelValue_MouseHover;
            lbl_ChannelValue.MouseMove += lbl_ChannelValue_MouseMove;
            lbl_ChannelValue.MouseLeave += lbl_ChannelValue_MouseLeave;

            lbl_ChannelName.MouseHover += lbl_ChannelName_MouseHover;
            lbl_ChannelName.MouseMove += lbl_ChannelName_MouseMove;
            lbl_ChannelName.MouseLeave += lbl_ChannelName_MouseLeave;
        }

        private void lbl_ChannelName_MouseHover(object sender, EventArgs e)
        {
            BackColor = Color.AliceBlue;
            IsMouseHover = true;
        }

        private void lbl_ChannelName_MouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.White;
            IsMouseHover = false;
        }

        private void lbl_ChannelName_MouseMove(object sender, MouseEventArgs e)
        {
            BackColor = Color.AliceBlue;
            IsMouseHover = true;
        }

        private void lbl_ChannelValue_MouseHover(object sender, EventArgs e)
        {
            BackColor = Color.AliceBlue;
            IsMouseHover = true;
        }

        private void lbl_ChannelValue_MouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.White;
            IsMouseHover = false;
        }

        private void lbl_ChannelValue_MouseMove(object sender, MouseEventArgs e)
        {
            BackColor = Color.AliceBlue;
            IsMouseHover = true;
        }
        protected override void OnContextMenuChanged(EventArgs e)
        {
            BackColor = Color.AliceBlue;
            IsMouseHover = true;
            base.OnContextMenuChanged(e);
        }
        protected override void OnMouseHover(EventArgs e)
        {
            BackColor = Color.AliceBlue;
            IsMouseHover = true;
            base.OnMouseHover(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            BackColor = Color.AliceBlue;
            IsMouseHover = true;
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            BackColor = Color.White;
            IsMouseHover = false;
            base.OnMouseLeave(e);
        }
        private void ResizeValueFont()
        {
            try
            {
                if (lbl_ChannelValue.Height > 2 | lbl_ChannelValue.Width > 3)
                {
                    if (lbl_ChannelValue.Width / 288F <= lbl_ChannelValue.Height / 74F)
                    {
                        lbl_ChannelValue.Font = new Font("微软雅黑", lbl_ChannelValue.Width / 288F * 31F);
                    }
                    else
                    {
                        lbl_ChannelValue.Font = new Font("微软雅黑", lbl_ChannelValue.Height / 74F * 31F);
                    }
                }
                else
                {
                    object sender = new object();
                    EventArgs e = new EventArgs();
                    OnSmall(sender, e);
                }
            }
            catch { }
        }
        private void ChannelMonitor_Resize(object sender, EventArgs e)
        {
            ResizeValueFont();
        }
        /// <summary>
        /// 设置要显示的测点
        /// </summary>
        /// <param name="channel">指定的测点</param>
        public void SetChannel(Channel channel)
        {
            myChannel = channel;
            ChannelName = channel.UserName;
            if (string.IsNullOrEmpty(myChannel.YUnit))
            {
                lbl_ChannelName.Text = myChannel.UserName + "：";
            }
            else
            {
                lbl_ChannelName.Text = myChannel.UserName + "(" + myChannel.YUnit + ")：";
            }
            //if (myChannel.Function == ChannelFunction.热源)
            //{
            //    lbl_ChannelName.Text = myChannel.UserName + "(A)：";
            //    lbl_ChannelValue.Text = myChannel.CurrentI.ToString("0.000");
            //}
            //else
            //{

            //    lbl_ChannelValue.Text = myChannel.LastValue.ToString("0.000");
            //}

        }
        /// <summary>
        /// 刷新测点显示
        /// </summary>
        public void RefreshData()
        {
            try
            { BeginInvoke(new deleRefreshData(RefreshDataNow)); }
            catch { }
        }
        private delegate void deleRefreshData();
        private void RefreshDataNow()
        {
            if (myChannel == null)
            { return; }
            if (myChannel.Function == ChannelFunction.热源)
            {
                lbl_ChannelValue.Text = myChannel.CurrentI.ToString("0.000");
            }
            else
            {
                lbl_ChannelValue.Text = myChannel.LastValue.ToString("0.000");
            }

            if (myChannel.Alert)
            {
                BackColor = Color.Red;
            }
            else
            {
                if (IsMouseHover)
                {
                    BackColor = Color.AliceBlue;
                }
                else
                {
                    BackColor = Color.White;
                }
            }
        }
        private Color myColor;
        internal Color Color
        {
            get => myColor;
            set
            {
                myColor = value;
                lbl_ChannelName.ForeColor = myColor;
            }
        }

    }
}
