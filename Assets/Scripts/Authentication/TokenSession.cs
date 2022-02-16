using System;
using UnityEngine;

namespace Authentication
{
    [Serializable]
    public struct TokenSession
    {
        public static event Action<TokenSession> TokenSavedToPlayerPrefs;

        [SerializeField] private string token;

        public string Token => token;

        private TokenSession(string token)
        {
            this.token = token;
        }

        public void SaveToPlayerPrefs()
        {
            Debug.Assert(!string.IsNullOrEmpty(Token), "Attempt save empty token to player prefs.");
            if (string.IsNullOrEmpty(Token))
            {
                return;
            }

            PlayerPrefs.SetString("token", Token);
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
            var token = PlayerPrefs.GetString("token");
            var tokenSession = new TokenSession(token);

            Debug.Assert(!string.IsNullOrEmpty(tokenSession.Token),
                $"Token haven't in player prefs. May be do not save it.");

            return tokenSession;
        }
    }
}