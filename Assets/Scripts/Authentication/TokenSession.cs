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
        private const string PlayerPrefsKeyName = "name";

        [JsonProperty("token")] 
        private string _token;

        [JsonProperty("name")]
        private string _name;

        [JsonProperty("error")]
        public string Error { get; set; }

        public string Token => _token;

        public string Name => _name;

        private bool IsNone => string.IsNullOrEmpty(_token);

        public static bool IsNoneToken => FromPlayerPrefs().IsNone;

        public TokenSession(string token, string name)
        {
            _token = token;
            _name = name;
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

        public static async UniTask<TokenSession> Register(string login, string password, string name)
        {
            var token = await RestAPI.Registrate(login, password, name);

            return token;
        }

        private void SaveToPlayerPrefs()
        {
            if (string.IsNullOrEmpty(Token))
            {
                throw new ArgumentNullException($"Attempt save empty token to player prefs");
            }

            PlayerPrefs.SetString(PlayerPrefsKey, Token);
            PlayerPrefs.SetString(PlayerPrefsKeyName, Name);
            Debug.Log("Token saved to player prefs");
        }

        public static TokenSession FromPlayerPrefs()
        {
            var token = PlayerPrefs.GetString(PlayerPrefsKey);
            var name = PlayerPrefs.GetString(PlayerPrefsKeyName);
            var tokenSession = new TokenSession(token, name);

            return tokenSession;
        }

        public static void DeleteFromPlayerPrefs() => PlayerPrefs.DeleteKey(PlayerPrefsKey);
    }
}