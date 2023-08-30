using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Dto.Descriptions;
using Shared.Models.Models;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TasksController : ControllerBase
{
    public ITaskGrpcService TaskGrpcService { get; }

    public TasksController(ITaskGrpcService taskGrpcService)
    {
        TaskGrpcService = taskGrpcService;
    }

    [HttpGet("{taskId}")]
    [ProducesResponseType(typeof(TaskDescription), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TaskDescription>> GetTask(string taskId)
    {
        var task = await TaskGrpcService.GetTask(new TaskId(taskId));
        return task;
    }
}