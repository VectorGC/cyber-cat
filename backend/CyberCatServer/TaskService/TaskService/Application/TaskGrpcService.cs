using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;
using Shared.Server.Application.Services;
using TaskService.Infrastructure;

namespace TaskService.Application;

public class TaskGrpcService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly TaskEntityMapper _taskEntityMapper;

    public TaskGrpcService(ITaskRepository taskRepository, TaskEntityMapper taskEntityMapper)
    {
        _taskEntityMapper = taskEntityMapper;
        _taskRepository = taskRepository;
    }

    public async Task<List<TaskDescription>> GetTasks()
    {
        var result = new List<TaskDescription>();
        var tasks = await _taskRepository.GetTasks();

        foreach (var task in tasks)
        {
            var description = await _taskEntityMapper.ToDescription(task);
            result.Add(description);
        }

        return result;
    }

    public async Task<List<TestCaseDescription>> GetTestCases(TaskId taskId)
    {
        var task = await _taskRepository.GetTask(taskId);
        var descriptions = _taskEntityMapper.ToTestCaseDescriptions(task);
        return descriptions;
    }

    public async Task<NeedSendWebHookResponse> NeedSendWebHook(TaskId taskId)
    {
        var task = await _taskRepository.GetTask(taskId);
        return new NeedSendWebHookResponse(task?.NeedSendWebHook ?? false);
    }
}