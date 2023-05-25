using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models;

namespace TaskService.Repositories.InternalModels
{
    [CollectionName("Tasks")]
    internal class TaskModel : IDocument<string>, ITask
    {
        [BsonId]
        [JsonPropertyName("_id")] // Для точной десериализации из файла.
        public string Id { get; set; }

        public int Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TestModel> Tests { get; set; }

        public TestsModel GetTests()
        {
            return new TestsModel(Tests);
        }
    }
}