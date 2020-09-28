using Microsoft.AspNetCore.Components;
using OpenFMB.Adapters.Config.Web.Services;
using OpenFMB.Adapters.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace OpenFMB.Adapters.Config.Web.Shared
{
    public partial class MainDrawerContent : ComponentBase
    {
        class NavItem
        {
            public NavGroup Group { get; set; }
            public string Name { get; set; }
            public string Url { get; set; } = string.Empty;
            public int Order { get; set; } = 0;
            public object Tag { get; set; } = null;
        }

        class NavGroup
        {
            public string Name { get; set; }
            public int Order { get; set; }

            public NavGroup(string name, int order = 0)
            {
                Name = name;
                Order = order;
            }
        }

        class NavGroupModel
        {
            public NavGroup Group;
            public NavItem[] Items;
        }        

        List<NavGroupModel> models = new List<NavGroupModel>();

        bool hasActiveProject = false;

        [Inject]
        NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();            

            string id = null;
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("projectid", out var token))
            {
                id = token.First();
            }

            if (id != null)
            {
                var project = projectService.Get(id);

                if (project == null)
                {
                    hasActiveProject = false;
                }
                else
                {
                    hasActiveProject = true;

                    var config = project.AdapterConfiguration;

                    var groups = new
                    {
                        Current = new NavGroup("General Information", 1),
                        PlugIns = new NavGroup("Plugins", 2)
                    };
                    var navItems = new List<NavItem>();

                    navItems.Add(new NavItem()
                    {
                        Name = config.FilePath,
                        Group = groups.Current,
                        Url = "Configuration?projectId=" + id
                    });

                    foreach (var p in config.Plugins.Plugins)
                    {
                        navItems.Add(new NavItem()
                        {
                            Name = p.Name,
                            Group = groups.PlugIns,
                            Url = p.Name + "?projectId=" + id
                        });
                    }

                    models = navItems
                        .GroupBy(i => i.Group)
                        .OrderBy(i => i.Key.Order)
                        .ThenBy(i => i.Key.Name)
                        .Select(g =>
                        {
                            return new NavGroupModel()
                            {
                                Group = g.Key,
                                Items = g
                                    .OrderBy(i => i.Order)
                                    .ThenBy(i => i.Name)
                                    .ToArray(),
                            };
                        })
                        .ToList();
                }
            }            
        }

        protected async Task CloseProject()
        {
            await Task.Run(() => {
                NavigationManager.NavigateTo("/", true);
            });
        }
    }
}
