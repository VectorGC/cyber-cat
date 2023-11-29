using System.Net;
using ApiGateway.Attributes;
using fastJSON;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Data;
using Shared.Models.Domain.Players;
using Shared.Models.Domain.Tasks;
using Shared.Models.Domain.Users;
using Shared.Server.Exceptions.PlayerService;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayerController(IPlayerService playerService)
    {
        _playerService = playerService;
    }

    [HttpPost("remove")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> RemovePlayer([FromPlayer] PlayerId playerId)
    {
        return await _playerService.RemovePlayer(playerId);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PlayerData), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<ActionResult<PlayerData>> GetPlayerById([FromPlayer] PlayerId playerId)
    {
        return await _playerService.GetPlayerById(playerId);
    }

    [HttpPost("signIn")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> SignIn([FromUser] UserId userId)
    {
        var response = await _playerService.GetPlayerByUserId(userId);
        if (!response.IsSucceeded && response.Exception is PlayerNotFoundException)
        {
            response = await _playerService.CreatePlayer(userId);
            return response.ToActionResult();
        }

        return response.ToActionResult();
    }

    [HttpPost("verify/{taskId}")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<string>> VerifySolution(string taskId, [FromForm] string sourceCode, [FromPlayer] PlayerId playerId)
    {
        if (string.IsNullOrEmpty(sourceCode))
        {
            throw new ArgumentNullException(nameof(sourceCode));
        }

        var response = await _playerService.GetVerdict(new(playerId, taskId, sourceCode));
        var json = JSON.ToJSON(response.Value);

        return json;
    }

    [HttpGet("tasks/{taskId}")]
    [ProducesResponseType(typeof(TaskProgressData), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TaskProgressData>> GetTaskData(string taskId, [FromPlayer] PlayerId playerId)
    {
        return await _playerService.GetTaskData(new(playerId, taskId));
    }
}