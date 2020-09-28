using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using OpenFMB.Adapters.Config.Web.Models;
using OpenFMB.Adapters.Config.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenFMB.Adapters.Config.Web.Pages
{
    public partial class Configuration : ComponentBase
    {
        [Inject]
        ProjectService ProjectService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        public Project Project { get; set; }

        protected async override Task OnInitializedAsync()
        {
            string id = null;
            var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

            if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("projectid", out var token))
            {
                id = token.First();
            }

            await Task.Run(() =>
            {
                if (!string.IsNullOrEmpty(id))
                {
                    Project = ProjectService.Get(id);
                }
            });            
           
        }


        void Download()
        {
            if (Project != null)
            {
                var path = Project.AdapterConfiguration.Save("adapter.yml");
                NavigationManager.NavigateTo($"Download?path={path}", true);
            }            
        }
    }
}
