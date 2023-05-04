// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class BaseNavigatorNode : UserControl, IDataNode, INavigatorNode, INotifyPropertyChanged
    {
        private Node _data;

        public event EventHandler OnDrillDown;
        public event PropertyChangedEventHandler PropertyChanged;

        public Node Data
        {
            get { return _data; }
            set
            {
                _data = value;
                navButton.Visible = !IsLeafNode;
                pictureBox.Visible = !_data.IsValid;
                toolTip.SetToolTip(pictureBox, _data.Error);
            }
        }

        public virtual bool IsValid
        {
            get { return Data.IsValid; }
        }

        public virtual bool IsLeafNode
        {
            get
            {
                return Data?.Nodes.Count == 0 && Data?.Schema?.Type != Newtonsoft.Json.Schema.JSchemaType.Array;
            }
        }

        public BaseNavigatorNode()
        {
            InitializeComponent();

        }

        protected void ValidateData()
        {
            Data.Validate();

            pictureBox.Visible = !Data.IsValid;
            toolTip.SetToolTip(pictureBox, Data.Error);

            Data.RaiseOnValueChangedEvent();
        }

        protected void RaisePropertyChangedEvent(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void RaiseDrillDownEvent()
        {
            OnDrillDown?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void NavButton_Click(object sender, EventArgs e)
        {
            RaiseDrillDownEvent();
        }

        protected virtual void PictureBox_Click(object sender, EventArgs e)
        {
            var error = Data.Error;
            if (string.IsNullOrWhiteSpace(error))
            {
                if (Data.Schema == null)
                {
                    error = "Missing schema.";
                }
                else
                {
                    error = "Unknown error";
                }
            }
            NodeErrorForm form = new NodeErrorForm(error);
            form.ShowDialog();
        }
    }
}
