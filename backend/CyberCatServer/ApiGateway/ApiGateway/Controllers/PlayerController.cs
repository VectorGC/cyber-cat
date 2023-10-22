using System.Net;
using ApiGateway.Attributes;
using fastJSON;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Data;
using Shared.Server.Exceptions.PlayerService;
using Shared.Server.Ids;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PlayerController : ControllerBase
{
    private readonly IPlayerGrpcService _playerGrpcService;

    public PlayerController(IPlayerGrpcService playerGrpcService)
    {
        _playerGrpcService = playerGrpcService;
    }

    [HttpPost("remove")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> RemovePlayer([FromPlayer] PlayerId playerId)
    {
        return await _playerGrpcService.RemovePlayer(playerId);
    }

    [HttpGet]
    [ProducesResponseType(typeof(PlayerData), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<ActionResult<PlayerData>> GetPlayerById([FromPlayer] PlayerId playerId)
    {
        return await _playerGrpcService.GetPlayerById(playerId);
    }

    [HttpPost("signIn")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> SignIn([FromUser] UserId userId)
    {
        var response = await _playerGrpcService.GetPlayerByUserId(userId);
        if (!response.IsSucceeded && response.Exception is PlayerNotFoundException)
        {
            response = await _playerGrpcService.CreatePlayer(userId);
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

        var response = await _playerGrpcService.GetVerdict(new(playerId, taskId, sourceCode));
        var json = JSON.ToJSON(response.Value);

        return json;
    }

    [HttpGet("tasks/{taskId}")]
    [ProducesResponseType(typeof(TaskData), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TaskData>> GetTaskData(string taskId, [FromPlayer] PlayerId playerId)
    {
        return await _playerGrpcService.GetTaskData(new(playerId, taskId));
    }

    /*
    [HttpPut("bitcoins/add")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<ActionResult> AddBitcoinsToPlayer([FromForm] string bitcoins)
    {
        var playerId = User.Identity.GetUserId();
        var args = new PlayerBtcArgs {PlayerId = playerId, BitcoinsAmount = Convert.ToInt32(bitcoins)};
        try
        {
            await _playerGrpcService.AddBitcoinsToPlayer(args);
            return Ok();
        }
        catch (PlayerNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("bitcoins/remove")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<ActionResult> TakeBitcoinsFromPlayer([FromForm] string bitcoins)
    {
        var playerId = User.Identity.GetUserId();
        var args = new PlayerBtcArgs {PlayerId = playerId, BitcoinsAmount = Convert.ToInt32(bitcoins)};
        try
        {
            await _playerGrpcService.TakeBitcoinsFromPlayer(args);
            return Ok();
        }
        catch (PlayerNotFoundException)
        {
            return NotFound();
        }
    }
    */
}