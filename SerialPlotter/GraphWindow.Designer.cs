namespace SerialPlotter {
    partial class GraphWindow {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartDefault = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslFps = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.chartDefault)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartDefault
            // 
            this.chartDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.LabelAutoFitMinFontSize = 10;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisX.Maximum = 0D;
            chartArea1.AxisX.MaximumAutoSize = 100F;
            chartArea1.AxisX.Minimum = -10D;
            chartArea1.AxisX.MinorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.MinorTickMark.Enabled = true;
            chartArea1.AxisX.Title = "[s]";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.LabelAutoFitMinFontSize = 10;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY.MaximumAutoSize = 100F;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.MinorTickMark.Enabled = true;
            chartArea1.AxisY2.IsLabelAutoFit = false;
            chartArea1.AxisY2.IsStartedFromZero = false;
            chartArea1.AxisY2.LabelAutoFitMinFontSize = 10;
            chartArea1.AxisY2.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.Gray;
            chartArea1.AxisY2.MaximumAutoSize = 100F;
            chartArea1.AxisY2.MinorGrid.Enabled = true;
            chartArea1.AxisY2.MinorGrid.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY2.MinorTickMark.Enabled = true;
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 85F;
            chartArea1.InnerPlotPosition.Width = 94F;
            chartArea1.InnerPlotPosition.X = 6F;
            chartArea1.InnerPlotPosition.Y = 2F;
            chartArea1.IsSameFontSizeForAllAxes = true;
            chartArea1.Name = "ChartAreaDefault";
            this.chartDefault.ChartAreas.Add(chartArea1);
            legend1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            legend1.IsTextAutoFit = false;
            legend1.Name = "Legend1";
            this.chartDefault.Legends.Add(legend1);
            this.chartDefault.Location = new System.Drawing.Point(0, 0);
            this.chartDefault.Name = "chartDefault";
            series1.ChartArea = "ChartAreaDefault";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartDefault.Series.Add(series1);
            this.chartDefault.Size = new System.Drawing.Size(784, 340);
            this.chartDefault.TabIndex = 49;
            this.chartDefault.Text = "chart1";
            this.chartDefault.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Chart_MouseMove);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslFps});
            this.statusStrip1.Location = new System.Drawing.Point(0, 339);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 50;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslFps
            // 
            this.tsslFps.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tsslFps.Name = "tsslFps";
            this.tsslFps.Size = new System.Drawing.Size(29, 17);
            this.tsslFps.Text = "fps:";
            // 
            // GraphWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 361);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.chartDefault);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::SerialPlotter.Properties.Settings.Default, "settingGraphWIndowLocation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::SerialPlotter.Properties.Settings.Default.settingGraphWIndowLocation;
            this.Name = "GraphWindow";
            this.Text = "GraphWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GraphWindow_FormClosing);
            this.Shown += new System.EventHandler(this.GraphWindow_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.chartDefault)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartDefault;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslFps;
    }
}