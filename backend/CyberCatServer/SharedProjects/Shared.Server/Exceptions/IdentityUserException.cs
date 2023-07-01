using System;

namespace Shared.Server.Exceptions;

public class IdentityUserException : Exception
{
    public IdentityUserException(string message) : base(message)
    {
    }
}