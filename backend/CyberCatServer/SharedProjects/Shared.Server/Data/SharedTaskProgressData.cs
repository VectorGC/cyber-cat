using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using ProtoBuf;

namespace Shared.Server.Data;

[ProtoContract]
[CollectionName("SharedTask")]
public class SharedTaskProgressData : IDocument<string>
{
    [ProtoMember(1)] [BsonId] public string Id { get; set; }
    [ProtoMember(2)] public long PlayerIdData { get; set; }
    [ProtoMember(3)] public SharedTaskStatus Status { get; set; }
    public int Version { get; set; }
}