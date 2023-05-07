namespace Shared.Exceptions;

public class IdentityUserException : Exception
{
    public override string Message { get; }

    public IdentityUserException(string message)
    {
        Message = message;
    }
}