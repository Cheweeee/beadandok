using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PotyogosAmoba {
    public partial class NewGameForm : Form {
        public int tableSize { get; set; }

        public NewGameForm() {
            InitializeComponent();
            radioButton1.Checked = true;
            tableSize = Convert.ToInt32(radioButton1.Tag.ToString());
        }

        private void RadioButton1_Click(object sender, EventArgs e) {
            RadioButton s = (RadioButton)sender;
            s.Checked = true;
            tableSize = Convert.ToInt32(s.Tag.ToString());
        }

        private void OkButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }
    }
}
