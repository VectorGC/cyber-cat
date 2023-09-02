using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Dto.Descriptions;
using Shared.Models.Ids;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TasksController : ControllerBase
{
    private readonly ITaskGrpcService _taskGrpcService;

    public TasksController(ITaskGrpcService taskGrpcService)
    {
        _taskGrpcService = taskGrpcService;
    }

    [HttpGet()]
    [ProducesResponseType(typeof(TaskDescription), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<List<TaskId>>> GetTasks()
    {
        return await _taskGrpcService.GetTasks();
    }

    [HttpGet("{taskId}")]
    [ProducesResponseType(typeof(TaskDescription), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TaskDescription>> GetTask(string taskId)
    {
        return await _taskGrpcService.GetTask(taskId);
    }
}