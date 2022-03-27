using System;
using System.Runtime.Serialization;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using RestAPIWrapper;
using UniRx;
using UnityEngine;

namespace Authentication
{
    public struct TokenSession
    {
        private const string PlayerPrefsKey = "token";

        [JsonProperty("token")] 
        private string _token;

        [JsonProperty("error")] 
        public string Error { get; set; }

        public string Token => _token;

        private bool IsNone => string.IsNullOrEmpty(_token);
        private bool IsError => !string.IsNullOrEmpty(Error);

        public static bool IsNoneToken() => FromPlayerPrefs().IsNone;

        public TokenSession(string token)
        {
            _token = token;
            Error = string.Empty;
        }

        public static implicit operator string(TokenSession tokenSession) => tokenSession.Token;

        public static async UniTask<TokenSession> RequestAndSaveFromServer(string login, string password)
        {
            var token = await RestAPI.GetToken(login, password);
            if (token.IsNone)
            {
                var requestException = new RequestTokenException(token.Error);
                MessageBroker.Default.Publish<Exception>(requestException);

                throw requestException;
            }

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