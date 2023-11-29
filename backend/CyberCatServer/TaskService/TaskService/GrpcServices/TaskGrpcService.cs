using Shared.Models.Domain.Tasks;
using Shared.Models.Ids;
using Shared.Models.Models.TestCases;
using Shared.Server.Data;
using Shared.Server.ExternalData;
using Shared.Server.ProtoHelpers;
using Shared.Server.Services;
using TaskService.Repositories;
using TaskService.Services;

namespace TaskService.GrpcServices;

public class TaskGrpcService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITestRepository _testRepository;
    private readonly ISharedTaskProgressRepository _sharedTaskProgressRepository;
    private readonly SharedTaskWebHookProcessor _taskWebHookProcessor;

    public TaskGrpcService(ITaskRepository taskRepository, ITestRepository testRepository, ISharedTaskProgressRepository sharedTaskProgressRepository, SharedTaskWebHookProcessor taskWebHookProcessor)
    {
        _taskWebHookProcessor = taskWebHookProcessor;
        _taskRepository = taskRepository;
        _testRepository = testRepository;
        _sharedTaskProgressRepository = sharedTaskProgressRepository;
    }

    public async Task<Response<List<TaskId>>> GetTasks()
    {
        return await _taskRepository.GetTasks();
    }

    public async Task<Response<TaskDescription>> GetTask(TaskId taskId)
    {
        return await _taskRepository.GetTask(taskId);
    }

    public async Task<Response<TestCases>> GetTestCases(TaskId taskId)
    {
        return await _testRepository.GetTestCases(taskId);
    }

    public async Task OnTaskSolved(OnTaskSolvedArgs args)
    {
        var sharedTask = await _sharedTaskProgressRepository.GetTask(args.TaskId);
        if (sharedTask == null || sharedTask.Status == SharedTaskStatus.NotSolved)
        {
            sharedTask = await _sharedTaskProgressRepository.SetSolved(args.TaskId, args.PlayerId);
            await _taskWebHookProcessor.ProcessWebHook(sharedTask);
        }
    }

    public async Task<Response<List<SharedTaskProgressData>>> GetSharedTasks()
    {
        var tasks = await _sharedTaskProgressRepository.GetTasks();
        return tasks;
    }

    public async Task<Response<WebHookResultStatus>> ProcessWebHookTest()
    {
        return await _taskWebHookProcessor.ProcessWebHook(SharedTaskExternalDto.Mock(true));
    }
}