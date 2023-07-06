#if UNITY_WEBGL
using System;

namespace ApiGateway.Client.Internal.WebClientAdapters.UnityWebRequest
{
    public class UnityWebException : Exception
    {
        public UnityWebException(long statusCode) : base($"Status '{statusCode.ToString()}'")
        {
        }

        public override string ToString()
        {
            return $"{base.ToString()}";
        }
    }
}
#endif