// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core;
using OpenFMB.Adapters.Core.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class ProfileSelectionControl : UserControl
    {
        public EventHandler<EventArgs> SelectionChanged;
        private CreateAdapterConfigurationSelectFile _selectFileControl;
        public ProfileCreateMode Mode { get; set; }
        public string LoadFromFile { get; set; }

        public List<string> SelectedProfiles
        {
            get
            {
                List<string> list = new List<string>();

                foreach(DataGridViewRow row in dataGridView.Rows)
                {
                    if ((bool)row.Cells[0].Value)
                    {
                        list.Add(row.Cells[1].Value as string);
                    }                    
                }

                return list;
            }
        }

        public bool SelectableEdition
        {
            get { return versionComboBox.Enabled; }
            set { versionComboBox.Enabled = value; }
        }

        public string SelectedEdition
        {
            get { return versionComboBox.SelectedItem as string; }
            set { versionComboBox.SelectedItem = value ?? value; }
        }

        public bool SelectProfileRadio
        {
            get { return selectProfileRadio.Checked; }
            set { selectProfileRadio.Checked = value; }
        }

        public bool SelectProfileRadioVisible
        {
            get { return selectProfileRadio.Visible; }
            set { selectProfileRadio.Visible = value; }
        }

        public bool FromFileRadio
        {
            get { return fromFileRadio.Checked;  }
            set { fromFileRadio.Checked = value; }
        }

        public bool FromFileRadioVisible
        {
            get { return fromFileRadio.Visible; }
            set { fromFileRadio.Visible = value; }
        }

        public ProfileSelectionControl()
        {
            InitializeComponent();

            _selectFileControl = new CreateAdapterConfigurationSelectFile();
            _selectFileControl.Dock = DockStyle.Fill;
            placeHolder.Controls.Add(_selectFileControl);

            _selectFileControl.SelectionChanged += OnFileSelectionChanged;

            versionComboBox.Items.AddRange(SchemaManager.SupportEditions);
            versionComboBox.SelectedItem = SchemaManager.DefaultEdition;
        }

        public void SelectProfiles(List<string> profiles)
        {
            dataGridView.CellValueChanged -= new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValueChanged);
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                var item = row.Cells[1].Value as string;
                if (profiles.Contains(item))
                {
                    row.Cells[0].Value = true;
                }
                else
                {
                    row.Cells[0].Value = false;
                }                
            }
            dataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView_CellValueChanged);
        }

        private void OnFileSelectionChanged(object sender, EventArgs e)
        {
            LoadFromFile = _selectFileControl.Path;
        }

        private void LoadProfiles()
        {
            dataGridView.Rows.Clear();
            var profiles = ProfileRegistry.Profiles.Keys.ToList();
            profiles.Sort();           

            foreach (var p in profiles)
            {
                // TODO:: remove this when the schemas are ready for these profiles
                if (p.StartsWith("Coordination") || p.StartsWith("Reserve") || p.StartsWith("PlannedInterconnection") || p.StartsWith("RequestedInterconnection") || p.StartsWith("CircuitSegment"))
                {
                    continue;
                }

                if (SchemaManager.GetSchemaForProfile("modbus-master", p, SelectedEdition) == null)
                {
                    continue;
                }

                dataGridView.Rows.Add(false, p);
            }
        }

        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                if (SelectionChanged != null)
                {
                    SelectionChanged(this, EventArgs.Empty);
                }
            }
        }

        private void DataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                dataGridView.EndEdit();
            }
        }

        private void SelectProfileRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (selectProfileRadio.Checked)
            {
                dataGridView.BringToFront();
                Mode = ProfileCreateMode.SelectedProfiles;
            }
            if (fromFileRadio.Checked)
            {
                _selectFileControl.BringToFront();
                Mode = ProfileCreateMode.LoadFromFile;
            }
        }

        private void VersionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProfiles();
        }
    }
}
