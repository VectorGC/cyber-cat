using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models.Dto.Descriptions;
using Shared.Models.Ids;

namespace TaskService.Repositories.InternalModels
{
    [CollectionName("Tasks")]
    internal class TaskDbModel : IDocument<string>
    {
        [BsonId]
        [JsonPropertyName("_id")] // For precise deserialization from a file.
        public string Id { get; set; }

        public int Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DefaultCode { get; set; }
        public TestsDbModel Tests { get; set; } = new TestsDbModel();

        public TaskDbModel(TaskId id, TaskDescription task)
        {
            Id = id.Value;
            Name = task.Name;
            Description = task.Description;
            DefaultCode = task.DefaultCode;
        }

        public TaskDbModel()
        {
        }

        public TaskDescription ToDescription()
        {
            return new TaskDescription
            {
                Name = Name,
                Description = Description,
                DefaultCode = DefaultCode
            };
        }
    }
}