using Shared.Models.Descriptions;
using Shared.Models.Ids;
using Shared.Models.Models.TestCases;
using Shared.Server.ProtoHelpers;
using Shared.Server.Services;
using TaskService.Repositories;

namespace TaskService.GrpcServices;

public class TaskGrpcService : ITaskGrpcService
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITestRepository _testRepository;

    public TaskGrpcService(ITaskRepository taskRepository, ITestRepository testRepository)
    {
        _taskRepository = taskRepository;
        _testRepository = testRepository;
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
}