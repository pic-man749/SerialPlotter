using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SerialPlotter {
    partial class SerialPlotter {

        const char IGNORE_START_CHAR = ';';

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
                
                bool isNeedRefresh = false;

                foreach(string key in kvs.Keys) {
                    double value = kvs[key];
                    // insert new data
                    dataManager.InsertData(recvTime, key, value);

                    // plot
                    if(!isPlotting) {
                        continue;
                    }

                    foreach(var g in graph) {
                        g.AddSeriesToChart(recvTime, key, value);
                    }

                    // is new key? then add btn
                    if(!knownKeyList.Contains(key)) {
                        knownKeyList.Add(key);
                        AddNewSeries(key);
                        isNeedRefresh = true;
                    }

                    // update Latest Value
                    UpdateLatestValue(key, value);
                }

                // 大量にテーブルを更新すると次の再描画まで表示されないことがあるのでRefresh()
                if(isNeedRefresh) {
                     RefreshGui(GBPlotSettings);
                }
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
            foreach(var g in graph) {
                g.UpdateChart(now);
            }
            chartRefreshTimer.Start();
        }

        private void UpdateLatestValue(string key, double val) {
            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { UpdateLatestValue(key, val); });
            } else {
                // check key existance and update
                if(dictSeriesLatestValue.Keys.Contains(key)) dictSeriesLatestValue[key].Text = val.ToString();
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
                if(dictSeriesEnableCb.Keys.Contains(key)) {
                    foreach(var g in graph) {
                        g.SetSeriesEnable(key, dictSeriesEnableCb[key].Checked);
                        g.ChangePlotAxis(key, dictSeriesUse2ndYAxis[key].Checked);
                    }
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
                dictSeriesEnableCb.Add(key, cbVisible);
                tblSeries.Controls.Add(cbVisible, colCount++, nowRow);

                // make 2ndAxis CheckBox and add Dict
                CheckBox cb2ndAxis = new CheckBox();
                cb2ndAxis.Checked = false;
                cb2ndAxis.Name = key;
                cb2ndAxis.Dock = System.Windows.Forms.DockStyle.Fill;
                cb2ndAxis.CheckedChanged += ChangeSeries2ndAxisCb;
                cb2ndAxis.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                dictSeriesUse2ndYAxis.Add(key, cb2ndAxis);
                tblSeries.Controls.Add(cb2ndAxis, colCount++, nowRow);

                // make new latest value label
                Label llv = new Label();
                llv.Text = "0.0";
                llv.Dock = System.Windows.Forms.DockStyle.Fill;
                llv.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
                dictSeriesLatestValue.Add(key, llv);
                tblSeries.Controls.Add(llv, colCount++, nowRow);
            }
        }
    }
}
