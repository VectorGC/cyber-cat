namespace Shared.Exceptions;

public class UnprocessableTokenException : Exception
{
    public override string Message => _innerException.Message;

    private readonly Exception _innerException;

    public UnprocessableTokenException(Exception innerException)
    {
        _innerException = innerException;
    }
}