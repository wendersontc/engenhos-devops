using MongoDB.Driver;
using ConsoleEngenhosDevops.Models;
using ConsoleEngenhosDevops.Settings;

namespace ConsoleEngenhosDevops.Services
{
    public class WorkItemService
    {
        private readonly IMongoCollection<WorkItemResult> _workItens;

        public WorkItemService(ServiceConfigurations settings){
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _workItens = database.GetCollection<WorkItemResult>(settings.WorkItensCollectionName);
        }

        public WorkItemResult Create(WorkItemResult item){

            _workItens.InsertOne(item);
            return item;

        }

        public bool GetByWorkId(int idWork) =>
           _workItens.Find<WorkItemResult>(work => work.IdWorkItem == idWork).Any();

        public WorkItemResult GetById(string id)=>
            _workItens.Find<WorkItemResult>(work => work.Id == id).FirstOrDefault();
    }
}