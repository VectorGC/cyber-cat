using Shared.Models.Dto.Descriptions;
using Shared.Models.Dto.ProtoHelpers;
using Shared.Models.Models;
using Shared.Server.Services;
using TaskService.Repositories;

namespace TaskService.GrpcServices;

public class TaskGrpcService : ITaskGrpcService
{
    private readonly ITaskRepository _taskRepository;

    public TaskGrpcService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskDescription> GetTask(TaskId taskId)
    {
        var task = await _taskRepository.GetTask(taskId);
        return task;
    }
}