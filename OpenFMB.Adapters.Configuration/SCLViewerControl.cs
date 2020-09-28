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

using OpenFMB.Adapters.Core.Models.Goose;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class SCLViewerControl : UserControl, IWindowViewControl
    {
        private GOOSEListControl _listControl;
        private GOOSEDetailsControl _detailControl;

        private readonly string _fileName;

        public SCLViewerControl()
        {
            InitializeComponent();

            _listControl = new GOOSEListControl();
            _listControl.Dock = DockStyle.Fill;

            _detailControl = new GOOSEDetailsControl();
            _detailControl.Dock = DockStyle.Fill;

            mainSplitContainer.Panel2.Controls.Add(_listControl);
            mainSplitContainer.Panel2.Controls.Add(_detailControl);
        }

        public SCLViewerControl(string fileName) : this()
        {
            _fileName = fileName;
        }

        public string Caption
        {
            get { return _fileName.TruncateFile(); }
        }

        public string WorkspaceDir
        {
            get { return _fileName; }
        }

        public void LoadIeds(List<IED> ieds)
        {
            iedCombo.DataSource = ieds;
            iedCombo.DisplayMember = "Name";
        }

        private void IedCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            IED ied = iedCombo.SelectedValue as IED;

            treeView.Nodes.Clear();

            TreeNode node = new TreeNode("GOOSE");
            node.Tag = ied;
            treeView.Nodes.Add(node);
            foreach (var gse in ied.GseControls)
            {
                TreeNode g = new TreeNode(gse.Name);
                g.Tag = gse;
                node.Nodes.Add(g);
            }
            treeView.ExpandAll();
            treeView.SelectedNode = node;

            createConfigButton.Visible = true;
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView.SelectedNode.Tag is GseControl)
            {
                _detailControl.LoadGseControl(treeView.SelectedNode.Tag as GseControl);
                _detailControl.BringToFront();
            }
            else if (treeView.SelectedNode.Tag is IED)
            {
                _listControl.LoadIed(treeView.SelectedNode.Tag as IED);
                _listControl.BringToFront();
            }
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (treeView.SelectedNode != null)
            {
                e.Cancel = !(treeView.SelectedNode.Tag is IED);
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void CreateOpenFMBAdapterConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateConfigurations();
        }

        private void CreateConfigurations()
        {
            if (iedCombo.SelectedValue != null)
            {
                var ied = iedCombo.SelectedValue as IED;
                GseControlSelectionForm form = new GseControlSelectionForm(ied);
                form.ShowDialog();
            }
        }

        private void CreateConfigButton_Click(object sender, EventArgs e)
        {
            CreateConfigurations();
        }
    }
}