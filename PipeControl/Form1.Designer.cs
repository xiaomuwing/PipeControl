
namespace PipeControl
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.txt_Send = new System.Windows.Forms.TextBox();
            this.lb = new System.Windows.Forms.ListBox();
            this.lb_Data = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1393, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "send";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_Send
            // 
            this.txt_Send.Location = new System.Drawing.Point(12, 12);
            this.txt_Send.Name = "txt_Send";
            this.txt_Send.Size = new System.Drawing.Size(1375, 26);
            this.txt_Send.TabIndex = 1;
            // 
            // lb
            // 
            this.lb.FormattingEnabled = true;
            this.lb.IntegralHeight = false;
            this.lb.ItemHeight = 20;
            this.lb.Location = new System.Drawing.Point(12, 51);
            this.lb.Name = "lb";
            this.lb.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lb.Size = new System.Drawing.Size(1637, 239);
            this.lb.TabIndex = 2;
            // 
            // lb_Data
            // 
            this.lb_Data.FormattingEnabled = true;
            this.lb_Data.IntegralHeight = false;
            this.lb_Data.ItemHeight = 20;
            this.lb_Data.Location = new System.Drawing.Point(12, 294);
            this.lb_Data.Name = "lb_Data";
            this.lb_Data.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lb_Data.Size = new System.Drawing.Size(1637, 520);
            this.lb_Data.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1661, 826);
            this.Controls.Add(this.lb_Data);
            this.Controls.Add(this.lb);
            this.Controls.Add(this.txt_Send);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_Send;
        private System.Windows.Forms.ListBox lb;
        private System.Windows.Forms.ListBox lb_Data;
    }
}

