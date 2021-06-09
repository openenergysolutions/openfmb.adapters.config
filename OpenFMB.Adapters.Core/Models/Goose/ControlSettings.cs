// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

namespace OpenFMB.Adapters.Core.Models.Goose
{
    public class ControlSettings
    {
        public OriginCategory OrCat { get; set; } = OriginCategory.RemoteControl;

        public string OrIdent { get; set; } = string.Empty;
    }

    public enum OriginCategory
    {
        NotSupported,
        BatControl,
        StationControl,
        RemoteControl,
        AutomaticBay,
        AutomaticStation,
        AutomaticRemote,
        Maintenance,
        Process
    }
}
