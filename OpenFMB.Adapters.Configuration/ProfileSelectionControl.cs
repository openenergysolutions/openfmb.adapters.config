// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class ProfileSelectionControl : UserControl
    {
        public EventHandler<EventArgs> SelectionChanged;

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
        public ProfileSelectionControl()
        {
            InitializeComponent();

            LoadProfiles();
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

        private void LoadProfiles()
        {
            var profiles = ProfileRegistry.Profiles.Keys.ToList();
            profiles.Sort();

            foreach (var p in profiles)
            {
                // TODO:: remove this when the schemas are ready for these profiles
                if (p.StartsWith("Coordination") || p.StartsWith("Reserve") || p.StartsWith("PlannedInterconnection") || p.StartsWith("RequestedInterconnection"))
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
    }
}
