// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Globalization;

namespace OpenFMB.Adapters.Core.Utility
{
    public static class Utils
    {
        public static string basePath = "";

        public static PlatformID OS = PlatformID.Unix;

        private static readonly string BadCharsRegex = "[" + Regex.Escape(new string(Path.GetInvalidFileNameChars())) + "]";

        private static readonly Regex ContainsABadCharacter = new Regex(BadCharsRegex);
        private static readonly Regex EndArrayRegex = new Regex("\\[\\d+\\]$");        
        private static readonly Regex PathWithArrayNoDot = new Regex("\\w\\[\\d+\\]");
        private static readonly Regex PathWithArray = new Regex("\\.\\[\\d+\\]");

        public static readonly Regex ArrayNode = new Regex("\\.\\[\\d+\\]$");

        private static readonly char[] Slashes = { '/', '\\' };

        public static T DeepCopy<T>(T source)
        {
            using (MemoryStream memoryStream = new MemoryStream(10))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(memoryStream, source);
                memoryStream.Seek(0, SeekOrigin.Begin);
                T clone = (T)serializer.Deserialize(memoryStream);
                return clone;
            }
        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, true);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">File name.  Important! This is not file path, so no slash allowed</param>
        /// <returns></returns>
        public static bool IsValidFileName(string fileName)
        {
            System.IO.FileInfo fi = null;
            try
            {
                fi = new System.IO.FileInfo(fileName);

                if (fileName.IndexOfAny(Slashes) > 0)
                {
                    return false;
                }

                if (ContainsABadCharacter.IsMatch(fileName))
                {
                    return false;
                }

                return fi != null;
            }
            catch
            {
                return false;
            }
        }
        

        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string ChecksumForFile(string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }

        public static string ChecksumForString(string str)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = GenerateStreamFromString(str))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }

        public static string ChecksumForByteArray(byte[] data)
        {
            using (MemoryStream stream = new MemoryStream(data))
            {
                using (var md5 = MD5.Create())
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLower();
                }
            }
        }

        public static string ChecksumForFolder(string path)
        {
            try
            {
                var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                                     .OrderBy(p => p).ToList();

                if (files.Count > 0)
                {
                    using (MD5 md5 = MD5.Create())
                    {
                        for (int i = 0; i < files.Count; i++)
                        {
                            string file = files[i];

                            // hash path
                            string relativePath = file.Substring(path.Length + 1);
                            byte[] pathBytes = Encoding.UTF8.GetBytes(relativePath.ToLower());
                            md5.TransformBlock(pathBytes, 0, pathBytes.Length, pathBytes, 0);

                            // hash contents
                            byte[] contentBytes = File.ReadAllBytes(file);
                            if (i == files.Count - 1)
                            {
                                md5.TransformFinalBlock(contentBytes, 0, contentBytes.Length);
                            }
                            else
                            {
                                md5.TransformBlock(contentBytes, 0, contentBytes.Length, contentBytes, 0);
                            }
                        }

                        return BitConverter.ToString(md5.Hash).Replace("-", "").ToLower();
                    }
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        public static DateTime ParseDateTime(string s)
        {
            string[] parts = s.Split('.');

            double seconds = double.Parse(parts[0].Trim(), CultureInfo.InvariantCulture);

            DateTime dt = Utils.ConvertFromUnixTimestamp(seconds);
            if (parts.Length == 2)
            {
                int milli = int.Parse(parts[1].Trim(), CultureInfo.InvariantCulture);
                dt = dt.AddMilliseconds(milli);
            }
            return dt;
        }
    
        public static string RemoveEndArray(string input)
        {
            return EndArrayRegex.Replace(input, string.Empty);
        }

        public static string RemoveDotBeforeArray(string input)
        {
            var matches = PathWithArray.Matches(input);

            foreach (Capture m in matches)
            {
                input = input.Replace(m.Value, m.Value.Substring(1));
            }
            return input;
        }        

        public static string AddDotBeforeArray(string input)
        {
            var matches = PathWithArrayNoDot.Matches(input);

            foreach (Capture m in matches)
            {
                input = input.Replace(m.Value, $"{m.Value.Insert(1, ".")}");
            }
            return input;
        }

        public static string ReplaceWithIndexZeroArray(string input)
        {
            return PathWithArray.Replace(input, ".[0]");
        }

        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
}