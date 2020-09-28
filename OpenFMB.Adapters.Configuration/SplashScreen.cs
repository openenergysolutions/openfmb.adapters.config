﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;
using OpenFMB.Adapters.Core.Utility.Logs;
using OpenFMB.Adapters.Core.Models.Schemas;

namespace OpenFMB.Adapters.Configuration
{
    public partial class SplashScreen : Form, ILogger
    {
        private MainForm _mainWindow;       

        public SplashScreen()
        {
            InitializeComponent();
            MasterLogger.Instance.Subscribe(this);            
        }

        public SplashScreen(MainForm mainWindow)
            : this()
        {
            _mainWindow = mainWindow;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var v = System.Configuration.ConfigurationManager.AppSettings[Program.DefaultSchemaVersionKey];
            SchemaManager.Init(v);
            
            status.BeginInvoke(new MethodInvoker(delegate()
            {
                timer.Enabled = false;
                _mainWindow = new MainForm();
                _mainWindow.Splash = this;
                status.Text = string.Empty;
                progressBar.Value = progressBar.Maximum;
                _mainWindow.Show(); 
            }));
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            timer.Enabled = true;
            backgroundWorker.RunWorkerAsync();
        }        

        public void Log(Level level, string message, object tag = null)
        {
            status.BeginInvoke(new MethodInvoker(delegate()
            {
                status.Text = message;
            }));
        }

        public void Log(Level level, string message, Exception relatedException, object tag = null)
        {
            status.BeginInvoke(new MethodInvoker(delegate()
            {
                status.Text = message;
            }));
        }        

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (progressBar.Value == progressBar.Maximum)
            {
                progressBar.Value = progressBar.Minimum;
            }
            else
            {
                progressBar.PerformStep();
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MasterLogger.Instance.Unsubscribe(this);
        }
    }
}