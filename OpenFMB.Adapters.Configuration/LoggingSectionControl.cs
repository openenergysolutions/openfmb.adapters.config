// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.ComponentModel;

namespace OpenFMB.Adapters.Configuration
{
    public partial class LoggingSectionControl : BaseDetailControl
    {
        public override object DataSource
        {
            get
            {
                return loggingSectionBindingSource.DataSource;
            }
            set
            {
                loggingSectionBindingSource.CurrentItemChanged -= LoggingSectionBindingSource_CurrentItemChanged;
                loggingSectionBindingSource.DataSource = value;
                loggingSectionBindingSource.CurrentItemChanged += LoggingSectionBindingSource_CurrentItemChanged;
            }
        }

        public LoggingSectionControl()
        {
            InitializeComponent();
            headerLabel.Text = "LOGGING";            
        }

        private void LoggingSectionBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            RaisePropertyChangedEvent(new PropertyChangedEventArgs("loggingsection"));
        }
    }
}
