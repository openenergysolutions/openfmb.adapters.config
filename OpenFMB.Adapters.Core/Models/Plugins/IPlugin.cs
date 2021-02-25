/*
 * Copyright 2017-2020 Duke Energy Corporation and Open Energy Solutions, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
        MQTT
    }
}