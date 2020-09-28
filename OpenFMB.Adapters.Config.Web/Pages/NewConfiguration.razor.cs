using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OpenFMB.Adapters.Config.Web.Models;
using OpenFMB.Adapters.Config.Web.Services;
using OpenFMB.Adapters.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenFMB.Adapters.Config.Web.Pages
{
    public partial class NewConfiguration : ComponentBase
    {
        [Inject]
        IJSRuntime jsRuntime { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        ProjectService ProjectService { get; set; }

        void OnItemSelected(ItemClass item, bool value)
        {
            item.IsSelected = item.Plugin.Enabled = value;
        }

        public string Name { get; set; } = "Adapter Configuration";
        public string Description { get; set; } = string.Empty;

        void RunOnClick(MouseEventArgs e)
        {            
            var temp = ProjectService.Create(new Project()
            {                     
                Name = !string.IsNullOrWhiteSpace(Name) ? Name : "Adapter Configuration",
                Description = Description,
                AdapterConfiguration = config
            });
            
            NavigationManager.NavigateTo("Configuration?projectid=" + temp.Id, true );
        }        

        AdapterConfiguration config = new AdapterConfiguration();

        List<ItemClass> items = new List<ItemClass>();

        class ItemClass
        {
            public string Name { get; set; }
            public bool IsSelected { get; set; }
            public OpenFMB.Adapters.Core.Models.Plugins.IPlugin Plugin { get; set; }

            public ItemClass(string name, bool isSelected, OpenFMB.Adapters.Core.Models.Plugins.IPlugin plugin)
            {
                Name = name;
                IsSelected = isSelected;
                Plugin = plugin;
            }
        }

        protected async override Task OnInitializedAsync()
        {
            foreach (var p in config.Plugins.Plugins)
            {
                items.Add(new ItemClass(p.Name, false, p));
            }
            await Task.Yield();
        }
    }
}
