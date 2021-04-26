// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using OpenFMB.Adapters.Configuration.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace OpenFMB.Adapters.Configuration
{
    public static class RecentFileManager
    {
        public static EventHandler<EventArgs> OnRecentFileChanged;

        public static List<string> ToList(this StringCollection collection)
        {
            return collection.Cast<string>().ToList();
        }

        public static List<string> RecentFiles()
        {
            List<string> list = new List<string>();
            if (Settings.Default.RecentFiles != null)
            {
                list.AddRange(Settings.Default.RecentFiles.ToList());
            }
            return list;
        }

        public static void AddFile(string filePath)
        {
            if (Settings.Default.RecentFiles == null)
            {
                Settings.Default.RecentFiles = new System.Collections.Specialized.StringCollection();
            }

            var list = Settings.Default.RecentFiles.ToList();

            var old = list.FirstOrDefault(x => x.Equals(filePath, StringComparison.InvariantCultureIgnoreCase));
            if (old != null)
            {
                list.Remove(old);
            }

            list.Add(filePath);
            if (list.Count > 10)
            {
                list.RemoveAt(0);
            }

            Settings.Default.RecentFiles.Clear();
            Settings.Default.RecentFiles.AddRange(list.ToArray());
            Settings.Default.Save();

            if (OnRecentFileChanged != null)
            {
                OnRecentFileChanged(filePath, EventArgs.Empty);
            }
        }

        public static void RemoveFile(string filePath)
        {
            if (Settings.Default.RecentFiles == null)
            {
                return;
            }

            var list = Settings.Default.RecentFiles.ToList();

            var item = list.FirstOrDefault(x => x.Equals(filePath, StringComparison.InvariantCultureIgnoreCase));
            list.Remove(item);

            Settings.Default.RecentFiles.Clear();
            Settings.Default.RecentFiles.AddRange(list.ToArray());
            Settings.Default.Save();

            if (OnRecentFileChanged != null)
            {
                OnRecentFileChanged(filePath, EventArgs.Empty);
            }
        }

        public static string TruncateFile(this string str, int maxLength = 50, char delimiter = '\\')
        {
            maxLength -= 3; //account for delimiter spacing

            if (str.Length <= maxLength)
            {
                return str;
            }

            string final = str;
            List<string> parts;

            int loops = 0;
            while (loops++ < 100)
            {
                parts = str.Split(delimiter).ToList();
                parts.RemoveRange(parts.Count - 1 - loops, loops);
                if (parts.Count == 1)
                {
                    return parts.Last();
                }

                parts.Insert(parts.Count - 1, "...");
                final = string.Join(delimiter.ToString(), parts);
                if (final.Length < maxLength)
                {
                    return final;
                }
            }

            return str.Split(delimiter).ToList().Last();
        }
    }
}
