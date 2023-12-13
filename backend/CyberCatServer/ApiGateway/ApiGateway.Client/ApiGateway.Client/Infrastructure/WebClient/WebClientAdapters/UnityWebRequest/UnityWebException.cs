#if UNITY_WEBGL
using System.Net;

namespace ApiGateway.Client.V3.Infrastructure.WebClientAdapters.UnityWebRequest
{
    public class UnityWebException : WebException
    {
        public HttpStatusCode StatusCode { get; }

        public UnityWebException(HttpStatusCode statusCode, string message) : base(message)
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