// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;

namespace OpenFMB.Adapters.Core.Utility.Logs
{
    public class MasterLogger : ILogger, ILogMessagePublisher
    {
        private static readonly MasterLogger _instance = new MasterLogger();
        private List<ILogger> _loggers = new List<ILogger>();

        public static MasterLogger Instance
        {
            get
            {
                return MasterLogger._instance;
            }
        }

        public Level MinimumLoglevel
        {
            get;
            set;
        }

        private MasterLogger()
        {
        }

        public void Subscribe(ILogger logger)
        {
            this._loggers.Add(logger);
        }

        public void Unsubscribe(ILogger logger)
        {
            this._loggers.RemoveAll((Predicate<ILogger>)(x => x == logger));
        }

        public void Log(Level level, string message, object tag = null)
        {
            this.Log(level, message, (Exception)null, tag);
        }

        public void Log(Level level, string message, Exception relatedException, object tag = null)
        {
            foreach (ILogger logger in this._loggers)
            {
                if (level >= this.MinimumLoglevel)
                    logger.Log(level, message, relatedException, tag);
            }
        }
    }
}
