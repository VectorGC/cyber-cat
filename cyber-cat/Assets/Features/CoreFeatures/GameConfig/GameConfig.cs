using System.Threading.Tasks;
using ApiGateway.Client;
using ApiGateway.Client.Models;
using UnityEngine;

namespace Features.GameManager
{
    public static class GameConfig
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

        private static IPlayer _player;

        public static async Task<IPlayer> GetOrPlayerClient()
        {
            if (_player == null)
            {
                var user = await ServerClientFactory.CreateAnonymous(ServerEnvironment).SignIn("cat", "cat");
                _player = await user.SignInAsPlayer();
            }

            return _player;
        }
    }
}