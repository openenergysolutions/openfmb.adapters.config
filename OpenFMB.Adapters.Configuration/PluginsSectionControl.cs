// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models.Plugins;
using System;

namespace OpenFMB.Adapters.Configuration
{
    public partial class PluginsSectionControl : BaseDetailControl
    {
        private PluginsSection _section;

        public event EventHandler OnPluginSelected;

        public IPlugin Plugin { get; private set; }

        public override object DataSource
        {
            get { return _section; }
            set
            {
                _section = value as PluginsSection;
                LoadData(_section);
            }
        }        

        public PluginsSectionControl()
        {
            InitializeComponent();
            headerLabel.Text = "PLUGINS";
        }

        private void LoadData(PluginsSection section)
        {
            if (section != null)
            {
                flowLayoutPanel.Controls.Clear();

                foreach(var p in section.Plugins)
                {
                    PluginSimpleControl c = new PluginSimpleControl(p);
                    c.PropertyChanged += (sender, e) =>
                    {
                        RaisePropertyChangedEvent(e);
                    };
                    c.OnSelected += (sender, e) =>
                    {
                        this.Plugin = c.Plugin;
                        OnPluginSelected(this, EventArgs.Empty);
                    };
                    flowLayoutPanel.Controls.Add(c);
                }
            }
        }
    }
}
