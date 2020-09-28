using System;
using System.Collections.Generic;
using System.Text;

namespace OpenFMB.Adapters.Core.Models.Schemas
{
    public class NoSchemaFoundException : Exception
    {
        public string ProfileName { get; private set; }
        public string PlugIn { get; private set; }
        public NoSchemaFoundException(string message, string profileName, string plugin) : base(message)
        {
            ProfileName = profileName;
            PlugIn = plugin;
        }
    }
}
