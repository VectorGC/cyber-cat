using System;

namespace Shared.Server.Exceptions;

public class BitcoinOperationException : Exception
{
    private BitcoinOperationException(string message) : base(message)
    {
    }

    public static BitcoinOperationException NotEnoughBitcoins(long playerId, int bitcoins) =>
        new($"Error taking {bitcoins} bitcoins from player with Id {playerId}: Not Enough Bitcoins");
}