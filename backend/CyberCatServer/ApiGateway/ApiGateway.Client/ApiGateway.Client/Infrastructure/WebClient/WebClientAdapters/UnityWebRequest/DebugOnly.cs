#if UNITY_WEBGL
using Debug = UnityEngine.Debug;

namespace ApiGateway.Client.V3.Infrastructure.WebClientAdapters.UnityWebRequest
{
    public static class DebugOnly
    {
        public static void Log(string message)
        {
            if (Debug.isDebugBuild)
                Debug.Log(message);
        }

        public static void LogError(string message)
        {
            if (Debug.isDebugBuild)
                Debug.LogError(message);
        }
    }
}
#endif