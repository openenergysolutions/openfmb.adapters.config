// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Configuration.Properties;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class OptionsForm : Form
    {
        private readonly List<IOptionControl> _optionControls = new List<IOptionControl>();
        public OptionsForm()
        {
            InitializeComponent();
            InitializeOptions();
        }

        private void InitializeOptions()
        {
            treeView.Nodes.Clear();

            EnvironmentOptionControl env = new EnvironmentOptionControl
            {
                Dock = DockStyle.Fill
            };
            placeHolder.Controls.Add(env);
            _optionControls.Add(env);

            // Json parsor
            TreeNode environmentNode = new TreeNode("Environment");
            TreeNode jGeneral = new TreeNode("General");
            environmentNode.Tag = env;
            jGeneral.Tag = env;
            environmentNode.Nodes.Add(jGeneral);

            ProfileOptionControl profileOptions = new ProfileOptionControl
            {
                Dock = DockStyle.Fill
            };
            placeHolder.Controls.Add(profileOptions);
            _optionControls.Add(profileOptions);

            TreeNode openFMBNode = new TreeNode("OpenFMB")
            {
                Tag = profileOptions
            };
            environmentNode.Nodes.Add(openFMBNode);

            treeView.Nodes.Add(environmentNode);
            treeView.SelectedNode = environmentNode;

            treeView.ExpandAll();

            env.BringToFront();
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = treeView.SelectedNode;
            if (node != null)
            {
                UserControl control = node.Tag as UserControl;
                control.BringToFront();
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            foreach (var c in _optionControls)
            {
                c.Save();
            }

            Settings.Default.Save();

            Close();
        }
    }
}
