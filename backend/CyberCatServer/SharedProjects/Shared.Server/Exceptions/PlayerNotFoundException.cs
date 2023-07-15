using System;

namespace Shared.Server.Exceptions;

public class PlayerNotFoundException : Exception
{
    public static PlayerNotFoundException UserIdNotFound(long userId)
    {
        throw new PlayerNotFoundException($"Player with UserId '{userId}' not found");
    }

    private PlayerNotFoundException(string message) : base(message)
    {
        
    }
}