using System;

namespace Shared.Server.Exceptions;

public class PlayerAlreadyExistsException : Exception
{
    public PlayerAlreadyExistsException(string message) : base(message)
    {}
}