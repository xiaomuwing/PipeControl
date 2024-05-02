
namespace InstrumentsCtrl
{
    partial class frm_Config
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
            this.components = new System.ComponentModel.Container();
            this.chk_OpenOrClose = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_Output = new System.Windows.Forms.Button();
            this.txt_VoltValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_CurrentValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Info = new System.Windows.Forms.Label();
            this.chk_TopMost = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chk_OpenOrClose
            // 
            this.chk_OpenOrClose.AutoSize = true;
            this.chk_OpenOrClose.Enabled = false;
            this.chk_OpenOrClose.Location = new System.Drawing.Point(16, 48);
            this.chk_OpenOrClose.Name = "chk_OpenOrClose";
            this.chk_OpenOrClose.Size = new System.Drawing.Size(112, 24);
            this.chk_OpenOrClose.TabIndex = 5;
            this.chk_OpenOrClose.Text = "打开电源输出";
            this.chk_OpenOrClose.UseVisualStyleBackColor = true;
            this.chk_OpenOrClose.CheckedChanged += new System.EventHandler(this.chk_OpenOrClose_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.btn_Output);
            this.groupBox1.Controls.Add(this.txt_VoltValue);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_CurrentValue);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(14, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(272, 182);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "输出控制";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(11, 141);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(132, 26);
            this.textBox1.TabIndex = 16;
            this.textBox1.Text = "0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_Output
            // 
            this.btn_Output.Location = new System.Drawing.Point(149, 133);
            this.btn_Output.Name = "btn_Output";
            this.btn_Output.Size = new System.Drawing.Size(117, 43);
            this.btn_Output.TabIndex = 15;
            this.btn_Output.Text = "输出电流和电压";
            this.btn_Output.UseVisualStyleBackColor = true;
            this.btn_Output.Click += new System.EventHandler(this.btn_Output_Click);
            // 
            // txt_VoltValue
            // 
            this.txt_VoltValue.Location = new System.Drawing.Point(111, 90);
            this.txt_VoltValue.Name = "txt_VoltValue";
            this.txt_VoltValue.Size = new System.Drawing.Size(132, 26);
            this.txt_VoltValue.TabIndex = 14;
            this.txt_VoltValue.Text = "0";
            this.txt_VoltValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "电压值(V)：";
            // 
            // txt_CurrentValue
            // 
            this.txt_CurrentValue.Location = new System.Drawing.Point(111, 35);
            this.txt_CurrentValue.Name = "txt_CurrentValue";
            this.txt_CurrentValue.Size = new System.Drawing.Size(132, 26);
            this.txt_CurrentValue.TabIndex = 2;
            this.txt_CurrentValue.Text = "0";
            this.txt_CurrentValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "电流值(A)：";
            // 
            // lbl_Info
            // 
            this.lbl_Info.AutoSize = true;
            this.lbl_Info.Location = new System.Drawing.Point(12, 14);
            this.lbl_Info.Name = "lbl_Info";
            this.lbl_Info.Size = new System.Drawing.Size(79, 20);
            this.lbl_Info.TabIndex = 6;
            this.lbl_Info.Text = "电源型号：";
            // 
            // chk_TopMost
            // 
            this.chk_TopMost.AutoSize = true;
            this.chk_TopMost.Checked = true;
            this.chk_TopMost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_TopMost.Location = new System.Drawing.Point(204, 12);
            this.chk_TopMost.Name = "chk_TopMost";
            this.chk_TopMost.Size = new System.Drawing.Size(84, 24);
            this.chk_TopMost.TabIndex = 10;
            this.chk_TopMost.Text = "总在最前";
            this.chk_TopMost.UseVisualStyleBackColor = true;
            this.chk_TopMost.CheckedChanged += new System.EventHandler(this.chk_TopMost_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frm_Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(298, 276);
            this.Controls.Add(this.chk_TopMost);
            this.Controls.Add(this.chk_OpenOrClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_Info);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(314, 316);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(314, 316);
            this.Name = "frm_Config";
            this.Text = "电源状态监控";
            this.TopMost = true;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox chk_OpenOrClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txt_CurrentValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Info;
        private System.Windows.Forms.Button btn_Output;
        private System.Windows.Forms.TextBox txt_VoltValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chk_TopMost;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox1;
    }
}