using Shared.Models.Domain.Tasks;
using TaskService.Application;
using TaskService.Domain;

namespace TaskService.Infrastructure;

internal class TaskModelConstCaseRepository : ITaskRepository
{
    public Task<List<TaskEntity>> GetTasks()
    {
        return Task.FromResult(TasksModels.Tasks);
    }

    public Task<TaskEntity> GetTask(TaskId taskId)
    {
        var task = TasksModels.Tasks.FirstOrDefault(task => task.Id == taskId);
        return Task.FromResult(task);
    }

    public Task<List<TestCaseEntity>> GetTestCases(TaskId id)
    {
        var task = TasksModels.Tasks.FirstOrDefault(task => task.Id == id);
        return Task.FromResult(task?.Tests);
    }
}