using System.Net;
using fastJSON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Domain.Tasks;
using Shared.Models.Infrastructure;
using Shared.Server.Infrastructure;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Authorize]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpGet(WebApi.GetTasksProgress)]
    [ProducesResponseType(typeof(List<TaskProgress>), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<List<TaskProgress>>> GetTasksProgress()
    {
        var userId = HttpContext.User.Id();
        var progresses = await _playerService.GetTasksProgress(new GetTasksProgressArgs(userId));
        return progresses ?? new List<TaskProgress>();
    }

    [HttpGet(WebApi.GetTaskProgressTemplate)]
    [ProducesResponseType(typeof(TaskProgress), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TaskProgress>> GetTaskProgress(string taskId)
    {
        var userId = HttpContext.User.Id();
        var response = await _playerService.GetTaskProgress(new GetTaskProgressArgs(userId, taskId));
        return response;
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
        var json = JSON.ToJSON(verdict);

        return json;
    }
}