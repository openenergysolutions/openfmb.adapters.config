// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

namespace OpenFMB.Adapters.Core.Models.Goose
{
    public class GseControlSelection
    {
        public static readonly string Subscribe = "Subscribe";
        public static readonly string Publish = "Publish";

        public bool Selected { get; set; } = true;

        public string Name { get; set; }

        public GseControl GseControl { get; set; }

        public string Profile { get; set; }

        public string Direction { get; set; } = Subscribe;

    }
}