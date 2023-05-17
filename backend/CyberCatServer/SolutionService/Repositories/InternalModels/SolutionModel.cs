using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models;

namespace SolutionService.Repositories.InternalModels;

[CollectionName("Solutions")]
public class SolutionModel : ISolution, IDocument<Guid>
{
    public string UserId { get; set; } = null!;
    public string TaskId { get; set; } = null!;
    public string SourceCode { get; set; } = null!;

    public Guid Id { get; set; }
    public int Version { get; set; }
}