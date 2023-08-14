using System;
using ApiGateway.Client;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ServerAPI
{
    public static class ServerEnvironment
    {
        public enum Types
        {
            Serverless = 0,
            LocalServer,
            DockerLocalServer,
            Production,
            Production_Http,
        }

        public static Types Current
        {
            get
            {
                if (Application.isEditor)
                {
                    return (Types) PlayerPrefs.GetInt("server_environment");
                }

                return Debug.isDebugBuild ? Types.DockerLocalServer : Types.Production;
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
                switch (Current)
                {
                    case Types.Serverless:
                        return string.Empty;
                    case Types.LocalServer:
                        // Send to Api Gateway local instance directly.
                        return "https://localhost:5000";
                    case Types.DockerLocalServer:
                        // Send to Nginx in local docker engine.
                        return "https://localhost";
                    case Types.Production:
                        return "https://server.cyber-cat.pro";
                    case Types.Production_Http:
                        return "http://server.cyber-cat.pro";
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public static IAnonymousClient CreateAnonymousClient()
        {
            switch (Current)
            {
                case Types.Serverless:
                    return ServerClient.CreateAnonymousServerless();
                default:
                    return ServerClient.Create(ServerUri);
            }
        }

        public static async UniTask<IAuthorizedClient> CreateAuthorizedClient()
        {
            switch (Current)
            {
                case Types.Serverless:
                    return ServerClient.CreateAuthorizedServerless();
                default:
                    var client = CreateAnonymousClient();
                    var token = await client.Authorization.GetAuthenticationToken("cat", "cat");

                    return ServerClient.Create(ServerUri, token);
            }
        }
    }
}