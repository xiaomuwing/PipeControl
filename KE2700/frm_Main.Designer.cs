
namespace Keithley
{
    partial class frm_Main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.spc_Main = new System.Windows.Forms.SplitContainer();
            this.tv = new System.Windows.Forms.TreeView();
            this.spc_Right = new System.Windows.Forms.SplitContainer();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_ReadConfig = new System.Windows.Forms.Button();
            this.btn_Download = new System.Windows.Forms.Button();
            this.btn_SaveConfig = new System.Windows.Forms.Button();
            this.cmb_Thermocouple = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_Transducer = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_ChannelType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_ChannelAddress = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txt_InstrumentAddress = new System.Windows.Forms.TextBox();
            this.dgv_Channel = new System.Windows.Forms.DataGridView();
            this.chk_ColdJunc = new System.Windows.Forms.CheckBox();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Main)).BeginInit();
            this.spc_Main.Panel1.SuspendLayout();
            this.spc_Main.Panel2.SuspendLayout();
            this.spc_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Right)).BeginInit();
            this.spc_Right.Panel1.SuspendLayout();
            this.spc_Right.Panel2.SuspendLayout();
            this.spc_Right.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Channel)).BeginInit();
            this.SuspendLayout();
            // 
            // spc_Main
            // 
            this.spc_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spc_Main.IsSplitterFixed = true;
            this.spc_Main.Location = new System.Drawing.Point(0, 0);
            this.spc_Main.Name = "spc_Main";
            // 
            // spc_Main.Panel1
            // 
            this.spc_Main.Panel1.Controls.Add(this.tv);
            // 
            // spc_Main.Panel2
            // 
            this.spc_Main.Panel2.Controls.Add(this.spc_Right);
            this.spc_Main.Size = new System.Drawing.Size(1204, 601);
            this.spc_Main.SplitterDistance = 225;
            this.spc_Main.SplitterWidth = 1;
            this.spc_Main.TabIndex = 1;
            // 
            // tv
            // 
            this.tv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(86)))), ((int)(((byte)(168)))));
            this.tv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tv.ForeColor = System.Drawing.Color.White;
            this.tv.LineColor = System.Drawing.Color.Gainsboro;
            this.tv.Location = new System.Drawing.Point(0, 0);
            this.tv.Name = "tv";
            this.tv.Size = new System.Drawing.Size(225, 601);
            this.tv.TabIndex = 0;
            // 
            // spc_Right
            // 
            this.spc_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_Right.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spc_Right.ForeColor = System.Drawing.Color.White;
            this.spc_Right.IsSplitterFixed = true;
            this.spc_Right.Location = new System.Drawing.Point(0, 0);
            this.spc_Right.Name = "spc_Right";
            this.spc_Right.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spc_Right.Panel1
            // 
            this.spc_Right.Panel1.BackColor = System.Drawing.Color.Navy;
            this.spc_Right.Panel1.Controls.Add(this.chk_ColdJunc);
            this.spc_Right.Panel1.Controls.Add(this.btn_Save);
            this.spc_Right.Panel1.Controls.Add(this.btn_ReadConfig);
            this.spc_Right.Panel1.Controls.Add(this.btn_Download);
            this.spc_Right.Panel1.Controls.Add(this.btn_SaveConfig);
            this.spc_Right.Panel1.Controls.Add(this.cmb_Thermocouple);
            this.spc_Right.Panel1.Controls.Add(this.label4);
            this.spc_Right.Panel1.Controls.Add(this.cmb_Transducer);
            this.spc_Right.Panel1.Controls.Add(this.label3);
            this.spc_Right.Panel1.Controls.Add(this.cmb_ChannelType);
            this.spc_Right.Panel1.Controls.Add(this.label2);
            this.spc_Right.Panel1.Controls.Add(this.label1);
            this.spc_Right.Panel1.Controls.Add(this.txt_ChannelAddress);
            this.spc_Right.Panel1.Controls.Add(this.label20);
            this.spc_Right.Panel1.Controls.Add(this.txt_InstrumentAddress);
            // 
            // spc_Right.Panel2
            // 
            this.spc_Right.Panel2.BackColor = System.Drawing.Color.Navy;
            this.spc_Right.Panel2.Controls.Add(this.dgv_Channel);
            this.spc_Right.Size = new System.Drawing.Size(978, 601);
            this.spc_Right.SplitterDistance = 129;
            this.spc_Right.SplitterWidth = 2;
            this.spc_Right.TabIndex = 0;
            // 
            // btn_Save
            // 
            this.btn_Save.FlatAppearance.BorderSize = 0;
            this.btn_Save.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(96)))), ((int)(((byte)(62)))));
            this.btn_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Save.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btn_Save.ForeColor = System.Drawing.Color.White;
            this.btn_Save.Location = new System.Drawing.Point(717, 23);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(118, 37);
            this.btn_Save.TabIndex = 44;
            this.btn_Save.Text = "保存配置";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_ReadConfig
            // 
            this.btn_ReadConfig.FlatAppearance.BorderSize = 0;
            this.btn_ReadConfig.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(96)))), ((int)(((byte)(62)))));
            this.btn_ReadConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ReadConfig.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btn_ReadConfig.ForeColor = System.Drawing.Color.White;
            this.btn_ReadConfig.Location = new System.Drawing.Point(839, 23);
            this.btn_ReadConfig.Name = "btn_ReadConfig";
            this.btn_ReadConfig.Size = new System.Drawing.Size(118, 37);
            this.btn_ReadConfig.TabIndex = 43;
            this.btn_ReadConfig.Text = "读取配置";
            this.btn_ReadConfig.UseVisualStyleBackColor = true;
            this.btn_ReadConfig.Click += new System.EventHandler(this.btn_ReadConfig_Click);
            // 
            // btn_Download
            // 
            this.btn_Download.FlatAppearance.BorderSize = 0;
            this.btn_Download.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(96)))), ((int)(((byte)(62)))));
            this.btn_Download.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Download.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btn_Download.ForeColor = System.Drawing.Color.White;
            this.btn_Download.Location = new System.Drawing.Point(839, 83);
            this.btn_Download.Name = "btn_Download";
            this.btn_Download.Size = new System.Drawing.Size(118, 37);
            this.btn_Download.TabIndex = 42;
            this.btn_Download.Text = "下发";
            this.btn_Download.UseVisualStyleBackColor = true;
            this.btn_Download.Click += new System.EventHandler(this.btn_Download_Click);
            // 
            // btn_SaveConfig
            // 
            this.btn_SaveConfig.FlatAppearance.BorderSize = 0;
            this.btn_SaveConfig.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(96)))), ((int)(((byte)(62)))));
            this.btn_SaveConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SaveConfig.Font = new System.Drawing.Font("微软雅黑", 13F);
            this.btn_SaveConfig.ForeColor = System.Drawing.Color.White;
            this.btn_SaveConfig.Location = new System.Drawing.Point(717, 83);
            this.btn_SaveConfig.Name = "btn_SaveConfig";
            this.btn_SaveConfig.Size = new System.Drawing.Size(118, 37);
            this.btn_SaveConfig.TabIndex = 41;
            this.btn_SaveConfig.Text = "修改";
            this.btn_SaveConfig.UseVisualStyleBackColor = true;
            this.btn_SaveConfig.Click += new System.EventHandler(this.btn_SaveConfig_Click);
            // 
            // cmb_Thermocouple
            // 
            this.cmb_Thermocouple.BackColor = System.Drawing.Color.Navy;
            this.cmb_Thermocouple.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Thermocouple.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_Thermocouple.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Thermocouple.ForeColor = System.Drawing.Color.White;
            this.cmb_Thermocouple.FormattingEnabled = true;
            this.cmb_Thermocouple.Location = new System.Drawing.Point(288, 89);
            this.cmb_Thermocouple.Name = "cmb_Thermocouple";
            this.cmb_Thermocouple.Size = new System.Drawing.Size(101, 28);
            this.cmb_Thermocouple.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(285, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.TabIndex = 39;
            this.label4.Text = "热电偶";
            // 
            // cmb_Transducer
            // 
            this.cmb_Transducer.BackColor = System.Drawing.Color.Navy;
            this.cmb_Transducer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Transducer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_Transducer.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Transducer.ForeColor = System.Drawing.Color.White;
            this.cmb_Transducer.FormattingEnabled = true;
            this.cmb_Transducer.Location = new System.Drawing.Point(156, 89);
            this.cmb_Transducer.Name = "cmb_Transducer";
            this.cmb_Transducer.Size = new System.Drawing.Size(101, 28);
            this.cmb_Transducer.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(150, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.TabIndex = 37;
            this.label3.Text = "传感器";
            // 
            // cmb_ChannelType
            // 
            this.cmb_ChannelType.BackColor = System.Drawing.Color.Navy;
            this.cmb_ChannelType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_ChannelType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_ChannelType.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_ChannelType.ForeColor = System.Drawing.Color.White;
            this.cmb_ChannelType.FormattingEnabled = true;
            this.cmb_ChannelType.Location = new System.Drawing.Point(24, 89);
            this.cmb_ChannelType.Name = "cmb_ChannelType";
            this.cmb_ChannelType.Size = new System.Drawing.Size(101, 28);
            this.cmb_ChannelType.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(20, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 35;
            this.label2.Text = "测量方式";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(155, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 33;
            this.label1.Text = "测点地址";
            // 
            // txt_ChannelAddress
            // 
            this.txt_ChannelAddress.BackColor = System.Drawing.Color.Navy;
            this.txt_ChannelAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ChannelAddress.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txt_ChannelAddress.ForeColor = System.Drawing.Color.White;
            this.txt_ChannelAddress.Location = new System.Drawing.Point(157, 30);
            this.txt_ChannelAddress.Name = "txt_ChannelAddress";
            this.txt_ChannelAddress.ReadOnly = true;
            this.txt_ChannelAddress.Size = new System.Drawing.Size(97, 27);
            this.txt_ChannelAddress.TabIndex = 34;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(20, 8);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(69, 20);
            this.label20.TabIndex = 31;
            this.label20.Text = "设备地址";
            // 
            // txt_InstrumentAddress
            // 
            this.txt_InstrumentAddress.BackColor = System.Drawing.Color.Navy;
            this.txt_InstrumentAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_InstrumentAddress.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.txt_InstrumentAddress.ForeColor = System.Drawing.Color.White;
            this.txt_InstrumentAddress.Location = new System.Drawing.Point(22, 30);
            this.txt_InstrumentAddress.Name = "txt_InstrumentAddress";
            this.txt_InstrumentAddress.ReadOnly = true;
            this.txt_InstrumentAddress.Size = new System.Drawing.Size(106, 27);
            this.txt_InstrumentAddress.TabIndex = 32;
            // 
            // dgv_Channel
            // 
            this.dgv_Channel.AllowUserToAddRows = false;
            this.dgv_Channel.AllowUserToDeleteRows = false;
            this.dgv_Channel.AllowUserToResizeRows = false;
            this.dgv_Channel.BackgroundColor = System.Drawing.Color.Navy;
            this.dgv_Channel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_Channel.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_Channel.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_Channel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Channel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column6});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 11F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_Channel.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_Channel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Channel.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgv_Channel.EnableHeadersVisualStyles = false;
            this.dgv_Channel.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgv_Channel.Location = new System.Drawing.Point(0, 0);
            this.dgv_Channel.Name = "dgv_Channel";
            this.dgv_Channel.RowHeadersVisible = false;
            this.dgv_Channel.RowTemplate.Height = 23;
            this.dgv_Channel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Channel.ShowCellToolTips = false;
            this.dgv_Channel.Size = new System.Drawing.Size(978, 470);
            this.dgv_Channel.TabIndex = 0;
            // 
            // chk_ColdJunc
            // 
            this.chk_ColdJunc.AutoSize = true;
            this.chk_ColdJunc.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.chk_ColdJunc.Location = new System.Drawing.Point(420, 93);
            this.chk_ColdJunc.Name = "chk_ColdJunc";
            this.chk_ColdJunc.Size = new System.Drawing.Size(88, 24);
            this.chk_ColdJunc.TabIndex = 45;
            this.chk_ColdJunc.Text = "冷端补偿";
            this.chk_ColdJunc.UseVisualStyleBackColor = true;
            this.chk_ColdJunc.Visible = false;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "设备地址";
            this.Column1.Name = "Column1";
            this.Column1.Width = 126;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "测点地址";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "测量方式";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "传感器";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "热偶";
            this.Column5.Name = "Column5";
            this.Column5.Width = 80;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "热阻";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "RTD";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "冷端补偿";
            this.Column9.Name = "Column9";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "当前值";
            this.Column6.Name = "Column6";
            this.Column6.Width = 130;
            // 
            // frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 601);
            this.Controls.Add(this.spc_Main);
            this.MinimumSize = new System.Drawing.Size(1220, 640);
            this.Name = "frm_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KE设备设置";
            this.spc_Main.Panel1.ResumeLayout(false);
            this.spc_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_Main)).EndInit();
            this.spc_Main.ResumeLayout(false);
            this.spc_Right.Panel1.ResumeLayout(false);
            this.spc_Right.Panel1.PerformLayout();
            this.spc_Right.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_Right)).EndInit();
            this.spc_Right.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Channel)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spc_Main;
        private System.Windows.Forms.TreeView tv;
        private System.Windows.Forms.SplitContainer spc_Right;
        private System.Windows.Forms.Button btn_Download;
        private System.Windows.Forms.Button btn_SaveConfig;
        private System.Windows.Forms.ComboBox cmb_Thermocouple;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmb_Transducer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_ChannelType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_ChannelAddress;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_InstrumentAddress;
        private System.Windows.Forms.DataGridView dgv_Channel;
        private System.Windows.Forms.Button btn_ReadConfig;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.CheckBox chk_ColdJunc;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
    }
}