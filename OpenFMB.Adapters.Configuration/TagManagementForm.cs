// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

            if (moduleCombo.SelectedItem is ModuleValue module && module.Value != null)
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

                if (profileCombo.SelectedItem is ProfileModel item)
                {
                    var list = item.Topics.Where(x => x.Attributes.Name.IndexOf(search, StringComparison.InvariantCultureIgnoreCase) >= 0 || x.Attributes.Path.IndexOf(search, StringComparison.InvariantCultureIgnoreCase) >= 0).Select(a => a.Attributes).ToList();
                    attributesBindingSource.DataSource = new BindingList<Attributes>(list.ToList());
                }

            }
            catch { }
        }
    }
}
