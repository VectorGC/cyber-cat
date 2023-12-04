using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Server.Domain;

namespace TaskService.Domain;

[CollectionName("SharedTask")]
public class SharedTaskProgressEntity : IDocument<string>
{
    [BsonId] public string Id { get; set; }
    public long UserId { get; set; }
    public SharedTaskStatus Status { get; set; }

    public int Version { get; set; }
}