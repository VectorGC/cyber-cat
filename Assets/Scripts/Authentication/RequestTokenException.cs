using System;

namespace Authentication
{
    public class RequestTokenException : Exception
    {
        public RequestTokenException(string message) : base(message)
        {
        }
    }
}