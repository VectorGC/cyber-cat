using System;

namespace Authentication
{
    public static class UniRxLoggerExt
    {
        public static void ThrowException(this UniRx.Diagnostics.Logger logger, Exception exception)
        {
            logger.Exception(exception);
            throw exception;
        }
    }
}