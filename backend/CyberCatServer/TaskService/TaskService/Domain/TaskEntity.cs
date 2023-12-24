using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;

namespace TaskService.Domain
{
    [CollectionName("Tasks")]
    public class TaskEntity : IDocument<string>
    {
        [BsonId] public string Id { get; set; }

        public int Version { get; set; }
        public string Name { get; set; }
        public bool NeedSendWebHook { get; set; } = false;
        public List<TestCaseEntity> Tests { get; set; } = new List<TestCaseEntity>();
    }
}