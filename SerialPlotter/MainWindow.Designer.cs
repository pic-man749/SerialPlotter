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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ファイルFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.書式SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ウィンドウToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GBPlotSettings = new System.Windows.Forms.GroupBox();
            this.LabelPoltPoint = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TrackBarPlotPoint = new System.Windows.Forms.TrackBar();
            this.BtnPlotReset = new System.Windows.Forms.Button();
            this.BtnPlotStart = new System.Windows.Forms.Button();
            this.GBSerialSettings = new System.Windows.Forms.GroupBox();
            this.CbLogWithTime = new System.Windows.Forms.CheckBox();
            this.CbNewLine = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TbLogFilePath = new System.Windows.Forms.TextBox();
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
            this.ChartDefault = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.SfdLogging = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1.SuspendLayout();
            this.GBPlotSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarPlotPoint)).BeginInit();
            this.GBSerialSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartDefault)).BeginInit();
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
            this.dataTableToolStripMenuItem.Click += new System.EventHandler(this.dataTableToolStripMenuItem_Click);
            // 
            // GBPlotSettings
            // 
            this.GBPlotSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GBPlotSettings.Controls.Add(this.LabelPoltPoint);
            this.GBPlotSettings.Controls.Add(this.label6);
            this.GBPlotSettings.Controls.Add(this.TrackBarPlotPoint);
            this.GBPlotSettings.Controls.Add(this.BtnPlotReset);
            this.GBPlotSettings.Controls.Add(this.BtnPlotStart);
            this.GBPlotSettings.Location = new System.Drawing.Point(13, 201);
            this.GBPlotSettings.Name = "GBPlotSettings";
            this.GBPlotSettings.Size = new System.Drawing.Size(759, 76);
            this.GBPlotSettings.TabIndex = 49;
            this.GBPlotSettings.TabStop = false;
            this.GBPlotSettings.Text = "Plot settings";
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
            this.label6.Size = new System.Drawing.Size(55, 12);
            this.label6.TabIndex = 55;
            this.label6.Text = "plot point:";
            // 
            // TrackBarPlotPoint
            // 
            this.TrackBarPlotPoint.Location = new System.Drawing.Point(193, 18);
            this.TrackBarPlotPoint.Maximum = 200;
            this.TrackBarPlotPoint.Minimum = 10;
            this.TrackBarPlotPoint.Name = "TrackBarPlotPoint";
            this.TrackBarPlotPoint.Size = new System.Drawing.Size(200, 45);
            this.TrackBarPlotPoint.TabIndex = 57;
            this.TrackBarPlotPoint.TickFrequency = 10;
            this.TrackBarPlotPoint.Value = 50;
            this.TrackBarPlotPoint.ValueChanged += new System.EventHandler(this.TrackBarPlotPoint_ValueChanged);
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
            this.GBSerialSettings.Controls.Add(this.CbLogWithTime);
            this.GBSerialSettings.Controls.Add(this.CbNewLine);
            this.GBSerialSettings.Controls.Add(this.label7);
            this.GBSerialSettings.Controls.Add(this.TbLogFilePath);
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
            this.GBSerialSettings.Size = new System.Drawing.Size(760, 168);
            this.GBSerialSettings.TabIndex = 50;
            this.GBSerialSettings.TabStop = false;
            this.GBSerialSettings.Text = "Serial settings";
            // 
            // CbLogWithTime
            // 
            this.CbLogWithTime.AutoSize = true;
            this.CbLogWithTime.Location = new System.Drawing.Point(457, 142);
            this.CbLogWithTime.Name = "CbLogWithTime";
            this.CbLogWithTime.Size = new System.Drawing.Size(71, 16);
            this.CbLogWithTime.TabIndex = 58;
            this.CbLogWithTime.Text = "with time";
            this.CbLogWithTime.UseVisualStyleBackColor = true;
            // 
            // CbNewLine
            // 
            this.CbNewLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbNewLine.FormattingEnabled = true;
            this.CbNewLine.Location = new System.Drawing.Point(585, 117);
            this.CbNewLine.MaxDropDownItems = 2;
            this.CbNewLine.Name = "CbNewLine";
            this.CbNewLine.Size = new System.Drawing.Size(169, 20);
            this.CbNewLine.TabIndex = 57;
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
            this.TbLogFilePath.Location = new System.Drawing.Point(132, 140);
            this.TbLogFilePath.Name = "TbLogFilePath";
            this.TbLogFilePath.ReadOnly = true;
            this.TbLogFilePath.Size = new System.Drawing.Size(319, 19);
            this.TbLogFilePath.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(97, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 55;
            this.label8.Text = "path:";
            // 
            // CbHandshake
            // 
            this.CbHandshake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbHandshake.FormattingEnabled = true;
            this.CbHandshake.Location = new System.Drawing.Point(585, 91);
            this.CbHandshake.MaxDropDownItems = 2;
            this.CbHandshake.Name = "CbHandshake";
            this.CbHandshake.Size = new System.Drawing.Size(169, 20);
            this.CbHandshake.TabIndex = 54;
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
            this.CbLoggingFlag.Location = new System.Drawing.Point(7, 142);
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
            this.CbStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbStopBit.FormattingEnabled = true;
            this.CbStopBit.Location = new System.Drawing.Point(585, 65);
            this.CbStopBit.MaxDropDownItems = 2;
            this.CbStopBit.Name = "CbStopBit";
            this.CbStopBit.Size = new System.Drawing.Size(169, 20);
            this.CbStopBit.TabIndex = 51;
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
            this.CbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbParity.FormattingEnabled = true;
            this.CbParity.Location = new System.Drawing.Point(585, 39);
            this.CbParity.MaxDropDownItems = 2;
            this.CbParity.Name = "CbParity";
            this.CbParity.Size = new System.Drawing.Size(169, 20);
            this.CbParity.TabIndex = 49;
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
            this.CbDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CbDataBits.FormattingEnabled = true;
            this.CbDataBits.Location = new System.Drawing.Point(585, 13);
            this.CbDataBits.MaxDropDownItems = 2;
            this.CbDataBits.Name = "CbDataBits";
            this.CbDataBits.Size = new System.Drawing.Size(169, 20);
            this.CbDataBits.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 43;
            this.label1.Text = "Baudrate:";
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Location = new System.Drawing.Point(6, 112);
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.Size = new System.Drawing.Size(120, 23);
            this.BtnRefresh.TabIndex = 40;
            this.BtnRefresh.Text = "refresh";
            this.BtnRefresh.UseVisualStyleBackColor = true;
            this.BtnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // CbBoudrateList
            // 
            this.CbBoudrateList.FormattingEnabled = true;
            this.CbBoudrateList.Location = new System.Drawing.Point(191, 114);
            this.CbBoudrateList.Name = "CbBoudrateList";
            this.CbBoudrateList.Size = new System.Drawing.Size(128, 20);
            this.CbBoudrateList.TabIndex = 41;
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(325, 112);
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
            this.LbComList.Size = new System.Drawing.Size(514, 88);
            this.LbComList.TabIndex = 39;
            // 
            // ChartDefault
            // 
            this.ChartDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelAutoFitMinFontSize = 10;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.MaximumAutoSize = 100F;
            chartArea1.AxisX.MinorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.MinorTickMark.Enabled = true;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.LabelAutoFitMinFontSize = 10;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MaximumAutoSize = 80F;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.MinorTickMark.Enabled = true;
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.IsSameFontSizeForAllAxes = true;
            chartArea1.Name = "ChartAreaDefault";
            this.ChartDefault.ChartAreas.Add(chartArea1);
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.ChartDefault.Legends.Add(legend1);
            this.ChartDefault.Location = new System.Drawing.Point(13, 283);
            this.ChartDefault.Name = "ChartDefault";
            series1.ChartArea = "ChartAreaDefault";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.ChartDefault.Series.Add(series1);
            this.ChartDefault.Size = new System.Drawing.Size(759, 366);
            this.ChartDefault.TabIndex = 48;
            this.ChartDefault.Text = "chart1";
            this.ChartDefault.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            // 
            // SfdLogging
            // 
            this.SfdLogging.DefaultExt = "txt";
            this.SfdLogging.Filter = "テキストファイル(*.txt)|*.txt|ログファイル(*.log)|*.log|すべてのファイル(*.*)|*.*";
            this.SfdLogging.InitialDirectory = "Environment.SpecialFolder.Desktop";
            // 
            // SerialPlotter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.GBSerialSettings);
            this.Controls.Add(this.GBPlotSettings);
            this.Controls.Add(this.ChartDefault);
            this.Controls.Add(this.menuStrip1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::SerialPlotter.Properties.Settings.Default, "WindowLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::SerialPlotter.Properties.Settings.Default.WindowLocation;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(750, 600);
            this.Name = "SerialPlotter";
            this.Text = "SerialPlotter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.GBPlotSettings.ResumeLayout(false);
            this.GBPlotSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBarPlotPoint)).EndInit();
            this.GBSerialSettings.ResumeLayout(false);
            this.GBSerialSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChartDefault)).EndInit();
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
        private System.Windows.Forms.TrackBar TrackBarPlotPoint;
        private System.Windows.Forms.Label LabelPoltPoint;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataVisualization.Charting.Chart ChartDefault;
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
    }
}

