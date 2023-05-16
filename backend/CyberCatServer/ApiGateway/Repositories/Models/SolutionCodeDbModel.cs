using ApiGateway.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiGateway.Repositories.Models;

[BsonIgnoreExtraElements]
public class SolutionCodeDbModel : ISourceCode
{
    public string AuthorEmail { get; set; } = null!;
    public string TaskId { get; set; } = null!;
    public string SourceCode { get; set; } = null!;

    // Используем явное определение интерфейса, чтобы в БД репозиториях нельзя было использовать это поле.
    // А любое приведение типов бросалось в глаза.
    //UserId ISourceCode.Author => new UserId(AuthorEmail);
}