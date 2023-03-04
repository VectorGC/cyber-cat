namespace ApiGateway;

public interface IMultiplicateService
{
    int Mult(int a, float b);
}

class OtherMultiplicateService : IMultiplicateService
{
    private readonly NumberOperationsAggregator _aggregator;

    public OtherMultiplicateService(NumberOperationsAggregator aggregator)
    {
        _aggregator = aggregator;
    }

    public int Mult(int a, float b)
    {
        _aggregator.Increment();
        return a;
    }
}

public class MultiplicateService : IMultiplicateService
{
    private readonly ConvertFloatToIntService _convertFloatToIntService;
    private readonly NumberOperationsAggregator _aggregator;

    public MultiplicateService(ConvertFloatToIntService convertFloatToIntService, NumberOperationsAggregator aggregator)
    {
        _convertFloatToIntService = convertFloatToIntService;
        _aggregator = aggregator;
    }

    public int Mult(int a, float b)
    {
        var bInt = _convertFloatToIntService.Convert(b);
        _aggregator.Increment();
        return a * bInt;
    }
}