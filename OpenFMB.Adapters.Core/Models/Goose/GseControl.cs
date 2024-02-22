// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

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
