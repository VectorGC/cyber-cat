using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers;

[Controller]
[Route("[controller]")]
public class SolutionController : Controller
{
    [HttpGet]
    public IActionResult GetLastSavedCode(string token, [FromQuery(Name = "task_id")] string taskId)
    {
        if (AuthenticationController.TOKEN != token)
        {
            return Forbid();
        }

        var obj = JsonObject.Parse("{\"text\" : \"Hallo world!\"}");

        return Json(obj);
    }
}