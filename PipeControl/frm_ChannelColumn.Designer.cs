namespace PipeControl
{
    partial class frm_ChannelColumn
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
            this.btn_Close = new System.Windows.Forms.Button();
            this.lvw = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.HotTrack;
            this.btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Font = new System.Drawing.Font("微软雅黑", 16F);
            this.btn_Close.ForeColor = System.Drawing.Color.Black;
            this.btn_Close.Location = new System.Drawing.Point(132, 472);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(112, 40);
            this.btn_Close.TabIndex = 66;
            this.btn_Close.TabStop = false;
            this.btn_Close.Text = "关闭";
            this.btn_Close.UseVisualStyleBackColor = true;
            // 
            // lvw
            // 
            this.lvw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvw.CheckBoxes = true;
            this.lvw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvw.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvw.FullRowSelect = true;
            this.lvw.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvw.HideSelection = false;
            this.lvw.Location = new System.Drawing.Point(22, 52);
            this.lvw.Margin = new System.Windows.Forms.Padding(2);
            this.lvw.Name = "lvw";
            this.lvw.Size = new System.Drawing.Size(222, 394);
            this.lvw.TabIndex = 65;
            this.lvw.UseCompatibleStateImageBehavior = false;
            this.lvw.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 330;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 16);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 20);
            this.label1.TabIndex = 64;
            this.label1.Text = "选择您想显示的测点详细信息：";
            // 
            // frm_ChannelColumn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(266, 524);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.lvw);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(282, 563);
            this.Name = "frm_ChannelColumn";
            this.Text = "选择测点列表的详细信息";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.ListView lvw;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Label label1;
    }
}