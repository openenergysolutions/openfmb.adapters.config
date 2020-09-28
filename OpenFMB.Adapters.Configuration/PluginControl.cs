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

            _bindingSource = new BindingSource();            
            _bindingSource.DataSource = null;
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
