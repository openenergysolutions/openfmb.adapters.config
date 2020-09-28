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

namespace OpenFMB.Adapters.Core.Models.Goose
{
    public class GseControl
    {
        public IED IED { get; set; }
        public string LogicalDevice { get; set; }
        public string LogicalNode { get; set; }
        public string Name { get; set; }       
        public string GooseId { get; set; }
        public int AppId { get; set; }
        public string ConfRev { get; set; }
        public string DestinationMACAddress { get; set; }
        public DataSet Dataset { get; set; }
        public string GseControlReference
        {
            get { return $"{IED.Name}{LogicalDevice}/{LogicalNode}$GO${Name}"; }
        }
        public string DataSetReference
        {
            get { return $"{IED.Name}{LogicalDevice}/{LogicalNode}${Dataset.Name}"; }
        }
    }
}
