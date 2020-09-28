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
