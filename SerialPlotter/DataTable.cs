using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialPlotter {
    public partial class DataTableWindow : Form {

        Dictionary<string, DataGridViewTextBoxColumn> recvedDataColumn = new Dictionary<string, DataGridViewTextBoxColumn>();
        public DataTableWindow(DataTable dt) {
            InitializeComponent();
            this.dataGridView1.DataSource = dt;
        }
    }
}
