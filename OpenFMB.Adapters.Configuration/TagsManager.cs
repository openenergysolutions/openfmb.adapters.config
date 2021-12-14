// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json;
using OpenFMB.Adapters.Core;
using OpenFMB.Adapters.Core.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OpenFMB.Adapters.Configuration
{
    public class TagsManager
    {
        private static string MasterModelFileName = "master-model.json";        

        private static TagsManager _instance;

        public static TagsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TagsManager();
                }
                return _instance;
            }
        }

        public Model Model { get; private set; }       

        private TagsManager() 
        {
            Init();
        }

        private void Init()
        {
            var appDataDir = FileHelper.GetAppDataFolder();
            var masterModelFile = Path.Combine(appDataDir, MasterModelFileName);
            if (!File.Exists(masterModelFile))
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "OpenFMB.Adapters.Configuration.master-model.json";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string content = reader.ReadToEnd();
                        File.WriteAllText(masterModelFile, content);
                    }
                }
            }

            Model = JsonConvert.DeserializeObject<Model>(File.ReadAllText(masterModelFile));
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(Model);
            var appDataDir = FileHelper.GetAppDataFolder();
            var masterModelFile = Path.Combine(appDataDir, MasterModelFileName);
            File.WriteAllText(masterModelFile, json);
        }

        public List<Topic> Search(string profileName, string searchString)
        {
            var topics = new List<Topic>();
            var model = Model.ProfileModels.FirstOrDefault(x => x.Name == profileName);
            if (model != null)
            {
                var list = model.Topics.Where(x => x.Tags.ToLower() == searchString.ToLower() || x.Label.ToLower() == searchString.ToLower());
                if (list.Count() > 0)
                {
                    topics.AddRange(list);
                }
                else
                {
                    var tokens = searchString.ToLower().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var topic in model.Topics)
                    {
                        var count = topic.Attributes.Keywords.Intersect(tokens).Count();

                        if (count > 0)
                        {
                            var copy = topic.Copy();
                            copy.Matches = count;
                            topics.Add(copy);
                        }
                    }
                }
            }

            return topics.OrderByDescending(x => x.Matches).ToList();
        }

        public List<string> GetTagsByProfileName(string profileName)
        {
            var model = Model.ProfileModels.FirstOrDefault(x => x.Name == profileName);
            if (model != null)
            {
                return model.Topics.Select(x => x.Attributes.Name).ToList();
            }
            return new List<string>();
        }

        public List<string> GetLabelsByProfileName(string profileName)
        {
            var model = Model.ProfileModels.FirstOrDefault(x => x.Name == profileName);
            if (model != null)
            {
                return model.Topics.Select(x => x.Attributes.Label).ToList();
            }
            return new List<string>();
        }
    }
}
