namespace ApiGateway.Controllers;

public class JudgeController
{
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