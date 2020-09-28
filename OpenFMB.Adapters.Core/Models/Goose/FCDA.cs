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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenFMB.Adapters.Core.Models.Goose
{
    public class FCDA
    {                       
        public string LDInst { get; set; }
        public string Prefix { get; set; }
        public string LnClass { get; set; }
        public int LnInst { get; set; }
        public string DoName { get; set; }
        public string DaName { get; set; }  
        public string LnTypeId { get; set; }
        public DataType DataType { get; set; }
        public string IEC61850DataType { get; set; }
        public string Name
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            return $"{Prefix}{LnClass}{LnInst}.{DoName}.{DaName}";
        }
    }
}
