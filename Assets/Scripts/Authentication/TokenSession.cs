using System;
using System.Collections.Specialized;
using System.Web;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;
using WebRequests.Extensions;
using WebRequests.Requesters;

namespace Authentication
{
    public class GetTokenRequest : IGetWebRequest<TokenSession>, IGetUriHandler
    {
        public NameValueCollection QueryParams => _authenticationData.AsQueryParams();

        private readonly AuthenticationData _authenticationData;

        public GetTokenRequest(string login, string password)
        {
            _authenticationData = new AuthenticationData(login, password);
        }

        public string GetUriDomain()
        {
            return WebRequestExt.DEFAULT_DOMAIN + "/login";
        }
    }

    public struct TokenSession
    {
        private const string QueryParam = "token";
        private const string PlayerPrefsKey = "token";

        public static event Action<TokenSession> TokenSavedToPlayerPrefs;

        [JsonProperty("token")] private string _token;

        public string Token => _token;

        public bool IsNone => string.IsNullOrEmpty(_token);

        public static bool IsNoneToken() => FromPlayerPrefs().IsNone;

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
            if (string.IsNullOrEmpty(Token))
            {
                throw new ArgumentNullException($"Attempt save empty token to player prefs");
            }

            PlayerPrefs.SetString(PlayerPrefsKey, Token);
            Debug.Log("Token saved to player prefs");

            TokenSavedToPlayerPrefs?.Invoke(this);
        }

        public static IObservable<TokenSession> ReceiveFromServer(string login, string password)
        {
            var getTokenRequest = new GetTokenRequest(login, password);

            var tokenSessionRequest = FromRequest(getTokenRequest);
            tokenSessionRequest.CatchIgnore().Subscribe(token => token.SaveToPlayerPrefs());
            
            return tokenSessionRequest;
        }

        private static IObservable<TokenSession> FromRequest(IGetWebRequest<TokenSession> webRequest) =>
            webRequest.SendWWWGetObject();

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

            return tokenSession;
        }

        public static void DeleteInPlayerPrefs() => PlayerPrefs.DeleteKey(PlayerPrefsKey);
    }
}