using System.Net;
using ApiGateway.Infrastructure.CompleteTaskWebHookService;
using Faker;
using Microsoft.AspNetCore.Mvc;
using Shared.Server.Application.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class CompleteTaskWebHookController : ControllerBase
{
    private readonly CompleteTaskWebHookService _completeTaskWebHookService;
    private readonly ITaskService _taskService;
    private readonly IPlayerService _playerService;
    private readonly IAuthService _authService;

    public CompleteTaskWebHookController(CompleteTaskWebHookService completeTaskWebHookService, ITaskService taskService, IPlayerService playerService, IAuthService authService)
    {
        _authService = authService;
        _playerService = playerService;
        _taskService = taskService;
        _completeTaskWebHookService = completeTaskWebHookService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<WhoSolvedTaskExternalDto>), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<List<WhoSolvedTaskExternalDto>>> GetWhoSolvedTask()
    {
        var whoSolvedTasks = new List<WhoSolvedTaskExternalDto>();

        var tasks = await _taskService.GetTasks();
        foreach (var task in tasks)
        {
            var whoSolvedTaskIds = await _playerService.GetUsersWhoSolvedTask(task.Id);
            var usersWhoSolvedTask = await _authService.GetUsersByIds(whoSolvedTaskIds);

            var user = usersWhoSolvedTask.FirstOrDefault();
            if (user != null)
                whoSolvedTasks.Add(WhoSolvedTaskExternalDto.Solved(task.Id, user));
            else
                whoSolvedTasks.Add(WhoSolvedTaskExternalDto.NotSolved(task.Id));
        }

        return whoSolvedTasks;
    }

    [HttpGet("test")]
    [ProducesResponseType(typeof(List<WhoSolvedTaskExternalDto>), (int) HttpStatusCode.OK)]
    public ActionResult<List<WhoSolvedTaskExternalDto>> GetTasksTest()
    {
        var tasks = new List<WhoSolvedTaskExternalDto>();

        var count = RandomNumber.Next(1, 6);
        for (var i = 0; i < count; i++)
        {
            tasks.Add(WhoSolvedTaskExternalDto.Mock());
        }

        return tasks;
    }

    [HttpPost("webHook/test")]
    [ProducesResponseType(typeof(WebHookResultStatus), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<WebHookResultStatus>> ProcessWebHookTest()
    {
        var result = await _completeTaskWebHookService.SendWebHook(WhoSolvedTaskExternalDto.Mock(true));
        return result;
    }
}