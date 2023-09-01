using System;
using Shared.Server.Models;

namespace Shared.Server.Exceptions;

public class NotEnoughBitcoinsException : Exception
{
    public NotEnoughBitcoinsException(PlayerId playerId, int bitcoins)
        : base($"Error taking {bitcoins} bitcoins from player with Id {playerId}: Not Enough Bitcoins")
    {
    }
}