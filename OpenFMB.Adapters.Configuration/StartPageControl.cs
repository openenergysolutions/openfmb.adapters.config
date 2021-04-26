// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class StartPageControl : UserControl
    {
        private Color _linkColor = Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));

        public StartPageControl()
        {
            InitializeComponent();            

            RecentFileManager.OnRecentFileChanged += OnRecentFileChanged;

            LoadRecentFiles();

            toolTip.SetToolTip(newConfigurationButton, "Create a main adapter configuration.");
            toolTip.SetToolTip(newTemplateButton, "Create a template file that can be referenced by a plug-in session.");
            toolTip.SetToolTip(openConfigurationButton, "Open a working folder where adapter configuration files are located.");
        }

        private void OnRecentFileChanged(object sender, EventArgs e)
        {
            LoadRecentFiles();
        }

        private void LoadRecentFiles()
        {
            recentPanel.Controls.Clear();

            var files = RecentFileManager.RecentFiles();

            foreach (var f in files)
            {
                if (Directory.Exists(f))
                {
                    LinkLabel c = new LinkLabel();
                    c.Text = Path.GetFileName(f);
                    c.Tag = f;
                    c.LinkColor = c.ActiveLinkColor = c.VisitedLinkColor = _linkColor;
                    c.LinkBehavior = LinkBehavior.HoverUnderline;
                    c.ContextMenuStrip = contextMenuStrip;
                    toolTip.SetToolTip(c, f);
                    c.Click += (sender, e) =>
                    {
                        if (Directory.Exists(f))
                        {
                            Program.Mainform.OpenConfigurationFolder(f);
                        }
                        else
                        {
                            var result = MessageBox.Show($"Folder '{c.Text}' could not be opened.{Environment.NewLine}Do you want to remove the references to it from Recent Work Folder?", Program.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                            if (result == DialogResult.Yes)
                            {
                                RecentFileManager.RemoveFile(f);
                            }
                        }
                    };
                    c.Dock = DockStyle.Top;
                    recentPanel.Controls.Add(c);
                }
            }
        }

        private void OpenSclButton_Click(object sender, EventArgs e)
        {
            Program.Mainform.OpenSCL();
        }

        private void OpenConfiguration_Click(object sender, EventArgs e)
        {
            Program.Mainform.OpenConfigurationFolder();
        }

        private void NewConfigurationButton_Click(object sender, EventArgs e)
        {
            Program.Mainform.CreateAdapterConfiguration();
        }

        private void NewTemplateButton_Click(object sender, EventArgs e)
        {
            Program.Mainform.CreateTemplate();
        }

        private void RemoveFromListToolStripMenuItem_Click(object sender, EventArgs e)
        {                        
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    LinkLabel c = owner.SourceControl as LinkLabel;
                    RecentFileManager.RemoveFile(c.Tag as string);
                }
            }
        }

        private void CopyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    LinkLabel c = owner.SourceControl as LinkLabel;
                    Clipboard.SetText(c.Tag as string);
                }
            }            
        }
    }
}
