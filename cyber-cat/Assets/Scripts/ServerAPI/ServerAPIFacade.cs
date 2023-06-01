using System;
using ApiGateway.Client;
using UnityEngine;

namespace ServerAPI
{
    public static class ServerAPIFacade
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

        public static IClient Create()
        {
            switch (ServerEnvironment)
            {
                case ServerEnvironment.Serverless:
                    return new ServerlessClient();
                case ServerEnvironment.LocalServer:
                    // Send to Api Gateway local instance directly.
                    return new Client("http://localhost:5000", new UnityRestClientProxy());
                case ServerEnvironment.DockerLocalServer:
                    // Send to Nginx in local docker engine.
                    return new Client("http://localhost", new UnityRestClientProxy());
                case ServerEnvironment.Production:
                    return new Client("https://server.cyber-cat.pro", new UnityRestClientProxy());
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}