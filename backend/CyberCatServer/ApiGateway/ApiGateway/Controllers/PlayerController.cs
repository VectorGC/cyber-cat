using System.Net;
using ApiGateway.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Dto;
using Shared.Models.Dto.Data;
using Shared.Models.Models;
using Shared.Server.Dto.Args;
using Shared.Server.Exceptions;
using Shared.Server.Models;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PlayerController : ControllerBase
{
    private readonly IPlayerGrpcService _playerGrpcService;
    private readonly IAuthGrpcService _authorizationService;

    public PlayerController(IPlayerGrpcService playerGrpcService, IAuthGrpcService authorizationService)
    {
        _authorizationService = authorizationService;
        _playerGrpcService = playerGrpcService;
    }

    [HttpDelete]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> RemovePlayer([FromPlayer] PlayerId playerId)
    {
        await _playerGrpcService.RemovePlayer(playerId);
        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(typeof(PlayerDto), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    public async Task<ActionResult<PlayerDto>> GetPlayerById([FromPlayer] PlayerId playerId)
    {
        try
        {
            var player = await _playerGrpcService.GetPlayerById(playerId);
            return Ok(player);
        }
        catch (PlayerNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost("register")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> Register([FromUser] UserId userId)
    {
        return await _playerGrpcService.CreatePlayer(userId);
    }

    [HttpPost("authorize")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> Authorize([FromUser] UserId userId)
    {
        return await _playerGrpcService.GetPlayerByUserId(userId);
    }

    [HttpPost("verify/{taskId}")]
    [ProducesResponseType(typeof(VerdictDto), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<VerdictDto>> VerifySolution(string taskId, [FromForm] string sourceCode, [FromPlayer] PlayerId playerId)
    {
        if (string.IsNullOrEmpty(sourceCode))
        {
            throw new ArgumentNullException(nameof(sourceCode));
        }

        var args = new GetVerdictForPlayerArgs()
        {
            PlayerId = playerId,
            SolutionDto = new SolutionDto()
            {
                TaskId = new TaskId(taskId),
                SourceCode = sourceCode
            }
        };

        var verdict = await _playerGrpcService.GetVerdict(args);
        return verdict;
    }

    [HttpGet("tasks/{taskId}")]
    [ProducesResponseType(typeof(TaskData), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<TaskData>> GetTaskData(string taskId, [FromPlayer] PlayerId playerId)
    {
        var args = new GetTaskDataArgs()
        {
            PlayerId = playerId,
            TaskId = new TaskId(taskId)
        };
        var taskData = await _playerGrpcService.GetTaskData(args);

        return taskData;
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