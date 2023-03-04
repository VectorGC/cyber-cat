using Microsoft.AspNetCore.Mvc;

namespace ApiGateway;

[Controller]
[Route("api/v1/[controller]")]
public class SummatorTenController : ControllerBase
{
    private readonly SummatorService _summator;

    public SummatorTenController(SummatorService summator)
    {
        _summator = summator;
    }


    [HttpGet]
    public IActionResult Sum(int number)
    {
        var sum = _summator.Sum(number, 10);
        return Ok(sum);
    }
}