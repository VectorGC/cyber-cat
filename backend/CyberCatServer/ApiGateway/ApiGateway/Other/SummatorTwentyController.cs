using Microsoft.AspNetCore.Mvc;

namespace ApiGateway;

[Controller]
[Microsoft.AspNetCore.Mvc.Route("api/v1/[controller]")]
public class SummatorTwentyController : ControllerBase
{
    private readonly SummatorService _summator;

    public SummatorTwentyController(SummatorService summator)
    {
        _summator = summator;
    }

    [HttpGet]
    public IActionResult Sum(int number)
    {
        var sum = _summator.Sum(number, 20);
        return Ok(sum);
    }
}