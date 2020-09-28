using MongoDB.Driver;
using Newtonsoft.Json;
using OpenFMB.Adapters.Config.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenFMB.Adapters.Config.Web.Services
{
    public class ProjectServiceOld
    {
        private readonly IMongoCollection<Project> _projects;
        
        public ProjectServiceOld(IProjectDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _projects = database.GetCollection<Project>(settings.ProjectCollectionName);           
        }

        public List<Project> Get() =>
            _projects.Find(project => true).ToList();

        public Project Get(string id) =>
            _projects.Find<Project>(project => project.Id == id).FirstOrDefault();

        public Project Create(Project project)
        {
            var json = JsonConvert.SerializeObject(project);
            
            _projects.InsertOne(project);
            return project;
        }

        public void Update(string id, Project projectIn) =>
            _projects.ReplaceOne(project => project.Id == id, projectIn);

        public void Remove(Project projectIn) =>
            _projects.DeleteOne(project => project.Id == projectIn.Id);

        public void Remove(string id) =>
            _projects.DeleteOne(project => project.Id == id);
    }
}
