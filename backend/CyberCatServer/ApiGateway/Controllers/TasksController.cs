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

    [HttpGet("hierarchy")]
    public IActionResult GetTasksHierarchy(string token)
    {
        if (AuthenticationController.TOKEN != token)
        {
            return Forbid();
        }

        var rootPath = _hostEnvironment.ContentRootPath;
        var fullPath = Path.Combine(rootPath, "TaskExamples/tasks_hierarchy.json");

        var jsonData = System.IO.File.ReadAllText(fullPath);

        var tasks = JsonSerializer.Deserialize<object>(jsonData);

        return Ok(tasks);
    }

    [HttpGet("flat")]
    public IActionResult GetTasksFlat(string token)
    {
        if (AuthenticationController.TOKEN != token)
        {
            return Forbid();
        }

        var rootPath = _hostEnvironment.ContentRootPath;
        var fullPath = Path.Combine(rootPath, "TaskExamples/tasks_flat.json");

        var jsonData = System.IO.File.ReadAllText(fullPath);

        var tasks = JsonSerializer.Deserialize<object>(jsonData);

        return Ok(tasks);
    }
}