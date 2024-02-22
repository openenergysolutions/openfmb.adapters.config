// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core.Models;
using System;

namespace OpenFMB.Adapters.Configuration
{
    public interface INavigatorNode : IDataNode
    {
        event EventHandler OnDrillDown;

        bool IsValid { get; }

        bool IsLeafNode { get; }
    }
}
