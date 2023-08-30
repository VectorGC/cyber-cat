using System;

namespace Shared.Server.Exceptions;

public class UserNotFound : Exception
{
    public UserNotFound(string email) : base($"User with email '{email}' not found")
    {
    }
}