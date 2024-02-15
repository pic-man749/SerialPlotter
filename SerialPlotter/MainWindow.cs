using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
using System.Xml;
using System.Timers;
using System.Threading;
using System.Drawing.Drawing2D;

namespace SerialPlotter {
    public partial class SerialPlotter : Form {

        private Serial serial = new Serial();

        private System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        private bool isPlotting = false;
        private Stream logFileStream = null;
        private string newLine = String.Empty;
        private DataManager dataManager = new DataManager();

        private System.Timers.Timer chartRefreshTimer = new System.Timers.Timer();

        private List<GraphWindow> graph = new List<GraphWindow>();
        private int graphCounte = 0;

        private double lastChartTime = 0;

        private List<string> knownKeyList = new List<string>();
        private Dictionary<string, CheckBox> seriesEnableCbDict = new Dictionary<string, CheckBox>();
        private Dictionary<string, CheckBox> seriesUse2ndYAxis = new Dictionary<string, CheckBox>();
        private Dictionary<string, Label> seriesLatestValue = new Dictionary<string, Label>();

        public SerialPlotter() {
            InitializeComponent();
            // show Serial Ports
            GetNowConnectedSerialPorts();
            // set combBoxes
            CbBoudrateList.Items.AddRange(Serial.getBoudrates().ToArray());
            CbBoudrateList.SelectedIndex = 1;   // select 9600

            CbDataBits.Items.AddRange(Serial.getDataBits().ToArray());
            CbDataBits.SelectedIndex = 0;       // select 8bit

            CbParity.Items.AddRange(Serial.getParity().ToArray());
            CbParity.SelectedIndex = 0;         // none

            CbStopBit.Items.AddRange(Serial.getStopBits().ToArray());
            CbStopBit.SelectedIndex = 1;        // One

            CbHandshake.Items.AddRange(Serial.getHandshake().ToArray());
            CbHandshake.SelectedIndex = 0;      // none

            CbNewLine.Items.AddRange(Serial.getNewLines().ToArray());
            CbNewLine.SelectedIndex = 0;        // \n
            newLine = "\n";
            // timeout
            serial.ReadTimeout = 50;

            // set range value
            LabelPoltPoint.Text = TrackBarPlotTime.Value.ToString();

            // select chart refresh rate default
            cbChartRefreshRate.SelectedIndex = 4;   // 30
            // set chart update timer
            chartRefreshTimer.Elapsed += UpdateChart;
            chartRefreshTimer.Interval = GetChartRefreshRatePeriod();

            // init tb
            if(cbAutoScale.Checked) {
                tbYMax.Enabled = false;
                tbYMin.Enabled = false;
            }
            if(cbAutoScale2nd.Checked) {
                tbY2ndMax.Enabled = false;
                tbY2ndMin.Enabled = false;
            }

            // init chart area
            graph.Add(new GraphWindow(graphCounte++,
                                    TrackBarPlotTime.Value,
                                    cbPlotMarker.Checked,
                                    cbBufferFullScale.Checked,
                                    TrackBarPlotTime.Maximum ));
        }

        private void BtnRefresh_Click(object sender, EventArgs e) {
            GetNowConnectedSerialPorts();
        }

        private void BtnConnect_Click(object sender, EventArgs e) {
            if(BtnConnect.Text == "connect") {

                // check com id
                try {
                    serial.PortName = serial.getComPorts().Keys.ToArray()[LbComList.SelectedIndex];
                } catch {
                    General.ShowErrMsgBox("Cannot access selected COM port");
                    return;
                }

                // check speed
                try {
                    serial.BaudRate = int.Parse(CbBoudrateList.Text);
                } catch {
                    General.ShowErrMsgBox("Invalid Baudrate. Please select or input number.");
                    return;
                }

                switch(CbDataBits.SelectedIndex) {
                    case 0:
                        serial.DataBits = 8;
                        break;
                    case 1:
                        serial.DataBits = 7;
                        break;
                }

                serial.Parity = (System.IO.Ports.Parity)CbParity.SelectedIndex;
                serial.StopBits = (System.IO.Ports.StopBits)CbStopBit.SelectedIndex;
                serial.Handshake = (System.IO.Ports.Handshake)CbHandshake.SelectedIndex;

                foreach(var g in graph) {
                    g.ClearChart();
                }
                dataManager.ClearDataTable();

                try {
                    serial.Open();
                } catch {
                    General.ShowErrMsgBox("Cannot open selected COM port.");
                    return;
                }

                // reset
                knownKeyList.Clear();
                ResetTimer();
                StartTimer();
                // start plot
                BtnPlotStart_Click(sender, e);
                BtnConnect.Text = "close";
                dataTableToolStripMenuItem.Enabled = false;

                // ready for recv thread
                Thread _ = new Thread(new ThreadStart(this.ThreadTaskSerialRecv));
                _.IsBackground = true;
                // read dust data before plotting
                try {
                    serial.ReadExisting();
                    serial.ReadLine();
                } catch(TimeoutException) {
                    ;
                }
                // start thread
                _.Start();

            } else if(BtnConnect.Text == "close") {
                if(isPlotting) {
                    // stop plot
                    BtnPlotStart_Click(sender, e);
                }
                BtnConnect.Text = "connect";
                dataTableToolStripMenuItem.Enabled = true;
                serial.Close();
                StopTimer();
            }
            ChangeSerialParamUI(serial.IsOpen);
        }

