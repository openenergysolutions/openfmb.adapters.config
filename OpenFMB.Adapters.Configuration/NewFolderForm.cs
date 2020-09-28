using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class NewFolderForm : Form
    {
        public string FolderName
        {
            get { return nameTextBox.Text.Trim(); }
        }

        public NewFolderForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
