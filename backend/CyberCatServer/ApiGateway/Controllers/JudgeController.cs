using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Dto;
using Shared.Services;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class JudgeController : ControllerBase
{
    private readonly IJudgeGrpcService _judgeService;

    public JudgeController(IJudgeGrpcService judgeService)
    {
        _judgeService = judgeService;
    }

    [HttpPut("verify/{taskId}")]
    [ProducesResponseType(typeof(VerdictResponse), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<VerdictResponse>> VerifySolution(string taskId, [FromBody] string sourceCode)
    {
        var userId = User.Identity.GetUserId();
        var args = new SolutionArgs
        {
            UserId = userId,
            TaskId = taskId,
            SolutionCode = sourceCode
        };

        var verdict = await _judgeService.GetVerdict(args);
        return verdict;
    }
}