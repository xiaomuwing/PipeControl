namespace PipeControl
{
    partial class ChannelMonitor
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_ChannelValue = new System.Windows.Forms.Label();
            this.lbl_ChannelName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_ChannelValue
            // 
            this.lbl_ChannelValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ChannelValue.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ChannelValue.Font = new System.Drawing.Font("微软雅黑", 34F);
            this.lbl_ChannelValue.ForeColor = System.Drawing.Color.Black;
            this.lbl_ChannelValue.Location = new System.Drawing.Point(0, 21);
            this.lbl_ChannelValue.Name = "lbl_ChannelValue";
            this.lbl_ChannelValue.Size = new System.Drawing.Size(284, 70);
            this.lbl_ChannelValue.TabIndex = 5;
            this.lbl_ChannelValue.Text = "12345.6789";
            this.lbl_ChannelValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ChannelName
            // 
            this.lbl_ChannelName.AutoSize = true;
            this.lbl_ChannelName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_ChannelName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_ChannelName.ForeColor = System.Drawing.Color.Black;
            this.lbl_ChannelName.Location = new System.Drawing.Point(2, 1);
            this.lbl_ChannelName.Name = "lbl_ChannelName";
            this.lbl_ChannelName.Size = new System.Drawing.Size(74, 21);
            this.lbl_ChannelName.TabIndex = 4;
            this.lbl_ChannelName.Text = "测点名称";
            // 
            // ChannelMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbl_ChannelValue);
            this.Controls.Add(this.lbl_ChannelName);
            this.DoubleBuffered = true;
            this.Name = "ChannelMonitor";
            this.Size = new System.Drawing.Size(284, 93);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_ChannelValue;
        private System.Windows.Forms.Label lbl_ChannelName;
    }
}
