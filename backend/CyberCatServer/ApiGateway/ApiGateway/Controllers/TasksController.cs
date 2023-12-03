using System.Net;
using fastJSON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;
using Shared.Models.Infrastructure;
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

    [HttpGet(WebApi.GetTestCasesTemplate)]
    [ProducesResponseType(typeof(List<TestCaseDescription>), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<string>> GetTestCases(string taskId)
    {
        var testCases = await _taskService.GetTestCases(taskId);
        var json = JSON.ToJSON(testCases);
        return json;
    }
}