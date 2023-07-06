namespace ApiGateway.Client.Internal.WebClientAdapters
{
    internal static class WebClientFactory
    {
        public static IWebClient Create()
        {
#if UNITY_WEBGL
            return new UnityWebRequest.UnityWebClient();
#endif
#if WEB_CLIENT
            return new WebClient.WebClientAdapter();
#endif
        }
    }
}