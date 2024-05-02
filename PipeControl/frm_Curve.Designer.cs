
namespace PipeControl
{
    partial class frm_Curve
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Curve));
            this.spc_Top = new System.Windows.Forms.SplitContainer();
            this.spc_Main = new System.Windows.Forms.SplitContainer();
            this.curve = new AxImcCurvesLib.AxCurveCtrl();
            this.btn_LastValue = new System.Windows.Forms.Button();
            this.btn_ShowLegend = new System.Windows.Forms.Button();
            this.pnl_right = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Top)).BeginInit();
            this.spc_Top.Panel1.SuspendLayout();
            this.spc_Top.Panel2.SuspendLayout();
            this.spc_Top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Main)).BeginInit();
            this.spc_Main.Panel1.SuspendLayout();
            this.spc_Main.Panel2.SuspendLayout();
            this.spc_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.curve)).BeginInit();
            this.SuspendLayout();
            // 
            // spc_Top
            // 
            this.spc_Top.BackColor = System.Drawing.Color.DarkOrange;
            this.spc_Top.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_Top.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spc_Top.Location = new System.Drawing.Point(0, 0);
            this.spc_Top.Name = "spc_Top";
            // 
            // spc_Top.Panel1
            // 
            this.spc_Top.Panel1.Controls.Add(this.spc_Main);
            // 
            // spc_Top.Panel2
            // 
            this.spc_Top.Panel2.BackColor = System.Drawing.Color.LightGray;
            this.spc_Top.Panel2.Controls.Add(this.pnl_right);
            this.spc_Top.Size = new System.Drawing.Size(624, 320);
            this.spc_Top.SplitterDistance = 417;
            this.spc_Top.SplitterWidth = 2;
            this.spc_Top.TabIndex = 0;
            // 
            // spc_Main
            // 
            this.spc_Main.BackColor = System.Drawing.Color.Gainsboro;
            this.spc_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spc_Main.IsSplitterFixed = true;
            this.spc_Main.Location = new System.Drawing.Point(0, 0);
            this.spc_Main.Name = "spc_Main";
            this.spc_Main.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spc_Main.Panel1
            // 
            this.spc_Main.Panel1.Controls.Add(this.curve);
            // 
            // spc_Main.Panel2
            // 
            this.spc_Main.Panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.spc_Main.Panel2.Controls.Add(this.btn_LastValue);
            this.spc_Main.Panel2.Controls.Add(this.btn_ShowLegend);
            this.spc_Main.Size = new System.Drawing.Size(417, 320);
            this.spc_Main.SplitterDistance = 283;
            this.spc_Main.SplitterWidth = 1;
            this.spc_Main.TabIndex = 0;
            // 
            // curve
            // 
            this.curve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curve.Location = new System.Drawing.Point(0, 0);
            this.curve.Name = "curve";
            this.curve.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("curve.OcxState")));
            this.curve.Size = new System.Drawing.Size(417, 283);
            this.curve.TabIndex = 0;
            // 
            // btn_LastValue
            // 
            this.btn_LastValue.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_LastValue.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(71)))), ((int)(((byte)(81)))));
            this.btn_LastValue.FlatAppearance.BorderSize = 0;
            this.btn_LastValue.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkGray;
            this.btn_LastValue.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.btn_LastValue.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btn_LastValue.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LastValue.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_LastValue.ForeColor = System.Drawing.Color.Black;
            this.btn_LastValue.Location = new System.Drawing.Point(106, 5);
            this.btn_LastValue.Margin = new System.Windows.Forms.Padding(0);
            this.btn_LastValue.Name = "btn_LastValue";
            this.btn_LastValue.Size = new System.Drawing.Size(105, 29);
            this.btn_LastValue.TabIndex = 17;
            this.btn_LastValue.Text = "显示最后值";
            this.btn_LastValue.UseVisualStyleBackColor = false;
            this.btn_LastValue.Click += new System.EventHandler(this.btn_LastValue_Click);
            // 
            // btn_ShowLegend
            // 
            this.btn_ShowLegend.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_ShowLegend.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(71)))), ((int)(((byte)(81)))));
            this.btn_ShowLegend.FlatAppearance.BorderSize = 0;
            this.btn_ShowLegend.FlatAppearance.CheckedBackColor = System.Drawing.Color.DarkGray;
            this.btn_ShowLegend.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkGray;
            this.btn_ShowLegend.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btn_ShowLegend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ShowLegend.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ShowLegend.ForeColor = System.Drawing.Color.Black;
            this.btn_ShowLegend.Location = new System.Drawing.Point(0, 5);
            this.btn_ShowLegend.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ShowLegend.Name = "btn_ShowLegend";
            this.btn_ShowLegend.Size = new System.Drawing.Size(105, 29);
            this.btn_ShowLegend.TabIndex = 16;
            this.btn_ShowLegend.Text = "显示图例";
            this.btn_ShowLegend.UseVisualStyleBackColor = false;
            this.btn_ShowLegend.Click += new System.EventHandler(this.btn_ShowLegend_Click);
            // 
            // pnl_right
            // 
            this.pnl_right.BackColor = System.Drawing.Color.Gainsboro;
            this.pnl_right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_right.Location = new System.Drawing.Point(0, 0);
            this.pnl_right.Name = "pnl_right";
            this.pnl_right.Size = new System.Drawing.Size(205, 320);
            this.pnl_right.TabIndex = 3;
            // 
            // frm_Curve
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(624, 320);
            this.Controls.Add(this.spc_Top);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ForeColor = System.Drawing.Color.Black;
            this.MinimumSize = new System.Drawing.Size(640, 360);
            this.Name = "frm_Curve";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "实时数据曲线";
            this.spc_Top.Panel1.ResumeLayout(false);
            this.spc_Top.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_Top)).EndInit();
            this.spc_Top.ResumeLayout(false);
            this.spc_Main.Panel1.ResumeLayout(false);
            this.spc_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_Main)).EndInit();
            this.spc_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.curve)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spc_Top;
        private System.Windows.Forms.SplitContainer spc_Main;
        private AxImcCurvesLib.AxCurveCtrl curve;
        internal System.Windows.Forms.Button btn_LastValue;
        internal System.Windows.Forms.Button btn_ShowLegend;
        private System.Windows.Forms.Panel pnl_right;
    }
}