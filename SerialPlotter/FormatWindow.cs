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
    public partial class FormatWindow : Form {
        public FormatWindow() {
            InitializeComponent();
            this.Show();
        }

        private void Button1_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
