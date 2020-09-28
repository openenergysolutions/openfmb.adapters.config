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

using OpenFMB.Adapters.Core;
using OpenFMB.Adapters.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class SearchForm : Form
    {
        private readonly Profile _profile;
        private readonly List<Topic> _topics;

        public Topic SelectedTopic
        {
            get; private set;
        }

        public SearchForm()
        {
            InitializeComponent();
        }

        public SearchForm(Profile profile, List<Topic> topics) : this()
        {
            _profile = profile;
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
