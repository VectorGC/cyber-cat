using System.Net;
using System.Text.Json;
using ApiGateway.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

/// <summary>
/// Контроллер отвечает за все что связано с кодом и решениями участников. Он много на себя берет, надо рефакторить.
/// </summary>
[Controller]
[Route("[controller]")]
public class SolutionController : ControllerBase
{
    /// <summary>
    /// Получить последний сохраненный код пользователя по задаче
    /// </summary>
    /// <returns>Последний сохраненный код</returns>
    [HttpGet]
    [ProducesResponseType(typeof(SavedCodeDto), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public IActionResult GetLastSavedCode(string token, [FromQuery(Name = "task_id")] string taskId)
    {
        if (AuthenticationController.Token != token)
        {
            return Unauthorized();
        }

        var jsonData = "{\"text\" : \"Hallo world!\"}";
        var savedCode = JsonSerializer.Deserialize<SavedCodeDto>(jsonData);

        return Ok(savedCode);
    }

    /// <summary>
    /// Проверить код.
    /// </summary>
    /// <param name="token">Токен доступа</param>
    /// <param name="taskId">ID задачи</param>
    /// <param name="sourceCode">Код</param>
    /// <param name="language">Язык программирования</param>
    /// <returns>Да или нет</returns>
    [HttpPost]
    [ProducesResponseType(typeof(VerdictResult), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    public IActionResult VerifyCodeSolution(string token,
        [FromForm(Name = "task_id")] string taskId,
        [FromForm(Name = "source_text")] string sourceCode,
        [FromForm(Name = "lang")] string language)
    {
        if (AuthenticationController.Token != token)
        {
            return Unauthorized();
        }

        var verdict = new VerdictResult
        {
            Error = 0,
            ErrorData = null
            /*
            new VerdictError
            {
                Expected = "Подключенный сервер",
                Msg = "Ваш код, сохранен",
                Params = "123",
                Result = "Ваш код, сохранен",
                Stage = "Отправка",
                TestsPassed = "1",
                TestsTotal = "1"
            }
            */
        };

        return Ok(verdict);
    }
}