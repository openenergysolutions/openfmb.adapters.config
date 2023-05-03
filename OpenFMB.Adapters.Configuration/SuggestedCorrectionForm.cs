// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using DiffMatchPatch;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Utility.Logs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
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
                _newToken = MigrationManager.AddMissingProperties(selectedNode);
                if (_newToken == null)
                {
                    _newToken = MigrationManager.AddMissingOneOf(selectedNode);
                }
                if (_newToken == null)
                {
                    _newToken = MigrationManager.SuggestCorrection(selectedNode);
                }

                if (_newToken != null)
                {
                    _correctionType = CorrectionType.Replace;

                    acceptButton.Visible = true;
                    rightText.Text = TokenToYaml(_newToken);
                    _correctionType = CorrectionType.Replace;

                    DoDiff();
                }
                else
                {
                    rightText.Text = "No suggestion";
                }
            }
            catch
            {
                rightText.Text = "No schema found";

                _correctionType = CorrectionType.Unknown;
            }            
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
                var result = MigrationManager.AcceptCorrection(_correctionType, _newToken, _selectedParentNode);
                if (result)
                {
                    DialogResult = DialogResult.OK;
                }
                Close();
            }
            catch
            {
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
