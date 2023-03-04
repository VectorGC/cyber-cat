namespace ApiGateway;

public class SummatorService
{
    private readonly ConvertFloatToIntService _convertFloatToIntService;
    private readonly NumberOperationsAggregator _aggregator;

    public SummatorService(NumberOperationsAggregator aggregator, ConvertFloatToIntService convertFloatToIntService)
    {
        _aggregator = aggregator;
        _convertFloatToIntService = convertFloatToIntService;
    }

    public int Sum(int a, float b)
    {
        var bInt = _convertFloatToIntService.Convert(b);
        _aggregator.Increment();
        return a + bInt;
    }
}