// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json.Linq;
using OpenFMB.Adapters.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class NavigatorCommandOrderNode : UserControl, IDataNode, INavigatorNode, INotifyPropertyChanged
    {
        private Rectangle _dragBoxFromMouseDown;
        private int _rowIndexFromMouseDown;
        private int _rowIndexOfItemUnderMouseToDrop;

        private readonly List<string> _commandIds = new List<string>();        

        public event EventHandler OnDrillDown
        {
            add { throw new NotSupportedException(); }
            remove { }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string[] Commands
        {
            get
            {
                return _commandIds.ToArray();
            }
        }

        public Node Data { get; private set; }

        public bool IsValid
        {
            get { return Data.IsValid; }
        }

        public bool IsLeafNode { get; } = true;

        public NavigatorCommandOrderNode()
        {
            InitializeComponent();
        }

        public NavigatorCommandOrderNode(Node commandOrderNode) : this()
        {
            Data = commandOrderNode;
            nodeText.Text = Data.Name;
            label1.Text = Data.Schema?.Description;

            var properties = commandOrderNode.Tag as JProperty;

            var list = properties.FirstOrDefault();
            foreach(JValue item in list)
            {
                dataGridView.Rows.Add(item.Value.ToString());
            }

            dataGridView.CellValueChanged += DataGridView_CellValueChanged;
        }

        private void DataGridView_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (_dragBoxFromMouseDown != Rectangle.Empty && !_dragBoxFromMouseDown.Contains(e.X, e.Y))
                {

                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = dataGridView.DoDragDrop(
                    dataGridView.Rows[_rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }
        }

        private void DataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            // Get the index of the item the mouse is below.
            _rowIndexFromMouseDown = dataGridView.HitTest(e.X, e.Y).RowIndex;
            if (_rowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;

                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                _dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                                                               e.Y - (dragSize.Height / 2)),
                                    dragSize);
            }
            else
            {
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                _dragBoxFromMouseDown = Rectangle.Empty;
            }
        }

        private void DataGridView_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void DataGridView_DragDrop(object sender, DragEventArgs e)
        {
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = dataGridView.PointToClient(new Point(e.X, e.Y));

            // Get the row index of the item the mouse is below. 
            _rowIndexOfItemUnderMouseToDrop =
                dataGridView.HitTest(clientPoint.X, clientPoint.Y).RowIndex;

            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow rowToMove = e.Data.GetData(
                    typeof(DataGridViewRow)) as DataGridViewRow;
                dataGridView.Rows.RemoveAt(_rowIndexFromMouseDown);
                dataGridView.Rows.Insert(_rowIndexOfItemUnderMouseToDrop, rowToMove);
                UpdateValues();
                
            }
        }

        private void UpdateValues()
        {
            _commandIds.Clear();

            var property = Data.Tag as JProperty;

            var array = new JArray();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (!row.IsNewRow)
                {
                    var cell = row.Cells[0].Value as string;
                    if (!string.IsNullOrWhiteSpace(cell))
                    {
                        array.Add(new JValue(cell));
                    }
                }
            }

            property.Value = array;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Data.Name));
        }

        private void DataGridView_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            Debug.WriteLine("delete " + e.Row.Index);
            UpdateValues();
        }

        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine("added " + e.RowIndex);
            UpdateValues();
        }
    }
}
