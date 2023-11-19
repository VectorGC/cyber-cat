using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using TaskService.Repositories;

namespace Shared.Server.Data;

[CollectionName("SharedTask")]
public class SharedTaskProgressData : IDocument<string>
{
    [BsonId] public string Id { get; set; }
    public int Version { get; set; }
    public long PlayerIdData { get; set; }
    public SharedTaskStatus Status { get; set; }
}