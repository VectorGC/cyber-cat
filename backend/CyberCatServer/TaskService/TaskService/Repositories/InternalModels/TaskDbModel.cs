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
        public TestsDbModel Tests { get; set; } = new TestsDbModel();

        public TaskDbModel(TaskId id, TaskDescription task)
        {
            Id = id.Value;
            Name = task.Name;
        }

        public TaskDbModel()
        {
        }

        public async Task<TaskDescription> ToDescription(IHostEnvironment hostEnvironment, ILogger logger)
        {
            return new TaskDescription
            {
                Name = Name,
                Description = await GetDescription(hostEnvironment, logger),
                DefaultCode = await GetDefaultCode(hostEnvironment, logger)
            };
        }

        private async Task<string> GetDescription(IHostEnvironment hostEnvironment, ILogger logger)
        {
            var rootPath = hostEnvironment.ContentRootPath;
            var fullPath = Path.Combine(rootPath, $"Tasks/{Id}.md");

            if (!File.Exists(fullPath))
            {
                logger.LogError("Not found description file by path \'{FullPath}\'", fullPath);
                return string.Empty;
            }

            using var stream = File.OpenText(fullPath);
            return await stream.ReadToEndAsync();
        }

        private async Task<string> GetDefaultCode(IHostEnvironment hostEnvironment, ILogger logger)
        {
            var rootPath = hostEnvironment.ContentRootPath;
            var fullPath = Path.Combine(rootPath, $"Tasks/{Id}_default_code.cpp");

            if (!File.Exists(fullPath))
            {
                logger.LogError("Not found default code file by path \'{FullPath}\'", fullPath);
                return string.Empty;
            }

            using var stream = File.OpenText(fullPath);
            return await stream.ReadToEndAsync();
        }
    }
}