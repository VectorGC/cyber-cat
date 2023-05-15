using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models;

namespace TaskService.Repositories
{
    [CollectionName("Tasks")]
    public class TaskModel : IDocument<string>, IEquatable<string>, ITask
    {
        [BsonId] public string Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool Equals(string? other)
        {
            return Id == other;
        }

        public static TaskModel FromTask(string id, ITask task)
        {
            return task as TaskModel ?? new TaskModel()
            {
                Id = id,
                Name = task.Name,
                Description = task.Description
            };
        }
    }
}