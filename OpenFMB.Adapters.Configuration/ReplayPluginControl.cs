// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models.Plugins;
using System;
using System.ComponentModel;

namespace OpenFMB.Adapters.Configuration
{
    public partial class ReplayPluginControl : BaseDetailControl
    {
        public override object DataSource
        {
            get
            {
                return replayPluginBindingSource.DataSource;
            }
            set
            {
                replayPluginBindingSource.CurrentItemChanged -= PluginBindingSource_CurrentItemChanged;
                replayPluginBindingSource.DataSource = value;
                headerLabel.Text = (value as IPlugin).Name.ToUpper();
                replayPluginBindingSource.CurrentItemChanged += PluginBindingSource_CurrentItemChanged;
            }
        }

        public ReplayPluginControl()
        {
            InitializeComponent();
        }

        private void PluginBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            RaisePropertyChangedEvent(new PropertyChangedEventArgs("replayplugin"));
        }
    }
}