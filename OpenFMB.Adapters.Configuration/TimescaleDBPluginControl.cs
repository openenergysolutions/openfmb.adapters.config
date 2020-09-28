/*
 * Copyright 2017-2020 Duke Energy Corporation and Open Energy Solutions, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
