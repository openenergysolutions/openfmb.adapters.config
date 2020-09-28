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

            foreach(var s in list)
            {
                dataGridView.Rows.Add(s);
            }

            schemaVersionCombo.Items.AddRange(SchemaManager.GetSchemaVersions().ToArray());

            var v = Settings.Default.DefaultSchemaVersion;
            schemaVersionCombo.SelectedItem = v;
        }

        public void Save()
        {
            Settings.Default.HideTimeAndQuality = hideTimeQuality.Checked;

            Settings.Default.DefaultSchemaVersion = schemaVersionCombo.SelectedItem as string;

            List<string> list = new List<string>();

            foreach(DataGridViewRow row in dataGridView.Rows)
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
