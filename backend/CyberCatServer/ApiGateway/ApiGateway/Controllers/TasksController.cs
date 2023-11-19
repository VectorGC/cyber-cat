using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Descriptions;
using Shared.Models.Ids;
using Shared.Models.Models.TestCases;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(TaskIdsDto), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TaskIdsDto>> GetTasks()
    {
        var response = await _taskService.GetTasks();
        response.EnsureSuccess();

        var taskIds = response.Value.Select(taskId => taskId.Value).ToList();
        var responseDto = new TaskIdsDto
        {
            taskIds = taskIds
        };

        return responseDto;
    }

    [HttpGet("{taskId}")]
    [ProducesResponseType(typeof(TaskDescription), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TaskDescription>> GetTask(string taskId)
    {
        var response = await _taskService.GetTask(taskId);
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