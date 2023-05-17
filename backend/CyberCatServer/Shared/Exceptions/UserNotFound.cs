namespace Shared.Exceptions;

public class UserNotFound : Exception
{
    public static UserNotFound NotFoundEmail(string email)
    {
        return new UserNotFound($"User with email '{email}' not found");
    }

    public static UserNotFound NotFoundToken(string token)
    {
        return new UserNotFound($"User with token '{token}' not found");
    }

    private UserNotFound(string message) : base(message)
    {
    }
}