// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
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
