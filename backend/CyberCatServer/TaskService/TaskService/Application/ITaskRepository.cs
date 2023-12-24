using Shared.Models.Domain.Tasks;
using TaskService.Domain;

namespace TaskService.Application
{
    public interface ITaskRepository
    {
        Task<TaskEntity> GetTask(TaskId taskId);
        Task<List<TaskEntity>> GetTasks();
        Task<List<TestCaseEntity>> GetTestCases(TaskId id);
    }
}