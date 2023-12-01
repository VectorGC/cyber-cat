using Shared.Models.Domain.Tasks;
using Shared.Models.Models.TestCases;
using TaskService.Application;
using TaskService.Domain;

namespace TaskService.Infrastructure;

internal class TaskModelConstRepository : ITaskRepository, ITestRepository
{
    public Task<List<TaskEntity>> GetTasks()
    {
        return Task.FromResult(TasksModels.Tasks);
    }

    public Task<TestCases> GetTestCases(TaskId id)
    {
        var task = TasksModels.Tasks.FirstOrDefault(task => task.Id == id);
        return Task.FromResult(task?.Tests.ToDescription(id));
    }
}