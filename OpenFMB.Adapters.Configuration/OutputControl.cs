// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class OutputControl : UserControl, ILogger
    {
        //public event EventHandler<OutputClickedEventArgs> OnOutputDoubleClicked;

        public OutputControl()
        {
            InitializeComponent();
            MasterLogger.Instance.Subscribe(this);
            listView.DoubleBuffered(true);
        }

        public void Log(Level level, string message, object tag = null)
        {
            Log(level, message, tag);
        }

        public void Log(Level level, string message, Exception relatedException, object tag = null)
        {
            string exception = relatedException != null ? relatedException.StackTrace : string.Empty;

            if (InvokeRequired)
            {
                listView.BeginInvoke(new MethodInvoker(delegate ()
                {
                    ListViewItem item = new ListViewItem
                    {
                        Tag = tag,
                        ImageIndex = GetImageIndex(level)
                    };

                    item.SubItems.AddRange(new string[] { level.ToString(), message, exception });
                    listView.Items.Add(item);
                }));
            }
            else
            {
                ListViewItem item = new ListViewItem
                {
                    Tag = tag,
                    ImageIndex = GetImageIndex(level)
                };

                item.SubItems.AddRange(new string[] { level.ToString(), message, exception });
                listView.Items.Add(item);
            }
        }

        private int GetImageIndex(Level level)
        {
            if (level == Level.Warning)
            {
                return 1;
            }
            else if (level > Level.Warning)
            {
                return 2;
            }
            return 0;
        }

        private void ContextMenuStrip_Opening(object sender, CancelEventArgs e)
        {
            copyToolStripMenuItem.Enabled = clearToolStripMenuItem.Enabled = listView.SelectedItems.Count > 0;
            copyAllToolStripMenuItem.Enabled = clearAllToolStripMenuItem.Enabled = listView.Items.Count > 0;
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ListViewItem item in listView.SelectedItems)
                {
                    sb.AppendFormat("{0}\t{1}\t{2}", item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text);
                    sb.AppendLine();
                }

                Clipboard.SetText(sb.ToString());
            }
        }

        private void CopyAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.Items.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ListViewItem item in listView.Items)
                {
                    sb.AppendFormat("{0}\t{1}\t{2}", item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text);
                    sb.AppendLine();
                }

                Clipboard.SetText(sb.ToString());
            }
        }

        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                foreach (ListViewItem item in listView.SelectedItems)
                {
                    listView.Items.Remove(item);
                }
            }
        }

        public void ClearAll()
        {
            listView.Items.Clear();
        }

        private void ClearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        private void ResizeOutputColumnHeaders()
        {
            listView.Columns[0].Width = 25;
            listView.Columns[1].Width = 70;
            listView.Columns[3].Width = 150;
            listView.Columns[2].Width = listView.Width - 250;
        }

        private void OutputControl_Resize(object sender, EventArgs e)
        {
            ResizeOutputColumnHeaders();
        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                ListViewHitTestInfo info = listView.HitTest(e.X, e.Y);
                ListViewItem item = info.Item;

                if (item != null)
                {
                    var ex = item.SubItems[3].Text;
                    if (!string.IsNullOrWhiteSpace(ex))
                    {
                        StackTraceViewerForm form = new StackTraceViewerForm
                        {
                            Content = ex
                        };
                        form.ShowDialog();
                    }
                }

            }
            catch { }
        }
    }

    public class OutputClickedEventArgs : EventArgs
    {
        public object Tag { get; private set; }
        public string Message { get; private set; }

        public OutputClickedEventArgs(object tag, string message) : base()
        {
            Tag = tag;
            Message = message;
        }
    }
}
