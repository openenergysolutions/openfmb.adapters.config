// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Core;
using System.Collections.Generic;

namespace OpenFMB.Adapters.Configuration
{
    public class ModuleValue
    {
        public string Name { get; set; }
        public List<ProfileModel> Value { get; set; }
    }
}
