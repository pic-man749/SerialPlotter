namespace SerialPlotter {
    partial class SerialPlotter {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent() {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.書式SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ウィンドウToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GBPlotSettings = new System.Windows.Forms.GroupBox();
            this.cbBufferFullScale = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbChartRefreshRate = new System.Windows.Forms.ComboBox();
            this.cbPlotMarker = new System.Windows.Forms.CheckBox();
            this.LabelPoltPoint = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TrackBarPlotTime = new System.Windows.Forms.TrackBar();
            this.BtnPlotReset = new System.Windows.Forms.Button();
            this.BtnPlotStart = new System.Windows.Forms.Button();
            this.GBSerialSettings = new System.Windows.Forms.GroupBox();
            this.cbSerialSendAddNl = new System.Windows.Forms.CheckBox();
            this.cbSerialSendAddCr = new System.Windows.Forms.CheckBox();
            this.btnSerialSend = new System.Windows.Forms.Button();
            this.tbSerialSend = new System.Windows.Forms.TextBox();
            this.CbLogWithTime = new System.Windows.Forms.CheckBox();
            this.CbNewLine = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TbLogFilePath = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CbHandshake = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CbLoggingFlag = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CbStopBit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CbParity = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CbDataBits = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnRefresh = new System.Windows.Forms.Button();
            this.CbBoudrateList = new System.Windows.Forms.ComboBox();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.LbComList = new System.Windows.Forms.ListBox();
            this.SfdLogging = new System.Windows.Forms.SaveFileDialog();
            this.lGraphFps = new System.Windows.Forms.Label();
            this.dgvGraphWindow = new System.Windows.Forms.DataGridView();
            this.series = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.graphWindowId = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.GBPlotSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarPlotTime)).BeginInit();
            this.GBSerialSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGraphWindow)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ファイルFToolStripMenuItem,
            this.書式SToolStripMenuItem,
            this.ウィンドウToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 47;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ファイルFToolStripMenuItem
            // 
            this.ファイルFToolStripMenuItem.Name = "ファイルFToolStripMenuItem";
            this.ファイルFToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.ファイルFToolStripMenuItem.Text = "ファイル(&F)";
            // 
            // 書式SToolStripMenuItem
            // 
            this.書式SToolStripMenuItem.Name = "書式SToolStripMenuItem";
            this.書式SToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.書式SToolStripMenuItem.Text = "書式(&S)";
            this.書式SToolStripMenuItem.Click += new System.EventHandler(this.書式SToolStripMenuItem_Click);
            // 
            // ウィンドウToolStripMenuItem
            // 
            this.ウィンドウToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataTableToolStripMenuItem});
            this.ウィンドウToolStripMenuItem.Name = "ウィンドウToolStripMenuItem";
            this.ウィンドウToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.ウィンドウToolStripMenuItem.Text = "ウィンドウ(&W)";
            // 
            // dataTableToolStripMenuItem
            // 
            this.dataTableToolStripMenuItem.Name = "dataTableToolStripMenuItem";
            this.dataTableToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.dataTableToolStripMenuItem.Text = "DataTable(&D)";
            this.dataTableToolStripMenuItem.Click += new System.EventHandler(this.DataTableToolStripMenuItem_Click);
            // 
            // GBPlotSettings
            // 
            this.GBPlotSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBPlotSettings.Controls.Add(this.dgvGraphWindow);
            this.GBPlotSettings.Controls.Add(this.lGraphFps);
            this.GBPlotSettings.Controls.Add(this.cbBufferFullScale);
            this.GBPlotSettings.Controls.Add(this.label9);
            this.GBPlotSettings.Controls.Add(this.cbChartRefreshRate);
            this.GBPlotSettings.Controls.Add(this.cbPlotMarker);
            this.GBPlotSettings.Controls.Add(this.LabelPoltPoint);
            this.GBPlotSettings.Controls.Add(this.label6);
            this.GBPlotSettings.Controls.Add(this.TrackBarPlotTime);
            this.GBPlotSettings.Controls.Add(this.BtnPlotReset);
            this.GBPlotSettings.Controls.Add(this.BtnPlotStart);
            this.GBPlotSettings.Location = new System.Drawing.Point(12, 204);
            this.GBPlotSettings.Name = "GBPlotSettings";
            this.GBPlotSettings.Size = new System.Drawing.Size(760, 245);
            this.GBPlotSettings.TabIndex = 49;
            this.GBPlotSettings.TabStop = false;
            this.GBPlotSettings.Text = "Plot settings";
            // 
            // cbBufferFullScale
            // 
            this.cbBufferFullScale.AutoSize = true;
            this.cbBufferFullScale.Checked = global::SerialPlotter.Properties.Settings.Default.settingBufferFullScale;
            this.cbBufferFullScale.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBufferFullScale.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SerialPlotter.Properties.Settings.Default, "settingBufferFullScale", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbBufferFullScale.Location = new System.Drawing.Point(548, 22);
            this.cbBufferFullScale.Name = "cbBufferFullScale";
            this.cbBufferFullScale.Size = new System.Drawing.Size(105, 16);
            this.cbBufferFullScale.TabIndex = 62;
            this.cbBufferFullScale.Text = "buffer full scale";
            this.cbBufferFullScale.UseVisualStyleBackColor = true;
            this.cbBufferFullScale.CheckedChanged += new System.EventHandler(this.CbBufferFullScale_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(454, 52);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 12);
            this.label9.TabIndex = 61;
            this.label9.Text = "refresh rate(Hz):";
            // 
            // cbChartRefreshRate
            // 
            this.cbChartRefreshRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbChartRefreshRate.FormattingEnabled = true;
            this.cbChartRefreshRate.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "20",
            "30",
            "60"});
            this.cbChartRefreshRate.Location = new System.Drawing.Point(548, 49);
            this.cbChartRefreshRate.Name = "cbChartRefreshRate";
            this.cbChartRefreshRate.Size = new System.Drawing.Size(97, 20);
            this.cbChartRefreshRate.TabIndex = 60;
            // 
            // cbPlotMarker
            // 
            this.cbPlotMarker.AutoSize = true;
            this.cbPlotMarker.Checked = global::SerialPlotter.Properties.Settings.Default.settingPlotMarker;
            this.cbPlotMarker.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPlotMarker.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SerialPlotter.Properties.Settings.Default, "settingPlotMarker", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbPlotMarker.Location = new System.Drawing.Point(456, 22);
            this.cbPlotMarker.Name = "cbPlotMarker";
            this.cbPlotMarker.Size = new System.Drawing.Size(82, 16);
            this.cbPlotMarker.TabIndex = 59;
            this.cbPlotMarker.Text = "plot marker";
            this.cbPlotMarker.UseVisualStyleBackColor = true;
            this.cbPlotMarker.CheckedChanged += new System.EventHandler(this.CbPlotMarker_CheckedChanged);
            // 
            // LabelPoltPoint
            // 
            this.LabelPoltPoint.AutoSize = true;
            this.LabelPoltPoint.Font = new System.Drawing.Font("MS UI Gothic", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LabelPoltPoint.Location = new System.Drawing.Point(399, 20);
            this.LabelPoltPoint.Name = "LabelPoltPoint";
            this.LabelPoltPoint.Size = new System.Drawing.Size(34, 15);
            this.LabelPoltPoint.TabIndex = 58;
            this.LabelPoltPoint.Text = "num";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(132, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 12);
            this.label6.TabIndex = 55;
            this.label6.Text = "plot time:";
            // 
            // TrackBarPlotTime
            // 
            this.TrackBarPlotTime.DataBindings.Add(new System.Windows.Forms.Binding("Value", global::SerialPlotter.Properties.Settings.Default, "settingPlotTime", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.TrackBarPlotTime.Location = new System.Drawing.Point(193, 18);
            this.TrackBarPlotTime.Maximum = 20;
            this.TrackBarPlotTime.Minimum = 1;
            this.TrackBarPlotTime.Name = "TrackBarPlotTime";
            this.TrackBarPlotTime.Size = new System.Drawing.Size(200, 45);
            this.TrackBarPlotTime.TabIndex = 57;
            this.TrackBarPlotTime.TickFrequency = 2;
            this.TrackBarPlotTime.Value = global::SerialPlotter.Properties.Settings.Default.settingPlotTime;
            this.TrackBarPlotTime.ValueChanged += new System.EventHandler(this.TrackBarPlotPoint_ValueChanged);
            // 
            // BtnPlotReset
            // 
            this.BtnPlotReset.Location = new System.Drawing.Point(6, 47);
            this.BtnPlotReset.Name = "BtnPlotReset";
            this.BtnPlotReset.Size = new System.Drawing.Size(120, 23);
            this.BtnPlotReset.TabIndex = 56;
            this.BtnPlotReset.Text = "reset";
            this.BtnPlotReset.UseVisualStyleBackColor = true;
            this.BtnPlotReset.Click += new System.EventHandler(this.BtnPlotReset_Click);
            // 
            // BtnPlotStart
            // 
            this.BtnPlotStart.Enabled = false;
            this.BtnPlotStart.Location = new System.Drawing.Point(6, 18);
            this.BtnPlotStart.Name = "BtnPlotStart";
            this.BtnPlotStart.Size = new System.Drawing.Size(120, 23);
            this.BtnPlotStart.TabIndex = 55;
            this.BtnPlotStart.Text = "plot start";
            this.BtnPlotStart.UseVisualStyleBackColor = true;
            this.BtnPlotStart.Click += new System.EventHandler(this.BtnPlotStart_Click);
            // 
            // GBSerialSettings
            // 
            this.GBSerialSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBSerialSettings.Controls.Add(this.cbSerialSendAddNl);
            this.GBSerialSettings.Controls.Add(this.cbSerialSendAddCr);
            this.GBSerialSettings.Controls.Add(this.btnSerialSend);
            this.GBSerialSettings.Controls.Add(this.tbSerialSend);
            this.GBSerialSettings.Controls.Add(this.CbLogWithTime);
            this.GBSerialSettings.Controls.Add(this.CbNewLine);
            this.GBSerialSettings.Controls.Add(this.label7);
            this.GBSerialSettings.Controls.Add(this.TbLogFilePath);
            this.GBSerialSettings.Controls.Add(this.label10);
            this.GBSerialSettings.Controls.Add(this.label8);
            this.GBSerialSettings.Controls.Add(this.CbHandshake);
            this.GBSerialSettings.Controls.Add(this.label5);
            this.GBSerialSettings.Controls.Add(this.CbLoggingFlag);
            this.GBSerialSettings.Controls.Add(this.label4);
            this.GBSerialSettings.Controls.Add(this.CbStopBit);
            this.GBSerialSettings.Controls.Add(this.label3);
            this.GBSerialSettings.Controls.Add(this.CbParity);
            this.GBSerialSettings.Controls.Add(this.label2);
            this.GBSerialSettings.Controls.Add(this.CbDataBits);
            this.GBSerialSettings.Controls.Add(this.label1);
            this.GBSerialSettings.Controls.Add(this.BtnRefresh);
            this.GBSerialSettings.Controls.Add(this.CbBoudrateList);
            this.GBSerialSettings.Controls.Add(this.BtnConnect);
            this.GBSerialSettings.Controls.Add(this.LbComList);
            this.GBSerialSettings.Location = new System.Drawing.Point(12, 27);
            this.GBSerialSettings.Name = "GBSerialSettings";
            this.GBSerialSettings.Size = new System.Drawing.Size(760, 171);
            this.GBSerialSettings.TabIndex = 50;
            this.GBSerialSettings.TabStop = false;
            this.GBSerialSettings.Text = "Serial settings";
            // 
            // cbSerialSendAddNl
            // 
            this.cbSerialSendAddNl.AutoSize = true;
            this.cbSerialSendAddNl.Checked = global::SerialPlotter.Properties.Settings.Default.settingSerialSendNl;
            this.cbSerialSendAddNl.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SerialPlotter.Properties.Settings.Default, "settingSerialSendNl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbSerialSendAddNl.Location = new System.Drawing.Point(574, 147);
            this.cbSerialSendAddNl.Name = "cbSerialSendAddNl";
            this.cbSerialSendAddNl.Size = new System.Drawing.Size(42, 16);
            this.cbSerialSendAddNl.TabIndex = 62;
            this.cbSerialSendAddNl.Text = "+\\n";
            this.cbSerialSendAddNl.UseVisualStyleBackColor = true;
            // 
            // cbSerialSendAddCr
            // 
            this.cbSerialSendAddCr.AutoSize = true;
            this.cbSerialSendAddCr.Checked = global::SerialPlotter.Properties.Settings.Default.settingSerialSendCr;
            this.cbSerialSendAddCr.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SerialPlotter.Properties.Settings.Default, "settingSerialSendCr", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cbSerialSendAddCr.Location = new System.Drawing.Point(528, 147);
            this.cbSerialSendAddCr.Name = "cbSerialSendAddCr";
            this.cbSerialSendAddCr.Size = new System.Drawing.Size(40, 16);
            this.cbSerialSendAddCr.TabIndex = 61;
            this.cbSerialSendAddCr.Text = "+\\r";
            this.cbSerialSendAddCr.UseVisualStyleBackColor = true;
            // 
            // btnSerialSend
            // 
            this.btnSerialSend.Enabled = false;
            this.btnSerialSend.Location = new System.Drawing.Point(621, 143);
            this.btnSerialSend.Name = "btnSerialSend";
            this.btnSerialSend.Size = new System.Drawing.Size(133, 23);
            this.btnSerialSend.TabIndex = 60;
            this.btnSerialSend.Text = "send";
            this.btnSerialSend.UseVisualStyleBackColor = true;
            this.btnSerialSend.Click += new System.EventHandler(this.BtnSerialSend_Click);
            // 
            // tbSerialSend
            // 
            this.tbSerialSend.Location = new System.Drawing.Point(76, 145);
            this.tbSerialSend.Name = "tbSerialSend";
            this.tbSerialSend.Size = new System.Drawing.Size(446, 19);
            this.tbSerialSend.TabIndex = 59;
            this.tbSerialSend.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TbSerialSend_KeyPress);
            // 
            // CbLogWithTime
            // 
            this.CbLogWithTime.AutoSize = true;
            this.CbLogWithTime.Checked = global::SerialPlotter.Properties.Settings.Default.settingLoggingWithTime;
            this.CbLogWithTime.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SerialPlotter.Properties.Settings.Default, "settingLoggingWithTime", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CbLogWithTime.Location = new System.Drawing.Point(449, 118);
            this.CbLogWithTime.Name = "CbLogWithTime";
            this.CbLogWithTime.Size = new System.Drawing.Size(71, 16);
            this.CbLogWithTime.TabIndex = 58;
            this.CbLogWithTime.Text = "with time";
            this.CbLogWithTime.UseVisualStyleBackColor = true;
            // 
            // CbNewLine
            // 
            this.CbNewLine.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SerialPlotter.Properties.Settings.Default, "settingNewLine", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CbNewLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbNewLine.FormattingEnabled = true;
            this.CbNewLine.Location = new System.Drawing.Point(585, 117);
            this.CbNewLine.MaxDropDownItems = 2;
            this.CbNewLine.Name = "CbNewLine";
            this.CbNewLine.Size = new System.Drawing.Size(169, 20);
            this.CbNewLine.TabIndex = 57;
            this.CbNewLine.Text = global::SerialPlotter.Properties.Settings.Default.settingNewLine;
            this.CbNewLine.SelectedIndexChanged += new System.EventHandler(this.CbNewLine_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(526, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 12);
            this.label7.TabIndex = 56;
            this.label7.Text = "NewLine";
            // 
            // TbLogFilePath
            // 
            this.TbLogFilePath.Location = new System.Drawing.Point(132, 116);
            this.TbLogFilePath.Name = "TbLogFilePath";
            this.TbLogFilePath.ReadOnly = true;
            this.TbLogFilePath.Size = new System.Drawing.Size(311, 19);
            this.TbLogFilePath.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 148);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 12);
            this.label10.TabIndex = 55;
            this.label10.Text = "Serial send:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(81, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 12);
            this.label8.TabIndex = 55;
            this.label8.Text = "Log file:";
            // 
            // CbHandshake
            // 
            this.CbHandshake.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SerialPlotter.Properties.Settings.Default, "settingFlowCtrl", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CbHandshake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbHandshake.FormattingEnabled = true;
            this.CbHandshake.Location = new System.Drawing.Point(585, 91);
            this.CbHandshake.MaxDropDownItems = 2;
            this.CbHandshake.Name = "CbHandshake";
            this.CbHandshake.Size = new System.Drawing.Size(169, 20);
            this.CbHandshake.TabIndex = 54;
            this.CbHandshake.Text = global::SerialPlotter.Properties.Settings.Default.settingFlowCtrl;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(526, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 12);
            this.label5.TabIndex = 53;
            this.label5.Text = "FlowCtrl:";
            // 
            // CbLoggingFlag
            // 
            this.CbLoggingFlag.AutoSize = true;
            this.CbLoggingFlag.Location = new System.Drawing.Point(7, 119);
            this.CbLoggingFlag.Name = "CbLoggingFlag";
            this.CbLoggingFlag.Size = new System.Drawing.Size(63, 16);
            this.CbLoggingFlag.TabIndex = 0;
            this.CbLoggingFlag.Text = "Logging";
            this.CbLoggingFlag.UseVisualStyleBackColor = true;
            this.CbLoggingFlag.CheckedChanged += new System.EventHandler(this.CbLoggingFlag_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(526, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 12);
            this.label4.TabIndex = 52;
            this.label4.Text = "StopBit:";
            // 
            // CbStopBit
            // 
            this.CbStopBit.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SerialPlotter.Properties.Settings.Default, "settingStopBit", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CbStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbStopBit.FormattingEnabled = true;
            this.CbStopBit.Location = new System.Drawing.Point(585, 65);
            this.CbStopBit.MaxDropDownItems = 2;
            this.CbStopBit.Name = "CbStopBit";
            this.CbStopBit.Size = new System.Drawing.Size(169, 20);
            this.CbStopBit.TabIndex = 51;
            this.CbStopBit.Text = global::SerialPlotter.Properties.Settings.Default.settingStopBit;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(526, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 12);
            this.label3.TabIndex = 50;
            this.label3.Text = "Parity:";
            // 
            // CbParity
            // 
            this.CbParity.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SerialPlotter.Properties.Settings.Default, "settingParity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbParity.FormattingEnabled = true;
            this.CbParity.Location = new System.Drawing.Point(585, 39);
            this.CbParity.MaxDropDownItems = 2;
            this.CbParity.Name = "CbParity";
            this.CbParity.Size = new System.Drawing.Size(169, 20);
            this.CbParity.TabIndex = 49;
            this.CbParity.Text = global::SerialPlotter.Properties.Settings.Default.settingParity;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(526, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 12);
            this.label2.TabIndex = 48;
            this.label2.Text = "DataBits:";
            // 
            // CbDataBits
            // 
            this.CbDataBits.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SerialPlotter.Properties.Settings.Default, "settingDataBits", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbDataBits.FormattingEnabled = true;
            this.CbDataBits.Location = new System.Drawing.Point(585, 13);
            this.CbDataBits.MaxDropDownItems = 2;
            this.CbDataBits.Name = "CbDataBits";
            this.CbDataBits.Size = new System.Drawing.Size(169, 20);
            this.CbDataBits.TabIndex = 47;
            this.CbDataBits.Text = global::SerialPlotter.Properties.Settings.Default.settingDataBits;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 43;
            this.label1.Text = "Baudrate:";
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(6, 88);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(120, 23);
            this.BtnRefresh.TabIndex = 40;
            this.BtnRefresh.Text = "refresh";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // CbBoudrateList
            // 
            this.CbBoudrateList.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::SerialPlotter.Properties.Settings.Default, "settingBaudrate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.CbBoudrateList.FormattingEnabled = true;
            this.CbBoudrateList.Location = new System.Drawing.Point(191, 90);
            this.CbBoudrateList.Name = "CbBoudrateList";
            this.CbBoudrateList.Size = new System.Drawing.Size(128, 20);
            this.CbBoudrateList.TabIndex = 41;
            this.CbBoudrateList.Text = global::SerialPlotter.Properties.Settings.Default.settingBaudrate;
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(325, 88);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(195, 22);
            this.BtnConnect.TabIndex = 42;
            this.BtnConnect.Text = "connect";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // LbComList
            // 
            this.LbComList.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LbComList.FormattingEnabled = true;
            this.LbComList.ItemHeight = 12;
            this.LbComList.Location = new System.Drawing.Point(6, 18);
            this.LbComList.Name = "LbComList";
            this.LbComList.Size = new System.Drawing.Size(514, 64);
            this.LbComList.TabIndex = 39;
            // 
            // SfdLogging
            // 
            this.SfdLogging.DefaultExt = "txt";
            this.SfdLogging.Filter = "テキストファイル(*.txt)|*.txt|ログファイル(*.log)|*.log|すべてのファイル(*.*)|*.*";
            this.SfdLogging.InitialDirectory = "Environment.SpecialFolder.Desktop";
            // 
            // lGraphFps
            // 
            this.lGraphFps.AutoSize = true;
            this.lGraphFps.Location = new System.Drawing.Point(651, 52);
            this.lGraphFps.Name = "lGraphFps";
            this.lGraphFps.Size = new System.Drawing.Size(23, 12);
            this.lGraphFps.TabIndex = 63;
            this.lGraphFps.Text = "fps:";
            // 
            // dgvGraphWindow
            // 
            this.dgvGraphWindow.AllowUserToAddRows = false;
            this.dgvGraphWindow.AllowUserToDeleteRows = false;
            this.dgvGraphWindow.AllowUserToOrderColumns = true;
            this.dgvGraphWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvGraphWindow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGraphWindow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.series,
            this.graphWindowId});
            this.dgvGraphWindow.Location = new System.Drawing.Point(7, 77);
            this.dgvGraphWindow.Name = "dgvGraphWindow";
            this.dgvGraphWindow.RowHeadersVisible = false;
            this.dgvGraphWindow.RowHeadersWidth = 50;
            this.dgvGraphWindow.RowTemplate.Height = 21;
            this.dgvGraphWindow.Size = new System.Drawing.Size(747, 162);
            this.dgvGraphWindow.TabIndex = 64;
            // 
            // series
            // 
            this.series.Frozen = true;
            this.series.HeaderText = "series";
            this.series.Name = "series";
            // 
            // graphWindowId
            // 
            this.graphWindowId.Frozen = true;
            this.graphWindowId.HeaderText = "graph window id";
            this.graphWindowId.Name = "graphWindowId";
            // 
            // SerialPlotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.GBSerialSettings);
            this.Controls.Add(this.GBPlotSettings);
            this.Controls.Add(this.menuStrip1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::SerialPlotter.Properties.Settings.Default, "WindowLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::SerialPlotter.Properties.Settings.Default.WindowLocation;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(750, 330);
            this.Name = "SerialPlotter";
            this.Text = "SerialPlotter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.GBPlotSettings.ResumeLayout(false);
            this.GBPlotSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarPlotTime)).EndInit();
            this.GBSerialSettings.ResumeLayout(false);
            this.GBSerialSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGraphWindow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ファイルFToolStripMenuItem;
        private System.Windows.Forms.GroupBox GBPlotSettings;
        private System.Windows.Forms.Button BtnPlotStart;
        private System.Windows.Forms.GroupBox GBSerialSettings;
        private System.Windows.Forms.ComboBox CbHandshake;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox CbStopBit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CbParity;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CbDataBits;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnRefresh;
        private System.Windows.Forms.ComboBox CbBoudrateList;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.ListBox LbComList;
        private System.Windows.Forms.Button BtnPlotReset;
        private System.Windows.Forms.Label LabelPoltPoint;
        private System.Windows.Forms.CheckBox CbLoggingFlag;
        private System.Windows.Forms.SaveFileDialog SfdLogging;
        private System.Windows.Forms.TextBox TbLogFilePath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox CbNewLine;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripMenuItem 書式SToolStripMenuItem;
        private System.Windows.Forms.CheckBox CbLogWithTime;
        private System.Windows.Forms.ToolStripMenuItem ウィンドウToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataTableToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbPlotMarker;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbChartRefreshRate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar TrackBarPlotTime;
        private System.Windows.Forms.CheckBox cbBufferFullScale;
        private System.Windows.Forms.Button btnSerialSend;
        private System.Windows.Forms.TextBox tbSerialSend;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox cbSerialSendAddNl;
        private System.Windows.Forms.CheckBox cbSerialSendAddCr;
        private System.Windows.Forms.Label lGraphFps;
        private System.Windows.Forms.DataGridView dgvGraphWindow;
        private System.Windows.Forms.DataGridViewTextBoxColumn series;
        private System.Windows.Forms.DataGridViewComboBoxColumn graphWindowId;
    }
}

