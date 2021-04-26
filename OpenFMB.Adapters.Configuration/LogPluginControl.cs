// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models.Plugins;

namespace OpenFMB.Adapters.Configuration
{
    public partial class LogPluginControl : BaseDetailControl
    {
        private LogPlugin _plugin;

        public override object DataSource
        {
            get
            {
                return _plugin;
            }
            set
            {
                _plugin = value as LogPlugin;
                LoadData(_plugin);
            }
        }

        public LogPluginControl()
        {
            InitializeComponent();
        }

        private void LoadData(LogPlugin plugin)
        {
            if (plugin != null)
            {
                headerLabel.Text = plugin.Name.ToUpper();
            }
        }
    }
}