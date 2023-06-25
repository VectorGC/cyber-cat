using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Dto;
using Shared.Server.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class JudgeController : ControllerBase
{
    private readonly IJudgeGrpcService _judgeGrpcService;

    public JudgeController(IJudgeGrpcService judgeGrpcService)
    {
        _judgeGrpcService = judgeGrpcService;
    }

    [HttpPost("verify/{taskId}")]
    [ProducesResponseType(typeof(VerdictDto), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<VerdictDto>> VerifySolution(string taskId, string sourceCode)
    {
        var sr = new StreamReader(Request.Body);
        var b = await sr.ReadToEndAsync();

        var args = new SolutionDto
        {
            TaskId = taskId,
            SourceCode = b
        };

        var verdict = await _judgeGrpcService.GetVerdict(args);
        return verdict;
    }
}