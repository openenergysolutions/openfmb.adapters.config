// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using OpenFMB.Adapters.Core;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CreateMappingTemplateStep2 : UserControl
    {
        private ProfileModel _profileModel;
        private List<Data> _datasource;

        public ProfileModel ProfileModel
        {
            get { return _profileModel; }
            set
            {
                if (_profileModel != value)
                {
                    _profileModel = value;

                    LoadGrid();
                }
            }
        }

        public List<Data> SelectedData
        {
            get
            {                
                if (_datasource != null)
                {
                    return _datasource.Where(x => x.Selected).ToList();
                }
                return new List<Data>();
            }
        }

        public CreateMappingTemplateStep2()
        {
            InitializeComponent();           
        }

        private void LoadGrid()
        {
            _datasource = _profileModel?.Topics.Select(x => x.Attributes).Select(y => new Data()
            {
                Label = y.Label,
                Path = y.Path
            }).ToList();

            dataBindingSource.DataSource = _datasource;
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = filterTextBox.Text.Trim();
                var list = _datasource.FindAll(x => x.Label.IndexOf(search, StringComparison.InvariantCultureIgnoreCase) >= 0 || x.Path.IndexOf(search, StringComparison.InvariantCultureIgnoreCase) >= 0);
                dataBindingSource.DataSource = list;
            }
            catch { }
        }
    }

    public class Data
    {
        public bool Selected { get; set; }
        public string Label { get; set; }
        public string Path { get; set; }
    }
}
