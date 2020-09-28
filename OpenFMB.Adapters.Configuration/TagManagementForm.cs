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

using Newtonsoft.Json;
using OpenFMB.Adapters.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class TagManagementForm : Form
    {                
        private readonly TagsManager _tagsManager = TagsManager.Instance;

        private readonly Model _model;

        public TagManagementForm()
        {
            InitializeComponent();
            _model = _tagsManager.Model;

            LoadComboBox();
        }

        private void LoadComboBox()
        {
            moduleCombo.Items.Clear();

            var properties = _model.GetType().GetProperties();

            foreach(var prop in properties)
            {               
                var val = prop.GetValue(_model) as List<ProfileModel>;

                moduleCombo.Items.Add(new ModuleValue () { Name = prop.Name, Value = val });
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
        }       

        private void ProfileCombo_SelectedIndexChanged(object sender, EventArgs e)
        {            
            var item = profileCombo.SelectedItem as ProfileModel;
            var list = item.Topics.Select(x => x.Attributes).ToList();            

            attributesBindingSource.DataSource = new BindingList<Attributes>(list.ToList());
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            _tagsManager.Save();
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = filterTextBox.Text.Trim();

                var item = profileCombo.SelectedItem as ProfileModel;
                if (item != null)
                {
                    var list = item.Topics.Where(x => x.Attributes.Name.IndexOf(search, StringComparison.InvariantCultureIgnoreCase) >= 0 || x.Attributes.Path.IndexOf(search, StringComparison.InvariantCultureIgnoreCase) >= 0).Select(a => a.Attributes).ToList();
                    attributesBindingSource.DataSource = new BindingList<Attributes>(list.ToList());
                }
                
            }
            catch { }
        }
    }    
}
