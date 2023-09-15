using System.Threading.Tasks;
using ApiGateway.Client;
using ApiGateway.Client.Models;
using UnityEngine;

namespace Features.ServerConfig
{
    public static class ServerAPI
    {
        public static ServerEnvironment ServerEnvironment
        {
            get
            {
                if (Application.isEditor)
                {
                    return (ServerEnvironment) PlayerPrefs.GetInt("server_environment");
                }

                return Debug.isDebugBuild ? ServerEnvironment.Localhost : ServerEnvironment.Production;
            }
            set
            {
                PlayerPrefs.SetInt("server_environment", (int) value);
                PlayerPrefs.Save();
            }
        }

        public static async Task<IPlayer> CreatePlayerClient()
        {
            var user = await ServerClientFactory.CreateAnonymous(ServerEnvironment).SignIn("cat", "cat");
            return await user.SignInAsPlayer();
        }
    }
}