using MongoDB.Driver;

namespace ApiGateway.Exceptions;

public class UnprocessableTokenException : Exception
{
    public override string Message => _innerException.Message;

    private readonly MongoException _innerException;

    public UnprocessableTokenException(MongoException innerException)
    {
        _innerException = innerException;
    }
}