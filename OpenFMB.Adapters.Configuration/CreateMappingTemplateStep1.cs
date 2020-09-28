using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Plugins;
using OpenFMB.Adapters.Core;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CreateMappingTemplateStep1 : UserControl
    {
        private readonly Model _model;
        private readonly TagsManager _tagsManager = TagsManager.Instance;

        public ModuleValue ModuleValue
        {
            get { return moduleCombo.SelectedItem as ModuleValue; }
        }

        public ProfileModel ProfileModel
        {
            get { return profileCombo.SelectedItem as ProfileModel; }
        }

        public string Plugin
        {
            get { return protocolComboBox.SelectedItem.ToString(); }
        }

        public CreateMappingTemplateStep1()
        {
            InitializeComponent();
            _model = _tagsManager.Model;

            //AdapterConfiguration config = new AdapterConfiguration();

            //foreach(var plugin in config.Plugins.Plugins)
            //{
            //    if (plugin is ISessionable)
            //    {
            //        protocolComboBox.Items.Add(plugin.Name);
            //    }
            //}
            protocolComboBox.Items.Add(PluginsSection.Dnp3Master);
            protocolComboBox.Items.Add(PluginsSection.ModbusMaster);
            protocolComboBox.SelectedIndex = 0;

            LoadComboBox();
        }

        private void LoadComboBox()
        {
            moduleCombo.Items.Clear();

            var properties = _model.GetType().GetProperties();

            foreach (var prop in properties)
            {
                var val = prop.GetValue(_model) as List<ProfileModel>;

                moduleCombo.Items.Add(new ModuleValue() { Name = prop.Name, Value = val });
                moduleCombo.DisplayMember = "Name";
                moduleCombo.ValueMember = "Topics";
            }
        }

        private void ModuleCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            profileCombo.Items.Clear();
            var module = moduleCombo.SelectedItem as ModuleValue;

            if (module != null && module.Value != null)
            {
                foreach (var p in module.Value)
                {
                    profileCombo.Items.Add(p);
                    profileCombo.DisplayMember = "Name";
                    profileCombo.ValueMember = "";
                }
            }

            if (profileCombo.Items.Count > 0)
            {
                profileCombo.SelectedIndex = 0;
            }
        }
    }
}
