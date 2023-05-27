using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models;

namespace SolutionService.Repositories.InternalModels;

[CollectionName("Solutions")]
public class SolutionModel : ISolution, IDocument<Guid>
{
    public string UserId { get; set; } = null!;
    public string TaskId { get; init; } = null!;
    public string SourceCode { get; set; } = null!;

    string ISolution.SourceCode
    {
        get => SourceCode;
        init => SourceCode = value;
    }

    public Guid Id { get; set; }
    public int Version { get; set; }
}