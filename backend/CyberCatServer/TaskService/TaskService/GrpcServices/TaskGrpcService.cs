using Shared.Models.Dto.Descriptions;
using Shared.Models.Ids;
using Shared.Server.ProtoHelpers;
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

    public async Task<Response<List<TaskId>>> GetTasks()
    {
        return await _taskRepository.GetTasks();
    }

    public async Task<Response<TaskDescription>> GetTask(TaskId taskId)
    {
        return await _taskRepository.GetTask(taskId);
    }
}