
namespace PipeControl
{
    partial class frm_History
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_History));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.spc_Main = new System.Windows.Forms.SplitContainer();
            this.lb_Channels = new System.Windows.Forms.ListBox();
            this.lb_HistoryDirectory = new System.Windows.Forms.ListBox();
            this.btn_ShowChannels = new System.Windows.Forms.Button();
            this.spc_Right = new System.Windows.Forms.SplitContainer();
            this.curve = new AxImcCurvesLib.AxCurveCtrl();
            this.btn_Cut = new System.Windows.Forms.Button();
            this.pnl_CutCurve = new System.Windows.Forms.Panel();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.mtxt_Right = new System.Windows.Forms.MaskedTextBox();
            this.mtxt_Left = new System.Windows.Forms.MaskedTextBox();
            this.lbl_RightValue = new System.Windows.Forms.Label();
            this.lbl_LeftValue = new System.Windows.Forms.Label();
            this.btn_ShowAll = new System.Windows.Forms.Button();
            this.btn_CutCurve = new System.Windows.Forms.Button();
            this.cmb_CursorLeft = new System.Windows.Forms.ComboBox();
            this.lbl_Right = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Left = new System.Windows.Forms.Label();
            this.btn_Export = new System.Windows.Forms.Button();
            this.btn_CurveMode = new System.Windows.Forms.Button();
            this.btn_ShowList = new System.Windows.Forms.Button();
            this.btn_Legend = new System.Windows.Forms.Button();
            this.pnl_Out = new System.Windows.Forms.Panel();
            this.chk = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_Inteval = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.btn_Out = new System.Windows.Forms.Button();
            this.txt_OutFileLocation = new System.Windows.Forms.TextBox();
            this.btn_CreateOutFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pnl_CurveMode = new System.Windows.Forms.Panel();
            this.cmb_CurveMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.historyList = new PipeControl.HistoryList();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Main)).BeginInit();
            this.spc_Main.Panel1.SuspendLayout();
            this.spc_Main.Panel2.SuspendLayout();
            this.spc_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Right)).BeginInit();
            this.spc_Right.Panel1.SuspendLayout();
            this.spc_Right.Panel2.SuspendLayout();
            this.spc_Right.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.curve)).BeginInit();
            this.pnl_CutCurve.SuspendLayout();
            this.pnl_Out.SuspendLayout();
            this.pnl_CurveMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.historyList)).BeginInit();
            this.SuspendLayout();
            // 
            // spc_Main
            // 
            this.spc_Main.BackColor = System.Drawing.Color.White;
            this.spc_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_Main.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spc_Main.Location = new System.Drawing.Point(0, 0);
            this.spc_Main.Margin = new System.Windows.Forms.Padding(4);
            this.spc_Main.Name = "spc_Main";
            // 
            // spc_Main.Panel1
            // 
            this.spc_Main.Panel1.Controls.Add(this.lb_Channels);
            this.spc_Main.Panel1.Controls.Add(this.lb_HistoryDirectory);
            this.spc_Main.Panel1.Controls.Add(this.btn_ShowChannels);
            // 
            // spc_Main.Panel2
            // 
            this.spc_Main.Panel2.Controls.Add(this.spc_Right);
            this.spc_Main.Size = new System.Drawing.Size(1350, 748);
            this.spc_Main.SplitterDistance = 220;
            this.spc_Main.SplitterWidth = 3;
            this.spc_Main.TabIndex = 3;
            // 
            // lb_Channels
            // 
            this.lb_Channels.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_Channels.BackColor = System.Drawing.Color.Gainsboro;
            this.lb_Channels.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_Channels.CausesValidation = false;
            this.lb_Channels.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Channels.IntegralHeight = false;
            this.lb_Channels.ItemHeight = 20;
            this.lb_Channels.Location = new System.Drawing.Point(0, 331);
            this.lb_Channels.Name = "lb_Channels";
            this.lb_Channels.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lb_Channels.Size = new System.Drawing.Size(219, 413);
            this.lb_Channels.TabIndex = 22;
            this.lb_Channels.TabStop = false;
            // 
            // lb_HistoryDirectory
            // 
            this.lb_HistoryDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_HistoryDirectory.BackColor = System.Drawing.Color.Gainsboro;
            this.lb_HistoryDirectory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_HistoryDirectory.CausesValidation = false;
            this.lb_HistoryDirectory.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_HistoryDirectory.IntegralHeight = false;
            this.lb_HistoryDirectory.ItemHeight = 20;
            this.lb_HistoryDirectory.Location = new System.Drawing.Point(0, 0);
            this.lb_HistoryDirectory.Name = "lb_HistoryDirectory";
            this.lb_HistoryDirectory.Size = new System.Drawing.Size(219, 269);
            this.lb_HistoryDirectory.TabIndex = 20;
            this.lb_HistoryDirectory.TabStop = false;
            // 
            // btn_ShowChannels
            // 
            this.btn_ShowChannels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ShowChannels.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_ShowChannels.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.btn_ShowChannels.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OrangeRed;
            this.btn_ShowChannels.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.btn_ShowChannels.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ShowChannels.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.btn_ShowChannels.ForeColor = System.Drawing.Color.Black;
            this.btn_ShowChannels.Location = new System.Drawing.Point(0, 270);
            this.btn_ShowChannels.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ShowChannels.Name = "btn_ShowChannels";
            this.btn_ShowChannels.Size = new System.Drawing.Size(219, 60);
            this.btn_ShowChannels.TabIndex = 21;
            this.btn_ShowChannels.TabStop = false;
            this.btn_ShowChannels.Text = "显示通道列表";
            this.btn_ShowChannels.UseVisualStyleBackColor = false;
            // 
            // spc_Right
            // 
            this.spc_Right.BackColor = System.Drawing.Color.White;
            this.spc_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_Right.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spc_Right.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.spc_Right.IsSplitterFixed = true;
            this.spc_Right.Location = new System.Drawing.Point(0, 0);
            this.spc_Right.Margin = new System.Windows.Forms.Padding(4);
            this.spc_Right.Name = "spc_Right";
            this.spc_Right.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spc_Right.Panel1
            // 
            this.spc_Right.Panel1.Controls.Add(this.historyList);
            this.spc_Right.Panel1.Controls.Add(this.curve);
            // 
            // spc_Right.Panel2
            // 
            this.spc_Right.Panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.spc_Right.Panel2.Controls.Add(this.btn_Cut);
            this.spc_Right.Panel2.Controls.Add(this.pnl_CutCurve);
            this.spc_Right.Panel2.Controls.Add(this.btn_Export);
            this.spc_Right.Panel2.Controls.Add(this.btn_CurveMode);
            this.spc_Right.Panel2.Controls.Add(this.btn_ShowList);
            this.spc_Right.Panel2.Controls.Add(this.btn_Legend);
            this.spc_Right.Panel2.Controls.Add(this.pnl_Out);
            this.spc_Right.Panel2.Controls.Add(this.pnl_CurveMode);
            this.spc_Right.Size = new System.Drawing.Size(1127, 748);
            this.spc_Right.SplitterDistance = 216;
            this.spc_Right.SplitterWidth = 3;
            this.spc_Right.TabIndex = 0;
            // 
            // curve
            // 
            this.curve.Dock = System.Windows.Forms.DockStyle.Fill;
            this.curve.Location = new System.Drawing.Point(0, 0);
            this.curve.Margin = new System.Windows.Forms.Padding(4);
            this.curve.Name = "curve";
            this.curve.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("curve.OcxState")));
            this.curve.Size = new System.Drawing.Size(1127, 216);
            this.curve.TabIndex = 0;
            // 
            // btn_Cut
            // 
            this.btn_Cut.BackColor = System.Drawing.Color.Silver;
            this.btn_Cut.FlatAppearance.BorderSize = 0;
            this.btn_Cut.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btn_Cut.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btn_Cut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Cut.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Cut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btn_Cut.Location = new System.Drawing.Point(0, 0);
            this.btn_Cut.Name = "btn_Cut";
            this.btn_Cut.Size = new System.Drawing.Size(75, 25);
            this.btn_Cut.TabIndex = 26;
            this.btn_Cut.Text = "剪切数据";
            this.btn_Cut.UseVisualStyleBackColor = false;
            // 
            // pnl_CutCurve
            // 
            this.pnl_CutCurve.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_CutCurve.BackColor = System.Drawing.Color.Silver;
            this.pnl_CutCurve.Controls.Add(this.lbl2);
            this.pnl_CutCurve.Controls.Add(this.lbl1);
            this.pnl_CutCurve.Controls.Add(this.mtxt_Right);
            this.pnl_CutCurve.Controls.Add(this.mtxt_Left);
            this.pnl_CutCurve.Controls.Add(this.lbl_RightValue);
            this.pnl_CutCurve.Controls.Add(this.lbl_LeftValue);
            this.pnl_CutCurve.Controls.Add(this.btn_ShowAll);
            this.pnl_CutCurve.Controls.Add(this.btn_CutCurve);
            this.pnl_CutCurve.Controls.Add(this.cmb_CursorLeft);
            this.pnl_CutCurve.Controls.Add(this.lbl_Right);
            this.pnl_CutCurve.Controls.Add(this.label1);
            this.pnl_CutCurve.Controls.Add(this.lbl_Left);
            this.pnl_CutCurve.Location = new System.Drawing.Point(0, 295);
            this.pnl_CutCurve.Margin = new System.Windows.Forms.Padding(2);
            this.pnl_CutCurve.Name = "pnl_CutCurve";
            this.pnl_CutCurve.Size = new System.Drawing.Size(1127, 120);
            this.pnl_CutCurve.TabIndex = 25;
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl2.Location = new System.Drawing.Point(419, 87);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(58, 20);
            this.lbl2.TabIndex = 29;
            this.lbl2.Text = "label12";
            this.lbl2.Visible = false;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl1.Location = new System.Drawing.Point(125, 87);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(58, 20);
            this.lbl1.TabIndex = 28;
            this.lbl1.Text = "label11";
            this.lbl1.Visible = false;
            // 
            // mtxt_Right
            // 
            this.mtxt_Right.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtxt_Right.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mtxt_Right.Location = new System.Drawing.Point(419, 51);
            this.mtxt_Right.Mask = "00日00时00分00秒";
            this.mtxt_Right.Name = "mtxt_Right";
            this.mtxt_Right.Size = new System.Drawing.Size(136, 26);
            this.mtxt_Right.TabIndex = 27;
            // 
            // mtxt_Left
            // 
            this.mtxt_Left.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtxt_Left.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mtxt_Left.Location = new System.Drawing.Point(125, 51);
            this.mtxt_Left.Mask = "00日00时00分00秒";
            this.mtxt_Left.Name = "mtxt_Left";
            this.mtxt_Left.Size = new System.Drawing.Size(136, 26);
            this.mtxt_Left.TabIndex = 26;
            // 
            // lbl_RightValue
            // 
            this.lbl_RightValue.AutoSize = true;
            this.lbl_RightValue.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_RightValue.Location = new System.Drawing.Point(327, 87);
            this.lbl_RightValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_RightValue.Name = "lbl_RightValue";
            this.lbl_RightValue.Size = new System.Drawing.Size(101, 20);
            this.lbl_RightValue.TabIndex = 22;
            this.lbl_RightValue.Text = "鼠标右键Y值：";
            // 
            // lbl_LeftValue
            // 
            this.lbl_LeftValue.AutoSize = true;
            this.lbl_LeftValue.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_LeftValue.Location = new System.Drawing.Point(33, 87);
            this.lbl_LeftValue.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_LeftValue.Name = "lbl_LeftValue";
            this.lbl_LeftValue.Size = new System.Drawing.Size(101, 20);
            this.lbl_LeftValue.TabIndex = 21;
            this.lbl_LeftValue.Text = "鼠标左键Y值：";
            // 
            // btn_ShowAll
            // 
            this.btn_ShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ShowAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OrangeRed;
            this.btn_ShowAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.btn_ShowAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ShowAll.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ShowAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btn_ShowAll.Location = new System.Drawing.Point(1015, 59);
            this.btn_ShowAll.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ShowAll.Name = "btn_ShowAll";
            this.btn_ShowAll.Size = new System.Drawing.Size(100, 29);
            this.btn_ShowAll.TabIndex = 16;
            this.btn_ShowAll.Text = "显示全部";
            this.btn_ShowAll.UseVisualStyleBackColor = true;
            // 
            // btn_CutCurve
            // 
            this.btn_CutCurve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_CutCurve.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OrangeRed;
            this.btn_CutCurve.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.btn_CutCurve.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CutCurve.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_CutCurve.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btn_CutCurve.Location = new System.Drawing.Point(1015, 15);
            this.btn_CutCurve.Margin = new System.Windows.Forms.Padding(2);
            this.btn_CutCurve.Name = "btn_CutCurve";
            this.btn_CutCurve.Size = new System.Drawing.Size(100, 29);
            this.btn_CutCurve.TabIndex = 15;
            this.btn_CutCurve.Text = "剪切曲线";
            this.btn_CutCurve.UseVisualStyleBackColor = true;
            // 
            // cmb_CursorLeft
            // 
            this.cmb_CursorLeft.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_CursorLeft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_CursorLeft.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_CursorLeft.FormattingEnabled = true;
            this.cmb_CursorLeft.Location = new System.Drawing.Point(125, 17);
            this.cmb_CursorLeft.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_CursorLeft.Name = "cmb_CursorLeft";
            this.cmb_CursorLeft.Size = new System.Drawing.Size(334, 28);
            this.cmb_CursorLeft.TabIndex = 10;
            // 
            // lbl_Right
            // 
            this.lbl_Right.AutoSize = true;
            this.lbl_Right.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Right.Location = new System.Drawing.Point(326, 54);
            this.lbl_Right.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Right.Name = "lbl_Right";
            this.lbl_Right.Size = new System.Drawing.Size(102, 20);
            this.lbl_Right.TabIndex = 14;
            this.lbl_Right.Text = "鼠标右键X值：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(55, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "操作对象：";
            // 
            // lbl_Left
            // 
            this.lbl_Left.AutoSize = true;
            this.lbl_Left.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Left.Location = new System.Drawing.Point(32, 54);
            this.lbl_Left.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Left.Name = "lbl_Left";
            this.lbl_Left.Size = new System.Drawing.Size(102, 20);
            this.lbl_Left.TabIndex = 13;
            this.lbl_Left.Text = "鼠标左键X值：";
            // 
            // btn_Export
            // 
            this.btn_Export.FlatAppearance.BorderSize = 0;
            this.btn_Export.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btn_Export.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btn_Export.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Export.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Export.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btn_Export.Location = new System.Drawing.Point(150, 0);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(75, 25);
            this.btn_Export.TabIndex = 24;
            this.btn_Export.Text = "导出数据";
            this.btn_Export.UseVisualStyleBackColor = true;
            // 
            // btn_CurveMode
            // 
            this.btn_CurveMode.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_CurveMode.FlatAppearance.BorderSize = 0;
            this.btn_CurveMode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btn_CurveMode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btn_CurveMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CurveMode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_CurveMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btn_CurveMode.Location = new System.Drawing.Point(75, 0);
            this.btn_CurveMode.Name = "btn_CurveMode";
            this.btn_CurveMode.Size = new System.Drawing.Size(75, 25);
            this.btn_CurveMode.TabIndex = 23;
            this.btn_CurveMode.Text = "曲线方式";
            this.btn_CurveMode.UseVisualStyleBackColor = false;
            // 
            // btn_ShowList
            // 
            this.btn_ShowList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ShowList.FlatAppearance.BorderSize = 0;
            this.btn_ShowList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OrangeRed;
            this.btn_ShowList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.btn_ShowList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ShowList.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_ShowList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btn_ShowList.Location = new System.Drawing.Point(975, 0);
            this.btn_ShowList.Name = "btn_ShowList";
            this.btn_ShowList.Size = new System.Drawing.Size(75, 25);
            this.btn_ShowList.TabIndex = 20;
            this.btn_ShowList.Text = "显示表格";
            this.btn_ShowList.UseVisualStyleBackColor = true;
            // 
            // btn_Legend
            // 
            this.btn_Legend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Legend.FlatAppearance.BorderSize = 0;
            this.btn_Legend.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OrangeRed;
            this.btn_Legend.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.btn_Legend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Legend.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Legend.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btn_Legend.Location = new System.Drawing.Point(1050, 0);
            this.btn_Legend.Name = "btn_Legend";
            this.btn_Legend.Size = new System.Drawing.Size(75, 25);
            this.btn_Legend.TabIndex = 19;
            this.btn_Legend.Text = "显示图例";
            this.btn_Legend.UseVisualStyleBackColor = true;
            // 
            // pnl_Out
            // 
            this.pnl_Out.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_Out.BackColor = System.Drawing.Color.Silver;
            this.pnl_Out.Controls.Add(this.chk);
            this.pnl_Out.Controls.Add(this.label5);
            this.pnl_Out.Controls.Add(this.txt_Inteval);
            this.pnl_Out.Controls.Add(this.label4);
            this.pnl_Out.Controls.Add(this.pb);
            this.pnl_Out.Controls.Add(this.btn_Out);
            this.pnl_Out.Controls.Add(this.txt_OutFileLocation);
            this.pnl_Out.Controls.Add(this.btn_CreateOutFile);
            this.pnl_Out.Controls.Add(this.label2);
            this.pnl_Out.Location = new System.Drawing.Point(0, 158);
            this.pnl_Out.Name = "pnl_Out";
            this.pnl_Out.Size = new System.Drawing.Size(1127, 120);
            this.pnl_Out.TabIndex = 21;
            // 
            // chk
            // 
            this.chk.AutoSize = true;
            this.chk.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chk.Location = new System.Drawing.Point(240, 49);
            this.chk.Name = "chk";
            this.chk.Size = new System.Drawing.Size(98, 24);
            this.chk.TabIndex = 37;
            this.chk.Text = "导出平均值";
            this.chk.UseVisualStyleBackColor = true;
            this.chk.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(165, 51);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 20);
            this.label5.TabIndex = 36;
            this.label5.Text = "采样周期";
            // 
            // txt_Inteval
            // 
            this.txt_Inteval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Inteval.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Inteval.Location = new System.Drawing.Point(125, 48);
            this.txt_Inteval.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Inteval.Name = "txt_Inteval";
            this.txt_Inteval.Size = new System.Drawing.Size(38, 26);
            this.txt_Inteval.TabIndex = 35;
            this.txt_Inteval.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(27, 51);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 20);
            this.label4.TabIndex = 34;
            this.label4.Text = "导出数据间隔：";
            // 
            // pb
            // 
            this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb.Location = new System.Drawing.Point(4, 97);
            this.pb.Margin = new System.Windows.Forms.Padding(4);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(1111, 16);
            this.pb.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb.TabIndex = 23;
            this.pb.Visible = false;
            // 
            // btn_Out
            // 
            this.btn_Out.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Out.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OrangeRed;
            this.btn_Out.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.btn_Out.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Out.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_Out.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btn_Out.Location = new System.Drawing.Point(1015, 59);
            this.btn_Out.Name = "btn_Out";
            this.btn_Out.Size = new System.Drawing.Size(100, 29);
            this.btn_Out.TabIndex = 19;
            this.btn_Out.Text = "导出数据";
            this.btn_Out.UseVisualStyleBackColor = true;
            // 
            // txt_OutFileLocation
            // 
            this.txt_OutFileLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_OutFileLocation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_OutFileLocation.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.txt_OutFileLocation.Location = new System.Drawing.Point(125, 18);
            this.txt_OutFileLocation.Name = "txt_OutFileLocation";
            this.txt_OutFileLocation.ReadOnly = true;
            this.txt_OutFileLocation.Size = new System.Drawing.Size(884, 26);
            this.txt_OutFileLocation.TabIndex = 18;
            // 
            // btn_CreateOutFile
            // 
            this.btn_CreateOutFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_CreateOutFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.OrangeRed;
            this.btn_CreateOutFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.btn_CreateOutFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CreateOutFile.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.btn_CreateOutFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(57)))), ((int)(((byte)(85)))));
            this.btn_CreateOutFile.Location = new System.Drawing.Point(1015, 15);
            this.btn_CreateOutFile.Name = "btn_CreateOutFile";
            this.btn_CreateOutFile.Size = new System.Drawing.Size(100, 29);
            this.btn_CreateOutFile.TabIndex = 17;
            this.btn_CreateOutFile.Text = "新建…";
            this.btn_CreateOutFile.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.label2.Location = new System.Drawing.Point(27, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "导出文件位置：";
            // 
            // pnl_CurveMode
            // 
            this.pnl_CurveMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_CurveMode.BackColor = System.Drawing.Color.Silver;
            this.pnl_CurveMode.Controls.Add(this.cmb_CurveMode);
            this.pnl_CurveMode.Controls.Add(this.label3);
            this.pnl_CurveMode.Location = new System.Drawing.Point(0, 25);
            this.pnl_CurveMode.Name = "pnl_CurveMode";
            this.pnl_CurveMode.Size = new System.Drawing.Size(1127, 120);
            this.pnl_CurveMode.TabIndex = 22;
            // 
            // cmb_CurveMode
            // 
            this.cmb_CurveMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_CurveMode.Enabled = false;
            this.cmb_CurveMode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmb_CurveMode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_CurveMode.FormattingEnabled = true;
            this.cmb_CurveMode.Items.AddRange(new object[] {
            "相同的X轴和Y轴",
            "相同的X轴，不同的Y轴",
            "不同的X轴，不同的Y轴"});
            this.cmb_CurveMode.Location = new System.Drawing.Point(125, 17);
            this.cmb_CurveMode.Name = "cmb_CurveMode";
            this.cmb_CurveMode.Size = new System.Drawing.Size(262, 28);
            this.cmb_CurveMode.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(27, 21);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "曲线显示方式：";
            // 
            // historyList
            // 
            this.historyList.AllowUserToAddRows = false;
            this.historyList.AllowUserToDeleteRows = false;
            this.historyList.AllowUserToOrderColumns = true;
            this.historyList.AllowUserToResizeRows = false;
            this.historyList.BackgroundColor = System.Drawing.Color.Gray;
            this.historyList.BeginTime = null;
            this.historyList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.historyList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.historyList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.historyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.historyList.DefaultCellStyle = dataGridViewCellStyle2;
            this.historyList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.historyList.EnableHeadersVisualStyles = false;
            this.historyList.EndTime = null;
            this.historyList.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.historyList.GridColor = System.Drawing.Color.DarkGray;
            this.historyList.Location = new System.Drawing.Point(353, 93);
            this.historyList.Name = "historyList";
            this.historyList.ReadOnly = true;
            this.historyList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.historyList.RowHeadersVisible = false;
            this.historyList.RowTemplate.Height = 23;
            this.historyList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.historyList.ShowCellErrors = false;
            this.historyList.ShowCellToolTips = false;
            this.historyList.ShowEditingIcon = false;
            this.historyList.Size = new System.Drawing.Size(590, 142);
            this.historyList.TabIndex = 1;
            this.historyList.VirtualMode = true;
            this.historyList.Visible = false;
            // 
            // frm_History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 748);
            this.Controls.Add(this.spc_Main);
            this.Name = "frm_History";
            this.Text = "历史数据管理";
            this.spc_Main.Panel1.ResumeLayout(false);
            this.spc_Main.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_Main)).EndInit();
            this.spc_Main.ResumeLayout(false);
            this.spc_Right.Panel1.ResumeLayout(false);
            this.spc_Right.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spc_Right)).EndInit();
            this.spc_Right.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.curve)).EndInit();
            this.pnl_CutCurve.ResumeLayout(false);
            this.pnl_CutCurve.PerformLayout();
            this.pnl_Out.ResumeLayout(false);
            this.pnl_Out.PerformLayout();
            this.pnl_CurveMode.ResumeLayout(false);
            this.pnl_CurveMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.historyList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer spc_Main;
        private System.Windows.Forms.ListBox lb_Channels;
        private System.Windows.Forms.ListBox lb_HistoryDirectory;
        internal System.Windows.Forms.Button btn_ShowChannels;
        private System.Windows.Forms.SplitContainer spc_Right;
        private AxImcCurvesLib.AxCurveCtrl curve;
        internal System.Windows.Forms.Button btn_Cut;
        private System.Windows.Forms.Panel pnl_CutCurve;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.MaskedTextBox mtxt_Right;
        private System.Windows.Forms.MaskedTextBox mtxt_Left;
        private System.Windows.Forms.Label lbl_RightValue;
        private System.Windows.Forms.Label lbl_LeftValue;
        internal System.Windows.Forms.Button btn_ShowAll;
        internal System.Windows.Forms.Button btn_CutCurve;
        private System.Windows.Forms.ComboBox cmb_CursorLeft;
        private System.Windows.Forms.Label lbl_Right;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Left;
        internal System.Windows.Forms.Button btn_Export;
        internal System.Windows.Forms.Button btn_CurveMode;
        internal System.Windows.Forms.Button btn_ShowList;
        internal System.Windows.Forms.Button btn_Legend;
        private System.Windows.Forms.Panel pnl_Out;
        private System.Windows.Forms.CheckBox chk;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_Inteval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar pb;
        internal System.Windows.Forms.Button btn_Out;
        private System.Windows.Forms.TextBox txt_OutFileLocation;
        internal System.Windows.Forms.Button btn_CreateOutFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnl_CurveMode;
        private System.Windows.Forms.ComboBox cmb_CurveMode;
        private System.Windows.Forms.Label label3;
        private HistoryList historyList;
    }
}