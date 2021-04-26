// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class CreateMappingCSVForm : Form
    {
        private readonly CreateMappingTemplateStep1 _step1;
        private readonly CreateMappingTemplateStep2 _step2;
        private readonly CreateMappingTemplateStep3 _step3;

        private Control _activeControl;

        public CreateMappingCSVForm()
        {
            InitializeComponent();

            _step1 = new CreateMappingTemplateStep1();
            _step1.Dock = DockStyle.Fill;

            _step2 = new CreateMappingTemplateStep2();
            _step2.Dock = DockStyle.Fill;

            _step3 = new CreateMappingTemplateStep3();
            _step3.Dock = DockStyle.Fill;

            placeHolder.Controls.Add(_step1);
            placeHolder.Controls.Add(_step2);
            placeHolder.Controls.Add(_step3);

            GoToStep(_step1);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (_activeControl == _step2)
            {
                GoToStep(_step1);
            }
            else if (_activeControl == _step3)
            {
                GoToStep(_step2);
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (_activeControl == _step1)
            {
                _step2.ProfileModel = _step1.ProfileModel;

                GoToStep(_step2);
            }
            else if (_activeControl == _step2)
            {
                _step3.Module = _step1.ModuleValue?.Name;
                _step3.ProfileName = _step1.ProfileModel?.Name;
                _step3.Plugin = _step1.Plugin;
                _step3.SelectedData = _step2.SelectedData;
                GoToStep(_step3);
            }
            else if (_activeControl == _step3)
            {
                // Generate and save file
                _step3.Generate();
            }
        }

        private void GoToStep(Control control)
        {
            _activeControl = control;
            control.BringToFront();

            backButton.Enabled = control != _step1;
            nextButton.Text = control == _step3 ? "Generate" : "Next";
        }
    }
}
