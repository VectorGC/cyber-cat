using MongoDbGenericRepository.Attributes;
using MongoDbGenericRepository.Models;
using Shared.Models.Dto;
using Shared.Server.Models;

namespace SolutionService.Repositories.InternalModels;

[CollectionName("Solutions")]
public class SolutionDbModel : IDocument<Guid>
{
    public long UserId { get; set; }
    public string TaskId { get; set; }
    public string SourceCode { get; set; }
    public Guid Id { get; set; }
    public int Version { get; set; }

    public SolutionDbModel(UserId userId, SolutionDto solution)
    {
        UserId = userId.Value;
        TaskId = solution.TaskId.Value;
        SourceCode = solution.SourceCode;
    }

    public SolutionDbModel()
    {
    }
}