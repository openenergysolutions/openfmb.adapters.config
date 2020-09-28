using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenFMB.Adapters.Config.Web.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        void NewConfiguration()
        {
            NavigationManager.NavigateTo("NewConfiguration");
        }

        void List()
        {
            NavigationManager.NavigateTo("List");
        }
    }
}
