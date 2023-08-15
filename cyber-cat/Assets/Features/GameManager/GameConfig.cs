using ApiGateway.Client.Factory;
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
    }
}