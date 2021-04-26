// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models;
using System;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public class ProfileTreeNode : TreeNode
    {
        private Profile _profile;

        public ProfileTreeNode(Profile profile) : base(profile.ProfileName)
        {
            _profile = profile;

            if (!_profile.IsValid)
            {
                this.SetErrorForeColor();
            }
            else
            {
                this.SetNormalForeColor();
            }

            _profile.OnValidated -= Profile_OnValidated;
            _profile.OnValidated += Profile_OnValidated;
        }        

        private void Profile_OnValidated(object sender, EventArgs e)
        {
            if (!_profile.IsValid)
            {
                this.SetErrorForeColor();
            }
            else
            {
                this.SetNormalForeColor();
            }
        }
    }
}
