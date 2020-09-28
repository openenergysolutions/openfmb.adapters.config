﻿/*
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenFMB.Adapters.Configuration
{
    static class Program
    {
        public static readonly string DefaultSchemaVersionKey = "schema-default-version";
        public static readonly string AppName = "OpenFMB Adapter Configuration";
        public static MainForm Mainform
        {
            get;
            set;
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Mainform = new MainForm();
            SplashScreen screen = new SplashScreen(); ;
            Application.Run(screen);
        }
    }
}