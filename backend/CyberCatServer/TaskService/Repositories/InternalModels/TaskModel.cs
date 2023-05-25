using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models;

namespace TaskService.Repositories.InternalModels
{
    [CollectionName("Tasks")]
    internal class TaskModel : IDocument<string>
    {
        [BsonId]
        [JsonPropertyName("_id")] // Для точной десериализации из файла.
        public string Id { get; set; }

        public int Version { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public List<TestModel> Tests { get; init; }
    }
    
    internal class TaskChallenge : ITask
    {
        public string Name { get; init; }
        public string Description { get; init; }
    }
}