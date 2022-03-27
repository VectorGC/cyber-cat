using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using RestAPIWrapper;
using UnityEngine;

namespace Authentication
{
    public struct TokenSession
    {
        private const string PlayerPrefsKey = "token";

        [JsonProperty("token")] 
        private string _token;

        public string Token => _token;

        public bool IsNone => string.IsNullOrEmpty(_token);

        public static bool IsNoneToken() => FromPlayerPrefs().IsNone;

        public TokenSession(string token)
        {
            _token = token;
        }

        public static implicit operator string(TokenSession tokenSession) => tokenSession.Token;

        public static async UniTask<TokenSession> RequestAndSaveFromServer(string login, string password)
        {
            var token = await RestAPI.GetToken(login, password);
            token.SaveToPlayerPrefs();

            return token;
        }

        private void SaveToPlayerPrefs()
        {
            if (string.IsNullOrEmpty(Token))
            {
                throw new ArgumentNullException($"Attempt save empty token to player prefs");
            }

            PlayerPrefs.SetString(PlayerPrefsKey, Token);
            Debug.Log("Token saved to player prefs");
        }

        public static TokenSession FromPlayerPrefs()
        {
            var token = PlayerPrefs.GetString(PlayerPrefsKey);
            var tokenSession = new TokenSession(token);

            return tokenSession;
        }

        public static void DeleteFromPlayerPrefs() => PlayerPrefs.DeleteKey(PlayerPrefsKey);
    }
}