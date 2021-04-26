// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core;
using OpenFMB.Adapters.Core.Models.Plugins;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class MqttPluginControl : BaseDetailControl
    {
        private MqttPlugin _plugin;

        public override object DataSource
        {
            get
            {
                return _plugin;
            }
            set
            {
                _plugin = value as MqttPlugin;
                LoadData(_plugin);
            }
        }

        public MqttPluginControl()
        {
            InitializeComponent();
        }

        private void LoadData(MqttPlugin plugin)
        {
            if (plugin != null)
            {
                headerLabel.Text = plugin.Name.ToUpper();
                pluginBindingSource.CurrentItemChanged -= BindingSource_CurrentItemChanged;
                securityBindingSource.CurrentItemChanged -= BindingSource_CurrentItemChanged;

                pluginBindingSource.DataSource = plugin;
                securityBindingSource.DataSource = plugin.Security;

                publishPanel.Controls.Clear();

                foreach (var p in plugin.Publishes)
                {
                    ProfileSubjectControl c = new ProfileSubjectControl(p, plugin);
                    c.PropertyChanged += ProfileSubjectPropertyChanged;
                    publishPanel.Controls.Add(c);
                }

                subscribePanel.Controls.Clear();

                foreach (var p in plugin.Subscribes)
                {
                    ProfileSubjectControl c = new ProfileSubjectControl(p, plugin);
                    c.PropertyChanged += ProfileSubjectPropertyChanged;
                    subscribePanel.Controls.Add(c);
                }

                pluginBindingSource.CurrentItemChanged += BindingSource_CurrentItemChanged;
                securityBindingSource.CurrentItemChanged += BindingSource_CurrentItemChanged;
            }
        }

        private void ProfileSubjectPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChangedEvent(new System.ComponentModel.PropertyChangedEventArgs("natsplugin"));
        }

        private void BindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            RaisePropertyChangedEvent(new System.ComponentModel.PropertyChangedEventArgs("mqttplugin"));
        }

        private void AddPublishProfileButton_Click(object sender, EventArgs e)
        {
            ProfileSelectionForm form = new ProfileSelectionForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var selectedProfiles = form.SelectedProfiles;

                foreach (var p in selectedProfiles)
                {
                    if (_plugin.Publishes.FirstOrDefault(x => x.Profile == p) == null)
                    {
                        Publish pub = new Publish()
                        {
                            Profile = p,
                            Subject = "*"
                        };
                        _plugin.Publishes.Add(pub);

                        ProfileSubjectControl c = new ProfileSubjectControl(pub, _plugin);
                        c.PropertyChanged += ProfileSubjectPropertyChanged;
                        publishPanel.Controls.Add(c);
                    }
                }
            }
        }

        private void AddSubscribeProfileButton_Click(object sender, EventArgs e)
        {
            ProfileSelectionForm form = new ProfileSelectionForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var selectedProfiles = form.SelectedProfiles;

                foreach (var p in selectedProfiles)
                {
                    if (_plugin.Subscribes.FirstOrDefault(x => x.Profile == p) == null)
                    {
                        Subscribe sub = new Subscribe()
                        {
                            Profile = p,
                            Subject = "*"
                        };
                        _plugin.Subscribes.Add(sub);

                        ProfileSubjectControl c = new ProfileSubjectControl(sub, _plugin);
                        c.PropertyChanged += ProfileSubjectPropertyChanged;
                        subscribePanel.Controls.Add(c);
                    }
                }
            }
        }

        private void SecurityType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var type = (SecurityType)securityTypeComboBox.SelectedIndex;
            switch (type)
            {
                case SecurityType.none:
                    caCertFile.Enabled = clientCertFile.Enabled = clientKeyFile.Enabled = false;
                    break;
                case SecurityType.tls_server_auth:
                    caCertFile.Enabled = true;
                    clientCertFile.Enabled = clientKeyFile.Enabled = false;
                    break;
                case SecurityType.tls_mutual_auth:
                    caCertFile.Enabled = clientCertFile.Enabled = clientKeyFile.Enabled = true;
                    break;
            }
        }

        private void ResetPubSub_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Pub/Sub topics will be reset and populated.  Proceed?", Program.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ConfigurationManager.Instance.UpdatePubSubTopics(type:TransportPluginType.MQTT, reset:true);
                LoadData(_plugin);
                RaisePropertyChangedEvent(new System.ComponentModel.PropertyChangedEventArgs("mqttplugin"));
            }
        }
    }
}