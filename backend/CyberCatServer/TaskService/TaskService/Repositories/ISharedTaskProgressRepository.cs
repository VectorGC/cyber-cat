using Shared.Models.Ids;
using Shared.Server.Data;
using Shared.Server.Ids;

namespace TaskService.Repositories;

public interface ISharedTaskProgressRepository
{
    Task<SharedTaskProgressData> GetTask(TaskId id);
    Task SetSolved(TaskId id, PlayerId playerId);
}