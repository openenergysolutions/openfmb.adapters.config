// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CreateAdapterConfigurationPluginOptions : UserControl
    {
        private readonly CreateAdapterConfigurationSelectFile _selectFileControl;
        private readonly CreateAdapterConfigurationSelectFolder _selectFolderControl;
        private readonly ProfileSelectionControl _selectProfilesControl;

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

        public string SelectedVersion
        {
            get { return _selectProfilesControl.SelectedEdition; }
        }

        public CreateAdapterConfigurationPluginOptions()
        {
            InitializeComponent();

            _selectFileControl = new CreateAdapterConfigurationSelectFile
            {
                Dock = DockStyle.Fill
            };
            placeHolder.Controls.Add(_selectFileControl);

            _selectFolderControl = new CreateAdapterConfigurationSelectFolder
            {
                Dock = DockStyle.Fill
            };
            placeHolder.Controls.Add(_selectFolderControl);

            _selectProfilesControl = new ProfileSelectionControl
            {
                Dock = DockStyle.Fill
            };
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
            Options.Edition = _selectProfilesControl.SelectedEdition;
        }

        private void SelectProfileRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (_selectProfilesControl.SelectProfileRadio)
            {
                _selectProfilesControl.BringToFront();
                Options.Mode = ProfileCreateMode.SelectedProfiles;
            }
            if (_selectProfilesControl.FromFileRadio)
            {
                _selectFileControl.BringToFront();
                Options.Mode = ProfileCreateMode.LoadFromFile;
            }
        }

        private void LoadOptions(PluginOptions options)
        {
            if (options.ModeSelectionEnabled)
            {
                _selectProfilesControl.SelectProfileRadioVisible = _selectProfilesControl.FromFileRadioVisible = true;

                if (options.Mode == ProfileCreateMode.LoadFromFile)
                {
                    _selectProfilesControl.FromFileRadio = true;
                }
                else
                {
                    _selectProfilesControl.SelectProfileRadio = true;
                }
            }
            else
            {
                _selectProfilesControl.SelectProfileRadio = true;
                _selectProfilesControl.SelectProfileRadioVisible = _selectProfilesControl.FromFileRadioVisible = false;
            }

            _selectProfilesControl.SelectProfiles(options.SelectedProfiles);
            _selectFileControl.Path = options.LoadFromFile;
            _selectFolderControl.Path = options.LoadFromFolder;
        }
    }
}
