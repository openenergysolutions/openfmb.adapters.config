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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CreateAdapterConfigurationPluginOptions : UserControl
    {
        private CreateAdapterConfigurationSelectFile _selectFileControl;
        private CreateAdapterConfigurationSelectFolder _selectFolderControl;
        private ProfileSelectionControl _selectProfilesControl;

        private PluginOptions _options;

        public PluginOptions Options
        {
            get { return _options; }
            set
            {
                _options = value;
                if (_options != null)
                    LoadOptions(_options);
            }
        }        

        public CreateAdapterConfigurationPluginOptions()
        {
            InitializeComponent();

            _selectFileControl = new CreateAdapterConfigurationSelectFile();
            _selectFileControl.Dock = DockStyle.Fill;
            placeHolder.Controls.Add(_selectFileControl);

            _selectFolderControl = new CreateAdapterConfigurationSelectFolder();
            _selectFolderControl.Dock = DockStyle.Fill;
            placeHolder.Controls.Add(_selectFolderControl);

            _selectProfilesControl = new ProfileSelectionControl();
            _selectProfilesControl.Dock = DockStyle.Fill;
            placeHolder.Controls.Add(_selectProfilesControl);
            _selectProfilesControl.BringToFront();

            _selectFileControl.SelectionChanged += OnFileSelectionChanged;
            _selectFolderControl.SelectionChanged += OnFolderSelectionChanged;
            _selectProfilesControl.SelectionChanged += OnProfileSelectionChanged;
        }

        private void OnFolderSelectionChanged(object sender, EventArgs e)
        {
            Options.LoadFromFolder = _selectFolderControl.Path;
        }

        private void OnFileSelectionChanged(object sender, EventArgs e)
        {
            Options.LoadFromFile = _selectFileControl.Path;
        }

        private void OnProfileSelectionChanged(object sender, EventArgs e)
        {
            Options.SelectedProfiles.Clear();
            Options.SelectedProfiles.AddRange(_selectProfilesControl.SelectedProfiles);
        }

        private void SelectProfileRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (selectProfileRadio.Checked)
            {
                _selectProfilesControl.BringToFront();
                Options.Mode = ProfileCreateMode.SelectedProfiles;
            }
            if (fromFileRadio.Checked)
            {
                _selectFileControl.BringToFront();
                Options.Mode = ProfileCreateMode.LoadFromFile;
            }
            if (fromFolderRadio.Checked)
            {
                _selectFolderControl.BringToFront();
                Options.Mode = ProfileCreateMode.LoadFromFolder;
            }
        }

        private void LoadOptions(PluginOptions options)
        {
            if (options.ModeSelectionEnabled)
            {
                modeSelectionPanel.Visible = true;
                selectProfileRadio.Enabled = fromFileRadio.Enabled = fromFolderRadio.Enabled = true;
                if (options.Mode == ProfileCreateMode.LoadFromFolder)
                {
                    fromFolderRadio.Checked = true;
                }
                else if (options.Mode == ProfileCreateMode.LoadFromFile)
                {
                    fromFileRadio.Checked = true;
                }
                else
                {
                    selectProfileRadio.Checked = true;
                }
            }
            else
            {
                selectProfileRadio.Checked = true;
                modeSelectionPanel.Visible = false;
                selectProfileRadio.Enabled = fromFileRadio.Enabled = fromFolderRadio.Enabled = false;
            }

            _selectProfilesControl.SelectProfiles(options.SelectedProfiles);
            _selectFileControl.Path = options.LoadFromFile;
            _selectFolderControl.Path = options.LoadFromFolder;
        }
    }
}
