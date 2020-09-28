using OpenFMB.Adapters.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenFMB.Adapters.Configuration
{
    public class ModuleValue
    {
        public string Name { get; set; }
        public List<ProfileModel> Value { get; set; }
    }
}
