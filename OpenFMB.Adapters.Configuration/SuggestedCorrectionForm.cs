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

using DiffMatchPatch;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using OpenFMB.Adapters.Core.Json;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    public partial class SuggestedCorrectionForm : Form
    {        
        private readonly Node _selectedParentNode;
        private readonly Node _selectedNode;
        private readonly Profile _profile;

        private static readonly ILogger _logger = MasterLogger.Instance;

        private readonly Color[] _colors = new Color[3] { Color.LightSalmon, Color.LightGreen, Color.White };
    
        private readonly diff_match_patch _diff = new diff_match_patch();

        private List<Diff> _diffList;

        private List<Chunk> _chunklist1;
        private List<Chunk> _chunklist2;

        private readonly YamlDotNet.Serialization.Serializer _serializer = new YamlDotNet.Serialization.Serializer();

        private JToken _newToken;

        private CorrectionType _correctionType;

        public CorrectionType CorrectionType
        {
            get
            {
                return _correctionType;
            }
        }

        public SuggestedCorrectionForm()
        {
            InitializeComponent();
        }

        public SuggestedCorrectionForm(Node selectedNode, Profile profile) : this()
        {
            _selectedNode = selectedNode;

            _selectedParentNode = selectedNode.Parent;

            _profile = profile;

            leftText.Text = TokenToYaml(_selectedParentNode.Tag as JToken);

            try
            {
                JSchema schema = _selectedParentNode.Parent != null ? _selectedParentNode.Parent.Schema : _selectedParentNode.Schema;

                if (schema != null)
                {
                    if (schema.Type == JSchemaType.Object)
                    {
                        var oldProp = _selectedParentNode.Tag as JProperty;

                        // Deep clone before merge
                        oldProp = oldProp.DeepClone() as JProperty;

                        JObject jObject = null;
                        if (oldProp.Value is JObject)
                        {
                            jObject = GenerateToken(schema, oldProp) as JObject;
                        }
                        else
                        {
                            jObject = JsonGenerator.Generate(schema) as JObject;
                        }
                        JToken token;

                        if (jObject.TryGetValue(_selectedParentNode.Name, out token))
                        {
                            // !!!! This token.Parent is same level as node.Tag object                                                        
                            var newProp = token.Parent as JProperty;

                            Merge(oldProp, newProp);                            

                            _newToken = newProp;
                            _correctionType = CorrectionType.Replace;

                            acceptButton.Visible = true;

                            rightText.Text = TokenToYaml(newProp);
                        }
                        else
                        {
                            rightText.Text = TokenToYaml(jObject);
                        }

                        DoDiff();
                    }
                    else if (schema.Type == JSchemaType.Array)
                    {
                        var firstElement = schema.Items.FirstOrDefault();

                        var oldObj = _selectedParentNode.Tag as JObject;
                        oldObj = oldObj.DeepClone() as JObject;                        

                        foreach (var oneOf in firstElement.OneOf)
                        {
                            foreach(var key in oneOf.Required)
                            {
                                if (oldObj.ContainsKey(key))
                                {
                                    var val = oldObj[key].ToString();
                                    var s = oneOf.Properties[key].Const?.ToString();

                                    if (val == s)
                                    {
                                        // found                                        
                                        var newObj = GenerateToken(oneOf, oldObj) as JObject;
                                        Merge(oldObj, newObj);

                                        _newToken = newObj;
                                        _correctionType = CorrectionType.Replace;

                                        acceptButton.Visible = true;

                                        rightText.Text = TokenToYaml(newObj);

                                        break;
                                    }
                                }
                            }
                        }

                        DoDiff();
                    }
                    else
                    {
                        _logger.Log(Level.Debug, "Suggested correction: Schema Type = " + schema.Type);
                        rightText.Text = "No suggestion";
                    }
                }
                else
                {
                    rightText.Text = "No schema found";
                }
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Error, ex.Message, ex);

                rightText.Text = "No schema found";

                _correctionType = CorrectionType.Unknown;
            }
        }

        private JToken GenerateToken(JSchema schema, JObject oldObj, JObject existing = null)
        {
            if (existing == null)
            {
                existing = JsonGenerator.Generate(schema) as JObject;
            }

            if (schema.OneOf.Count > 0)
            {
                foreach(var oneOf in schema.OneOf)
                {
                    foreach (var key in oneOf.Required)
                    {
                        if (oldObj.ContainsKey(key))
                        {
                            var val = oldObj[key].ToString();
                            var s = oneOf.Properties[key].Const?.ToString();

                            if (val == s)
                            {
                                var newToken = JsonGenerator.Generate(oneOf) as JObject;
                                existing.Merge(newToken, new JsonMergeSettings()
                                {
                                    MergeArrayHandling = MergeArrayHandling.Union
                                });

                                return GenerateToken(oneOf, oldObj, existing);
                            }
                        }
                    }
                }                
            }            

            return existing;
        }

        private JToken GenerateToken(JSchema schema, JProperty oldProperty, JObject existing = null)
        {
            if (existing == null)
            {
                existing = JsonGenerator.Generate(schema) as JObject;
            }

            if (schema.Properties.Count > 0)
            {
                foreach (var prop in schema.Properties)
                {
                    if (prop.Key == oldProperty.Name)
                    {
                        return GenerateToken(prop.Value, oldProperty, existing);
                    }
                }
            }

            if (schema.OneOf.Count > 0)
            {
                var myObj = oldProperty.Value as JObject;
                foreach (var oneOf in schema.OneOf)
                {
                    foreach (var key in oneOf.Required)
                    {
                        if (myObj.ContainsKey(key))
                        {
                            var val = myObj[key].ToString();
                            var s = oneOf.Properties[key].Const?.ToString();

                            if (val == s)
                            {
                                var newToken = JsonGenerator.Generate(oneOf) as JObject;
                                var temp = existing[oldProperty.Name] as JObject;
                                temp.Merge(newToken, new JsonMergeSettings()
                                {
                                    MergeArrayHandling = MergeArrayHandling.Union
                                });

                                return GenerateToken(oneOf, oldProperty, existing);
                            }
                        }
                    }
                }
            }
            return existing;
        }

        private void Merge(JObject oldObj, JObject newObj)
        {            
            if (newObj == null || oldObj == null)
            {
                return;
            }

            foreach (var prop in newObj.Properties())
            {
                string oldName;

                if (oldObj.ContainsKey(prop.Name))
                {
                    var value = oldObj[prop.Name];
                    prop.Value = value;

                    oldObj.Remove(prop.Name);
                }
                else if ((oldName = GetNameChanged(oldObj, prop.Name)) != null)
                {
                    var value = oldObj[oldName];
                    prop.Value = value;

                    oldObj.Remove(oldName);
                }                
            }
        }

        private void Merge(JProperty oldProp, JProperty newProp)
        {            
            var newObj = newProp.Value as JObject;
            var oldObj = oldProp.Value as JObject;

            if (newObj == null || oldObj == null)
            {
                return;
            }

            foreach(var prop in newObj.Properties())
            {
                string oldName;

                if (oldObj.ContainsKey(prop.Name))
                {                    
                    var value = oldObj[prop.Name];                    
                    prop.Value = value;

                    oldObj.Remove(prop.Name);
                }
                else if ((oldName = GetNameChanged(oldObj, prop.Name)) != null)
                {
                    var value = oldObj[oldName];
                    prop.Value = value;

                    oldObj.Remove(oldName);
                }
                else
                {
                    Merge(oldProp, prop);
                }
            }
        }

        private string GetNameChanged(JObject obj, string currentName)
        {
            var name = MigrationManager.GetOldName(currentName);
            if (name != null && obj.ContainsKey(name))
            {
                return name;
            }
            return null;
        }

        private string TokenToYaml(JToken token)
        {
            string json = JsonConvert.SerializeObject(token);

            if (!json.StartsWith("{"))
            {
                json = "{" + json + "}";
            }
            var dict = JsonConvert.DeserializeObject<ExpandoObject>(json, new ExpandoObjectConverter());

            var yaml = _serializer.Serialize(dict);

            return yaml;
        }

        private void DoDiff()
        {
            _diffList = _diff.diff_main(leftText.Text, rightText.Text);
            _diff.diff_cleanupSemanticLossless(_diffList);

            _chunklist1 = CollectChunks(leftText);
            _chunklist2 = CollectChunks(rightText);

            PaintChunks(leftText, _chunklist1);
            PaintChunks(rightText, _chunklist2);

            leftText.SelectionLength = 0;
            rightText.SelectionLength = 0;
        }


        private List<Chunk> CollectChunks(RichTextBox richTextBox)
        {
            richTextBox.Text = "";
            List<Chunk> chunkList = new List<Chunk>();
            foreach (Diff d in _diffList)
            {
                if (richTextBox == rightText && d.operation == Operation.DELETE) continue;  
                if (richTextBox == leftText && d.operation == Operation.INSERT) continue; 

                Chunk ch = new Chunk();
                int length = richTextBox.TextLength;
                richTextBox.AppendText(d.text);
                ch.startpos = length;
                ch.length = d.text.Length;                
                ch.BackColor = _colors[(int)d.operation];
                chunkList.Add(ch);
            }
            return chunkList;

        }

        private static void PaintChunks(RichTextBox richTextBox, List<Chunk> chunks)
        {
            foreach (Chunk ch in chunks)
            {
                richTextBox.Select(ch.startpos, ch.length);
                richTextBox.SelectionBackColor = ch.BackColor;
            }
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_correctionType == CorrectionType.Replace)
                {
                    if (_newToken != null)
                    {
                        if (_newToken is JProperty)
                        {
                            var prop = _newToken as JProperty;
                            var parent = _selectedParentNode.Parent;
                            var tag = parent.Tag as JProperty;
                            if (tag != null)
                            {
                                var properties = tag.Value as JObject;

                                if (properties.ContainsKey(prop.Name))
                                {
                                    properties[prop.Name] = prop.Value;

                                    DialogResult = DialogResult.OK;
                                }
                            }
                            else
                            {
                                var properties = parent.Tag as JObject;
                                if (properties.ContainsKey(prop.Name))
                                {
                                    properties[prop.Name] = prop.Value;

                                    DialogResult = DialogResult.OK;
                                }
                            }
                        }
                        else if (_newToken is JObject)
                        {                            
                            var tag = _selectedParentNode.Tag as JObject;
                            tag.RemoveAll();

                            tag.Merge(_newToken);

                            DialogResult = DialogResult.OK;                                                   
                        }
                    }
                }
                else if (_correctionType == CorrectionType.Delete)
                {
                    // TODO
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                }

                Close();
            }
            catch (Exception ex)
            {
                _logger.Log(Level.Error, ex.Message, ex);
                MessageBox.Show(this, "An unexpected error has occurred.  Check logs for more information.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public struct Chunk
    {
        public int startpos;
        public int length;
        public Color BackColor;
    }

    public enum CorrectionType
    {
        Unknown,
        Replace,
        Delete
    }
}
