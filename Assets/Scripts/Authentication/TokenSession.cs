using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Authentication
{
    public struct TokenSession
    {
        private const string PlayerPrefsKey = "token";
        private const string PlayerPrefsKeyName = "name";

        [JsonProperty("token")] private string _token;

        [JsonProperty("name")] private string _name;

        [JsonProperty("error")] public string Error { get; set; }

        public string Token => _token;

        public string Name => _name;

        private bool IsNone => string.IsNullOrEmpty(_token);

        public static bool IsNoneToken => FromPlayerPrefs(false).IsNone;

        private static readonly IAuthTokenRequestWrapper RequestWrapper = new AuthTokenRequestWrapper();

        public TokenSession(string token)
        {
            _token = token;
            _name = string.Empty;
            Error = string.Empty;
        }

        public TokenSession(string token, string name)
        {
            _token = token;
            _name = name;
            Error = string.Empty;
        }

        public static implicit operator string(TokenSession tokenSession) => tokenSession.Token;

        public static async UniTask<TokenSession> RequestAndSaveFromServer(string login, string password)
        {
            var token = await RequestWrapper.GetAuthData(login, password);
            token.SaveToPlayerPrefs();

            return token;
        }

        public static async UniTask RegisterUser(string login, string password, string name)
        {
            await RequestWrapper.RegisterUser(login, password, name);
        }

        public static async UniTask RestorePassword(string login, string password)
        {
            await RequestWrapper.RestorePassword(login, password);
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

        public static TokenSession FromPlayerPrefs(bool checkToken = true)
        {
            var token = PlayerPrefs.GetString(PlayerPrefsKey);
            var name = PlayerPrefs.GetString(PlayerPrefsKeyName);
            var tokenSession = new TokenSession(token, name);

            if (token.IsNullOrEmpty() && checkToken)
            {
                ModalPanel.ShowModalDialog("Вы не зарегестрированы", "Пожалуйста, зарегестируйтесь", () =>
                {
                    var async = SceneManager.LoadSceneAsync("StartScene");
                    async.ViaLoadingScreen();
                });
                throw new ArgumentNullException("Token is empty");
            }

            return tokenSession;
        }

        public static void DeleteFromPlayerPrefs() => PlayerPrefs.DeleteKey(PlayerPrefsKey);
    }
}