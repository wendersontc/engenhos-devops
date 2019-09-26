using ApiEngenhosDevops.Settings;
using ApiEngenhosDevops.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace ApiEngenhosDevops.Services
{
    public class WorkItemService
    {
        private readonly IMongoCollection<WorkItem> _workItens;

        public WorkItemService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _workItens = database.GetCollection<WorkItem>(settings.WorkItensCollectionName);
        }

        public List<WorkItem> Get() =>
            _workItens.Find(item => true).ToList();

        public WorkItem Get(string id) =>
            _workItens.Find<WorkItem>(item => item.Id == id).FirstOrDefault();

        public WorkItem Create(WorkItem item)
        {
            _workItens.InsertOne(item);
            return item;
        }

        public void Update(string id, WorkItem workItem) =>
            _workItens.ReplaceOne(item => item.Id == id, workItem);

        public void Remove(WorkItem workItem) =>
            _workItens.DeleteOne(item => item.Id == workItem.Id);

        public void Remove(string id) => 
            _workItens.DeleteOne(item => item.Id == id);
    }
}