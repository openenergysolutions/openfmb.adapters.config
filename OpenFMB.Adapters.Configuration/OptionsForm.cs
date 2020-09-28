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

            EnvironmentOptionControl env = new EnvironmentOptionControl();
            env.Dock = DockStyle.Fill;
            placeHolder.Controls.Add(env);
            _optionControls.Add(env);
                       
            // Json parsor
            TreeNode environmentNode = new TreeNode("Environment");            
            TreeNode jGeneral = new TreeNode("General");
            environmentNode.Tag = env;
            jGeneral.Tag = env;
            environmentNode.Nodes.Add(jGeneral);

            ProfileOptionControl profileOptions = new ProfileOptionControl();
            profileOptions.Dock = DockStyle.Fill;
            placeHolder.Controls.Add(profileOptions);
            _optionControls.Add(profileOptions);

            TreeNode openFMBNode = new TreeNode("OpenFMB");
            openFMBNode.Tag = profileOptions;
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

            foreach(var c in _optionControls)
            {
                c.Save();
            }

            Settings.Default.Save();

            Close();
        }
    }
}
