namespace RestAPIWrapper
{
    public static class RestAPI
    {
        public static readonly IRestAPI Instance =
#if SERVERLESS
            new Serverless.RestAPIServerless();
#else
            new Server.RestAPIKeeReel();
#endif
    }
}