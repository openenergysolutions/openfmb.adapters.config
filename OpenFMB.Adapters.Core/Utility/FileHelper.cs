// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;

namespace OpenFMB.Adapters.Core.Utility
{
    public static class FileHelper
    {
        private static readonly string AppName = "OpenFMB Adapter Configuration";

        public static string GetNextConfigFileName(this string folder, string fileBaseName)
        {
            int i = 1;
            string output = fileBaseName;
            while (true)
            {
                if (!File.Exists(Path.Combine(folder, output + ".yml")) && !File.Exists(Path.Combine(folder, output + ".yaml")))
                {
                    break;
                }
                else
                {
                    output = $"{fileBaseName}{i++}";
                }
            }

            return Path.Combine(folder, $"{output}.yaml");
        }

        public static string GetNextFileName(this string folder, string baseName, string extension = "")
        {
            int i = 1;
            string output = baseName;
            while (true)
            {
                if (!File.Exists(Path.Combine(folder, output + extension)))
                {
                    break;
                }
                else
                {
                    if (!baseName.EndsWith(" copy"))
                    {
                        baseName += " copy";
                        output = baseName;
                    }
                    else
                    {
                        output = $"{baseName}{i++}";
                    }
                }
            }

            return Path.Combine(folder, $"{output}{extension}");
        }

        public static string GetNextFolderName(this string folder, string baseName)
        {
            int i = 1;
            string output = baseName;
            while (true)
            {
                if (!Directory.Exists(Path.Combine(folder, output)))
                {
                    break;
                }
                else
                {
                    if (!baseName.EndsWith(" copy"))
                    {
                        baseName += " copy";
                        output = baseName;
                    }
                    else
                    {
                        output = $"{baseName}{i++}";
                    }
                }
            }

            return Path.Combine(folder, $"{output}");
        }

        public static bool FileExists(this string folder, string filename)
        {
            if (File.Exists(Path.Combine(folder, filename)))
            {
                return true;
            }

            foreach (string subDir in Directory.GetDirectories(folder))
            {
                if (FileExists(subDir, filename))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsInFolder(this string folder, string fileName)
        {
            var dir = NormalizePath(folder).ToLower();
            var file = NormalizePath(fileName).ToLower();

            return file.StartsWith(dir);
        }

        public static string NormalizePath(string path)
        {
            try
            {
                return Path.GetFullPath(new Uri(path).LocalPath)
                           .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                           .ToString();
            }
            catch
            {
                return Path.GetFullPath(path)
                           .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                           .ToString();
            }
        }

        public static string ConvertToForwardSlash(string path)
        {
            return path.Replace('\\', '/');
        }

        public static string MakeRelativePath(string basePath, string fullPath)
        {
            if (string.IsNullOrEmpty(basePath)) throw new ArgumentNullException("basePath");
            if (string.IsNullOrEmpty(fullPath)) throw new ArgumentNullException("fullPath");

            Uri fromUri = new Uri(basePath);
            Uri toUri = new Uri(fullPath);

            if (fromUri.Scheme != toUri.Scheme) { return fullPath; } // path can't be made relative.

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

                var folder = Path.GetFileName(basePath);
                relativePath = relativePath.Substring(folder.Length + 1);
            }

            return relativePath;
        }

        public static string GetRelativePath(string relativeTo, string path)
        {
            var uri = new Uri(relativeTo);
            var rel = Uri.UnescapeDataString(uri.MakeRelativeUri(new Uri(path)).ToString()).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            if (rel.Contains(Path.DirectorySeparatorChar.ToString()) == false)
            {
                rel = $".{Path.DirectorySeparatorChar}{rel}";
            }
            return rel;
        }

        public static bool FileEquals(string file1, string file2)
        {
            var f1 = NormalizePath(file1);
            var f2 = NormalizePath(file2);
            return f1.Equals(f2);
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs = true)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the destination directory doesn't exist, create it.       
            Directory.CreateDirectory(destDirName);

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string tempPath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                }
            }
        }

        public static string GetAppDataFolder()
        {
            var roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            Directory.CreateDirectory(Path.Combine(roaming, AppName));

            return Path.Combine(roaming, AppName);
        }

    }
}
