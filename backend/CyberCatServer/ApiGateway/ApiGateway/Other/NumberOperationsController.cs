using Microsoft.AspNetCore.Mvc;

namespace ApiGateway;

[Controller]
[Route("api/v1/[controller]")]
public class NumberOperationsController : ControllerBase
{
    private readonly NumberOperationsAggregator _aggregator;

    public NumberOperationsController(NumberOperationsAggregator aggregator)
    {
        _aggregator = aggregator;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_aggregator.GetCount());
    }
}