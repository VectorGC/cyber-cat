using System.Net;
using fastJSON;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.TestCase;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure;
using Shared.Server.Application.Services;

namespace ApiGateway.Controllers;

[Controller]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;
    private readonly IPlayerService _playerService;
    private readonly IAuthService _authService;

    public TasksController(ITaskService taskService, IPlayerService playerService, IAuthService authService)
    {
        _authService = authService;
        _playerService = playerService;
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