using Shared.Dto;
using Shared.Dto.ProtoHelpers;
using Shared.Services;
using TaskService.Repositories;

namespace TaskService.GrpcServices;

public class TaskGrpcService : ITaskGrpcService
{
    private readonly ITaskRepository _taskRepository;

    public TaskGrpcService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskDto> GetTask(StringProto taskId)
    {
        var task = await _taskRepository.GetTask(taskId);
        return new TaskDto(task);
    }
}