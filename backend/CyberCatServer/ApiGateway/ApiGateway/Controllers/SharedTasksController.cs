using System.Net;
using Faker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Server.Application.Services;
using Shared.Server.Infrastructure;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class SharedTasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public SharedTasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<SharedTaskExternalDto>), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<List<SharedTaskExternalDto>>> GetSharedTasks()
    {
        var progresses = await _taskService.GetSharedTasks();
        if (progresses == null || progresses.Count == 0)
            return new List<SharedTaskExternalDto>();

        var tasksDto = progresses.Select(task => new SharedTaskExternalDto(task)).ToList();
        return tasksDto;
    }

    [HttpGet("test")]
    [ProducesResponseType(typeof(List<SharedTaskExternalDto>), (int) HttpStatusCode.OK)]
    public ActionResult<List<SharedTaskExternalDto>> GetSharedTasksTest()
    {
        var tasks = new List<SharedTaskExternalDto>();

        var count = RandomNumber.Next(1, 6);
        for (var i = 0; i < count; i++)
        {
            tasks.Add(SharedTaskExternalDto.Mock());
        }

        return tasks;
    }

    [HttpPost("webHook/test")]
    [ProducesResponseType(typeof(WebHookResultStatus), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<WebHookResultStatus>> ProcessWebHookTest()
    {
        var response = await _taskService.ProcessWebHookTest();
        return response;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("wip/clearProgress")]
    [ProducesResponseType(typeof(List<SharedTaskExternalDto>), (int) HttpStatusCode.OK)]
    public async Task<ActionResult> ClearProgress()
    {
        return Ok();
    }
}