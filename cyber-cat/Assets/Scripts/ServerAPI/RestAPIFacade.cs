using System;
using ServerAPI.ServerAPIImplements;
using UnityEngine;

namespace ServerAPI
{
    public static class RestAPIFacade
    {
        public static ServerEnvironment ServerEnvironment
        {
            get => (ServerEnvironment) PlayerPrefs.GetInt("server_environment");
            set
            {
                PlayerPrefs.SetInt("server_environment", (int) value);
                PlayerPrefs.Save();
            }
        }

        public static IServerAPI Create()
        {
            switch (ServerEnvironment)
            {
                case ServerEnvironment.Serverless:
                    return new ServerlessAPI();
                case ServerEnvironment.LocalServer:
                    // Send to Api Gateway local instance directly.
                    return new ServerAPIImplements.ServerAPI("http://localhost:5000");
                case ServerEnvironment.DockerLocalServer:
                    // Send to Nginx in local docker engine.
                    return new ServerAPIImplements.ServerAPI("http://localhost");
                case ServerEnvironment.Production:
                    return new ServerAPIImplements.ServerAPI("https://server.cyber-cat.pro");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}