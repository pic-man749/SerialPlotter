using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPlotter {

    internal class DataManager {
        
        private System.Data.DataTable recvedDataTable = new System.Data.DataTable();
        private System.Data.DataTable recvedDataSeries = new System.Data.DataTable();
        private uint sequentialNumber = 0;
        private Dictionary<string, uint> sequentialNumber4key = new Dictionary<string, uint>();

        public DataManager() {
            // init recved data table
            this.recvedDataTable.Columns.Add("time", Type.GetType("System.Double"));
            this.recvedDataTable.Columns.Add("seqNumber", Type.GetType("System.Int64"));
            this.recvedDataTable.Columns.Add("SeqNumber(key)", Type.GetType("System.Int64"));
            this.recvedDataTable.Columns.Add("key");
            this.recvedDataTable.Columns.Add("value");

            // init recved data series table
            DataGridViewComboBoxColumn colGraphWindowId = new DataGridViewComboBoxColumn();
            colGraphWindowId.Name = "graphWindowId";
            colGraphWindowId.HeaderText = "graph window id";
            this.recvedDataSeries.Columns.Add("series");
            this.recvedDataSeries.Columns.Add("graphWindowId");
        }

        public bool InsertRecvedData(double time, string key, double value) {
            // if unknown key..
            if(!sequentialNumber4key.ContainsKey(key)) {
                // add seqNum Dict
                sequentialNumber4key.Add(key, 0);
                // add series table
                DataRow dr = recvedDataSeries.NewRow();
                dr["series"] = key;
                dr["graphWindowId"] = 0;
                recvedDataSeries.Rows.Add(dr);
            }
            this.recvedDataTable.Rows.Add(time, ++sequentialNumber, ++sequentialNumber4key[key], key, value);
            return true;
        }

        public ref DataTable GetRecvedDataTable() {
            return ref this.recvedDataTable;
        }

        public ref DataTable GetRecvedDataSeriesTable() {
            return ref this.recvedDataSeries;
        }

        public void ClearDataTable() {
            this.recvedDataTable.Clear();
            this.recvedDataSeries.Clear();
            sequentialNumber = 0;
            sequentialNumber4key.Clear();

        }

    }
}
