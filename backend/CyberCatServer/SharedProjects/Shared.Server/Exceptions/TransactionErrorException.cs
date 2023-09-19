using System;

namespace Shared.Server.Exceptions;

public class TransactionErrorException : Exception
{
    private TransactionErrorException(string message) : base(message)
    {
    }

    public static TransactionErrorException AddTransactionError(long userId, int bitcoins)
    {
        return new TransactionErrorException($"Failed to add {bitcoins} to user with UserId {userId}");
    }
    
}