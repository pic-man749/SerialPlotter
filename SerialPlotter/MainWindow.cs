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

        const char IGNORE_START_CHAR = ';';
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

            // init chart area
            graph.Add(new GraphWindow(graphCounte++,
                                    TrackBarPlotTime.Value,
                                    cbPlotMarker.Checked,
                                    cbBufferFullScale.Checked,
                                    TrackBarPlotTime.Maximum ));
        }
        // get COM port name and refresh ListBox
        private void GetNowConnectedSerialPorts() {
            // clear ListBox
            LbComList.Items.Clear();
            // get COMs with device name
            LbComList.Items.AddRange(serial.getComPorts().Values.ToArray());
            if(0 < LbComList.Items.Count) {
                LbComList.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Serial受信コールバック関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThreadTaskSerialRecv() {
            // get recved time
            DataParser parser = new DataParser();

            while(serial.IsOpen) {
                string data;
                double recvTime;
                try {
                    data = serial.ReadLine();
                    recvTime = stopWatch.Elapsed.TotalSeconds;
                } catch(TimeoutException) {
                    continue;
                } catch {
                    return;
                }

                // output log file
                if(logFileStream != null) {
                    if(CbLogWithTime.Checked) {
                        byte[] logTime = Encoding.GetEncoding("UTF-8").GetBytes("[" + recvTime.ToString("0.000000") + "]");
                        logFileStream.Write(logTime, 0, logTime.Length);
                    }
                    byte[] byteData = Encoding.GetEncoding("UTF-8").GetBytes(data + newLine);
                    logFileStream.Write(byteData, 0, byteData.Length);
                    logFileStream.FlushAsync();
                }

                // if no data or comment, then skip
                if(data == string.Empty) continue;
                if(data[0] == IGNORE_START_CHAR) continue;

                // parse
                Dictionary<string, double> kvs = parser.parse(data);

                foreach(string key in kvs.Keys) {
                    // insert new data
                    dataManager.InsertData(recvTime, key, kvs[key]);

                    // plot
                    if(!isPlotting) {
                        continue;
                    }
                    graph[0].AddSeriesToChart(recvTime, key, kvs[key]);

                    // is new key? then add btn
                    if(!knownKeyList.Contains(key)) {
                        knownKeyList.Add(key);
                        AddNewSeries(key);
                    }
                }
            }
        }

        private void UpdateChart(Object source, ElapsedEventArgs e) {
            double now = stopWatch.Elapsed.TotalSeconds;
            foreach(var g in graph) {
                g.UpdateChart(now);
            }
        }

        private int GetTrackBarPlotTime() {

            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { GetTrackBarPlotTime(); });
                return 10;
            } else {
                return TrackBarPlotTime.Value;
            }
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

        private void ChangeSerialParamUI(bool isComOpen) {
            // open -> disable
            LbComList.Enabled = !isComOpen;
            CbBoudrateList.Enabled = !isComOpen;
            CbDataBits.Enabled = !isComOpen;
            CbParity.Enabled = !isComOpen;
            CbStopBit.Enabled = !isComOpen;
            CbHandshake.Enabled = !isComOpen;
            CbNewLine.Enabled = !isComOpen;
            // open -> enable
            BtnPlotStart.Enabled = isComOpen;
            btnSerialSend.Enabled = isComOpen;
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

        /// <summary>
        /// リフレッシュレートを周期(ms)で返す関数
        /// </summary>
        /// <returns></returns>
        private float GetChartRefreshRatePeriod() {
            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { GetChartRefreshRatePeriod(); });
                return 0.0f;    //dummy
            } else {
                string hzValStr = cbChartRefreshRate.SelectedItem.ToString();
                float hzVal = float.Parse(hzValStr);
                return 1000.0f / hzVal;
            }
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
                g.SetYScale(min, max);
            }
        }

        private void ChangeSeriesCheckBox(object sender, EventArgs e) {
            var s = (CheckBox)sender;
            graph[0].SetSeriesEnable(s.Text, s.Checked);
        }

        private Dictionary<string, CheckBox> seriesEnableCbDict = new Dictionary<string, CheckBox>();

        private void AddNewSeries(string key) {
            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { AddNewSeries(key); });
            } else {
                int addBtnIdx = seriesEnableCbDict.Count + 1;
                // add row
                if(addBtnIdx % tblSeries.ColumnCount == 0) {
                    // add new row
                    tblSeries.RowCount += 1;
                    tblSeries.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
                    // add new col
                    for(int i = 0; i < tblSeries.ColumnCount; i++) {
                        tblSeries.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
                    }
                }
                if(!seriesEnableCbDict.Keys.Contains(key)) {
                    // make new CheckBox and add Dict
                    CheckBox cb = new CheckBox();
                    cb.Text = key;
                    cb.Checked = true;
                    cb.CheckedChanged += ChangeSeriesCheckBox;
                    seriesEnableCbDict.Add(key, cb);
                    // put checkbox to table
                    int putCol = addBtnIdx % (tblSeries.ColumnCount) - 1;
                    tblSeries.Controls.Add(cb, putCol, tblSeries.RowCount - 1);
                } else {
                    graph[0].SetSeriesEnable(key, seriesEnableCbDict[key].Checked);
                }
            }
        }

        private void DockingGraphWindowEventCallback(object sender, EventArgs e) {
            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { DockingGraphWindowEventCallback(sender, e); });
            } else {
                if(cbDockingGeaphWindow.Checked) {
                    graph[0].Activate();
                    int x = this.Location.X;
                    int y = this.Location.Y + this.Height;
                    graph[0].Location = new Point(x, y);
                    graph[0].Width = this.Width;
                }
            }
        }
    }
}
