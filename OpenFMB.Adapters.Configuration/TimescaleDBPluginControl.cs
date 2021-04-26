// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.ComponentModel;
using OpenFMB.Adapters.Core.Models.Plugins;

namespace OpenFMB.Adapters.Configuration
{
    public partial class TimescaleDBPluginControl : BaseDetailControl
    {
        private TimescaleDBPlugin _plugin;

        public override object DataSource
        {
            get
            {
                return _plugin;

            }
            set
            {
                _plugin = value as TimescaleDBPlugin;
                LoadData(_plugin);
            }
        }        

        public TimescaleDBPluginControl()
        {
            InitializeComponent();
            rawFormatCombo.DataSource = Enum.GetValues(typeof(RawDataFormat));            
        }

        private void LoadData(TimescaleDBPlugin plugin)
        {
            if (plugin != null)
            {
                headerLabel.Text = plugin.Name.ToUpper();
                timescaleDBPluginBindingSource.DataSource = plugin;

                timescaleDBPluginBindingSource.CurrentItemChanged += (sender, e) =>
                {
                    RaisePropertyChangedEvent(new PropertyChangedEventArgs("timescaledb"));
                };
            }
        }

        private void StoreMessageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            measureTableName.Enabled = storeMessageCheckBox.Checked;
        }

        private void storeRawMessageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            rawFormatCombo.Enabled = rawMessageTableName.Enabled = storeRawMessageCheckBox.Checked;
        }
    }
}
