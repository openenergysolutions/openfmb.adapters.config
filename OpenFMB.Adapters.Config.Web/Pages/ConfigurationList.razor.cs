using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OpenFMB.Adapters.Config.Web.Models;
using OpenFMB.Adapters.Config.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenFMB.Adapters.Config.Web.Pages
{
    public partial class ConfigurationList : ComponentBase
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Inject]
        ProjectService ProjectService { get; set; }

        List<Project> Projects { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Projects = ProjectService.Get();
            await Task.Yield();
        }

        protected void SelectionChangedEvent(object row)
        {
            JSRuntime.InvokeVoidAsync("console.log", row);
            Project project = row as Project;

            NavigationManager.NavigateTo("Configuration?projectid=" + project.Id, true);
            StateHasChanged();
        }

        public void Open()
        {
            JSRuntime.InvokeVoidAsync("console.log", "test");
        }
    }
}
