// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models.Plugins;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class PluginControl : BaseDetailControl
    {
        private IPlugin _plugin;

        private readonly BindingSource _bindingSource;

        public override object DataSource
        {
            get
            {
                return _plugin;
            }
            set
            {
                _bindingSource.CurrentItemChanged -= PluginBindingSource_CurrentItemChanged;
                _plugin = value as IPlugin;
                LoadData(_plugin);
                _bindingSource.CurrentItemChanged += PluginBindingSource_CurrentItemChanged;
            }
        }

        public PluginControl()
        {
            InitializeComponent();

            _bindingSource = new BindingSource
            {
                DataSource = null
            };
        }

        private void LoadData(IPlugin plugin)
        {
            if (plugin != null)
            {
                // !important:  The orders here are crucial.  Set datasource first, then do the binding
                headerLabel.Text = plugin.Name.ToUpper();
                _bindingSource.DataSource = plugin;
                enableCheckBox.DataBindings.Clear();
                enableCheckBox.DataBindings.Add(new Binding("Checked", _bindingSource, "Enabled", true, DataSourceUpdateMode.OnPropertyChanged));
            }
        }

        private void PluginBindingSource_CurrentItemChanged(object sender, System.EventArgs e)
        {
            RaisePropertyChangedEvent(new PropertyChangedEventArgs("plugincontrol"));
        }
    }
}
