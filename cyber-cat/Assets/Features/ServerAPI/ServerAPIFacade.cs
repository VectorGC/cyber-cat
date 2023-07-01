using System;
using UnityEngine;

namespace ServerAPI
{
    public static class ServerAPIFacade
    {
        public static ServerEnvironment ServerEnvironment
        {
            get
            {
                if (Application.isEditor)
                {
                    return (ServerEnvironment) PlayerPrefs.GetInt("server_environment");
                }

                //return ServerEnvironment.LocalServer;
                return ServerEnvironment.Production;
            }
            set
            {
                PlayerPrefs.SetInt("server_environment", (int) value);
                PlayerPrefs.Save();
            }
        }

        public static string ServerUri
        {
            get
            {
                switch (ServerEnvironment)
                {
                    case ServerEnvironment.Serverless:
                        return string.Empty;
                    case ServerEnvironment.LocalServer:
                        // Send to Api Gateway local instance directly.
                        return "http://localhost:5000";
                    case ServerEnvironment.DockerLocalServer:
                        // Send to Nginx in local docker engine.
                        return "http://localhost";
                    case ServerEnvironment.Production:
                        return "https://server.cyber-cat.pro";
                    case ServerEnvironment.Production_Http:
                        return "http://server.cyber-cat.pro";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public static IServerAPI Create()
        {
            switch (ServerEnvironment)
            {
                case ServerEnvironment.Serverless:
                    return new Serverless();
                case ServerEnvironment.LocalServer:
                    // Send to Api Gateway local instance directly.
                    return new RestClientServerAPI("http://localhost:5000");
                case ServerEnvironment.DockerLocalServer:
                    // Send to Nginx in local docker engine.
                    return new RestClientServerAPI("http://localhost");
                case ServerEnvironment.Production:
                    return new RestClientServerAPI("https://server.cyber-cat.pro");
                case ServerEnvironment.Production_Http:
                    return new RestClientServerAPI("http://server.cyber-cat.pro");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}