using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Server.Data;

namespace TaskService.Application;

public interface ISharedTaskProgressRepository
{
    Task<SharedTaskProgressData> GetTask(TaskId id);
    Task<List<SharedTaskProgressData>> GetTasks();
    Task<SharedTaskProgressData> SetSolved(TaskId id, UserId userId);
}