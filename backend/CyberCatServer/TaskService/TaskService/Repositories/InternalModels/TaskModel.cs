using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models;
using Shared.Models.Dto;
using Shared.Models.Models;

namespace TaskService.Repositories.InternalModels
{
    [CollectionName("Tasks")]
    internal class TaskModel : IDocument<string>
    {
        private class TestsModel : List<TestModel>, ITests
        {
            public TestsModel(IEnumerable<TestModel> tests)
            {
                AddRange(tests);
            }

            IEnumerator<ITest> IEnumerable<ITest>.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        [BsonId]
        [JsonPropertyName("_id")] // Для точной десериализации из файла.
        public string Id { get; set; }

        public int Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<TestModel> Tests { get; set; } = new List<TestModel>();

        public TaskModel(string id, TaskDto task)
        {
            Id = id;
            Name = task.Name;
            Description = task.Description;
        }

        public TaskModel()
        {
        }

        public ITests GetTests()
        {
            return new TestsModel(Tests);
        }

        public TaskDto ToDto()
        {
            return new TaskDto
            {
                Name = Name,
                Description = Description
            };
        }
    }
}