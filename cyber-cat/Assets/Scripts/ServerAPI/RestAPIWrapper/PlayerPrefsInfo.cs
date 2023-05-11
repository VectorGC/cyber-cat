using UnityEngine;

namespace RestAPIWrapper
{
    public static class PlayerPrefsInfo
    {
        public static string Key { get; } = "token";
        public static string Token { get; } = "token";
        public static string Name { get; } = "name";

        public static TokenSession GetToken()
        {
            var token = PlayerPrefs.GetString(Key);
            var name = PlayerPrefs.GetString(Name);
            return new TokenSession();
        }
    }
}