        private void BtnPlotStart_Click(object sender, EventArgs e) {
            isPlotting = !isPlotting;
            cbChartRefreshRate.Enabled = !isPlotting;
            lastChartTime = stopWatch.Elapsed.TotalSeconds;
            if(isPlotting) {
                BtnPlotStart.Text = "plot stop";
                chartRefreshTimer.Interval = GetChartRefreshRatePeriod();
                chartRefreshTimer.Start();
            } else {
                BtnPlotStart.Text = "plot start";
                chartRefreshTimer.Stop();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            isPlotting = false;
            chartRefreshTimer.Stop();
            if(serial.IsOpen) {
                serial.Close();
            }
            if(logFileStream != null) {
                logFileStream.Close();
            }
            Properties.Settings.Default.Save();
            // wait for serial timeout
            Task.Delay(serial.ReadTimeout*2);
        }

        private void MainWindow_Shown(object sender, EventArgs e) {

            // window position setting
            int needRestorePosition = 0;
            foreach(Screen scr in Screen.AllScreens) {
                if(scr.WorkingArea.Contains(this.Location.X, this.Location.Y)) {
                    needRestorePosition++;
                }
            }
            if(needRestorePosition == 0) {
                this.Location = new System.Drawing.Point(100, 100);
            }
        }

        public bool StartTimer() {
            stopWatch.Start();
            return true;
        }

        public bool StopTimer() {
            stopWatch.Stop();
            return true;
        }

        public bool ResetTimer() {
            if(stopWatch.IsRunning) {
                stopWatch.Restart();
            } else {
                stopWatch.Reset();
            }
            return true;
        }

        private void BtnPlotReset_Click(object sender, EventArgs e) {
            foreach(var g in graph) {
                g.ClearChart();
            }
            knownKeyList.Clear();
        }

        private void TrackBarPlotPoint_ValueChanged(object sender, EventArgs e) {
            int range = TrackBarPlotTime.Value;
            LabelPoltPoint.Text = range.ToString();
            double now = stopWatch.Elapsed.TotalSeconds;
            if(!isPlotting) {
                now = lastChartTime;
            }
            foreach(var g in graph) {
                g.ChangePlotRange(now, range);
            }

        }

        private void CbLoggingFlag_CheckedChanged(object sender, EventArgs e) {
            bool isLoggingEnabled = CbLoggingFlag.Checked;

            // check(first)
            if(isLoggingEnabled) {
                SfdLogging.FileName = String.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now);
                if(SfdLogging.ShowDialog() == DialogResult.OK) {
                    if((logFileStream = SfdLogging.OpenFile()) != null) {
                        TbLogFilePath.Text = SfdLogging.FileNames[0];
                    }
                }
            } else {
                TbLogFilePath.Text = string.Empty;
                if(logFileStream != null) {
                    logFileStream.Close();
                    logFileStream = null;
                }
            }
        }

        private void CbNewLine_SelectedIndexChanged(object sender, EventArgs e) {
            switch(CbNewLine.SelectedItem) {
                case "\\n":
                    serial.setNewLine("\n");
                    newLine = "\n";
                    break;
                case "\\r":
                    serial.setNewLine("\r");
                    newLine = "\r";
                    break;
                case "\\r\\n":
                    serial.setNewLine("\r\n");
                    newLine = "\r\n";
                    break;
            }
        }

        private void 書式SToolStripMenuItem_Click(object sender, EventArgs e) {
            FormatWindow _ = new FormatWindow();
        }

        private void DataTableToolStripMenuItem_Click(object sender, EventArgs e) {
            DataTableWindow _ = new DataTableWindow(dataManager.GetDataSource());
        }

        private void CbPlotMarker_CheckedChanged(object sender, EventArgs e) {
            foreach(var g in graph) {
                g.ChangeMarkerStyle(cbPlotMarker.Checked);
            }
        }

