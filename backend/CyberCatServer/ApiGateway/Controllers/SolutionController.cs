using System.Net;
using ApiGateway.Dto;
using ApiGateway.Models;
using ApiGateway.Repositories;
using ApiGateway.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SolutionController : ControllerBase
{
    private readonly ISolutionService _solutionService;
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;

    public SolutionController(ILogger<SolutionController> logger)
    {
        _logger = logger;
    }

    [HttpPost("save")]
    [ProducesResponseType((int) HttpStatusCode.OK)]
    public async Task<IActionResult> SaveCode([FromBody] SaveCodeArgsDto args)
    {
        //var userId = (UserId) (HttpContext.Items[typeof(UserId)] ?? throw new InvalidOperationException());
        //var savedCode = await _solutionService.GetLastSavedCode(userId, taskId);

        //var user = await _userRepository.GetUser(userId);
        //_logger.LogInformation("'{User}' get last saved code for task '{Task}'", user, taskId);

        return Ok();
    }

    [HttpGet]
    [ProducesResponseType(typeof(SaveCodeArgsDto), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<IActionResult> GetLastSavedCode([FromQuery(Name = "task_id")] string taskId)
    {
        var userId = (UserId) (HttpContext.Items[typeof(UserId)] ?? throw new InvalidOperationException());
        var savedCode = await _solutionService.GetLastSavedCode(userId, taskId);

        var user = await _userRepository.GetUser(userId);
        _logger.LogInformation("'{User}' get last saved code for task '{Task}'", user, taskId);

        return Ok();
    }

    /*
    [HttpPost]
    [ProducesResponseType(typeof(VerdictResult), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> VerifyCodeSolution([FromForm(Name = "task_id")] string taskId, [FromForm(Name = "source_text")] string sourceCode)
    {
        var userId = (UserId) (HttpContext.Items[typeof(UserId)] ?? throw new InvalidOperationException());

        try
        {
            await _solutionService.SaveCode(userId, taskId, sourceCode);
            _logger.LogInformation("'{User}' saved code for task '{Task}'", userId, taskId);
            return Ok(new VerdictResult());
        }
        catch (Exception e)
        {
            var user = await _userRepository.GetUser(userId);
            _logger.LogError(e, "User '{User}'", user);

            return Ok(new VerdictResult
            {
                Error = 1000,
                ErrorData = new VerdictError
                {
                    Msg = "При сохранении произошла ошибка. Попробуйте ещё раз или обратитесь к организатору."
                }
            });
        }
    }
    */
}