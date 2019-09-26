using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiEngenhosDevops.Models
{
    public class WorkItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int IdWorkItem { get; set; }
        public string Tipo { get; set;}
        public string Titulo { get; set; }
        public DateTime DataCriacaoWorkItem { get; set; }
    }
}