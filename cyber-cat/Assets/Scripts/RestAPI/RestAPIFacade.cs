using System;
using RestAPI.RestAPIImplements;
using UnityEngine;

namespace RestAPI
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

        public static IRestAPI Create()
        {
            switch (ServerEnvironment)
            {
                case ServerEnvironment.Serverless:
                    return new ServerlessRestAPI();
                case ServerEnvironment.LocalServer:
                    return new RestAPIImplements.RestAPI("https://localhost:5000");
                case ServerEnvironment.Production:
                    return new RestAPIImplements.RestAPI("https://server.cyber-cat.pro");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}