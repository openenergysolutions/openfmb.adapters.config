// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.ComponentModel;
using OpenFMB.Adapters.Core.Models.Plugins;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CapturePluginControl : BaseDetailControl
    {        
        public override object DataSource
        {
            get
            {
                return capturePluginBindingSource.DataSource;
            }
            set
            {
                capturePluginBindingSource.CurrentItemChanged -= CapturePluginBindingSource_CurrentItemChanged;
                LoadData(value as IPlugin);
                capturePluginBindingSource.CurrentItemChanged += CapturePluginBindingSource_CurrentItemChanged;
            }
        }

        public CapturePluginControl()
        {
            InitializeComponent();
        }

        private void LoadData(IPlugin plugin)
        {
            if (plugin != null)
            {                
                headerLabel.Text = plugin.Name.ToUpper();               
                capturePluginBindingSource.DataSource = plugin;                
            }
        }

        private void CapturePluginBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            RaisePropertyChangedEvent(new PropertyChangedEventArgs("captureplugin"));
        }
    }
}
