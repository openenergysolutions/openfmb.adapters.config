// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;

namespace OpenFMB.Adapters.Core.Utility.Logs
{
    public class OutputWriter : IOutput
    {
        private static readonly OutputWriter _instance = new OutputWriter();
        private readonly List<IOutput> _outputTerminals = new List<IOutput>();

        public static OutputWriter Instance
        {
            get
            {
                return OutputWriter._instance;
            }
        }

        private OutputWriter()
        {
        }

        public void Subscribe(IOutput terminal)
        {
            this._outputTerminals.Add(terminal);
        }

        public void Unsubscribe(IOutput terminal)
        {
            this._outputTerminals.RemoveAll((Predicate<IOutput>)(x => x == terminal));
        }

        public void Output(OutputLevel level, string message)
        {
            foreach (IOutput terminal in this._outputTerminals)
            {
                terminal.Output(level, message);
            }
        }
    }
}
