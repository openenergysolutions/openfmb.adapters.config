using MongoDB.Driver;
using Newtonsoft.Json;
using OpenFMB.Adapters.Config.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OpenFMB.Adapters.Config.Web.Services
{
    public class ProjectService
    {
        private readonly static List<Project> _projects = new List<Project>();

        static ProjectService()
        {
            try
            {
                string[] files = Directory.GetFiles("db");

                foreach (var f in files)
                {
                    _projects.Add(JsonConvert.DeserializeObject<Project>(File.ReadAllText(f)));
                }

            }
            catch (Exception ex)
            { 
            }
        }       

        public List<Project> Get() =>
            _projects.ToList();

        public Project Get(string id) =>
            _projects.FirstOrDefault(x => x.Id == id);

        public Project Create(Project project)
        {
            project.Id = Guid.NewGuid().ToString().ToLower().Replace("-", "");
            _projects.Add(project);
            Save(project);
            return project;
        }

        private void Save(Project project)
        {            
            var json = JsonConvert.SerializeObject(project, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });            
            File.WriteAllText(Path.Combine("db", $"{project.Id}.json"), json);
        }

        public void Update(string id, Project projectIn)
        {
            var proj = _projects.FirstOrDefault(x => x.Id == id);
            if (proj != null)
            {
                _projects.Remove(proj);
            }
            _projects.Add(projectIn);
            Save(projectIn);
        }

        public void Remove(Project projectIn)
        {
            if (projectIn != null)
            {
                _projects.Remove(projectIn);
                File.Delete(Path.Combine("db", $"{projectIn.Id}.json"));
            }
        }

        public void Remove(string id)
        {
            var proj = _projects.FirstOrDefault(x => x.Id == id);
            Remove(proj);
        }            
    }
}
