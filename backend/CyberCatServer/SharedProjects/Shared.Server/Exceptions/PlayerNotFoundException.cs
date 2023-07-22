using System;

namespace Shared.Server.Exceptions;

public class PlayerNotFoundException : Exception
{
    public static PlayerNotFoundException UserIdNotFound(long userId) => new($"Player with UserId '{userId}' not found");
    private PlayerNotFoundException(string message) : base(message)
    {
        
    }
}