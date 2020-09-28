using System;
using System.Collections.Generic;
using System.Text;

namespace OpenFMB.Adapters.Core
{
    public interface Editable
    {
        string FullPath { get; }
        void Save();
        bool HasChanged();
    }
}
