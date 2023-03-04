namespace ApiGateway;

public class ConvertFloatToIntService
{
    private readonly LoggerForConvertingNumbers _logger;

    public ConvertFloatToIntService(LoggerForConvertingNumbers logger)
    {
        _logger = logger;
    }

    public int Convert(float number)
    {
        _logger.Log(number);
        return (int) number;
    }
}