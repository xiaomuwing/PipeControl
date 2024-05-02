
namespace DataObjects
{
    partial class ChannelProperty
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lb_ChannelConfigCellEdit = new System.Windows.Forms.ListBox();
            this.btn_ChannelConfigOK = new System.Windows.Forms.Button();
            this.spc_Main = new System.Windows.Forms.SplitContainer();
            this.dgv_ChannelProperty = new PipeControl.Common.DataList();
            this.btn_Close = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Main)).BeginInit();
            this.spc_Main.Panel1.SuspendLayout();
            this.spc_Main.Panel2.SuspendLayout();
            this.spc_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ChannelProperty)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_ChannelConfigCellEdit
            // 
            this.lb_ChannelConfigCellEdit.BackColor = System.Drawing.Color.White;
            this.lb_ChannelConfigCellEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_ChannelConfigCellEdit.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_ChannelConfigCellEdit.FormattingEnabled = true;
            this.lb_ChannelConfigCellEdit.IntegralHeight = false;
            this.lb_ChannelConfigCellEdit.ItemHeight = 20;
            this.lb_ChannelConfigCellEdit.Location = new System.Drawing.Point(89, 179);
            this.lb_ChannelConfigCellEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lb_ChannelConfigCellEdit.Name = "lb_ChannelConfigCellEdit";
            this.lb_ChannelConfigCellEdit.Size = new System.Drawing.Size(157, 88);
            this.lb_ChannelConfigCellEdit.TabIndex = 1;
            this.lb_ChannelConfigCellEdit.Visible = false;
            // 
            // btn_ChannelConfigOK
            // 
            this.btn_ChannelConfigOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ChannelConfigOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(71)))), ((int)(((byte)(81)))));
            this.btn_ChannelConfigOK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(96)))), ((int)(((byte)(130)))));
            this.btn_ChannelConfigOK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btn_ChannelConfigOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ChannelConfigOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ChannelConfigOK.ForeColor = System.Drawing.Color.White;
            this.btn_ChannelConfigOK.Location = new System.Drawing.Point(157, 14);
            this.btn_ChannelConfigOK.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ChannelConfigOK.Name = "btn_ChannelConfigOK";
            this.btn_ChannelConfigOK.Size = new System.Drawing.Size(104, 43);
            this.btn_ChannelConfigOK.TabIndex = 77;
            this.btn_ChannelConfigOK.Text = "保   存";
            this.btn_ChannelConfigOK.UseVisualStyleBackColor = false;
            this.btn_ChannelConfigOK.Click += new System.EventHandler(this.Btn_ChannelConfigOK_Click);
            // 
            // spc_Main
            // 
            this.spc_Main.BackColor = System.Drawing.Color.White;
            this.spc_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spc_Main.IsSplitterFixed = true;
            this.spc_Main.Location = new System.Drawing.Point(0, 0);
            this.spc_Main.Name = "spc_Main";
            this.spc_Main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spc_Main.Panel1
            // 
            this.spc_Main.Panel1.Controls.Add(this.lb_ChannelConfigCellEdit);
            this.spc_Main.Panel1.Controls.Add(this.dgv_ChannelProperty);
            // 
            // spc_Main.Panel2
            // 
            this.spc_Main.Panel2.BackColor = System.Drawing.Color.White;
            this.spc_Main.Panel2.Controls.Add(this.btn_Close);
            this.spc_Main.Panel2.Controls.Add(this.btn_ChannelConfigOK);
            this.spc_Main.Size = new System.Drawing.Size(270, 671);
            this.spc_Main.SplitterDistance = 606;
            this.spc_Main.SplitterWidth = 1;
            this.spc_Main.TabIndex = 83;
            // 
            // dgv_ChannelProperty
            // 
            this.dgv_ChannelProperty.AllowUserToAddRows = false;
            this.dgv_ChannelProperty.AllowUserToDeleteRows = false;
            this.dgv_ChannelProperty.AllowUserToResizeRows = false;
            this.dgv_ChannelProperty.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_ChannelProperty.BackgroundColor = System.Drawing.Color.White;
            this.dgv_ChannelProperty.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_ChannelProperty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_ChannelProperty.ColumnHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_ChannelProperty.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_ChannelProperty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_ChannelProperty.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_ChannelProperty.GridColor = System.Drawing.Color.Gray;
            this.dgv_ChannelProperty.ImeMode = System.Windows.Forms.ImeMode.Alpha;
            this.dgv_ChannelProperty.Location = new System.Drawing.Point(0, 0);
            this.dgv_ChannelProperty.Margin = new System.Windows.Forms.Padding(0);
            this.dgv_ChannelProperty.Name = "dgv_ChannelProperty";
            this.dgv_ChannelProperty.RowHeadersVisible = false;
            this.dgv_ChannelProperty.RowTemplate.Height = 23;
            this.dgv_ChannelProperty.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv_ChannelProperty.Size = new System.Drawing.Size(270, 606);
            this.dgv_ChannelProperty.TabIndex = 0;
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(71)))), ((int)(((byte)(81)))));
            this.btn_Close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(96)))), ((int)(((byte)(130)))));
            this.btn_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Close.ForeColor = System.Drawing.Color.White;
            this.btn_Close.Location = new System.Drawing.Point(44, 14);
            this.btn_Close.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(104, 43);
            this.btn_Close.TabIndex = 78;
            this.btn_Close.Text = "退   出";
            this.btn_Close.UseVisualStyleBackColor = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // ChannelProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.spc_Main);
            this.Name = "ChannelProperty";
            this.Size = new System.Drawing.Size(270, 671);
            this.spc_Main.Panel1.ResumeLayout(false);
            this.spc_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_Main)).EndInit();
            this.spc_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ChannelProperty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PipeControl.Common.DataList dgv_ChannelProperty;
        private System.Windows.Forms.ListBox lb_ChannelConfigCellEdit;
        private System.Windows.Forms.Button btn_ChannelConfigOK;
        private System.Windows.Forms.SplitContainer spc_Main;
        private System.Windows.Forms.Button btn_Close;
    }
}
