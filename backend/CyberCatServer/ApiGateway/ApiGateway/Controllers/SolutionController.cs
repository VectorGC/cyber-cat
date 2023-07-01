using System.Net;
using ApiGateway.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Dto;
using Shared.Models.Dto.Args;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SolutionController : ControllerBase
{
    private readonly ISolutionGrpcService _solutionGrpcService;

    public SolutionController(ISolutionGrpcService solutionGrpcService)
    {
        _solutionGrpcService = solutionGrpcService;
    }

    [HttpGet("{taskId}")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<string>> GetLastSavedCode(string taskId)
    {
        var userId = User.Identity.GetUserId();
        var args = new GetSavedCodeArgs
        {
            UserId = userId,
            TaskId = taskId
        };

        var savedCode = await _solutionGrpcService.GetSavedCode(args);
        return (string) savedCode;
    }

    [HttpPost("{taskId}")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> SaveCode(string taskId, [FromBody] string sourceCode)
    {
        if (string.IsNullOrEmpty(sourceCode))
        {
            throw new ArgumentNullException(nameof(sourceCode));
        }

        var userId = User.Identity.GetUserId();
        var args = new SaveCodeArgs()
        {
            UserId = userId,
            Solution = new SolutionDto()
            {
                TaskId = taskId,
                SourceCode = sourceCode
            }
        };

        await _solutionGrpcService.SaveCode(args);

        return Ok();
    }

    [HttpDelete("{taskId}")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<string>> DeleteSavedCode(string taskId)
    {
        var userId = User.Identity.GetUserId();
        var args = new RemoveCodeArgs
        {
            UserId = userId,
            TaskId = taskId
        };

        await _solutionGrpcService.RemoveCode(args);

        return Ok();
    }
}