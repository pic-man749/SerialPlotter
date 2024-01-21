using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPlotter {

    internal class DataManager {
        
        private System.Data.DataTable dataTable = new System.Data.DataTable();

        public DataManager() {
            this.dataTable.Columns.Add("time");
            this.dataTable.Columns.Add("key");
            this.dataTable.Columns.Add("value");
        }

        public bool insertData(double time, string key, double value) {
            this.dataTable.Rows.Add(time, key, value);
            return true;
        }

        public ref DataTable getDataSource() {
            return ref this.dataTable;
        }

    }
}
