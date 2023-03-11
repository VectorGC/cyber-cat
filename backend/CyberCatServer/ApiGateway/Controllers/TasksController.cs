using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class TasksController : ControllerBase
{
    private readonly IHostEnvironment _hostEnvironment;

    public TasksController(IHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    /// <summary>
    /// Возвращает таски в виде иерархии.
    /// </summary>
    [HttpGet("hierarchy")]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public IActionResult GetTasksHierarchy(string token)
    {
        if (AuthenticationController.TOKEN != token)
        {
            return Unauthorized();
        }

        var rootPath = _hostEnvironment.ContentRootPath;
        var fullPath = Path.Combine(rootPath, "TaskExamples/tasks_hierarchy.json");

        var jsonData = System.IO.File.ReadAllText(fullPath);

        var tasks = JsonSerializer.Deserialize<object>(jsonData);

        return Ok(tasks);
    }

    /// <summary>
    /// Возвращает таски в плоском виде
    /// </summary>
    [HttpGet("flat")]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public IActionResult GetTasksFlat(string token)
    {
        if (AuthenticationController.TOKEN != token)
        {
            return Unauthorized();
        }

        var rootPath = _hostEnvironment.ContentRootPath;
        var fullPath = Path.Combine(rootPath, "TaskExamples/tasks_flat.json");

        var jsonData = System.IO.File.ReadAllText(fullPath);

        var tasks = JsonSerializer.Deserialize<object>(jsonData);

        return Ok(tasks);
    }
}