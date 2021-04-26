// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace OpenFMB.Adapters.Core.Models.Goose
{
    public class IED
    {
        private readonly List<GseControl> _gseControls = new List<GseControl>();
        public string Name { get; set; }

        public string MRID { get; set; }

        public List<GseControl> GseControls { get { return _gseControls; } }
    }
}
