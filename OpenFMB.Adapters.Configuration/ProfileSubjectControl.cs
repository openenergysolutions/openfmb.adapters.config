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
    public partial class ProfileSubjectControl : UserControl, INotifyPropertyChanged
    {
        private readonly BindingSource _bindingSource;

        private ITransportPlugin _plugin;

        private ToolTip _toolTip;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProfileSubjectControl()
        {
            InitializeComponent();

            _bindingSource = new BindingSource();

            profileName.DataBindings.Add(new Binding("Text", _bindingSource, "Profile", true));
            subjectTextBox.DataBindings.Add(new Binding("Text", _bindingSource, "Subject", true));
            _toolTip = new ToolTip();
        }

        public ProfileSubjectControl(object ds, ITransportPlugin plugin) : this()
        {
            _bindingSource.DataSource = ds;
            _plugin = plugin;

            _bindingSource.CurrentItemChanged += (sender, e) =>
            {
                RaisePropertyChangedEvent();
            };
        }

        private void RaisePropertyChangedEvent()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(profileName.Text));
        }

        private void subjectTextBox_Enter(object sender, EventArgs e)
        {
            _toolTip.InitialDelay = 0;           
            _toolTip.Active = true;
            _toolTip.Show(string.Empty, subjectTextBox);
            _toolTip.Show("\"*\" or MRID", subjectTextBox, 0);
        }

        private void subjectTextBox_Leave(object sender, EventArgs e)
        {
            _toolTip.Active = false;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (_bindingSource.DataSource is Publish)
            {
                var p = _bindingSource.DataSource as Publish;
                _plugin.Publishes.Remove(p);
            }
            else if (_bindingSource.DataSource is Subscribe)
            {
                var p = _bindingSource.DataSource as Subscribe;
                _plugin.Subscribes.Remove(p);
            }
            Parent.Controls.Remove(this);
            RaisePropertyChangedEvent();
        }
    }
}
