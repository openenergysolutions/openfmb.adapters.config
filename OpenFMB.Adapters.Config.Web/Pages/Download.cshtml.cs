using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OpenFMB.Adapters.Config.Web.Pages
{
    public class DownloadModel : PageModel
    {
        public IActionResult OnGet(string path)
        {            
            var content = System.IO.File.ReadAllBytes(path);
            return File(content, "application/octet-stream", "adapter.zip");
        }
    }
}
