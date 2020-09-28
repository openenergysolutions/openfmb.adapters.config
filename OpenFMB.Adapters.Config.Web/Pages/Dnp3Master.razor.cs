using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using OpenFMB.Adapters.Config.Web.Models;
using OpenFMB.Adapters.Config.Web.Services;
using OpenFMB.Adapters.Core.Models;
using OpenFMB.Adapters.Core.Models.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace OpenFMB.Adapters.Config.Web.Pages
{
    public partial class Dnp3Master : ComponentBase
    {
        public class TreeNode
        {
            public string Text { get; set; }
            public string Path { get; set; }
            public List<TreeNode> Nodes { get; set; } = new List<TreeNode>();
        }

        [Inject]
        ProjectService ProjectService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        IJSRuntime JSRuntime { get; set; }

        public bool IsPageReady { get; set; }

        public TreeNode Root { get; set; }
        public List<TreeNode> Items { get; set; } = new List<TreeNode>();

        public Profile Profile { get; set; }

        public List<Session> Sessions { get; set; } = new List<Session>();
        

        public Project Project { get; set; }

        public Node Mapping { get; set; } = new Node("");

        protected async override Task OnInitializedAsync()
        {                        
            string id = null;
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("projectid", out var token))
            {
                id = token.First();
            }

            if (!string.IsNullOrEmpty(id))
            {
                Project = ProjectService.Get(id);
                IsPageReady = false;
                await Task.Run(() =>
                {
                    try
                    {                        
                        var config = Project.AdapterConfiguration;

                        Sessions = config.Plugins.Dnp3MasterPlugin.Sessions;

                        var session = Sessions.FirstOrDefault();

                        if (session == null)
                        {
                            session = new Session("dnp3-master");
                            config.Plugins.Dnp3MasterPlugin.Sessions.Add(session);
                        }                        

                        var sessionConfig = session.SessionConfiguration;

                        Profile = session.SessionConfiguration.Profiles.FirstOrDefault();
                        if (Profile == null)
                        {
                            Profile = Profile.Create("BreakerReadingProfile", "dnp3-master");
                            sessionConfig.Profiles.Add(Profile);
                            ProjectService.Update(id, Project);
                        }

                        Mapping = Profile.GetAllNodes().FirstOrDefault(x => x.Name == "mapping");
                    }
                    finally
                    {
                        IsPageReady = true;
                    }
                });
            }
        }

        void DoneClick()
        {
            validating = false;            
            var session = new Core.Models.Plugins.Session("dnp3-master");
            session.Name = this.sessionName;
            Sessions.Add(session);           
        }

        private static void CollectTreeNode(TreeNode parent, Node node)
        {
            foreach (var child in node.Nodes)
            {
                if (child.Name == "Option")
                {
                    var tag = node.Tag as List<Node>;
                    if (tag == null)
                    {
                        tag = new List<Node>();
                        node.Tag = tag;
                    }
                    tag.Add(child);
                    continue;
                }
                var nn = new TreeNode()
                {
                    Text = child.Name,
                    Path = child.Path
                };
                parent.Nodes.Add(nn);

                CollectTreeNode(nn, child);
            }
        }

        protected void UpdateTree(string path)
        {
            Items.Clear();            
           
            var node = Profile.GetAllNodes().FirstOrDefault(x => x.Path == path);

            Root = new TreeNode()
            {
                Text = node.Name,
                Path = node.Path
            };
            Items.Add(Root);
            foreach (var child in node.Nodes)
            {                               
                var n = new TreeNode()
                {
                    Text = child.Name,
                    Path = child.Path
                };
                Root.Nodes.Add(n);

                CollectTreeNode(n, child);
            }            
        }

        void Validate()
        {
            validating = true;
            var path = Project.AdapterConfiguration.Save("adapter.yml");            
            NavigationManager.NavigateTo($"Download?path={path}", true);
            validating = false;
        }
    }
}
