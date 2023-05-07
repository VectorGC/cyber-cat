namespace Shared.Exceptions;

public class UserNotFound : Exception
{
    private string _message;

    public override string Message => _message;

    public static UserNotFound NotFoundEmail(string email)
    {
        return new UserNotFound
        {
            _message = $"User with email '{email}' not found"
        };
    }

    public static UserNotFound NotFoundToken(string token)
    {
        return new UserNotFound
        {
            _message = $"User with token '{token}' not found"
        };
    }

    private UserNotFound()
    {
    }
}