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
using System.IO;
using System.Diagnostics;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CreateMappingTemplateStep3 : UserControl
    {
        private readonly Model _model;
        private readonly TagsManager _tagsManager = TagsManager.Instance;

        private List<Data> _selectedData;

        public string Module
        {
            set { moduleTextBox.Text = value; }
        }

        public string ProfileName
        {
            set { profileTextBox.Text = value; }
        }

        public string Plugin
        {
            get { return protocolTextBox.Text; }
            set { protocolTextBox.Text = value; }
        }


        public List<Data> SelectedData
        {
            get
            {
                return _selectedData;
            }
            set
            {
                _selectedData = value;
                if (_selectedData == null || _selectedData.Count == 0)
                {
                    errorLabel.Text = "No tags selected";
                    errorLabel.Visible = true;
                }
                else
                {
                    errorLabel.Visible = false;
                }

            }
        }

        public CreateMappingTemplateStep3()
        {
            InitializeComponent();
            _model = _tagsManager.Model;           
        }

        public void Generate()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(saveFilePathTextBox.Text))
                {
                    MessageBox.Show(this, "Please specify where to save the mapping file.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (SelectedData != null && SelectedData.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        if (Plugin == PluginsSection.Dnp3Master)
                        {
                            //Path,Description,Index,Point Type
                            sb.AppendLine("Path,Description,Index,Point Type");
                            foreach(var s in SelectedData)
                            {
                                sb.AppendLine($"{s.Path},{s.Label},,");
                            }
                            
                        }
                        else if (Plugin == PluginsSection.ModbusMaster)
                        {
                            //Path,Description,Index,UpperIndex,Point Type
                            sb.AppendLine("Path,Description,Index,UpperIndex,Point Type");
                            foreach (var s in SelectedData)
                            {
                                sb.AppendLine($"{s.Path},{s.Label},,,");
                            }
                        }

                        var directory = Path.GetDirectoryName(saveFilePathTextBox.Text);
                        Directory.CreateDirectory(directory);

                        File.WriteAllText(saveFilePathTextBox.Text, sb.ToString());

                        MessageBox.Show(this, $"Mapping template has been saved at '{saveFilePathTextBox.Text}'.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        linkLabel.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show(this, "No tag selected.  Please go back and select tags to be mapped.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        linkLabel.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                saveFilePathTextBox.Text = saveFileDialog.FileName;
            }
        }

        private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Process.Start(saveFilePathTextBox.Text);
            }
            catch { }
        }
    }
}
