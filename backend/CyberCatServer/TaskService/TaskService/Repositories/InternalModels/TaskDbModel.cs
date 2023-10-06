using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models.Descriptions;
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
        public string DefaultCode { get; set; }
        public TestsDbModel Tests { get; set; } = new TestsDbModel();

        public TaskDbModel(TaskId id, TaskDescription task)
        {
            Id = id.Value;
            Name = task.Name;
            DefaultCode = task.DefaultCode;
        }

        public TaskDbModel()
        {
        }

        public async Task<TaskDescription> ToDescription(IHostEnvironment hostEnvironment, ILogger logger)
        {
            var rootPath = hostEnvironment.ContentRootPath;
            var fullPath = Path.Combine(rootPath, $"Tasks/{Id}.md");

            var description = string.Empty;
            if (File.Exists(fullPath))
            {
                using var stream = File.OpenText(fullPath);
                description = await stream.ReadToEndAsync();
            }
            else
            {
                logger.LogError("Not found description file by path \'{FullPath}\'", fullPath);
            }

            return new TaskDescription
            {
                Name = Name,
                DefaultCode = DefaultCode
            };
        }
    }
}