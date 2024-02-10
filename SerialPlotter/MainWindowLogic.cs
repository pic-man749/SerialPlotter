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

                foreach(string key in kvs.Keys) {
                    // insert new data
                    dataManager.InsertData(recvTime, key, kvs[key]);

                    // plot
                    if(!isPlotting) {
                        continue;
                    }

                    foreach(var g in graph) {
                        g.AddSeriesToChart(recvTime, key, kvs[key]);
                    }

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
                    foreach(var g in graph) {
                        g.SetSeriesEnable(key, seriesEnableCbDict[key].Checked);
                    }
                }
            }
        }
    }
}
