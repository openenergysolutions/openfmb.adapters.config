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
                pictureBox.Visible = _data.IsValid ? false : true;                
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

            pictureBox.Visible = Data.IsValid ? false : true;
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
