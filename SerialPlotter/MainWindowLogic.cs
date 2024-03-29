﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SerialPlotter {
    partial class SerialPlotter {

        const char IGNORE_START_CHAR = ';';
        Dictionary<string, int> DictDownSamplingCounter = new Dictionary<string, int>();

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
            DictDownSamplingCounter = new Dictionary<string, int>();

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

                // for latest value kvs
                Dictionary<string, string> kvss = new Dictionary<string, string>();

                bool isNeedRefresh = false;

                foreach(string key in kvs.Keys) {
                    kvss[key] = kvs[key].ToString();
                    double value = kvs[key];
                    // insert new data
                    dataManager.InsertData(recvTime, key, value);

                    // plot
                    if(!isPlotting) {
                        continue;
                    }

                    // is new key? then add keyList and series table row
                    if(!knownKeyList.Contains(key)) {
                        knownKeyList.Add(key);
                        DictDownSamplingCounter[key] = downSampleNum;
                        AddNewSeries(key);
                        isNeedRefresh = true;
                    }

                    // incriment downSampling counter
                    DictDownSamplingCounter[key]++;
                }

                // update Latest Value in table
                UpdateLatestValue(kvss);

                // 大量にテーブルを更新すると次の再描画まで表示されないことがあるのでRefresh()
                if(isNeedRefresh) {
                    RefreshGui(gbDetectedSeries);
                }

                // update chart
                Dictionary<string, double> chartKvs = new Dictionary<string, double>();
                foreach(string key in kvs.Keys) {
                    // disable down sampling
                    if(dictTableRows.ContainsKey(key)) {
                        if(!dictTableRows[key].cbDownSampling.Checked || (DictDownSamplingCounter[key] >= downSampleNum)) {
                            chartKvs[key] = kvs[key];
                            DictDownSamplingCounter[key] = 0;
                        }
                    } else {
                        chartKvs[key] = kvs[key];
                    }
                }
                graphWindow.AddDatapointToSeries(recvTime, chartKvs);
            }
        }

        private void RefreshGui(Control ctrl) {
            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { RefreshGui(ctrl); });
            } else {
                ctrl.Refresh();
            }
        }

        private void UpdateChart(Object source, ElapsedEventArgs e) {
            double now = stopWatch.Elapsed.TotalSeconds;
            graphWindow.UpdateChart(now);
            chartRefreshTimer.Start();
        }

        private void UpdateLatestValue(Dictionary<string, string> kvs) {
            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { UpdateLatestValue(kvs); });
            } else {
                // check key existance and update
                foreach(string  key in kvs.Keys) {
                    if(dictTableRows.Keys.Contains(key)) dictTableRows[key].lSeriesLatestValue.Text = kvs[key];
                }
            }
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

        /// <summary>
        /// リフレッシュレートを周期(ms)で返す関数
        /// </summary>
        /// <returns></returns>
        private float GetChartRefreshRatePeriod() {
            string hzValStr = cbChartRefreshRate.SelectedItem.ToString();
            float hzVal = float.Parse(hzValStr);
            return 1000f / hzVal;
        }

        private void AddNewSeries(string key) {
            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { AddNewSeries(key); });
            } else {
                if(dictTableRows.Keys.Contains(key)) {
                    graphWindow.SetSeriesEnable(key, dictTableRows[key].cbSeriesEnable.Checked);
                    graphWindow.ChangePlotAxis(key, dictTableRows[key].cbSeriesUse2ndYAxis.Checked);
                    return;
                }

                // add row
                tblSeries.RowCount += 1;
                tblSeries.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                this.tblSeries.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
                this.tblSeries.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
                this.tblSeries.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
                this.tblSeries.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));

                int nowRow = tblSeries.RowCount - 1;
                int colCount = 0;

                // make new series label
                Label l = new Label();
                l.Text = key;
                l.Dock = System.Windows.Forms.DockStyle.Fill;
                l.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                tblSeries.Controls.Add(l, colCount++, nowRow);

                // make visible CheckBox and add Dict
                CheckBox cbVisible = new CheckBox();
                cbVisible.Checked = true;
                cbVisible.Name = key;
                cbVisible.Dock = System.Windows.Forms.DockStyle.Fill;
                cbVisible.CheckedChanged += ChangeSeriesVisibleCb;
                cbVisible.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tblSeries.Controls.Add(cbVisible, colCount++, nowRow);

                // make 2ndAxis CheckBox and add Dict
                CheckBox cb2ndAxis = new CheckBox();
                cb2ndAxis.Checked = false;
                cb2ndAxis.Name = key;
                cb2ndAxis.Dock = System.Windows.Forms.DockStyle.Fill;
                cb2ndAxis.CheckedChanged += ChangeSeries2ndAxisCb;
                cb2ndAxis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tblSeries.Controls.Add(cb2ndAxis, colCount++, nowRow);

                // make down sampling CheckBox and add Dict
                CheckBox cbDownSampling = new CheckBox();
                cbDownSampling.Checked = true;
                cbDownSampling.Name = key;
                cbDownSampling.Dock = System.Windows.Forms.DockStyle.Fill;
                cbDownSampling.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                tblSeries.Controls.Add(cbDownSampling, colCount++, nowRow);

                // make new latest value label
                Label llv = new Label();
                llv.Text = "0.0";
                llv.Dock = System.Windows.Forms.DockStyle.Fill;
                llv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                tblSeries.Controls.Add(llv, colCount++, nowRow);

                TableRow tr = new TableRow();
                tr.cbSeriesEnable = cbVisible;
                tr.cbSeriesUse2ndYAxis = cb2ndAxis;
                tr.cbDownSampling = cbDownSampling;
                tr.lSeriesLatestValue = llv;

                dictTableRows.Add(key, tr);
            }
        }
    }
}
