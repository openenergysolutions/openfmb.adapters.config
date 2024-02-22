// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

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
