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
        private int graphCounter = 0;

        private Dictionary<string, int> knownSeriesNameDict = new Dictionary<string,int >();
        private DataTable dtGraphWindow = new DataTable();

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

            // set range value
            LabelPoltPoint.Text = TrackBarPlotTime.Value.ToString();

            // select chart refresh rate default
            cbChartRefreshRate.SelectedIndex = 4;   // 30
            // set chart update timer
            chartRefreshTimer.Elapsed += UpdateChart;
            chartRefreshTimer.Interval = GetChartRefreshRatePeriod();

            // init series table
            dtGraphWindow.Columns.Add("id");
            dtGraphWindow.Columns.Add("displayName");
            DataRow dr = dtGraphWindow.NewRow();
            dr["id"] = graphCounter;
            dr["displayName"] = graphCounter.ToString();
            dtGraphWindow.Rows.Add(dr);
            DataGridViewComboBoxColumn colGraphWindowId = new DataGridViewComboBoxColumn();
            colGraphWindowId.DataPropertyName = "graphWindowId";
            colGraphWindowId.DataSource = dtGraphWindow;
            colGraphWindowId.ValueMember = "id";
            colGraphWindowId.DisplayMember = "displayName";
            colGraphWindowId.Name = "graph window id";
            // insert after 0 : "series"
            dgvGraphWindow.Columns.Insert(1, colGraphWindowId);

            // init chart area
            graph.Add(new GraphWindow(graphCounter++,
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
        private void SerialDataReceivedEventHandler(object sender, SerialDataReceivedEventArgs e) {
            // get recved time
            double recvTime = stopWatch.Elapsed.TotalSeconds;
            while(serial.IsOpen && serial.BytesToRead > 0) {
                string data;
                try {
                    data = serial.ReadLine();
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
                DataParser parser = new DataParser();
                Dictionary<string, double> kvs = parser.parse(data);

                foreach(string key in kvs.Keys) {
                    // insert series table
                    if(!knownSeriesNameDict.Keys.Contains(key)) {
                        knownSeriesNameDict.Add(key, 0);
                        AddSeries(key);
                    }

                    // insert new data
                    dataManager.InsertRecvedData(recvTime, key, kvs[key]);

                    // plot
                    if(!isPlotting) {
                        return;
                    }
                    int id = knownSeriesNameDict[key];
                    foreach(var g in graph) {
                        if(g.GID == id) {
                            g.AddSeriesToChart(recvTime, key, kvs[key]);
                        }
                    }
                }
            }
        }

        private double lastUpdateTime = 0;

        private void UpdateChart(Object source, ElapsedEventArgs e) {
            double now = stopWatch.Elapsed.TotalSeconds;
            foreach(var g in graph) {
                g.UpdateChart(now);
            }
            // show fps
            UpdateGraphFps(1.0 / (now - lastUpdateTime));
            lastUpdateTime = now;
        }
        private void AddSeries(string key) {
            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { AddSeries(key); });
            } else {
                int row = dgvGraphWindow.Rows.Add(key);
                dgvGraphWindow[1, row].Value = "0"; // default window id
            }
        }

        private void AddGraphWindowId(int id) {
            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { AddGraphWindowId(id); });
            } else {
                DataRow dr = dtGraphWindow.NewRow();
                dr["id"] = id;
                dr["displayName"] = id.ToString();
                dtGraphWindow.Rows.Add(dr);
            }
        }

        private void UpdateGraphFps(double fps) {
            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { UpdateGraphFps(fps); });
            } else {
                lGraphFps.Text = $"fps:{fps:00.00}";
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
                ResetTimer();
                StartTimer();
                // start plot
                BtnPlotStart_Click(sender, e);
                BtnConnect.Text = "close";
                dataTableToolStripMenuItem.Enabled = false;
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
        }

        private void Form1_Shown(object sender, EventArgs e) {
            // set DataRecv EventHandler
            serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.SerialDataReceivedEventHandler);

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
        }

        private void TrackBarPlotPoint_ValueChanged(object sender, EventArgs e) {
            int range = TrackBarPlotTime.Value;
            LabelPoltPoint.Text = range.ToString();
            double now = stopWatch.Elapsed.TotalSeconds;
            foreach(var g in graph) {
                g.ChangePlotRange(now, range);
            }
        }

        private void CbLoggingFlag_CheckedChanged(object sender, EventArgs e) {
            bool isLoggingEnabled = CbLoggingFlag.Checked;

            // check(first)
            if(isLoggingEnabled) {
                sfdLogging.FileName = String.Format("{0:yyyyMMdd_HHmmss}", DateTime.Now);
                if(sfdLogging.ShowDialog() == DialogResult.OK) {
                    if((logFileStream = sfdLogging.OpenFile()) != null) {
                        TbLogFilePath.Text = sfdLogging.FileNames[0];
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
            RecvedDataTableWindow _ = new RecvedDataTableWindow(dataManager.GetRecvedDataTable());
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

        private void btnNewWindow_Click(object sender, EventArgs e) {
            AddGraphWindowId(graphCounter);
            graph.Add(new GraphWindow(graphCounter++,
                                    TrackBarPlotTime.Value,
                                    cbPlotMarker.Checked,
                                    cbBufferFullScale.Checked,
                                    TrackBarPlotTime.Maximum));
        }

        private void dgvGraphWindow_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
            // if graph window id changed, update knownSeriesNameDict
            if(e.ColumnIndex == 1) {
                string key = dgvGraphWindow[0, e.RowIndex].Value.ToString();
                int val = int.Parse(dgvGraphWindow[e.ColumnIndex, e.RowIndex].Value.ToString());
                knownSeriesNameDict[key] = val;
            }
            
        }
    }
}
