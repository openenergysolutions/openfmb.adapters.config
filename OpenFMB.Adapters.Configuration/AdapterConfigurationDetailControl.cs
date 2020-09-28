using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class AdapterConfigurationDetailControl : BaseDetailControl
    {
        public AdapterConfigurationDetailControl()
        {
            InitializeComponent();
            headerLabel.Text = "Adapter Configuration";
        }
    }
}
