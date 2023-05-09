namespace ServerAPIBase
{
    public static class ServerConfig
    {
        public static APIVersion VersionOfAPI { get; private set; } = APIVersion.Serverless;


        public enum APIVersion
        {
            Serverless,
            V1,
            V2,
        }
    }
}