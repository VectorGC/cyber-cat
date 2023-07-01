using System;

namespace Shared.Server.Exceptions;

public class SaveCodeException : Exception
{
    public SaveCodeException(string message) : base(message)
    {
    }
}