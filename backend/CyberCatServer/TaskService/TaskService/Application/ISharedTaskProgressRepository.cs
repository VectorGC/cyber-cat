using Shared.Models.Domain.Tasks;
using TaskService.Domain;

namespace TaskService.Application;

public interface ISharedTaskProgressRepository
{
    Task<SharedTaskProgressEntity> GetTaskProgress(TaskId id);
    Task<List<SharedTaskProgressEntity>> GetTasksProgresses();
    Task Update(SharedTaskProgressEntity progress);
}