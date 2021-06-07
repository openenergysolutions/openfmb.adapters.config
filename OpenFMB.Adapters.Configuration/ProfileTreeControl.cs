// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using OpenFMB.Adapters.Configuration.Properties;
using OpenFMB.Adapters.Core;
using OpenFMB.Adapters.Core.Json;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Plugins;
using OpenFMB.Adapters.Core.Utility;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class ProfileTreeControl : UserControl, INotifyPropertyChanged
    {
        private ILogger _logger = MasterLogger.Instance;
        private Profile _profile;
        private readonly List<string> _hideTagList = new List<string>();
        private bool _hideTimeAndQuality = true;

        private Node _selectedNode;       

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly List<DataTreeNode> _filterResults = new List<DataTreeNode>();
        private string _lastFilter;

        private readonly LinkedList<Node> _navigatorList = new LinkedList<Node>();
        private LinkedListNode<Node> _selectedNavNode;

        public object DataSource
        {
            get
            {
                return _profile;
            }
            set
            {
                if (_profile != value)
                {
                    _profile = value as Profile;
                    LoadMappings(_profile);

                    headerLabel.Text = _profile.ProfileName;

                    AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
                    allowedTypes.AddRange(TagsManager.Instance.GetLabelsByProfileName(_profile.ProfileName).ToArray());
                    searchTextBox.AutoCompleteCustomSource = allowedTypes;
                    searchTextBox.AutoCompleteMode = AutoCompleteMode.Suggest;
                    searchTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    SetDeviceMRIDHeader();
                }
            }
        }

        public ProfileTreeControl()
        {
            InitializeComponent();            
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            
            flowLayoutPanel.DoubleBuffered(true);
            searchTextBox.DoubleBuffered(true);
            navTreeSearch.DoubleBuffered(true);
            navTreeView.DoubleBuffered(true);

            toolTip.SetToolTip(leftButton, "Previous");
            toolTip.SetToolTip(rightButton, "Next");
            toolTip.SetToolTip(filterButton, "Filter");
            toolTip.SetToolTip(searchButton, "Search Topics");

            string list = Settings.Default.HideTagList;
            if (!string.IsNullOrWhiteSpace(list))
            {
                _hideTagList.AddRange(list.Split(new string[] { ",", ";", "|" }, StringSplitOptions.RemoveEmptyEntries));
            }

            _hideTimeAndQuality = Settings.Default.HideTimeAndQuality;

            navTreeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
            navTreeView.DrawNode += (o, e) =>
            {
                if (e.Node == e.Node.TreeView.SelectedNode)
                {
                    Font font = e.Node.NodeFont ?? e.Node.TreeView.Font;
                    Rectangle r = e.Bounds;
                    r.Offset(0, 1);
                    //Brush brush = e.Node.TreeView.Focused ? SystemBrushes.Highlight : Brushes.Gray;
                    Brush brush = SystemBrushes.Highlight;
                    e.Graphics.FillRectangle(brush, e.Bounds);
                    TextRenderer.DrawText(e.Graphics, e.Node.Text, font, r, e.Node.ForeColor, TextFormatFlags.GlyphOverhangPadding);
                }
                else
                    e.DrawDefault = true;
            };

        }

        private void SetDeviceMRIDHeader()
        {
            deviceMridLabel.Text =  $"mRID: {_profile.GetDeviceMRID()}";
        }

        private void LoadMappings(Profile profile)
        {
            AddNode(profile);
            ShowMappingNodes(profile.NavigatorRoot);
            UpdateProfileDetails();
        }

        private LinkLabel CreateLinkLabel(Node node)
        {
            LinkLabel l = new LinkLabel();
            l.AutoSize = true;
            l.LinkColor = Color.Maroon;
            l.LinkBehavior = LinkBehavior.NeverUnderline;
            l.ForeColor = Color.DarkGray;
            l.Text = node.Name + "  ::";
            l.Click += (sender, e) =>
            {
                ShowMappingNodes(node);
            };
            return l;
        }

        private bool IsProfileName(string name)
        {
            try
            {
                ProfileRegistry.GetProfileFullName(name);
                return true;
            }
            catch
            {
                // ignored
            }
            return false;
        }

        private Node FindNode(Node parent)
        {
            if (parent.Nodes.Count == 0) // reach leaf node
            {
                return parent.Parent;
            }
            else if (parent.Nodes.Count > 1) // has mutiple child nodes
            {
                return parent;
            }
            else // only one child
            {
                return FindNode(parent.Nodes[0]);
            }
        }

        private void ShowMappingNodes(DataTreeNode node)
        {
            if (node.Nodes.Count > 0)
            {
                ShowMappingNodes(node.Data);
            }
            else
            {
                var parent = node?.Parent as DataTreeNode;
                if (parent != null)
                {
                    ShowMappingNodes(parent.Data);
                    // Select
                }
            }
        }

        private void ShowMappingNodes(Node parentNode)
        {
            if (parentNode != null)
            {                
                _selectedNode = parentNode;

                addNewElementButton.Enabled = !parentNode.IsRepeatable && parentNode.Schema?.Type == JSchemaType.Array;
                deleteElementButton.Enabled = parentNode.IsRepeatable && parentNode.Schema?.Type == JSchemaType.Array;
                deleteElementButton.Text = deleteElementButton.Enabled ? $"Delete {parentNode.Name}" : "Delete";

                buttonPanel.Visible = deleteElementButton.Enabled || addNewElementButton.Enabled;

                _selectedNavNode = _navigatorList.Find(_selectedNode);
                if (_selectedNavNode == null)
                {
                    _selectedNavNode = _navigatorList.AddLast(_selectedNode);
                }
                leftButton.Enabled = _selectedNavNode != _navigatorList.First;
                rightButton.Enabled = _selectedNavNode != _navigatorList.Last;

                var temp = parentNode.DataNode as DataTreeNode;
                navTreeView.SelectedNode = temp;

                try
                {
                    flowLayoutPanel.SuspendLayout();

                    flowLayoutPanel.Controls.Clear();

                    if (parentNode.Name == "command-order")
                    {
                        var mapping = new NavigatorCommandOrderNode(parentNode);
                        mapping.PropertyChanged += NavigatorNode_PropertyChanged;
                        flowLayoutPanel.Controls.Add(mapping);
                    }
                    else
                    {
                        foreach (var node in parentNode.Nodes)
                        {
                            if ((node.Tag is JProperty) && (node.Tag as JProperty).Value is JValue)
                            {
                                if (node.Schema == null)
                                {
                                    // Error node
                                    var mapping = new NavigatorStringNode(node);
                                    // profile name is not allowed to modify.  Make it readonly
                                    mapping.ReadOnly = true;
                                    flowLayoutPanel.Controls.Add(mapping);
                                }
                                else
                                {
                                    var val = (node.Tag as JProperty).Value as JValue;

                                    if (node.Schema.Type == JSchemaType.Number || node.Schema.Type == JSchemaType.Integer)
                                    {
                                        var mapping = new NavigatorNumericNode(node);
                                        mapping.PropertyChanged += NavigatorNode_PropertyChanged;
                                        flowLayoutPanel.Controls.Add(mapping);
                                    }
                                    else if (node.Schema.Type == JSchemaType.String || (node.Schema.Type == null && node.Schema.Const != null))
                                    {
                                        if (node.Name == "command-id")
                                        {
                                            var commandOrderNode = _profile.GetAllNavigatorNodes().FirstOrDefault(x => x.Name == "command-order");
                                            var commandOrderProperty = commandOrderNode.Tag as JProperty;
                                            var commandIds = (commandOrderProperty.Value as JArray).Select(x => x.ToString()).ToList();
                                            var mapping = new NavigatorCommandIdNode(node, commandIds);
                                            mapping.PropertyChanged += NavigatorNode_PropertyChanged;
                                            flowLayoutPanel.Controls.Add(mapping);
                                        }
                                        else if (node.HasEnums || (node.Name != "name" && node.Parent.HasOptionsForKey(node.Name)))
                                        {
                                            var mapping = new NavigatorStringSelectionNode(node);
                                            mapping.PropertyChanged += NavigatorNode_PropertyChanged;
                                            flowLayoutPanel.Controls.Add(mapping);
                                        }
                                        else
                                        {
                                            // check schema format
                                            if (node.Schema.Pattern == "[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}")
                                            {
                                                var mapping = new NavigatorGuidNode(node);
                                                mapping.PropertyChanged += NavigatorNode_PropertyChanged;
                                                flowLayoutPanel.Controls.Add(mapping);
                                            }
                                            else
                                            {
                                                if (node.Name == "poll-name" && _profile.PluginName == PluginsSection.Dnp3Master)
                                                {
                                                    // DNP3 poll-name
                                                    var sessionConfig = _profile.SessionConfiguration as Dnp3SessionConfiguration;
                                                    var specific = sessionConfig.SessionSpecificConfig as Dnp3MasterSpecificConfig;
                                                    var polls = specific.Polls.Select(x => x.Name);

                                                    var mapping = new NavigatorPollNameNode(node, polls);
                                                    mapping.PropertyChanged += NavigatorNode_PropertyChanged;
                                                    flowLayoutPanel.Controls.Add(mapping);
                                                }
                                                else
                                                {
                                                    if (node.Name == "name" && IsProfileName(parentNode.Name))
                                                    {
                                                        var mapping = new NavigatorStringNode(node, "Name of OpenFMB profile");
                                                        // profile name is not allowed to modify.  Make it readonly
                                                        mapping.ReadOnly = true;
                                                        mapping.PropertyChanged += NavigatorNode_PropertyChanged;
                                                        flowLayoutPanel.Controls.Add(mapping);
                                                    }
                                                    else
                                                    {
                                                        var mapping = new NavigatorStringNode(node);
                                                        mapping.PropertyChanged += NavigatorNode_PropertyChanged;
                                                        flowLayoutPanel.Controls.Add(mapping);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (node.Schema.Type == JSchemaType.Boolean)
                                    {
                                        var mapping = new NavigatorBooleanNode(node);
                                        mapping.PropertyChanged += NavigatorNode_PropertyChanged;
                                        flowLayoutPanel.Controls.Add(mapping);
                                    }
                                }
                            }
                            else if (node.Tag is JValue)
                            {
                                var mapping = new NavigatorStringNode(node);
                                mapping.PropertyChanged += NavigatorNode_PropertyChanged;
                                flowLayoutPanel.Controls.Add(mapping);
                            }
                            else
                            {
                                NavigatorNode mapping = new NavigatorNode(node);
                                mapping.OnDrillDown += MappingOnDrillDown;
                                flowLayoutPanel.Controls.Add(mapping);
                            }
                        }
                    }
                }
                finally
                {
                    flowLayoutPanel.ResumeLayout(true);
                }

                BuildBreadScrum(_selectedNode);
            }
        }

        private void BuildBreadScrum(Node parentNode)
        {            
            breadScrumFlow.Controls.Clear();
            Node temp = parentNode;
            List<LinkLabel> labels = new List<LinkLabel>();
            labels.Add(CreateLinkLabel(parentNode));
            while (true)
            {
                if (temp.Parent != null)
                {
                    labels.Add(CreateLinkLabel(temp.Parent));
                    temp = temp.Parent;
                }
                else
                {
                    break;
                }
            }
            labels.Reverse();
            breadScrumFlow.Controls.AddRange(labels.ToArray());
        }        

        private void MappingOnDrillDown(object sender, EventArgs e)
        {
            var mappingNode = sender as NavigatorNode;
            DrillDown(mappingNode);
        }

        private void DrillDown(NavigatorNode mappingNode)
        {            
            ShowMappingNodes(mappingNode.Data);
        }

        private void ValidateNode(DataTreeNode treeNode)
        {
            ValidateNode(treeNode.Data);
            treeNode.Update();
        }

        private void ValidateNode(Node node)
        {
            var message = _profile.ErrorMessages.FirstOrDefault(x => x.NodePath != "" && node.Path.EndsWith(x.NodePath, StringComparison.InvariantCultureIgnoreCase));
            if (message != null)
            {
                node.Error = message.Message;
            }
            else
            {
                node.Error = string.Empty;
            }
        }

        private void AddNode(Profile profile)
        {
            Node root = new Node(profile.ProfileName);
            root.Tag = profile.Token;
            root.Schema = profile.Schema;

            profile.NavigatorRoot = root;

            DataTreeNode treeRoot = new DataTreeNode(root);            
            navTreeView.Nodes.Add(treeRoot);

            AddNode(profile.Token, root, treeRoot);
        }        

        private void AddNode(JToken token, Node nodeParent, DataTreeNode treeParent)
        {
            if (token == null)
            {
                return;
            }

            if (token is JValue)
            {
                if (nodeParent.Schema == null)
                {                    
                    nodeParent.Schema = _profile.GetSchemaByPath(nodeParent, token.SchemaType())?.Schema;
                }                
            }
            else if (token is JObject)
            {
                var obj = token as JObject;
                foreach (var property in obj.Properties())
                {
                    if (CanDisplayNode(property.Name, nodeParent))
                    {
                        var childNode = new Node(property.Name);
                        childNode.Tag = property;
                        childNode.Parent = nodeParent;
                        nodeParent.Nodes.Add(childNode);                       
                        childNode.Schema = _profile.GetSchemaByPath(childNode, property.Value.SchemaType())?.Schema;                        

                        ValidateNode(childNode);

                        var treeNode = new DataTreeNode(childNode);                        
                        treeParent.Nodes.Add(treeNode);

                        AddNode(property.Value, childNode, treeNode);
                    }
                }
            }
            else if (token is JArray)
            {
                var array = token as JArray;
                for (int i = 0; i < array.Count; i++)
                {
                    var childNode = new Node($"[{i}]");
                    childNode.IsRepeatable = true;
                    childNode.Tag = array[i];
                    childNode.Parent = nodeParent;
                    nodeParent.Nodes.Add(childNode);
                    childNode.Schema = _profile.GetSchemaByPath(childNode, token.SchemaType())?.Schema;                   

                    ValidateNode(childNode);

                    var treeNode = new DataTreeNode(childNode);                    
                    treeParent.Nodes.Add(treeNode);

                    AddNode(array[i], childNode, treeNode);
                }
            }
            else if (token is JProperty)
            {
                var property = token as JProperty;

                foreach(JToken p in property)
                {
                    AddNode(p, nodeParent, treeParent);                    
                }
            }
            else
            {
                Debug.WriteLine(string.Format("{0} not implemented", token.Type));
            }
        }        

        private bool CanDisplayNode(string name, Node parent)
        {
            if (_hideTimeAndQuality && (name == "t" || name == "q"))
            {
                return false;
            }

            if (_hideTagList.Contains(name))
            {
                return false;
            }

            return parent.Nodes.FirstOrDefault(x => x.Name == name) == null;
        }

        private static void SetNodeError(TreeNode node, bool isError)
        {
            node.ForeColor = isError ? Color.Red : Color.Black;
        }

        private static void SetNodeDisabled(TreeNode node, bool enabled)
        {
            node.ForeColor = enabled ? Color.Black : Color.Gray;
        }        

        private void NavigatorNode_PropertyChanged(object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));

            if (sender is NavigatorStringSelectionNode)
            {
                NavigatorStringSelectionNode navigatorNode = sender as NavigatorStringSelectionNode;
                HandleNodePropertyChanged(navigatorNode.Data, navigatorNode.Value);
            }
            else if (sender is NavigatorCommandOrderNode)
            {
                NavigatorCommandOrderNode navigatorNode = sender as NavigatorCommandOrderNode;
                Node parentNode = navigatorNode.Data;

                var treeNode = parentNode.DataNode as DataTreeNode;
                for (int i = treeNode.Nodes.Count - 1; i >= 0; --i)
                {
                    treeNode.Nodes[i].Remove();
                }
                parentNode.Nodes.Clear();
                AddNode(parentNode.Tag as JToken, parentNode, treeNode);
            }
            else if (sender is NavigatorCommandIdNode)
            {
                var temp = _profile.GetAllNavigatorNodes(true).Where(x => x.Name == "command-id");
                var commandIds = _profile.GetAllNavigatorNodes(true).Where(x => x.Name == "command-id").Select(x => x.Value).Distinct().ToList();
                _profile.UpdateCommandIds(commandIds);

                var navNode = _profile.GetAllNavigatorNodes().FirstOrDefault(n => n.Name == "command-order");
                if (navNode != null)
                {
                    var treeNode = navNode.DataNode as DataTreeNode;
                    for (int i = treeNode.Nodes.Count - 1; i >= 0; --i)
                    {
                        treeNode.Nodes[i].Remove();
                    }
                    navNode.Nodes.Clear();

                    var navTag = navNode.Tag as JProperty;
                    var array = navTag.Value as JArray;
                    for (int i = 0; i < array.Count; ++i)
                    {
                        var childNode = new Node($"[{i}]");
                        childNode.IsRepeatable = true;
                        childNode.Tag = array[i];
                        childNode.Parent = navNode;
                        navNode.Nodes.Add(childNode);
                        childNode.Schema = navNode.Schema;

                        var childTreeNode = new DataTreeNode(childNode);
                        treeNode.Nodes.Add(childTreeNode);
                    }
                }
            }

            UpdateProfileDetails();
            SetDeviceMRIDHeader();
        }

        private void HandleNodePropertyChanged(Node data, string selectedValue)
        {            
            if (data.HasEnums)
            {
                // this is option from Enum 
                return;
            }

            var schema = data.Parent.Schema;

            // Get all required node for its parent
            var allRequiredKeys = Node.GetRequiredProperties(data.Parent);

            var selectedOptionSchema = Node.GetOptionSchema(data.Name, selectedValue, schema);

            if (selectedOptionSchema != null)
            {
                allRequiredKeys = allRequiredKeys.Union(selectedOptionSchema.Required).ToList();

                // Token must be instance of JObject 
                var token = JsonGenerator.Generate(selectedOptionSchema) as JObject;

                try
                {
                    var parentNode = data.Parent;

                    JObject val = null;
                    if (parentNode.Tag is JProperty)
                    {
                        var tag = parentNode.Tag as JProperty;
                        if (tag.Value is JObject)
                        {
                            val = tag.Value as JObject;
                        }
                    }
                    else if (parentNode.Tag is JObject)
                    {
                        val = parentNode.Tag as JObject;
                    }

                    if (val != null)
                    {
                        var properties = val.Properties().ToList();

                        // remove all properties
                        foreach (var p in properties)
                        {
                            if (!allRequiredKeys.Contains(p.Name))
                            {
                                val.Remove(p.Name);
                            }
                        }

                        // Remove all child TreeNodes. Can't just clear!!!
                        var treeNode = parentNode.DataNode as DataTreeNode;
                        for (int i = treeNode.Nodes.Count - 1; i >= 0; --i)
                        {
                            treeNode.Nodes[i].Remove();
                        }

                        // add new properties
                        foreach (var p in token.Properties())
                        {
                            if (!val.ContainsKey(p.Name))
                            {
                                //val.Add(p.Name, p.Value);  
                                ApplyValue(parentNode, val, p);
                            }  
                            else
                            {
                                ResetValue(val, p);
                            }
                        }                        

                        foreach(var n in parentNode.Nodes)
                        {
                            parentNode.ReserveValue(n.Name, n.Value);
                        }

                        parentNode.Nodes.Clear();

                        AddNode(val, parentNode, parentNode.DataNode as DataTreeNode);
                        ShowMappingNodes(_selectedNode);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _logger.Log(Level.Error, ex.Message, ex);
                }
            }
        }

        private static void ApplyValue(Node parentNode, JObject val, JProperty prop)
        {
            // try to use value from previous settings
            var reserved = parentNode.GetReservedValue(prop.Name);
            if (reserved != null)
            {                
                 if (prop.Value.Type == JTokenType.Integer)
                {
                    val.Add(prop.Name, new JValue(Convert.ToInt32(reserved)));
                }
                else if (prop.Value.Type == JTokenType.Float)
                {
                    val.Add(prop.Name, new JValue(Convert.ToDouble(reserved)));
                }
                else if (prop.Value.Type == JTokenType.Boolean)
                {
                    val.Add(prop.Name, new JValue(Convert.ToBoolean(reserved)));
                }
                else
                {
                    val.Add(prop.Name, prop.Value);
                }
            }
            else
            {
                val.Add(prop.Name, prop.Value);
            }
        }

        private static void ResetValue(JObject val, JProperty prop)
        {
            // reset value only it is string (selectable values)
            if (val.ContainsKey(prop.Name))
            {
                if (prop.Value.Type == JTokenType.String)
                {
                    val[prop.Name] = prop.Value;
                }
            }
        }

        private void LeftButton_Click(object sender, EventArgs e)
        {
            if (_selectedNavNode != null)
            {
                var node = _selectedNavNode.Previous;
                if (node != null)
                {
                    ShowMappingNodes(node.Value);
                }
            }
        }

        private void RightButton_Click(object sender, EventArgs e)
        {
            if (_selectedNavNode != null)
            {
                var node = _selectedNavNode.Next;
                if (node != null)
                {
                    ShowMappingNodes(node.Value);
                }
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            HandleSearchTopics();
        }

        private void HandleSearchTopics()
        {
            if (searchTextBox.Text.Trim().Length > 0)
            {
                var topics = TagsManager.Instance.Search(_profile.ProfileName, searchTextBox.Text.Trim());

                if (topics.Count == 0)
                {
                    MessageBox.Show("No match found.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (topics.Count == 1)
                {
                    var topic = topics.First();

                    ShowMappingNodesForTopic(topic);
                }
                else // > 1
                {
                    SearchForm form = new SearchForm(_profile, topics);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        var topic = form.SelectedTopic;
                        ShowMappingNodesForTopic(topic);
                    }
                }
            }
        }

        private void ShowMappingNodesForTopic(Topic topic)
        {
            var node = _profile.GetAllNavigatorNodes().FirstOrDefault(x => x.Path.Equals(topic.Attributes.Path, StringComparison.InvariantCultureIgnoreCase));
            if (node != null)
            {
                ShowMappingNodes(node.Parent);
            }
            else
            {
                _logger.Log(Level.Error, $"Unble to find schema for topic with path {topic.Attributes.Path}");
            }
        }

        private void NavTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var node = e.Node as DataTreeNode;
                ShowMappingNodes(node);
            }
        }

        private void ExpandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = navTreeView.SelectedNode;
            if (selectedNode != null)
            {
                selectedNode.ExpandAll();
            }
        }

        private void ExpandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = navTreeView.SelectedNode;
            if (selectedNode != null)
            {
                selectedNode.Expand();
            }
        }

        private void CollapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = navTreeView.SelectedNode;
            if (selectedNode != null)
            {
                selectedNode.Collapse(false);
            }
        }

        private void CollapseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = navTreeView.SelectedNode;
            if (selectedNode != null)
            {
                selectedNode.Collapse(true);
            }
        }

        private void ShowAllMappedNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (navTreeView.Nodes.Count > 0)
            {
                try
                {
                    navTreeView.BeginUpdate();

                    navTreeView.CollapseAll();
                    var mappedNodes = (navTreeView.Nodes[0] as DataTreeNode).GetAllNodes().Where(x => x.Data.Value == "mapped");
                    foreach (var n in mappedNodes)
                    {
                        n.Expand();
                        var temp = n.Parent;
                        while (true)
                        {
                            if (temp != null)
                            {
                                temp.Expand();
                                temp = temp.Parent;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                finally
                {
                    navTreeView.EndUpdate();
                }
            }
        }

        private void ShowAllErrorNodesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (navTreeView.Nodes.Count > 0)
            {
                try
                {
                    navTreeView.BeginUpdate();

                    navTreeView.CollapseAll();
                    var mappedNodes = (navTreeView.Nodes[0] as DataTreeNode).GetAllNodes().Where(x => x.IsValid == false);
                    foreach (var n in mappedNodes)
                    {
                        var temp = n.Parent;
                        while (true)
                        {
                            if (temp != null)
                            {
                                temp.Expand();
                                temp = temp.Parent;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
                finally
                {
                    navTreeView.EndUpdate();
                }
            }
        }

        private void UpdateProfileDetails()
        {
            if (navTreeView.Nodes.Count > 0)
            {
                var allNodes = (navTreeView.Nodes[0] as DataTreeNode).GetAllNodes();
                var mapped = allNodes.Where(x => x.Data.Value == "mapped").Count();
                var errors = allNodes.Where(x => x.Data.IsValid == false).Count();

                mappedValue.Visible = mappedLabel.Visible = true;
                mappedValue.Text = mapped.ToString();

                if (errors > 0)
                {
                    errorValue.Visible = errorLabel.Visible = true;
                    errorValue.Text = errors.ToString();
                }
                else
                {
                    errorValue.Visible = errorLabel.Visible = false;
                }
            }
            else
            {
                mappedValue.Visible = mappedLabel.Visible = false;
                errorValue.Visible = errorLabel.Visible = false;
            }
        }

        private void FilterButton_Click(object sender, EventArgs e)
        {
            HandleNavTreeFilter();
        }
        
        private void HandleNavTreeFilter()
        {
            if (navTreeView.Nodes.Count > 0)
            {
                var searchText = navTreeSearch.Text.ToLower();

                if (_lastFilter == searchText)
                {
                    FilterNext();
                }
                else
                {
                    _filterResults.Clear();
                    _lastFilter = searchText;

                    navTreeView.CollapseAll();

                    var allNodes = (navTreeView.Nodes[0] as DataTreeNode).GetAllNodes();
                    var nodes = allNodes.Where(x => x.Data.Name.ToLower() == searchText);
                    _filterResults.AddRange(nodes);
                    if (_filterResults.Count > 0)
                    {
                        navTreeView.SelectedNode = _filterResults.First();
                        ShowMappingNodes(navTreeView.SelectedNode as DataTreeNode);
                    }
                }
            }
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HandleSearchTopics();
            }
        }

        private void NavTreeSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                HandleNavTreeFilter();
            }
        }

        private void NavTreeView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                FilterNext();
            }
        }

        private void FilterNext()
        {
            if (_filterResults.Count > 0)
            {
                var selected = navTreeView.SelectedNode as DataTreeNode;
                if (selected != null)
                {
                    int index = _filterResults.IndexOf(selected);
                    if (index >= 0)
                    {
                        if (_filterResults.Count > index + 1)
                        {
                            navTreeView.SelectedNode = _filterResults[index + 1];
                            ShowMappingNodes(navTreeView.SelectedNode as DataTreeNode);
                        }
                        else if (index == _filterResults.Count - 1)
                        {
                            navTreeView.SelectedNode = _filterResults[0];
                            ShowMappingNodes(navTreeView.SelectedNode as DataTreeNode);
                        }
                    }
                }
            }
        }

        private void CopyPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selected = navTreeView.SelectedNode as DataTreeNode;
            if (selected != null)
            {
                var path = selected.Data?.Path;
                if (!string.IsNullOrWhiteSpace(path))
                {
                    Clipboard.SetText(Utils.RemoveDotBeforeArray(path));
                }
            }
        }

        private void AddNewElementButton_Click(object sender, EventArgs e)
        {
            if (_selectedNode != null)
            {
                if (_selectedNode.Schema.Type == JSchemaType.Array)
                {                    
                    var prop = _selectedNode.Tag as JProperty;
                    var array = prop.Value as JArray;
                    int i = -1;

                    foreach(var n in _selectedNode.Nodes)
                    {
                        var index = Convert.ToInt32(n.Name.TrimStart('[').TrimEnd(']'));
                        if (index > i)
                        {
                            i = index;
                        }
                    }

                    ++i;

                    bool cloning = false;
                    if (array.Count > 0)
                    {
                        var result = MessageBox.Show("Copy from previous element?", Program.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            cloning = true;
                        }
                    }
                    
                    JToken token = null;
                    if (cloning == true)
                    {
                        token = array[0].DeepClone();
                    }
                    else
                    {
                        token = JsonGenerator.Generate(_selectedNode.Schema.Items.FirstOrDefault());
                    }

                    if (token != null)
                    {
                        array.Add(token);

                        var childNode = new Node($"[{i}]");
                        childNode.IsRepeatable = true;
                        childNode.Tag = token;
                        childNode.Parent = _selectedNode;
                        _selectedNode.Nodes.Add(childNode);
                        childNode.Schema = _selectedNode.Schema;

                        var treeNode = new DataTreeNode(childNode);
                        (_selectedNode.DataNode as DataTreeNode).Nodes.Add(treeNode);

                        AddNode(token, childNode, treeNode);
                        ShowMappingNodes(_selectedNode);
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
                        UpdateProfileDetails();
                    }
                }
            }
        }

        private void DeleteElementButton_Click(object sender, EventArgs e)
        {
            if (_selectedNode != null && _selectedNode.IsRepeatable)
            {
                if (MessageBox.Show("Item will be deleted.  Are you sure?", Program.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var parentNode = _selectedNode.Parent;
                    var prop = parentNode.Tag as JProperty;
                    var array = prop.Value as JArray;
                    array.Remove(_selectedNode.Tag as JToken);

                    parentNode.Nodes.Remove(_selectedNode);

                    (_selectedNode.DataNode as DataTreeNode).Remove();
                    navTreeView.SelectedNode = (parentNode.DataNode as DataTreeNode);

                    // rename index
                    for(int i = 0; i < parentNode.Nodes.Count; ++i)
                    {
                        parentNode.Nodes[i].Name = $"[{i}]";
                        (parentNode.Nodes[i].DataNode as DataTreeNode).Text = $"[{i}]";
                    }

                    ShowMappingNodes(parentNode);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
                }
            }
        }

        private void SuggestedCorrectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (navTreeView.SelectedNode != null)
            {
                if (navTreeView.SelectedNode.Parent != null)
                {
                    var parentTreeNode = navTreeView.SelectedNode.Parent as DataTreeNode;
                    var selectedTreeNode = navTreeView.SelectedNode as DataTreeNode;

                    Node parentNode = parentTreeNode.Data;

                    SuggestedCorrectionForm form = new SuggestedCorrectionForm(selectedTreeNode.Data, _profile);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        if (form.CorrectionType == CorrectionType.Replace)
                        {
                            try
                            {
                                navTreeView.BeginUpdate();
                                parentTreeNode.Nodes.Clear();
                                parentNode.Nodes.Clear();

                                _profile.Validate();

                                AddNode(parentNode.Tag as JToken, parentNode, parentTreeNode);
                                ShowMappingNodes(parentNode);                                

                                if (!parentNode.IsValid)
                                {                                    
                                    ValidateNode(parentTreeNode);
                                }

                                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
                                UpdateProfileDetails();
                            }
                            finally
                            {
                                navTreeView.EndUpdate();
                            }
                        }
                    }
                }                
            }
        }

        private void QuickFixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (navTreeView.SelectedNode == navTreeView.Nodes[0]) // root
            {
                // mass correction for "f" and "i"
                var nodes = _profile.GetAllNavigatorNodes().Where(x => x.Name == "f" && x.Schema == null);

                var allTreeNodes = (navTreeView.SelectedNode as DataTreeNode).GetAllNodes();

                try
                {
                    navTreeView.BeginUpdate();

                    foreach (var n in nodes)
                    {
                        try
                        {
                            var treeNode = allTreeNodes.FirstOrDefault(x => x.Data == n);
                            if (treeNode != null)
                            {
                                var suggested = MigrationManager.SuggestCorrection(n);
                                if (suggested != null)
                                {
                                    if (MigrationManager.AcceptCorrection(CorrectionType.Replace, suggested, n.Parent))
                                    {
                                        var parentTreeNode = treeNode.Parent as DataTreeNode;
                                        var selectedTreeNode = treeNode as DataTreeNode;

                                        Node parentNode = parentTreeNode.Data;
                                        
                                        parentTreeNode.Nodes.Clear();
                                        parentNode.Nodes.Clear();

                                        AddNode(parentNode.Tag as JToken, parentNode, parentTreeNode);                                        
                                    }
                                }
                            }
                        }
                        catch
                        {
                            // ignore
                        }
                    }

                    _profile.Validate();

                    // mag node for modbus
                    var errorNodes = _profile.GetAllNavigatorNodes(true).Where(x => (x.Name == "mag" || x.Name == "ang") && x.IsValid == false);
                    foreach(var n in errorNodes)
                    {
                        ValidateNode(n);
                        var treeNode = allTreeNodes.FirstOrDefault(x => x.Data == n);
                        if (treeNode != null)
                        {
                            treeNode.Update();
                        }
                    }
                }
                finally
                {
                    navTreeView.EndUpdate();

                    ShowMappingNodes(navTreeView.Nodes[0] as DataTreeNode);

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
                    UpdateProfileDetails();
                }
            }
        }

        private void NavTreeContextMenu_Opening(object sender, CancelEventArgs e)
        {
            var selectedNode = navTreeView.SelectedNode as DataTreeNode;
            suggestedCorrectionToolStripMenuItem.Enabled = false;
            quickFixToolStripMenuItem.Visible = false;
            resetToolStripMenuItem.Enabled = false;
            if (selectedNode != null)
            {
                if (selectedNode == navTreeView.Nodes[0]) // root
                {
                    var count = _profile.GetAllNavigatorNodes(true).Where(x => x.Name == "f" && x.Schema == null).Count();
                    if (count > 0)
                    {
                        quickFixToolStripMenuItem.Visible = true;
                    }
                }
                else
                {
                    if (!selectedNode.Data.IsValid)
                    {
                        if (selectedNode.Data.Parent != null && selectedNode.Data.Parent.Schema != null)
                        {
                            suggestedCorrectionToolStripMenuItem.Enabled = true;
                        }
                    }
                }

                resetToolStripMenuItem.Enabled = viewSchemaToolStripMenuItem.Enabled = selectedNode.Data.Schema != null;
                resetToolStripMenuItem.Enabled = selectedNode.Data.Schema != null && selectedNode.Parent != null && !selectedNode.IsArrayNode;
                viewErrorToolStripMenuItem.Enabled = !selectedNode.Data.IsValid;
            }
        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = navTreeView.SelectedNode as DataTreeNode;
            if (selectedNode != null)
            {
                if (MessageBox.Show($"Node '{selectedNode.Text}' shall be reset to default mapping.{Environment.NewLine}Continue?", Program.AppName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // reset
                    try
                    {
                        var token = JsonGenerator.Generate(selectedNode.Data.Schema);

                        Node data = selectedNode.Data;
                        if (data.Tag is JProperty)
                        {
                            (data.Tag as JProperty).Value = token;

                            try
                            {
                                navTreeView.BeginUpdate();
                                selectedNode.Nodes.Clear();
                                selectedNode.Data.Nodes.Clear();

                                AddNode(token, selectedNode.Data, selectedNode);
                                ShowMappingNodes(selectedNode);

                                _profile.Validate();

                                ValidateNode(selectedNode);                                

                                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
                                UpdateProfileDetails();
                            }
                            finally
                            {
                                navTreeView.EndUpdate();
                            }
                        }
                        else
                        {
                            try
                            {
                                navTreeView.BeginUpdate();
                                var obj = data.Tag as JObject;
                                data.Nodes.Clear();
                                (data.DataNode as DataTreeNode).Nodes.Clear();

                                AddNode(token, selectedNode.Data, selectedNode);
                                ShowMappingNodes(selectedNode.Data);

                                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
                                UpdateProfileDetails();
                            }
                            finally
                            {
                                navTreeView.EndUpdate();
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Log(Level.Debug, ex.Message, ex);
                    }                    
                }
            }
        }

        private void DeviceMridLabel_Click(object sender, EventArgs e)
        {
            try
            {
                var node = _profile.GetDeviceMRIDNode();
                ShowMappingNodes(node.Parent);
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Debug, ex.Message, ex);
            }
        }

        private void ViewSchemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedNode = navTreeView.SelectedNode as DataTreeNode;
            if (selectedNode != null)
            {
                var node = selectedNode.Data;
                if (node.Schema != null)
                {
                    ViewSchemaForm form = new ViewSchemaForm(node.Schema.ToString());
                    form.ShowDialog();
                }
            }
        }

        private void ShowError_Click(object sender, EventArgs e)
        {
            var selectedNode = navTreeView.SelectedNode as DataTreeNode;
            if (selectedNode != null)
            {
                var error = selectedNode.Data.Error;
                if (string.IsNullOrWhiteSpace(error))
                {
                    if (selectedNode.Data.Schema == null)
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

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedNode = navTreeView.SelectedNode as DataTreeNode;
                if (selectedNode != null)
                {
                    var token = selectedNode.Data.Tag as JToken;

                    Clipboard.SetText(token.ToYamlString());
                }
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Debug, ex.Message, ex);
            }
                 
        }        
    }
}