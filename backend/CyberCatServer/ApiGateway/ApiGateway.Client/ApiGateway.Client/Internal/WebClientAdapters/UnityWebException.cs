using System;

namespace ApiGateway.Client
{
    public class UnityWebException : Exception
    {
        public UnityWebException(long statusCode) : base(statusCode.ToString())
        {
        }
    }
}