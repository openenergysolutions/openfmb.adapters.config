using OpenFMB.Adapters.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenFMB.Adapters.Configuration
{
    public interface INavigatorNode : IDataNode
    {
        event EventHandler OnDrillDown;
       
        bool IsValid { get; }

        bool IsLeafNode { get; }
    }
}
