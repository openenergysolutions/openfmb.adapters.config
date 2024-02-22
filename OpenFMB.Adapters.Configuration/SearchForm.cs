// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class SearchForm : Form
    {
        private readonly List<Topic> _topics;

        public Topic SelectedTopic
        {
            get; private set;
        }

        public SearchForm()
        {
            InitializeComponent();
        }

        public SearchForm(List<Topic> topics) : this()
        {
            _topics = topics;
            topicBindingSource.DataSource = new BindingList<Topic>(_topics);
        }

        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                SelectedTopic = grid.Rows[e.RowIndex].DataBoundItem as Topic;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            var subset = _topics.FindAll(x => x.Path.IndexOf(searchTextBox.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                x.Tags.IndexOf(searchTextBox.Text, StringComparison.CurrentCultureIgnoreCase) >= 0 ||
                x.Label.IndexOf(searchTextBox.Text, StringComparison.CurrentCultureIgnoreCase) >= 0);
            topicBindingSource.DataSource = new BindingList<Topic>(subset);
        }
    }
}
