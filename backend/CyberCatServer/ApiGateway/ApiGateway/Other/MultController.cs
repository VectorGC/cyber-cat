using Microsoft.AspNetCore.Mvc;

namespace ApiGateway;

[Controller]
[Route("api/v1/[controller]")]
public class MultController : ControllerBase
{
    private readonly IMultiplicateService _multiplicate;

    public MultController(IMultiplicateService multiplicate, NumberOperationsAggregator aggregator)
    {
        _multiplicate = multiplicate;
    }

    [HttpGet]
    public IActionResult Mult(int a, float b)
    {
        var sum = _multiplicate.Mult(a, b);
        return Ok(sum);
    }
}