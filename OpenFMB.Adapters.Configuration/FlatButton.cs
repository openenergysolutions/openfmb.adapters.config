// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public class FlatButton : Button
    {
        protected override void OnMouseMove(MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            base.OnMouseMove(e);
        }
    }
}
