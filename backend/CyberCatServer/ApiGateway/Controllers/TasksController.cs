using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;
using Shared.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TasksController : ControllerBase
{
    public ITaskGrpcService TaskService { get; }

    public TasksController(ITaskGrpcService taskService)
    {
        TaskService = taskService;
    }

    [HttpGet("{taskId}")]
    [ProducesResponseType(typeof(TaskDto), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TaskDto>> ShouldGetTutorialTask(string taskId)
    {
        var task = await TaskService.GetTask(taskId);
        return task;
    }
}