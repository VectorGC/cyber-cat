using System.Net;
using System.Text.Json.Nodes;
using ApiGateway.Authorization;
using ApiGateway.Dto;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[Controller]
[AuthorizeTokenGuard]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ITaskService _taskService;

    public TasksController(IHostEnvironment hostEnvironment, ITaskService taskService)
    {
        _hostEnvironment = hostEnvironment;
        _taskService = taskService;
    }

    /// <summary>
    /// Возвращает таски в виде иерархии.
    /// </summary>
    [HttpGet("hierarchy")]
    [ProducesResponseType(typeof(JsonObject), (int) HttpStatusCode.Forbidden)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<IActionResult> GetTasksHierarchy(string token)
    {
        var tasks = await _taskService.GetTasksAsHierarchy();
        return Ok(tasks);
    }

    /// <summary>
    /// Возвращает таски в плоском виде
    /// </summary>
    [HttpGet("flat")]
    [ProducesResponseType(typeof(TasksData), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<IActionResult> GetTasksFlat(string token)
    {
        var tasks = await _taskService.GetTasksAsFlat();
        return Ok(tasks);
    }
}