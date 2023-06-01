using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;
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

    [HttpPut("verify/{taskId}")]
    [ProducesResponseType(typeof(VerdictDto), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<VerdictDto>> VerifySolution(string taskId, [FromBody] string sourceCode)
    {
        var args = new SolutionDto
        {
            TaskId = taskId,
            SourceCode = sourceCode
        };

        var verdict = await _judgeGrpcService.GetVerdict(args);
        return verdict;
    }
}