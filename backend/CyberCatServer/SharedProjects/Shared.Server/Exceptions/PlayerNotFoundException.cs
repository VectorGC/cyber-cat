using System;
using Shared.Server.Models;

namespace Shared.Server.Exceptions;

public class PlayerNotFoundException : Exception
{
    public PlayerNotFoundException(PlayerId playerId) : base($"Player '{playerId}' not found")
    {
    }
}