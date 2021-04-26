// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.ComponentModel;
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
