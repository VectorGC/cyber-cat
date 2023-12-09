using System.Net;
using fastJSON;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Domain.Verdicts;
using Shared.Models.Infrastructure;
using Shared.Server.Application.Services;

namespace ApiGateway.Controllers;

[Controller]
public class JudgeController : Controller
{
    private readonly IJudgeService _judgeService;

    public JudgeController(IJudgeService judgeService)
    {
        _judgeService = judgeService;
    }

    [HttpPost(WebApi.JudgeGetVerdict)]
    [ProducesResponseType(typeof(Verdict), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<string>> GetVerdict(string taskId, string solution)
    {
        var verdict = await _judgeService.GetVerdict(new GetVerdictArgs(taskId, solution));
        var json = JSON.ToJSON(verdict);
        return json;
    }
}