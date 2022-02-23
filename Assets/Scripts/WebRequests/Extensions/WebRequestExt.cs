using System;

namespace WebRequests.Extensions
{
    public static class WebRequestExt
    {
        public const string DEFAULT_DOMAIN = "https://kee-reel.com/cyber-cat";

        public static Uri GetUri(this IWebRequest webRequest, string domain = DEFAULT_DOMAIN)
        {
            var builder = new UriBuilder(domain)
            {
                Query = webRequest.QueryParams.ToString()
            };

            return builder.Uri;
        }
    }
}