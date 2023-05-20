using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models;

namespace TaskService.Repositories.InternalModels
{
    [CollectionName("Tasks")]
    internal class TaskWithTestsModel : IDocument<string>, ITask, ITests
    {
        [BsonId]
        [JsonPropertyName("_id")] // Для точной десериализации из файла.
        public string Id { get; set; }

        public int Version { get; set; }
        public string Name { get; init; }
        public string Description { get; init; }
        public List<TestModel> Tests { get; init; }

        IReadOnlyList<ITest> ITests.Tests
        {
            get => Tests;
            init => Tests = value.Select(test => test.To<TestModel>()).ToList();
        }
    }
}