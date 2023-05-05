using ServerAPIBase;

namespace RestAPIWrapper
{
    public static class RestAPI
    {
        private static IRestAPI _instance;
        public static IRestAPI Instance
        {
            get
            {
                if (_instance == null)
                {
                    switch (ServerConfig.VersionOfAPI)
                    {
                        case ServerConfig.APIVersion.V1:
                            _instance = new Server.RestAPIServer();
                            break;
                        case ServerConfig.APIVersion.V2:
                            _instance = new Server.RestAPIServer();
                            break;

                        case ServerConfig.APIVersion.Serverless:
                            _instance = new Serverless.RestAPIServerless();
                            break;
                    }
                }
                return _instance;
            }
        }
    }
}