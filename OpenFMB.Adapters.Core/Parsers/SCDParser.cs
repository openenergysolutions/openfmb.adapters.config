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

using OpenFMB.Adapters.Core.Models.Goose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace OpenFMB.Adapters.Core.Parsers
{
    public class SCDParser
    {
        public static List<IED> Parse(string filePath)
        {
            List<IED> list = new List<IED>();

            XDocument doc = XDocument.Load(filePath);
            var ns = doc.Root.GetDefaultNamespace();

            var iedNodes = doc.Root.Elements(ns + "IED");

            foreach (var node in iedNodes)
            {
                string mrid = string.Empty;

                var mridNode = node.Elements(ns + "Private").FirstOrDefault(x => x.Attribute("type").Value == "mRID");
                if (mridNode != null)
                {
                    mrid = mridNode.Value;
                }
                else
                {
                    mrid = Guid.NewGuid().ToString().ToLower();
                }
                IED ied = new IED()
                {
                    Name = node.Attribute("name").Value,
                    MRID = mrid
                };

                // Access point LDO
                //var accessPoint = node.Elements(ns + "AccessPoint").FirstOrDefault(x => x.Attribute("name").Value == "LD0");
                var accessPoint = GetAccessPoint(node, ns);
                if (accessPoint != null)
                {
                    // Server

                    var server = accessPoint.Element(ns + "Server");

                    if (server != null)
                    {
                        // LD0
                        //var ld0 = server.Elements(ns + "LDevice").FirstOrDefault(x => x.Attribute("inst").Value == "LD0");
                        var ld0 = GetLD0(server, ns);
                        if (ld0 != null)
                        {
                            // LN0
                            var ln0 = ld0.Element(ns + "LN0");
                            if (ln0 != null)
                            {
                                // GSEControl
                                var gseControls = ln0.Elements(ns + "GSEControl");
                                if (gseControls != null && gseControls.Count() > 0)
                                {
                                    // Found GOOSE control blocks
                                    list.Add(ied);

                                    foreach (var gseControlNode in gseControls)
                                    {
                                        GseControl gseControl = new GseControl()
                                        {
                                            IED = ied,
                                            LogicalDevice = "LD0",
                                            LogicalNode = ln0.Attribute("lnClass").Value,
                                            Name = gseControlNode.Attribute("name").Value,
                                            GooseId = gseControlNode.Attribute("appID").Value,
                                            ConfRev = gseControlNode.Attribute("confRev").Value,
                                            Dataset = new DataSet()
                                            {
                                                Name = gseControlNode.Attribute("datSet").Value
                                            }
                                        };
                                        SetCommunication(doc.Root, ns, gseControl);

                                        ied.GseControls.Add(gseControl);

                                        var dataSetNode = ln0.Elements(ns + "DataSet").FirstOrDefault(x => x.Attribute("name").Value == gseControl.Dataset.Name);
                                        if (dataSetNode != null)
                                        {
                                            // FCDAs
                                            foreach (var fcdaNode in dataSetNode.Elements(ns + "FCDA"))
                                            {
                                                FCDA fcda = new FCDA()
                                                {
                                                    LDInst = fcdaNode.Attribute("ldInst").Value,
                                                    Prefix = fcdaNode.Attribute("prefix")?.Value,
                                                    LnClass = fcdaNode.Attribute("lnClass").Value,
                                                    LnInst = Convert.ToInt32(fcdaNode.Attribute("lnInst").Value),
                                                    DoName = fcdaNode.Attribute("doName").Value,
                                                    DaName = fcdaNode.Attribute("daName").Value
                                                };

                                                var logicalDevice = server.Elements(ns + "LDevice").FirstOrDefault(x => x.Attribute("inst").Value == fcda.LDInst);
                                                //Get lnType
                                                var ln = logicalDevice.Elements(ns + "LN").
                                                    FirstOrDefault(x => x.Attribute("prefix")?.Value == fcda.Prefix &&
                                                    x.Attribute("lnClass")?.Value == fcda.LnClass &&
                                                    x.Attribute("inst")?.Value == fcda.LnInst.ToString());

                                                fcda.LnTypeId = ln.Attribute("lnType")?.Value;

                                                SetDataTypeForFCDA(doc.Root, ns, fcda);

                                                gseControl.Dataset.FCDAs.Add(fcda);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        private static void SetCommunication(XElement root, XNamespace ns, GseControl gseControl)
        {
            var communication = root.Element(ns + "Communication");
            if (communication != null)
            {
                var subnetwork = communication.Element(ns + "SubNetwork");
                if (subnetwork != null)
                {
                    var connectedAP = subnetwork.Elements(ns + "ConnectedAP").FirstOrDefault(x => x.Attribute("iedName").Value == gseControl.IED.Name);
                    if (connectedAP != null)
                    {
                        var gse = connectedAP.Elements(ns + "GSE").FirstOrDefault(x => x.Attribute("cbName").Value == gseControl.Name);
                        if (gse != null)
                        {
                            var address = gse.Element(ns + "Address");
                            if (address != null)
                            {
                                var mac = address.Elements(ns + "P").FirstOrDefault(x => x.Attribute("type").Value == "MAC-Address");
                                if (mac != null)
                                {
                                    gseControl.DestinationMACAddress = mac.Value.Replace('-', ':');
                                }
                                var appId = address.Elements(ns + "P").FirstOrDefault(x => x.Attribute("type").Value == "APPID");
                                if (appId != null)
                                {
                                    gseControl.AppId = int.Parse(appId.Value, System.Globalization.NumberStyles.HexNumber);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void SetDataTypeForFCDA(XElement root, XNamespace ns, FCDA fcda)
        {
            var dataTypeTemplate = root.Element(ns + "DataTypeTemplates");

            if (dataTypeTemplate != null)
            {
                // LNodeType                                

                var lNodeType = dataTypeTemplate.Elements(ns + "LNodeType").FirstOrDefault(x => x.Attribute("id").Value == fcda.LnTypeId);

                if (lNodeType != null)
                {
                    // DO       
                    var doNames = fcda.DoName.Split('.');

                    string sdo = null;
                    if (doNames.Length > 1)
                    {
                        sdo = doNames[1];
                    }

                    var doNode = lNodeType.Elements(ns + "DO").FirstOrDefault(x => x.Attribute("name").Value == doNames[0]);

                    if (doNode != null)
                    {
                        var type = doNode.Attribute("type").Value;

                        var doTypeNode = dataTypeTemplate.Elements(ns + "DOType").FirstOrDefault(x => x.Attribute("id").Value == type);

                        if (doTypeNode != null)
                        {
                            if (!string.IsNullOrEmpty(sdo))
                            {
                                var sdoNode = doTypeNode.Elements(ns + "SDO").FirstOrDefault(x => x.Attribute("name")?.Value == sdo);
                                if (sdoNode != null)
                                {
                                    type = sdoNode.Attribute("type")?.Value;
                                    doTypeNode = dataTypeTemplate.Elements(ns + "DOType").FirstOrDefault(x => x.Attribute("id").Value == type);
                                }
                            }

                            // DA
                            var daNames = fcda.DaName.Split('.');

                            var da = doTypeNode.Elements(ns + "DA").FirstOrDefault(x => x.Attribute("name").Value == daNames[0]);
                            if (da != null)
                            {
                                var bType = da.Attribute("bType").Value;                                
                                
                                if (bType == "Struct")
                                {
                                    var daType = da.Attribute("type").Value;
                                    SetDataTypeForFCDA(dataTypeTemplate, ns, fcda, daType, daNames);
                                }
                                else if (bType == "Dbpos")
                                {
                                    // Check enum type
                                    var enumType = dataTypeTemplate.Elements(ns + "EnumType").FirstOrDefault(x => x.Attribute("id").Value == bType);
                                    if (enumType != null)
                                    {
                                        fcda.IEC61850DataType = bType;
                                        fcda.DataType = DataType.bitstring;
                                    }
                                }
                                else if (bType == "Quality")
                                {
                                    fcda.IEC61850DataType = bType;
                                    fcda.DataType = DataType.bitstring;
                                }
                                else if (bType == "Timestamp")
                                {
                                    fcda.IEC61850DataType = bType;
                                    fcda.DataType = DataType.utc_time;
                                }                                
                                else if (bType == "FLOAT32")
                                {
                                    fcda.IEC61850DataType = bType;
                                    fcda.DataType = DataType.floating;
                                }
                                else
                                {
                                    System.Diagnostics.Debug.WriteLine("********* Unhandled BTYPE = " + bType);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void SetDataTypeForFCDA(XElement dataTypeTemplate, XNamespace ns, FCDA fcda, string daTypeId, string[] daNames)
        {
            var daTypeNode = dataTypeTemplate.Elements(ns + "DAType").FirstOrDefault(x => x.Attribute("id")?.Value == daTypeId);
            if (daTypeNode != null)
            {
                foreach(var daName in daNames)
                {
                    var bda = daTypeNode.Elements(ns + "BDA")?.FirstOrDefault(x => x.Attribute("name")?.Value == daName);
                    if (bda != null)
                    {
                        var bType = bda.Attribute("bType").Value;
                        
                        if (bType == "Struct")
                        {
                            var daType = bda.Attribute("type").Value;
                            SetDataTypeForFCDA(dataTypeTemplate, ns, fcda, daType, daNames);
                        }
                        else if (bType == "Quality")
                        {
                            fcda.IEC61850DataType = bType;
                            fcda.DataType = DataType.bitstring;
                        }
                        else if (bType == "Timestamp")
                        {
                            fcda.IEC61850DataType = bType;
                            fcda.DataType = DataType.utc_time;
                        }
                        else if (bType == "FLOAT32")
                        {
                            fcda.IEC61850DataType = bType;
                            fcda.DataType = DataType.floating;
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("********* Unhandled BTYPE = " + bType);
                        }
                    }
                }
            }
        }

        private static XElement GetAccessPoint(XElement node, XNamespace ns)
        {
            var accessPoint = node.Elements(ns + "AccessPoint").FirstOrDefault(x => x.Attribute("name").Value == "LD0");

            if (accessPoint == null)
            {
                foreach(var elem in node.Elements(ns + "AccessPoint"))
                {
                    if (elem.Element(ns + "Server") != null) {
                        accessPoint = elem;
                        break;
                    }
                }
            }
            return accessPoint;
        }

        private static XElement GetLD0(XElement server, XNamespace ns)
        {
            var ld0 = server.Elements(ns + "LDevice").FirstOrDefault(x => x.Attribute("inst").Value == "LD0");
            if (ld0 == null)
            {
                foreach(var ldevice in server.Elements(ns + "LDevice"))
                {
                    var ln0 = ldevice.Element(ns + "LN0");
                    if (ln0 != null)
                    {
                        var datasets = ln0.Elements(ns + "DataSet");
                        if (datasets != null && datasets.Count() > 0)
                        {
                            ld0 = ldevice;
                            break;
                        }
                    }
                }
            }
            return ld0;
        }
    }
}