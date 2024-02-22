// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Configuration.Properties;
using OpenFMB.Adapters.Core.Models.Schemas;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class ProfileOptionControl : UserControl, IOptionControl
    {
        public ProfileOptionControl()
        {
            InitializeComponent();
            hideTimeQuality.Checked = Settings.Default.HideTimeAndQuality;
            var list = Settings.Default.HideTagList.Split(',');

            foreach (var s in list)
            {
                dataGridView.Rows.Add(s);
            }

            schemaVersionCombo.Items.AddRange(SchemaManager.SupportEditions);

            var v = Settings.Default.DefaultSchemaVersion;
            schemaVersionCombo.SelectedItem = v;
        }

        public void Save()
        {
            Settings.Default.HideTimeAndQuality = hideTimeQuality.Checked;

            Settings.Default.DefaultSchemaVersion = SchemaManager.DefaultEdition = schemaVersionCombo.SelectedItem as string;

            List<string> list = new List<string>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var cell = row.Cells[0].Value as string;
                    if (!string.IsNullOrWhiteSpace(cell))
                    {
                        list.Add(cell);
                    }
                }
            }

            Settings.Default.HideTagList = string.Join(",", list.ToArray());
            Settings.Default.Save();
        }
    }
}
