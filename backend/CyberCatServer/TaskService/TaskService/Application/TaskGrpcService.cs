using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;
using Shared.Server.Application.Services;
using Shared.Server.Domain;
using Shared.Server.Infrastructure;
using TaskService.Domain;
using TaskService.Infrastructure;

namespace TaskService.Application;

public class TaskGrpcService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ISharedTaskProgressRepository _sharedTaskProgressRepository;
    private readonly SharedTaskWebHookProcessor _taskWebHookProcessor;
    private readonly TaskEntityMapper _taskEntityMapper;

    public TaskGrpcService(ITaskRepository taskRepository, ISharedTaskProgressRepository sharedTaskProgressRepository,
        SharedTaskWebHookProcessor taskWebHookProcessor, TaskEntityMapper taskEntityMapper)
    {
        _taskEntityMapper = taskEntityMapper;
        _taskWebHookProcessor = taskWebHookProcessor;
        _taskRepository = taskRepository;
        _sharedTaskProgressRepository = sharedTaskProgressRepository;
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

    public async Task OnTaskSolved(OnTaskSolvedArgs args)
    {
        var sharedTask = await _sharedTaskProgressRepository.GetTaskProgress(args.TaskId);
        if (sharedTask == null || sharedTask.Status == SharedTaskStatus.NotSolved)
        {
            var progress = new SharedTaskProgressEntity()
            {
                Id = args.TaskId,
                Status = SharedTaskStatus.Solved,
                UserId = args.UserId
            };

            await _sharedTaskProgressRepository.Update(progress);

            var progressDomain = _taskEntityMapper.ToSharedTaskProgressDomain(progress);
            await _taskWebHookProcessor.ProcessWebHook(progressDomain);
        }
    }

    public async Task<List<SharedTaskProgress>> GetSharedTasks()
    {
        var tasks = await _sharedTaskProgressRepository.GetTasksProgresses();
        return tasks.Select(progress => _taskEntityMapper.ToSharedTaskProgressDomain(progress)).ToList();
    }

    public async Task<WebHookResultStatus> ProcessWebHookTest()
    {
        return await _taskWebHookProcessor.ProcessWebHook(SharedTaskExternalDto.Mock(true));
    }
}