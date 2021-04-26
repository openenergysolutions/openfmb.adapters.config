// SPDX-FileCopyrightText: 2021 Open Energy Solutions Inc
//
// SPDX-License-Identifier: Apache-2.0

using Newtonsoft.Json;
using OpenFMB.Adapters.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OpenFMB.Adapters.Configuration
{
    public class TagsManager
    {
        public static string MasterModelFile = "master-model.json";
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
            if (!File.Exists(MasterModelFile))
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "OpenFMB.Adapters.Configuration.master-model.json";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string content = reader.ReadToEnd();
                        File.WriteAllText(MasterModelFile, content);
                    }
                }
            }

            Model = JsonConvert.DeserializeObject<Model>(File.ReadAllText(MasterModelFile));
        }

        public void Save()
        {
            var json = JsonConvert.SerializeObject(Model);
            File.WriteAllText(MasterModelFile, json);
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

            return model.Topics.Select(x => x.Attributes.Name).ToList();
        }

        public List<string> GetLabelsByProfileName(string profileName)
        {
            var model = Model.ProfileModels.FirstOrDefault(x => x.Name == profileName);

            return model.Topics.Select(x => x.Attributes.Label).ToList();
        }
    }
}
