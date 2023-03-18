using ApiGateway.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiGateway.Repositories.Models;

[BsonIgnoreExtraElements]
public class SolutionCodeDbModel : ISolutionCode
{
    public ObjectId AuthorId { get; set; }
    public string TaskId { get; set; }
    public string SourceCode { get; set; }

    // Используем явное определение интерфейса, чтобы в БД репозиториях нельзя было использовать это поле.
    // А любое приведение типов бросалось в глаза.
    UserId ISolutionCode.Author => new UserId(AuthorId);

    // Все классы БД моделей, должны иметь пустой конструктор для десериализации.
    public SolutionCodeDbModel()
    {
    }
}