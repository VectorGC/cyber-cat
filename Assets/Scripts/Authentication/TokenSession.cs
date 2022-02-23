using System;
using System.Collections.Specialized;
using System.Web;
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

        public static void ReceiveFromServer(Action<TokenSession> onNext)
        {
            var getTokenRequest = new GetTokenRequest("test123@gmail.com", "123456qwer");
            FromRequest(getTokenRequest, onNext);
        }

        public static void FromRequest(IGetWebRequest<TokenSession> webRequest, Action<TokenSession> onNext) =>
            webRequest.SendWWWGetObject(onNext);

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