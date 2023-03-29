using Authentication;
using Cysharp.Threading.Tasks;
using GameCodeEditor.Scripts;
using System;
using Newtonsoft.Json;
using TaskChecker;
using TaskUnits;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RequestAPI.Proxy
{
    public static class RequestAPIProxy
    {
        private const string PlayerPrefsKey = "token";
        private const string PlayerPrefsKeyName = "name";

        private static readonly AuthTokenRequestWrapper _authTokenRequestWrapper = new AuthTokenRequestWrapper();

        public static async UniTask Register(string login, string password, string name)
        {
            await _authTokenRequestWrapper.RegisterUser(login, password, name);
        }

        public static async UniTask RestorePassword(string login, string password)
        {
            await _authTokenRequestWrapper.RestorePassword(login, password);
        }

        public static async UniTask<TokenSession> Authenticate(string login, string password)
        {
            var token = await GetTokenFromServer(login, password);
            SaveTokenToPlayerPrefs(token);

            return token;
        }

        public static async UniTask<ICodeConsoleMessage> CheckCode(ITaskData task, string token, string code, string progLanguage)
        {
            return await task.CheckCodeAsync(token, code, progLanguage);
        }

        public static async UniTask<string> GetSavedCode(string token, string taskId)
        {
            return await new GetLastSaveCodeRequest().GetLastSavedCode(token, taskId);
        }

        public static async UniTask<TokenSession> GetTokenFromServer(string login, string password)
        {
            var tokenObj = await _authTokenRequestWrapper.GetAuthData(login, password);
            var tokenSession = JsonConvert.DeserializeObject<TokenSession>(tokenObj);
            return tokenSession;
        }

        public static async UniTask<ITaskDataCollection> GetTasks(string token, IProgress<float> progress = null)
        {
            return await TaskFacade.GetAllTasks(token, progress);
        }

        public static TokenSession GetTokenFromPlayerPrefs(bool checkToken = true)
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

        public static void SaveTokenToPlayerPrefs(TokenSession token)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentNullException($"Attempt save empty token to player prefs");
            }

            PlayerPrefs.SetString(PlayerPrefsKey, token);
            PlayerPrefs.SetString(PlayerPrefsKeyName, token.Name);
            Debug.Log("Token saved to player prefs");
        }

        public static void DeleteTokenFromPlayerPrefs() => PlayerPrefs.DeleteKey(PlayerPrefsKey);
    }
}