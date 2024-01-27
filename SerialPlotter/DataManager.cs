using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPlotter {

    internal class DataManager {
        
        private System.Data.DataTable dataTable = new System.Data.DataTable();
        private uint sequentialNumber = 0;
        private Dictionary<string, uint> sequentialNumber4key = new Dictionary<string, uint>();

        public DataManager() {
            this.dataTable.Columns.Add("time", Type.GetType("System.Double"));
            this.dataTable.Columns.Add("seqNumber", Type.GetType("System.Int64"));
            this.dataTable.Columns.Add("SeqNumber(key)", Type.GetType("System.Int64"));
            this.dataTable.Columns.Add("key");
            this.dataTable.Columns.Add("value");
        }

        public bool insertData(double time, string key, double value) {
            if(!sequentialNumber4key.ContainsKey(key)) {
                sequentialNumber4key.Add(key, 0);
            }
            this.dataTable.Rows.Add(time, ++sequentialNumber, ++sequentialNumber4key[key], key, value);
            return true;
        }

        public ref DataTable getDataSource() {
            return ref this.dataTable;
        }

        public void clearDataTable() {
            this.dataTable.Clear();
            sequentialNumber = 0;
            sequentialNumber4key.Clear();

        }

    }
}
