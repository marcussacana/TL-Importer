using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TL_Importer {
    public partial class Form2 : Form {
        public Form2() {
            InitializeComponent();
        }
        public string result;
        private void button1_Click(object sender, EventArgs e) {
            result = textBox1.Text;
            Close();
        }
    }
}
