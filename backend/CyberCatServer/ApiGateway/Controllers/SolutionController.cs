using System.Net;
using ApiGateway.Authorization;
using ApiGateway.Dto;
using ApiGateway.Models;
using ApiGateway.Repositories;
using ApiGateway.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

/// <summary>
/// Контроллер отвечает за все что связано с кодом и решениями участников. Он много на себя берет, надо рефакторить.
/// </summary>
[Controller]
[AuthorizeTokenGuard]
[Route("[controller]")]
public class SolutionController : ControllerBase
{
    private readonly ISolutionService _solutionService;
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;

    public SolutionController(ISolutionService solutionService, IUserRepository userRepository, ILogger<SolutionController> logger)
    {
        _solutionService = solutionService;
        _userRepository = userRepository;
        _logger = logger;
    }

    /// <summary>
    /// Получить последний сохраненный код пользователя по задаче
    /// </summary>
    /// <returns>Последний сохраненный код</returns>
    [HttpGet]
    [ProducesResponseType(typeof(SavedCodeDto), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public async Task<IActionResult> GetLastSavedCode(string token, [FromQuery(Name = "task_id")] string taskId)
    {
        var userId = (UserId) (HttpContext.Items[typeof(UserId)] ?? throw new InvalidOperationException());
        var savedCode = await _solutionService.GetLastSavedCode(userId, taskId);

        var user = await _userRepository.GetUser(userId);
        _logger.LogInformation("'{User}' get last saved code for task '{Task}'", user, taskId);

        return Ok(new SavedCodeDto(savedCode));
    }

    /// <summary>
    /// Проверить код.
    /// На самом деле - просто сохранить его в базу и выдать клиенту, что код сохранен и он может приступать к следующей задаче.
    /// </summary>
    /// <param name="token">Токен доступа</param>
    /// <param name="taskId">ID задачи</param>
    /// <param name="sourceCode">Код</param>
    /// <param name="language">Язык программирования</param>
    /// <returns>Да или нет</returns>
    [HttpPost]
    [ProducesResponseType(typeof(VerdictResult), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> VerifyCodeSolution(string token,
        [FromForm(Name = "task_id")] string taskId,
        [FromForm(Name = "source_text")] string sourceCode,
        [FromForm(Name = "lang")] string language)
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
}