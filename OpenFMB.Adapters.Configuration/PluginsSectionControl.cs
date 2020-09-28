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
