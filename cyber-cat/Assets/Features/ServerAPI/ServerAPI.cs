using System;
using System.Threading.Tasks;
using ApiGateway.Client;
using ApiGateway.Client.Models;
using ApiGateway.Client.V2;
using UnityEngine;
using Random = UnityEngine.Random;

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

        public static async Task<IUser> CreateUserClient()
        {
            var userSeed = PlayerPrefs.GetString("user_seed");
            if (string.IsNullOrEmpty(userSeed))
            {
                userSeed = Random.Range(0, 10000).ToString();
                var newEmail = $"cat_{userSeed}";
                var newPassword = $"{userSeed}_cat";
                await ServerClientFactory.CreateAnonymous(ServerEnvironment).SignUp(newEmail, newPassword, $"Cat_{userSeed}");
                PlayerPrefs.SetString("user_seed", userSeed);
            }

            var email = $"cat_{userSeed}";
            var password = $"{userSeed}_cat";
            try
            {
                var user = await ServerClientFactory.CreateAnonymous(ServerEnvironment).SignIn(email, password);
                return user;
            }
            catch (Exception ex)
            {
                PlayerPrefs.DeleteKey("user_seed");
                throw;
            }
        }

        public static async Task<IPlayer> CreatePlayerClient()
        {
            var user = await CreateUserClient();
            return await user.SignInAsPlayer();
        }

        public static User CreateUserProxy()
        {
            var client = new ApiGateway.Client.V2.ApiGateway.Client(ServerEnvironment);
            return client.User;
        }
    }
}