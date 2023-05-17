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

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaskResponse), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TaskResponse>> ShouldGetTutorialTask(string id)
    {
        var task = await TaskService.GetTask(new TaskIdArg
        {
            Id = id
        });
        return TaskResponse.FromTask(task);
    }
}