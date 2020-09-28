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
    public partial class ViewSchemaForm : Form
    {
        public ViewSchemaForm()
        {
            InitializeComponent();
        }

        public ViewSchemaForm(string text) : this()
        {
            schemaTextBox.Text = text;
        }
    }
}
