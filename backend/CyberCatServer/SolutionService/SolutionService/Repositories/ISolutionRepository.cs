using Shared.Models.Dto;
using Shared.Models.Models;
using Shared.Server.Models;

namespace SolutionService.Repositories;

public interface ISolutionRepository
{
    Task<string> GetSavedCode(UserId userId, TaskId taskId);
    Task Save(UserId userId, SolutionDto solution);
    Task RemoveCode(UserId userId, TaskId taskId);
}