using System;

namespace WebRequests.Extensions
{
    public interface IGetUriHandler
    {
        string GetUriDomain();
    }

    public static class WebRequestExt
    {
        public const string DEFAULT_DOMAIN = "https://kee-reel.com/cyber-cat/v2";

        public static Uri GetUri(this IWebRequest webRequest)
        {
            var domain = GetUriDomain(webRequest);
            var builder = new UriBuilder(domain)
            {
                Query = webRequest.QueryParams.ToString()
            };

            return builder.Uri;
        }

        private static string GetUriDomain(IWebRequest webRequest)
        {
            if (webRequest is IGetUriHandler uriHandler)
            {
                return uriHandler.GetUriDomain();
            }

            return DEFAULT_DOMAIN;
        }
    }
}