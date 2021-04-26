// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace OpenFMB.Adapters.Core.Models.Goose
{
    public class DataSet
    {
        private readonly List<FCDA> _fcdas = new List<FCDA>();

        public string Name { get; set; }
        public List<FCDA> FCDAs { get { return _fcdas; } }

    }
}
