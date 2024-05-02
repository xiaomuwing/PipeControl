
namespace PipeControl
{
    partial class frm_ControlCircle
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.spc_Main = new System.Windows.Forms.SplitContainer();
            this.lbl_ControlInfo = new System.Windows.Forms.Label();
            this.chk_TopMost = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Main)).BeginInit();
            this.spc_Main.Panel1.SuspendLayout();
            this.spc_Main.SuspendLayout();
            this.SuspendLayout();
            // 
            // spc_Main
            // 
            this.spc_Main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(71)))), ((int)(((byte)(81)))));
            this.spc_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spc_Main.IsSplitterFixed = true;
            this.spc_Main.Location = new System.Drawing.Point(0, 0);
            this.spc_Main.Name = "spc_Main";
            this.spc_Main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spc_Main.Panel1
            // 
            this.spc_Main.Panel1.BackColor = System.Drawing.Color.Black;
            this.spc_Main.Panel1.Controls.Add(this.lbl_ControlInfo);
            this.spc_Main.Panel1.Controls.Add(this.chk_TopMost);
            this.spc_Main.Size = new System.Drawing.Size(864, 272);
            this.spc_Main.SplitterDistance = 32;
            this.spc_Main.SplitterWidth = 1;
            this.spc_Main.TabIndex = 0;
            // 
            // lbl_ControlInfo
            // 
            this.lbl_ControlInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_ControlInfo.BackColor = System.Drawing.Color.Black;
            this.lbl_ControlInfo.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.lbl_ControlInfo.ForeColor = System.Drawing.Color.White;
            this.lbl_ControlInfo.Location = new System.Drawing.Point(3, 0);
            this.lbl_ControlInfo.Name = "lbl_ControlInfo";
            this.lbl_ControlInfo.Size = new System.Drawing.Size(743, 32);
            this.lbl_ControlInfo.TabIndex = 97;
            this.lbl_ControlInfo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.lbl_ControlInfo.Visible = false;
            // 
            // chk_TopMost
            // 
            this.chk_TopMost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chk_TopMost.AutoSize = true;
            this.chk_TopMost.Checked = true;
            this.chk_TopMost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_TopMost.ForeColor = System.Drawing.Color.White;
            this.chk_TopMost.Location = new System.Drawing.Point(777, 4);
            this.chk_TopMost.Name = "chk_TopMost";
            this.chk_TopMost.Size = new System.Drawing.Size(84, 24);
            this.chk_TopMost.TabIndex = 11;
            this.chk_TopMost.Text = "总在最前";
            this.chk_TopMost.UseVisualStyleBackColor = true;
            this.chk_TopMost.CheckedChanged += new System.EventHandler(this.chk_TopMost_CheckedChanged);
            // 
            // frm_ControlCircle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(864, 272);
            this.Controls.Add(this.spc_Main);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(880, 312);
            this.Name = "frm_ControlCircle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "执行过程列表";
            this.TopMost = true;
            this.spc_Main.Panel1.ResumeLayout(false);
            this.spc_Main.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Main)).EndInit();
            this.spc_Main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spc_Main;
        private System.Windows.Forms.CheckBox chk_TopMost;
        private System.Windows.Forms.Label lbl_ControlInfo;
    }
}