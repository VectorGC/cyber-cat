using System;
using RestAPIWrapper;

namespace Authentication
{
    public class TokenException : Exception
    {
        public TokenException(string message) : base(message)
        {
        }

        public override string ToString()
        {
            return WebErrorLocalize.Localize(Message);
        }
    }
}