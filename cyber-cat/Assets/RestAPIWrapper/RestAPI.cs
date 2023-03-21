#define OFFLINE

namespace RestAPIWrapper
{
    public static class RestAPI
    {
        public static readonly IRestAPI Instance =
#if SERVERLESS
            new Serverless.RestAPIServerless();
#elif OFFLINE
            new Server.RestAPIFiles();
#else
            new Server.RestAPIServer();
#endif
    }
}