using UnityEngine;

namespace ServerAPIBase
{
    public static class ServerData
    {
        public static string URL { get; } = "https://server.cyber-cat.pro";
        public static bool DebugBuild => Debug.isDebugBuild;
    }
}

