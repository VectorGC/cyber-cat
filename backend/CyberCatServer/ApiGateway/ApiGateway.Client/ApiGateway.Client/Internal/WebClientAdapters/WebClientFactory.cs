namespace ApiGateway.Client
{
    internal static class WebClientFactory
    {
        public static IWebClient Create()
        {
#if UNITY_WEBGL
            return new UnityWebClient();
#else
            return new WebClientAdapter();
#endif
        }
    }
}