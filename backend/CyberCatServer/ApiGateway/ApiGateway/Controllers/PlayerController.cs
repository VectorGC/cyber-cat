using System.Net;
using ApiGateway.Infrastructure.CompleteTaskWebHookService;
using fastJSON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Models.Infrastructure;
using Shared.Server.Application.Services;
using Shared.Server.Infrastructure;

namespace ApiGateway.Controllers;

[Controller]
[Authorize]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;
    private readonly ITaskService _taskService;
    private readonly CompleteTaskWebHookService _completeTaskWebHookService;
    private readonly IAuthService _authService;

    public PlayerController(IPlayerService playerService, ITaskService taskService, IAuthService authService,
        CompleteTaskWebHookService completeTaskWebHookService)
    {
        _authService = authService;
        _completeTaskWebHookService = completeTaskWebHookService;
        _taskService = taskService;
        _playerService = playerService;
    }

    [HttpGet(WebApi.GetTasksProgress)]
    [ProducesResponseType(typeof(List<TaskProgress>), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<List<TaskProgress>>> GetTasksProgress()
    {
        var userId = HttpContext.User.Id();
        var progresses = await _playerService.GetTasksProgress(userId);
        return progresses ?? new List<TaskProgress>();
    }

    [HttpGet(WebApi.GetTaskProgressTemplate)]
    [ProducesResponseType(typeof(TaskProgress), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TaskProgress>> GetTaskProgress(string taskId)
    {
        var userId = HttpContext.User.Id();
        var progresses = await _playerService.GetTasksProgress(userId);
        return progresses.FirstOrDefault();
    }

    [HttpPost(WebApi.SubmitSolutionTemplate)]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<string>> SubmitSolution(string taskId, [FromForm] string solution)
    {
        if (string.IsNullOrEmpty(solution))
        {
            throw new ArgumentNullException(nameof(solution));
        }

        var userId = HttpContext.User.Id();
        var verdict = await _playerService.SubmitSolution(new SubmitSolutionArgs(userId, taskId, solution));

        var needSendWebHookResponse = await _taskService.NeedSendWebHook(taskId);
        if (verdict.IsSuccess && needSendWebHookResponse.NeedSendWebHook)
        {
            await _completeTaskWebHookService.SendWebHook(new WhoSolvedTaskExternalDto()
            {
                IsSolved = true,
                PlayerName = HttpContext.User.Identity?.Name ?? "Unknown",
                TaskId = taskId
            });
        }

        var json = JSON.ToJSON(verdict);
        return json;
    }

    [AllowAnonymous]
    [HttpGet(WebApi.GetUsersWhoSolvedTaskTemplate)]
    [ProducesResponseType(typeof(List<UserModel>), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<string>> GetUsersWhoSolvedTask(string taskId)
    {
        var userIds = await _playerService.GetUsersWhoSolvedTask(taskId);
        var users = await _authService.GetUsersByIds(userIds);

        var json = JSON.ToJSON(users);
        return json;
    }
}