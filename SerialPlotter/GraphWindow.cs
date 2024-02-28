using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SerialPlotter {
    public partial class GraphWindow : Form {

        public readonly int GID;
        private readonly int BUFFER_MAX;
        private int plotRange;
        private bool isMarkerPlot;
        private bool isFullScaleBuffer;

        private Dictionary<string, Series> buffer = new Dictionary<string, Series>();

        public GraphWindow(int gid, int range, bool marker, bool fsBuf, int bufMax) {
            GID = gid;
            plotRange = range;
            isMarkerPlot = marker;
            isFullScaleBuffer = fsBuf;
            BUFFER_MAX = bufMax;

            InitializeComponent();

            // this.Text += " - " + GID.ToString();

            // init chart area
            chartDefault.Series.Clear();

            this.Show();

        }

        public void AddDatapointToSeries(double now, Dictionary<string, double> kvs) {

            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { AddDatapointToSeries(now, kvs); });
            } else {
                foreach(string key in kvs.Keys) {
                    // check key existance and set data
                    if(!buffer.ContainsKey(key)) {
                        // make new series
                        buffer[key] = MakeNewSeries(key);
                        // set graph
                        chartDefault.Series.Add(buffer[key]);
                    }
                    DataPoint dp = new DataPoint(now, kvs[key]);
                    buffer[key].Points.Add(dp);
                }
            }
        }

        public void ClearChart() {
            foreach(var s in chartDefault.Series) {
                s.Points.Clear();
            }
        }

        public void SetPlotRange(int range) {
            plotRange = range;
        }

        public void SetIsFullScaleBuffer(bool f) {
            isFullScaleBuffer = f;
        }

        public void ChangeMarkerStyle(bool f) {

            isMarkerPlot = f;

            foreach(var s in buffer.Values) {
                s.MarkerStyle = isMarkerPlot ? MarkerStyle.Circle : MarkerStyle.None;
            }
        }

        public void UpdateChart(double now) {
            if(this.InvokeRequired) {
                this.BeginInvoke((MethodInvoker)delegate { UpdateChart(now); });
            } else {

                for(int cnt = 0; cnt < this.chartDefault.ChartAreas.Count; ++cnt) {
                    this.chartDefault.ChartAreas[cnt].AxisX.Maximum = now;
                    this.chartDefault.ChartAreas[cnt].AxisX.Minimum = now - plotRange;
                }
                // delete old point data
                double bufferXSize = (isFullScaleBuffer) ? now - BUFFER_MAX : chartDefault.ChartAreas[0].AxisX.Minimum;
                foreach(string k in buffer.Keys) {
                    while(true) {
                        // to prevent the line from breaking within the frame, leave one point outside the range.
                        if(buffer[k].Points.Count <= 1) {
                            break;
                        }
                        if(buffer[k].Points[1].XValue >= bufferXSize) {
                            break;
                        }
                        buffer[k].Points.RemoveAt(0);
                    }
                }
                chartDefault.ResetAutoValues();
            }
        }

        public void ChangePlotRange(double now, int range) {
            SetPlotRange(range);
            for(int cnt = 0; cnt < this.chartDefault.ChartAreas.Count; ++cnt) {
                this.chartDefault.ChartAreas[cnt].AxisX.Maximum = now;
                this.chartDefault.ChartAreas[cnt].AxisX.Minimum = now - plotRange;
            }
        }

        public void ChangePlotAxis(string key, bool is2ndAxis) {
            if(buffer.ContainsKey(key)) buffer[key].YAxisType = is2ndAxis? AxisType.Secondary : AxisType.Primary;
        }

        public void SetYScale(double min, double max, bool is2ndAxis) {
            for(int cnt = 0; cnt < this.chartDefault.ChartAreas.Count; ++cnt) {
                if(is2ndAxis) {
                    this.chartDefault.ChartAreas[cnt].AxisY2.Minimum = min;
                    this.chartDefault.ChartAreas[cnt].AxisY2.Maximum = max;
                } else {
                    this.chartDefault.ChartAreas[cnt].AxisY.Minimum = min;
                    this.chartDefault.ChartAreas[cnt].AxisY.Maximum = max;
                }
            }
        }

        public void SetSeriesEnable(string name, bool isEnable) {
            for(int cnt = 0; cnt < this.chartDefault.ChartAreas.Count; ++cnt) {
                foreach(var s in this.chartDefault.Series) {
                    if(s.LegendText == name) {
                        s.Enabled = isEnable;
                    }
                }
            }
        }

        // ref : https://stackoverflow.com/questions/33978447/display-tooltip-when-mouse-over-the-line-chart
        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();

        private void Chart_MouseMove(object sender, MouseEventArgs e) {
            var pos = e.Location;
            if(prevPosition.HasValue && pos == prevPosition.Value) {
                return;
            }
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = chartDefault.HitTest(pos.X, pos.Y, false, ChartElementType.DataPoint);
            foreach(var result in results) {
                if(result.ChartElementType == ChartElementType.DataPoint) {
                    var valueX = result.ChartArea.AxisX.PixelPositionToValue(pos.X);
                    var valueY = result.ChartArea.AxisY.PixelPositionToValue(pos.Y);
                    string s = $"({(float)valueX}, {(float)valueY})";
                    tooltip.Show(s, chartDefault, pos.X, pos.Y - 15);
                }
            }
        }

        private Series MakeNewSeries(string name) {
            Series seriesLine = new Series();
            seriesLine.ChartType = SeriesChartType.Line;
            seriesLine.LegendText = name;
            seriesLine.BorderWidth = 2;
            seriesLine.MarkerStyle = isMarkerPlot ? MarkerStyle.Circle : MarkerStyle.None;
            seriesLine.MarkerSize = 6;
            seriesLine.ChartArea = "ChartAreaDefault";
            return seriesLine;
        }

        private void GraphWindow_FormClosing(object sender, FormClosingEventArgs e) {
            // default graph window, then cancel
            if(GID == 0) {
                e.Cancel = true;
            }
        }

        private void GraphWindow_Shown(object sender, EventArgs e) {
            // window position setting
            int needRestorePosition = 0;
            foreach(Screen scr in Screen.AllScreens) {
                if(scr.WorkingArea.Contains(this.Location.X, this.Location.Y)) {
                    needRestorePosition++;
                }
            }
            if(needRestorePosition == 0) {
                this.Location = new System.Drawing.Point(110, 110);
            }
        }
    }
}
