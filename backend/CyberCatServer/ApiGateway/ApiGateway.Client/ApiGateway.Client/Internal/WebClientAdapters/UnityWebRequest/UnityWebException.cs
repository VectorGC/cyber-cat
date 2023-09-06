#if UNITY_WEBGL
using System.Net;

namespace ApiGateway.Client.Internal.WebClientAdapters.UnityWebRequest
{
    public class UnityWebException : WebException
    {
        public HttpStatusCode StatusCode { get; }

        public UnityWebException(HttpStatusCode statusCode) : base($"Status '{statusCode.ToString()}'")
        {
            StatusCode = statusCode;
        }

        public override string ToString()
        {
            return $"{base.ToString()}";
        }
    }
}
#endif