        private void BtnSerialSend_Click(object sender, EventArgs e) {
            if(!serial.IsOpen) {
                return;
            }
            Byte[] payload;
            try {
                payload = General.String2Bytes(tbSerialSend.Text, false, cbSerialSendAddCr.Checked, cbSerialSendAddNl.Checked);
            } catch(Exception err) {
                General.ShowErrMsgBox(err.Message);
                return;
            }
            serial.Write(payload, 0, payload.Length);
        }

        private void TbSerialSend_KeyPress(object sender, KeyPressEventArgs e) {
            if(e.KeyChar == '\r') {
                // disable beep
                e.Handled = true;
                // call send
                BtnSerialSend_Click(sender, e);
            }
        }

        private void CbBufferFullScale_CheckedChanged(object sender, EventArgs e) {
            foreach(var g in graph) {
                g.SetIsFullScaleBuffer(cbBufferFullScale.Checked);
            }
        }

        private void SetYScale(object sender, EventArgs e) {
            double min = double.NaN;
            double max = double.NaN;
            if(!cbAutoScale.Checked) {
                tbYMax.Enabled = true;
                tbYMin.Enabled = true;
                try {
                    // parse and validation
                    double _min = double.Parse(tbYMin.Text);
                    double _max = double.Parse(tbYMax.Text);
                    if(_min < _max) {
                        min = _min;
                        max = _max;
                    }
                } catch {
                    min = double.NaN;
                    max = double.NaN;
                }
            } else {
                tbYMax.Enabled = false;
                tbYMin.Enabled = false;
            }
            foreach(var g in graph) {
                g.SetYScale(min, max ,false);
            }
        }
        private void Set2ndYScale(object sender, EventArgs e) {
            double min = double.NaN;
            double max = double.NaN;
            if(!cbAutoScale2nd.Checked) {
                tbY2ndMin.Enabled = true;
                tbY2ndMax.Enabled = true;
                try {
                    // parse and validation
                    double _min = double.Parse(tbY2ndMin.Text);
                    double _max = double.Parse(tbY2ndMax.Text);
                    if(_min < _max) {
                        min = _min;
                        max = _max;
                    }
                } catch {
                    min = double.NaN;
                    max = double.NaN;
                }
            } else {
                tbY2ndMax.Enabled = false;
                tbY2ndMin.Enabled = false;
            }
            foreach(var g in graph) {
                g.SetYScale(min, max, true);
            }
        }

        private void ChangeSeriesVisibleCb(object sender, EventArgs e) {
            var s = (CheckBox)sender;
            foreach(var g in graph) {
                g.SetSeriesEnable(s.Name, s.Checked);
            }
        }

        private void ChangeSeries2ndAxisCb(object sender, EventArgs e) {
            var s = (CheckBox)sender;
            foreach(var g in graph) {
                g.ChangePlotAxis(s.Name, s.Checked);
            }
        }

        private void DockingGraphWindowEventCallback(object sender, EventArgs e) {
            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { DockingGraphWindowEventCallback(sender, e); });
            } else {
                if(cbDockingGeaphWindow.Checked) {
                    foreach(var g in graph) {
                        g.Activate();
                        int x = this.Location.X;
                        int y = this.Location.Y + this.Height;
                        g.Location = new Point(x, y);
                        g.Width = this.Width;
                    }
                }
            }
        }

        private void cbDockingGeaphWindow_CheckedChanged(object sender, EventArgs e) {
            if(cbDockingGeaphWindow.Checked) {
                DockingGraphWindowEventCallback(sender, e);
            }
        }

        private void btnDetectedSeriesClear_Click(object sender, EventArgs e) {
            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { btnDetectedSeriesClear_Click(sender, e); });
            } else {
                // reset
                this.tblSeries.Controls.Clear();
                this.tblSeries.RowStyles.Clear();
                this.tblSeries.RowCount = 1;
                this.tblSeries.Controls.Add(this.lLatestValue, 3, 0);
                this.tblSeries.Controls.Add(this.lUseRightYAxis, 2, 0);
                this.tblSeries.Controls.Add(this.lVisible, 1, 0);
                this.tblSeries.Controls.Add(this.lSeriesName, 0, 0);
                // チェックボックスが外れた状態でクリアすると次回更新時該当グラフが表示されないためチェック状態にする
                btnDetectedSeriesAllCheck_Click(sender, e);
                this.knownKeyList.Clear();
                this.seriesEnableCbDict.Clear();
            }
        }

        private void btnDetectedSeriesAllCheck_Click(object sender, EventArgs e) {
            foreach(var cb in seriesEnableCbDict.Values) {
                cb.Checked = true;
            }
        }

        private void btnDetectedSeriesAllUncheck_Click(object sender, EventArgs e) {
            foreach(var cb in seriesEnableCbDict.Values) {
                cb.Checked = false;
            }
        }
    }
}
