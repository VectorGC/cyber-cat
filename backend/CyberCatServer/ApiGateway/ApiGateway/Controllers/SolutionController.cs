using System.Net;
using ApiGateway.Attributes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Dto;
using Shared.Models.Models;
using Shared.Server.Dto.Args;
using Shared.Server.Models;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SolutionController : ControllerBase
{
    private readonly ISolutionGrpcService _solutionGrpcService;
    private readonly IAuthGrpcService _authGrpcService;

    public SolutionController(ISolutionGrpcService solutionGrpcService, IAuthGrpcService authGrpcService)
    {
        _authGrpcService = authGrpcService;
        _solutionGrpcService = solutionGrpcService;
    }

    [HttpGet("{taskId}")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<string>> GetLastSavedCode(string taskId, [FromUser] UserId userId)
    {
        var args = new GetSavedCodeArgs
        {
            UserId = userId,
            TaskId = new TaskId(taskId)
        };

        var savedCode = await _solutionGrpcService.GetSavedCode(args);
        return (string) savedCode;
    }

    [HttpPost("{taskId}")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<ActionResult> SaveCode(string taskId, [FromForm] string sourceCode, [FromUser] UserId userId)
    {
        if (string.IsNullOrEmpty(sourceCode))
        {
            throw new ArgumentNullException(nameof(sourceCode));
        }

        var args = new SaveCodeArgs()
        {
            UserId = userId,
            Solution = new SolutionDto()
            {
                TaskId = new TaskId(taskId),
                SourceCode = sourceCode
            }
        };

        await _solutionGrpcService.SaveCode(args);

        return Ok();
    }

    [HttpDelete("{taskId}")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<ActionResult<string>> DeleteSavedCode(string taskId, [FromUser] UserId userId)
    {
        var args = new RemoveCodeArgs
        {
            UserId = userId,
            TaskId = new TaskId(taskId)
        };

        await _solutionGrpcService.RemoveCode(args);

        return Ok();
    }
}