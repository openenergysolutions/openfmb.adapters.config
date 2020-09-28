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
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class PluginSimpleControl : UserControl, INotifyPropertyChanged
    {
        private readonly BindingSource _bindingSource;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler OnSelected;

        public IPlugin Plugin { get; private set; }

        public PluginSimpleControl()
        {
            InitializeComponent();
        }

        public PluginSimpleControl(IPlugin plugin) : this()
        {
            Plugin = plugin;
            _bindingSource = new BindingSource();

            linkLabel.DataBindings.Add(new Binding("Text", _bindingSource, "Name", true));

            enableCheckBox.DataBindings.Add(new Binding("Checked", _bindingSource, "Enabled", true, DataSourceUpdateMode.OnPropertyChanged));

            _bindingSource.DataSource = plugin;

            _bindingSource.CurrentItemChanged += (sender, e) =>
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(linkLabel.Text));
            };
        }        

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OnSelected?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
