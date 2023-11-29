using Shared.Models.Domain.Players;
using Shared.Models.Ids;
using Shared.Server.Data;

namespace TaskService.Repositories;

public interface ISharedTaskProgressRepository
{
    Task<SharedTaskProgressData> GetTask(TaskId id);
    Task<List<SharedTaskProgressData>> GetTasks();
    Task<SharedTaskProgressData> SetSolved(TaskId id, PlayerId playerId);
}