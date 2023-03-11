using System.Net;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

/// <summary>
/// Контроллер отвечает за все что связано с кодом и решениями участников. Он много на себя берет, надо рефакторить.
/// </summary>
[Controller]
[Route("[controller]")]
public class SolutionController : Controller
{
    /// <summary>
    /// Получить последний сохраненный код пользователя по задаче
    /// </summary>
    /// <returns>Последний сохраненный код</returns>
    [HttpGet]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.Forbidden)]
    public IActionResult GetLastSavedCode(string token, [FromQuery(Name = "task_id")] string taskId)
    {
        if (AuthenticationController.TOKEN != token)
        {
            return Unauthorized();
        }

        var obj = JsonNode.Parse("{\"text\" : \"Hallo world!\"}");

        return Json(obj);
    }

    /// <summary>
    /// Проверить код.
    /// </summary>
    /// <param name="taskId">ID задачи</param>
    /// <param name="sourceCode">Код</param>
    /// <param name="language">Язык программирования</param>
    /// <returns>Да или нет</returns>
    [HttpPost]
    [ProducesResponseType((int) HttpStatusCode.Unauthorized)]
    public IActionResult VerifyCodeSolution(string token,
        [FromForm(Name = "task_id")] string taskId,
        [FromForm(Name = "source_text")] string sourceCode,
        [FromForm(Name = "lang")] string language)
    {
        if (AuthenticationController.TOKEN != token)
        {
            return Unauthorized();
        }

        return Ok(language);
    }
}