﻿using System;
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

namespace SerialPlotter {
    public partial class SerialPlotter : Form {

        private Serial serial = new Serial();

        const char IGNORE_START_CHAR = ';';
        private System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        private System.Timers.Timer graphRenderTimer = new System.Timers.Timer();
        private Dictionary<string, Series> buffer = new Dictionary<string, Series>();
        private bool isPlotting = false;
        private Stream logFileStream = null;
        private string newLine = String.Empty;
        private DataManager dataManager = new DataManager();

        public SerialPlotter() {
            InitializeComponent();
            // init chart area
            ChartDefault.Series.Clear();
            // show Serial Ports
            getNowConnectedSerialPorts();
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

            // timer callBack func
            graphRenderTimer.Elapsed += updateGraph;
        }
        // get COM port name and refresh ListBox
        private void getNowConnectedSerialPorts() {
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
        private void serialDataReceivedEventHandler(object sender, SerialDataReceivedEventArgs e) {
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
                    // insert new data
                    dataManager.insertData(recvTime, key, kvs[key]);

                    // plot
                    if(isPlotting) {
                        AddSeriesToChart(recvTime, key, kvs[key]);
                    }
                }
            }
        }

        private delegate void DelegateAddSeriesToChart(double now, string key, double value);

        private void AddSeriesToChart(double now, string key, double value) {

            if(this.InvokeRequired) {
                this.Invoke((MethodInvoker)delegate { AddSeriesToChart(now, key, value); });
            } else {
                // check key existance and set data
                if(!buffer.ContainsKey(key)) {
                    // make new series
                    buffer[key] = makeNewSeries(key);
                    // set graph
                    ChartDefault.Series.Add(buffer[key]);
                }
                DataPoint dp = new DataPoint(now, value);
                buffer[key].Points.Add(dp);
                for(int cnt=0; cnt< this.ChartDefault.ChartAreas.Count; ++cnt) {
                    this.ChartDefault.ChartAreas[cnt].AxisX.Maximum = now;
                    this.ChartDefault.ChartAreas[cnt].AxisX.Minimum = now - this.TrackBarPlotTime.Value;
                }
                ChartDefault.ResetAutoValues();
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e) {
            getNowConnectedSerialPorts();
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

                try {
                    serial.Open();
                } catch {
                    General.ShowErrMsgBox("Cannot open selected COM port.");
                    return;
                }
                startTimer();
                // start plot
                BtnPlotStart_Click(sender, e);
                BtnConnect.Text = "close";
            } else if(BtnConnect.Text == "close") {
                if(isPlotting) {
                    // stop plot
                    BtnPlotStart_Click(sender, e);
                }
                BtnConnect.Text = "connect";
                serial.Close();
                stopTimer();
                resetTimer();
            }
            changeSerialParamUI(serial.IsOpen);
        }

        private void changeSerialParamUI(bool isComOpen) {
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
        }

        private void BtnPlotStart_Click(object sender, EventArgs e) {
            isPlotting = !isPlotting;
            if(isPlotting) {
                BtnPlotStart.Text = "plot stop";
            } else {
                BtnPlotStart.Text = "plot start";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e) {
            if(logFileStream != null) {
                logFileStream.Close();
            }
            Properties.Settings.Default.Save();
        }

        private void Form1_Shown(object sender, EventArgs e) {
            // set DataRecv EventHandler
            serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialDataReceivedEventHandler);

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

        private Series makeNewSeries(string name) {
            Series seriesLine = new Series();
            seriesLine.ChartType = SeriesChartType.Line;
            seriesLine.LegendText = name;
            seriesLine.BorderWidth = 2;
            seriesLine.MarkerStyle = MarkerStyle.Circle;
            seriesLine.MarkerSize = 6;
            seriesLine.ChartArea = "ChartAreaDefault";
            return seriesLine;
        }

        public bool startTimer() {
            stopWatch.Start();
            return true;
        }

        public bool stopTimer() {
            stopWatch.Stop();
            return true;
        }

        public bool resetTimer() {
            if(stopWatch.IsRunning) {
                stopWatch.Restart();
            } else {
                stopWatch.Reset();
            }
            return true;
        }

        private void updateGraph(Object sender, ElapsedEventArgs e) {

        }

        private void BtnPlotReset_Click(object sender, EventArgs e) {
            ChartDefault.Series.Clear();
            buffer.Clear();
        }

        // https://stackoverflow.com/questions/33978447/display-tooltip-when-mouse-over-the-line-chart
        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();

        private void chart1_MouseMove(object sender, MouseEventArgs e) {
            var pos = e.Location;
            if(prevPosition.HasValue && pos == prevPosition.Value) {
                return;
            }
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = ChartDefault.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
            foreach(var result in results) {
                if(result.ChartElementType == ChartElementType.DataPoint) {
                    var valueY = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);
                    tooltip.Show(((float)valueY).ToString(), ChartDefault, pos.X, pos.Y - 15);
                }
            }
        }

        private void TrackBarPlotPoint_ValueChanged(object sender, EventArgs e) {
            LabelPoltPoint.Text = TrackBarPlotTime.Value.ToString();
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
            General.ShowMsgBox("書式",
                "シリアルで転送するデータはASCII形式で下記の書式です。" + Environment.NewLine +
                "空白は無視されません。" + Environment.NewLine +
                "    {key}:{val},{key};{val},...\\n" + Environment.NewLine +
                "" + Environment.NewLine +
                "なお、先頭が「;」の場合はコメント行として無視されます。"
                );
        }


        DataTableWindow dtw;
        private void dataTableToolStripMenuItem_Click(object sender, EventArgs e) {
            dtw = new DataTableWindow(dataManager.getDataSource());
            dtw.Show();
        }
    }
}