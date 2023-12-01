using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Domain.Tasks;
using Shared.Models.Infrastructure;
using Shared.Models.Models.TestCases;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Authorize]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet(WebApi.GetTaskDescriptions)]
    [ProducesResponseType(typeof(List<TaskDescription>), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<List<TaskDescription>>> GetTasks()
    {
        var response = await _taskService.GetTasks();
        return response;
    }

    [HttpGet("{taskId}/tests")]
    [ProducesResponseType(typeof(TestCases), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TestCases>> GetTestCases(string taskId)
    {
        var response = await _taskService.GetTestCases(taskId);
        return response;
    }
}