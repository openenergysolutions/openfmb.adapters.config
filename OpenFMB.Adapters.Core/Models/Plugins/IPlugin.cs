// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace OpenFMB.Adapters.Core.Models.Plugins
{
    public interface IPlugin : IYamlNode
    {
        bool Enabled { get; set; }
    }

    public interface ISessionable : IPlugin
    {
        List<Session> Sessions { get; }
    }

    public interface ITransportPlugin : IPlugin
    {
        List<ITopic> Publishes { get; }
        List<ITopic> Subscribes { get; }
    }

    public interface ITopic
    {
        string Profile { get; set; }
        string Subject { get; set; }
    }

    public class Publish : ITopic
    {
        public string Profile { get; set; } = "ENTER_PROFILE_NAME";

        public string Subject { get; set; } = "*";
    }

    public class Subscribe : ITopic
    {
        public string Profile { get; set; } = "ENTER_PROFILE_NAME";

        public string Subject { get; set; } = "*";
    }

    public enum SessionablePluginType
    {
        MODBUS,
        DNP3,
        GOOSEPUB,
        GOOSESUB,
        ICCP
    }

    public enum TransportPluginType
    {
        NATS,
        MQTT,
        ZENOH
    }
}