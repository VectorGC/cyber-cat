using System;
using System.Collections.Specialized;
using System.Web;
using Newtonsoft.Json;
using UnityEngine;

namespace Authentication
{
    public struct TokenSession
    {
        private const string QueryParam = "token";
        private const string PlayerPrefsKey = "token";
        
        public static event Action<TokenSession> TokenSavedToPlayerPrefs;

        [JsonProperty("token")]
        private string _token;

        public string Token => _token;

        public TokenSession(string token)
        {
            _token = token;
        }

        public static implicit operator string(TokenSession tokenSession) => tokenSession.Token;

        public readonly NameValueCollection ToQueryParam()
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query.Add(QueryParam, Token);

            return query;
        }

        public void SaveToPlayerPrefs()
        {
            Debug.Assert(!string.IsNullOrEmpty(Token), "Attempt save empty token to player prefs.");
            if (string.IsNullOrEmpty(Token))
            {
                return;
            }

            PlayerPrefs.SetString(PlayerPrefsKey, Token);
            Debug.Log("Token saved to player prefs");

            TokenSavedToPlayerPrefs?.Invoke(this);
        }

        public static TokenSession FromJson(string jsonText)
        {
            var tokenSession = JsonUtility.FromJson<TokenSession>(jsonText);
            Debug.Assert(!string.IsNullOrEmpty(tokenSession.Token),
                $"Token non serialized from json '{jsonText}'. Check format.");

            return JsonUtility.FromJson<TokenSession>(jsonText);
        }

        public static TokenSession FromPlayerPrefs()
        {
            var token = PlayerPrefs.GetString(PlayerPrefsKey);
            var tokenSession = new TokenSession(token);

            Debug.Assert(!string.IsNullOrEmpty(tokenSession.Token),
                $"Token haven't in player prefs. May be do not save it.");

            return tokenSession;
        }
    }
}