// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

namespace OpenFMB.Adapters.Core.Models.Schemas
{
    public class ValidationErrorMessage
    {
        public string File { get; set; } = string.Empty;
        public string Message { get; set; }
        public string NodePath { get; set; }

        public static ValidationErrorMessage Parse(string message)
        {
            ValidationErrorMessage msg = new ValidationErrorMessage();

            var index = message.IndexOf("Path '");
            if (index >= 0)
            {
                msg.NodePath = message.Substring(index + 6).Trim(new char[] { '\'', '.' });
                msg.Message = message.Substring(0, index).Trim();
            }

            return msg;
        }
    }
}
