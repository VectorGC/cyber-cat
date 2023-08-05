using System.Net;
using ApiGateway.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Dto;
using Shared.Models.Dto.Args;
using Shared.Server.Exceptions;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PlayerController: ControllerBase
{
    private readonly IPlayerGrpcService _playerGrpcService;

    public PlayerController(IPlayerGrpcService playerGrpcService)
    {
        _playerGrpcService = playerGrpcService;
    }

    [HttpPost("create")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<ActionResult> AddNewPlayer()
    {
        var newPlayerId = User.Identity.GetUserId();
        var args = new PlayerIdArgs { PlayerId = newPlayerId };
        try
        {
            await _playerGrpcService.AddNewPlayer(args);
            return Ok();
        }
        catch (PlayerAlreadyExistsException)
        {
            return BadRequest();
        }
        
    }

    [HttpDelete("delete")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult> DeletePlayer()
    {
        var playerId = User.Identity.GetUserId();
        var args = new PlayerIdArgs { PlayerId = playerId };
        await _playerGrpcService.DeletePlayer(args);
    
        return Ok();
    }

    [HttpGet("get")]
    [ProducesResponseType(typeof(PlayerDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<PlayerDto>> GetPlayerById()
    {
        var playerId = User.Identity.GetUserId();
        var args = new PlayerIdArgs { PlayerId = playerId };
        try
        {
            var player = await _playerGrpcService.GetPlayerById(args);
            return Ok(player);
        }
        catch (PlayerNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("bitcoins/add")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> AddBitcoinsToPlayer([FromForm] string bitcoins)
    {
        var playerId = User.Identity.GetUserId();
        var args = new PlayerBtcArgs { PlayerId = playerId, BitcoinsCount = Convert.ToInt32(bitcoins) };
        try
        {
            await _playerGrpcService.AddBitcoinsToPlayer(args);
            return Ok();
        }
        catch(PlayerNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPut("bitcoins/take")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.Forbidden)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<ActionResult> TakeBitcoinsFromPlayer([FromForm] string bitcoins)
    {
        var playerId = User.Identity.GetUserId();
        var args = new PlayerBtcArgs { PlayerId = playerId, BitcoinsCount = Convert.ToInt32(bitcoins) };
        try
        {
            await _playerGrpcService.TakeBitcoinsFromPlayer(args);
            return Ok();
        }
        catch(PlayerNotFoundException)
        {
            return NotFound();
        }
    }
    
}