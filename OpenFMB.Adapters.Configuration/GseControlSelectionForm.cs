// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core;
using OpenFMB.Adapters.Core.Models.Goose;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class GseControlSelectionForm : Form
    {
        private readonly IED _ied;
        
        public GseControlSelectionForm()
        {
            InitializeComponent();
        }

        public GseControlSelectionForm(IED ied) : this()
        {
            _ied = ied;
            
            LoadGrid(ied);

            AddDescriptions();
        }

        private void AddDescriptions()
        {
            outputText.Text = $"Subscribe: GOOSE --> OpenFMB\nPublish: OpenFMB --> GOOSE";
        }

        private void LoadGrid(IED ied)
        {
            var column = dataGridView.Columns[3] as DataGridViewComboBoxColumn;
            column.Items.Clear();
            column.Items.AddRange(ProfileRegistry.Profiles.Keys.ToArray());

            List<GseControlSelection> ds = new List<GseControlSelection>();
            foreach(var gse in ied.GseControls)
            {
                ds.Add(new GseControlSelection()
                {
                    Name = gse.Name,
                    GseControl = gse
                });              
            }
            dataGridView.DataSource = ds;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                outputText.Text = string.Empty;

                var files = GenerateConfigurationFiles();

                if (files.Count > 0)
                {
                    outputText.Text += "Adapter configurations are written to: ";
                    foreach (var f in files)
                    {
                        outputText.Text += string.Format("{0}\n", f);
                    }
                }
            }
            catch (Exception ex)
            {
                outputText.Text += ex.Message;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private List<string> GenerateConfigurationFiles()
        {
            List<string> files = new List<string>();

            var ds = dataGridView.DataSource as List<GseControlSelection>;

            var selections = ds.Where(x => x.Selected == true).ToList();

            // any selection?
            if (selections.Count == 0)
            {
                MessageBox.Show("Please select at least one GOOSE Control Block.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return files;
            }
            else
            {
                // selected profile?
                foreach(var gse in selections)
                {
                    if (string.IsNullOrWhiteSpace(gse.Direction))
                    {
                        MessageBox.Show($"Please select either Publish or Subscribe. [{gse.Name}]", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return files;
                    }

                    if (string.IsNullOrWhiteSpace(gse.Profile))
                    {
                        MessageBox.Show($"Please select OpenFMB profile to map. [{gse.Name}]", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return files;
                    }                    
                }

                // Everything is ok
                ConfigurationWriter writer = new ConfigurationWriter();
                files.AddRange(writer.WriteGooseConfigurationFiles(selections, "adapter"));

            }

            return files;
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            outputText.Text = string.Empty;
        }

        private void OpenFolderButton_Click(object sender, EventArgs e)
        {
            Process.Start(Directory.GetCurrentDirectory());
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
