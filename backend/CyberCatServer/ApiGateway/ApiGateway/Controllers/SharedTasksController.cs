using System.Net;
using Faker;
using Microsoft.AspNetCore.Mvc;
using Shared.Server.ExternalData;
using Shared.Server.Services;
using Boolean = Faker.Boolean;
using Name = Faker.Name;

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
        var response = await _taskService.GetSharedTasks();
        response.EnsureSuccess();

        var tasksDto = response.Value.Select(task => new SharedTaskExternalDto(task)).ToList();
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
            var model = new SharedTaskExternalDto()
            {
                TaskId = Lorem.Words(1).First(),
                PlayerName = Name.First(),
                IsSolved = Boolean.Random()
            };

            tasks.Add(model);
        }

        return tasks;
    }

    [HttpPost("wip/clearProgress")]
    [ProducesResponseType(typeof(List<SharedTaskExternalDto>), (int) HttpStatusCode.OK)]
    public async Task<ActionResult> ClearProgress()
    {
        return Ok();
    }
}