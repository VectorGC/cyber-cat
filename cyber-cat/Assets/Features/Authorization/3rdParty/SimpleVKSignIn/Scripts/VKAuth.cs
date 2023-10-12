using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.SimpleVKSignIn.Scripts
{
    /// <summary>
    /// https://dev.vk.com/ru/api/access-token/getting-started
    /// </summary>
    public class VKAuth : MonoBehaviour
    {
        public static event Action<TokenResponse> OnTokenResponse = response => { };

        public static TokenResponse TokenResponse { get; private set; }

        public static SavedAuth SavedAuth
        {
            get { if (PlayerPrefs.HasKey(PlayerPrefsKey)) try { return JsonUtility.FromJson<SavedAuth>(PlayerPrefs.GetString(PlayerPrefsKey)); } catch { return null; } return null; }
            private set => PlayerPrefs.SetString(PlayerPrefsKey, JsonUtility.ToJson(value));
        }

        private const string PlayerPrefsKey = "VK.SavedAuth";

        private const string AuthorizationEndpoint = "https://oauth.vk.com/authorize";
        private const string TokenEndpoint = "https://oauth.vk.com/access_token";
        private const string UserInfoEndpoint = "https://api.vk.com/method/users.get";
        private const string AccessScope = "4194304"; // email https://dev.vk.com/ru/reference/access-rights

        private const string AuthorizationMiddleware = "https://hippogames.dev/api/oauth";
        
        private static VKAuth _instance;
        private static string _redirectUri;
        private static string _state;
        private static Action<bool, string, UserInfo> _callback;

        public static void SignIn(Action<bool, string, UserInfo> callback)
        {
            _callback = callback;

            #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL

            _redirectUri = "";

            #elif UNITY_WSA || UNITY_ANDROID || UNITY_IOS

            _redirectUri = $"{Settings.Instance.CustomUriScheme}:/oauth2callback/vk";

            #endif

            if (SavedAuth == null)
            {
                Auth();
            }
            else
            {
                UseSavedToken();
            }
        }

        public static void SignOut()
        {
            TokenResponse = null;
            PlayerPrefs.DeleteKey(PlayerPrefsKey);
        }

        #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL

        private static void GetAuthCode()
        {
            if (_instance == null)
            {
                _instance = new GameObject(nameof(VKAuth)).AddComponent<VKAuth>();
                DontDestroyOnLoad(_instance);
            }

            _instance.StopAllCoroutines();
            _instance.StartCoroutine(GetCodeLoopback());
        }

        private static IEnumerator GetCodeLoopback()
        {
            yield return new WaitForSeconds(1);

            for (var i = 0; i < 10; i++)
            {
                using var request = UnityWebRequest.Post(AuthorizationMiddleware + "/getcode", new Dictionary<string, string> { { "state", _state } });

                Log($"Obtaining auth code: {request.url}");

                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    var code = request.downloadHandler.text;

                    Log($"code={code}");

                    PerformCodeExchange(code);

                    yield break;
                }
                
                if (request.responseCode == 704)
                {
                    yield return new WaitForSeconds(5);
                }
                else
                {
                    _callback(false, $"{request.error}: {request.downloadHandler.text}", null);
                }
            }
        }

        #elif UNITY_WSA || UNITY_ANDROID || UNITY_IOS

        static VKAuth()
        {
            Application.deepLinkActivated += OnDeepLinkActivated;
        }

        private static void OnDeepLinkActivated(string deepLink)
        {
            Log($"Deep link activated: {deepLink}");

            if (!deepLink.Contains("/oauth2callback/vk")) return;

            var parameters = Utils.ParseQueryString(deepLink);
            var error = parameters.Get("error");

            if (error != null)
            {
                _callback?.Invoke(false, error, null);
                return;
            }

            var state = parameters.Get("state");
            var code = parameters.Get("code");
            
            if (state == null || code == null) return;

            if (state == _state)
            {
                PerformCodeExchange(code);
            }
            else
            {
                Log("Unexpected response.");
            }
        }

        #endif

        private static void Auth()
        {
            _state = Guid.NewGuid().ToString();
            
            var request = UnityWebRequest.Post(AuthorizationMiddleware + "/init", new Dictionary<string, string> { { "state", _state }, { "redirectUri", _redirectUri }, { "clientName", Application.productName } });

            Log($"Initializing auth middleware: {request.url}");

            request.SendWebRequest().completed += obj =>
            {
                if (request.result == UnityWebRequest.Result.Success)
                {
                    var authorizationRequest = $"{AuthorizationEndpoint}?client_id={Settings.Instance.ClientId}&redirect_uri={Uri.EscapeDataString(AuthorizationMiddleware + "/redirect")}&display=page&scope={AccessScope}&response_type=code&state={_state}";

                    Log($"Authorization: {authorizationRequest}");
                    Application.OpenURL(authorizationRequest);

                    #if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL

                    GetAuthCode();

                    #endif
                }
                else
                {
                    _callback(false, $"{request.error}: {request.downloadHandler.text}", null);
                }

                request.Dispose();
            };
        }

        private static void UseSavedToken()
        {
            if (SavedAuth == null) throw new Exception("Initial authorization is required.");

            if (SavedAuth.ClientId == Settings.Instance.ClientId && SavedAuth.ExpirationTime > DateTime.UtcNow)
            {
                Debug.Log("Using saved access token...");
                RequestUserInfo(SavedAuth.TokenResponse, (success, error, userInfo) =>
                {
                    if (success)
                    {
                        _callback(true, null, userInfo);
                    }
                    else
                    {
                        SignOut();
                        SignIn(_callback);
                    }
                });
            }
            else
            {
                SignOut();
                SignIn(_callback);
            }
        }

        private static void PerformCodeExchange(string code)
        {
            var request = UnityWebRequest.Get($"{TokenEndpoint}?client_id={Settings.Instance.ClientId}&client_secret={Settings.Instance.ClientSecret}&redirect_uri={Uri.EscapeDataString(AuthorizationMiddleware + "/redirect")}&code={code}");

            #if UNITY_WEBGL // CORS workaround.

            request = UnityWebRequest.Post($"{AuthorizationMiddleware}/download", new Dictionary<string, string> { { "url", request.url } });
            
            #endif

            Log($"Exchanging code for access token: {request.url}");

            request.SendWebRequest().completed += _ =>
            {
                if (request.error == null)
                {
                    Log($"TokenExchangeResponse={request.downloadHandler.text}");

                    TokenResponse = JsonUtility.FromJson<TokenResponse>(request.downloadHandler.text);
                    SavedAuth = new SavedAuth(Settings.Instance.ClientId, TokenResponse);
                    OnTokenResponse(TokenResponse);
                    RequestUserInfo(TokenResponse, _callback);
                }
                else
                {
                    _callback(false, $"{request.error}: {request.downloadHandler.text}", null);
                }
            };
        }

        /// <summary>
        /// You can move this function to your backend for more security.
        /// Fields specification: https://dev.vk.com/ru/method/users.get
        /// </summary>
        public static void RequestUserInfo(TokenResponse tokenResponse, Action<bool, string, UserInfo> callback)
        {
            var request = UnityWebRequest.Get($"{UserInfoEndpoint}?access_token={tokenResponse.access_token}&user_ids={tokenResponse.user_id}&fields=has_photo,photo_200&v=5.131");

            #if UNITY_WEBGL // CORS workaround.

            request = UnityWebRequest.Post($"{AuthorizationMiddleware}/download", new Dictionary<string, string> { { "url", request.url } });
            
            #endif

            Log($"Requesting user info: {request.url}");

            request.SendWebRequest().completed += _ =>
            {
                if (request.error == null)
                {
                    Log($"UserInfo={request.downloadHandler.text}");

                    var response = JObject.Parse(request.downloadHandler.text);

                    if (response.TryGetValue("response", out var value) && value[0] != null)
                    {
                        Log($"UserInfo={value[0]}");

                        var savedAuth = SavedAuth;
                        var userInfo = JsonUtility.FromJson<UserInfo>(value[0].ToString());

                        savedAuth.UserInfo = userInfo;
                        SavedAuth = savedAuth;
                        callback(true, null, userInfo);
                    }
                    else if (response.TryGetValue("error", out var error))
                    {
                        callback(false, (string)error["error_msg"], null);
                    }
                    else
                    {
                        callback(false, $"Unexpected response: {request.downloadHandler.text}", null);
                    }
                }
                else
                {
                    callback(false, $"{request.error}: {request.downloadHandler.text}", null);
                }
            };
        }

        private static void Log(string message)
        {
            Debug.Log(message); // TODO: Remove in Release.
        }
    }
}