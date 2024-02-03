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
        private uint sequentialNumber = 0;
        private Dictionary<string, uint> sequentialNumber4key = new Dictionary<string, uint>();

        public DataManager() {
            // init recved data table
            this.recvedDataTable.Columns.Add("time", Type.GetType("System.Double"));
            this.recvedDataTable.Columns.Add("seqNumber", Type.GetType("System.Int64"));
            this.recvedDataTable.Columns.Add("SeqNumber(key)", Type.GetType("System.Int64"));
            this.recvedDataTable.Columns.Add("key");
            this.recvedDataTable.Columns.Add("value");
        }

        public bool InsertRecvedData(double time, string key, double value) {
            // if unknown key..
            if(!sequentialNumber4key.ContainsKey(key)) {
                // add seqNum Dict
                sequentialNumber4key.Add(key, 0);
            }
            this.recvedDataTable.Rows.Add(time, ++sequentialNumber, ++sequentialNumber4key[key], key, value);
            return true;
        }

        public ref DataTable GetRecvedDataTable() {
            return ref this.recvedDataTable;
        }

        public void ClearDataTable() {
            this.recvedDataTable.Clear();
            sequentialNumber = 0;
            sequentialNumber4key.Clear();

        }

    }
}
