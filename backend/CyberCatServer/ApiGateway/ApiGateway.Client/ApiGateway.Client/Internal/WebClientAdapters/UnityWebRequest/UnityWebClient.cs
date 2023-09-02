#if UNITY_WEBGL
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEngine;

namespace ApiGateway.Client.Internal.WebClientAdapters.UnityWebRequest
{
    internal class UnityWebClient : IWebClient
    {
        private string _authorizationHeaderValue;

        public void Dispose()
        {
        }   

        public void AddAuthorizationHeader(string type, string value)
        {
            _authorizationHeaderValue = $"{type} {value}";
            DebugOnly.Log($"Add authorization header '{_authorizationHeaderValue}'");
        }

        public void RemoveAuthorizationHeader()
        {
            _authorizationHeaderValue = string.Empty;
            DebugOnly.Log("Remove authorization header");
        }

        public async Task<string> GetStringAsync(string uri)
        {
            DebugOnly.Log($"Send GET to '{uri}'");
            using (var request = UnityEngine.Networking.UnityWebRequest.Get(uri))
            {
                SetAuthorizationIfNeeded(request);
                await request.SendWebRequest().WaitAsync();
                request.EnsureSuccessStatusCode();
                var response = request.downloadHandler.text;

                DebugOnly.Log($"Response from {uri} is '{response}'");
                return response;
            }
        }

        public async Task<T> GetFromJsonAsync<T>(string uri)
        {
            DebugOnly.Log($"Send GET to '{uri}'");
            using (var request = UnityEngine.Networking.UnityWebRequest.Get(uri))
            {
                SetAuthorizationIfNeeded(request);
                await request.SendWebRequest().WaitAsync();
                request.EnsureSuccessStatusCode();
                var response = request.downloadHandler.text;

                DebugOnly.Log($"Response from {uri} is '{response}'");
                var json = request.downloadHandler.text;
                DebugOnly.Log($"Try deserialize response to object of type '{typeof(T)}'");
                var obj = JsonUtility.FromJson<T>(json);
                DebugOnly.Log($"Success deserialize object '{obj}'");

                return obj;
            }
        }

        public async Task<string> PostAsync(string uri, NameValueCollection form)
        {
            DebugOnly.Log($"Send POST to '{uri}'");
            using (var request = UnityEngine.Networking.UnityWebRequest.Post(uri, form.ToString()))
            {
                SetAuthorizationIfNeeded(request);
                await request.SendWebRequest().WaitAsync();
                request.EnsureSuccessStatusCode();
                var response = request.downloadHandler.text;

                DebugOnly.Log($"Response from {uri} is '{response}'");
                return response;
            }
        }

        public async Task<string> PostAsync(string uri)
        {
            DebugOnly.Log($"Send POST to '{uri}'");
            using (var request = UnityEngine.Networking.UnityWebRequest.Post(uri, ""))
            {
                SetAuthorizationIfNeeded(request);
                await request.SendWebRequest().WaitAsync();
                request.EnsureSuccessStatusCode();
                var response = request.downloadHandler.text;

                DebugOnly.Log($"Response from {uri} is '{response}'");
                return response;
            }
        }

        public async Task<TResponse> PostAsJsonAsync<TResponse>(string uri, NameValueCollection form)
        {
            var json = await PostAsync(uri, form);
            DebugOnly.Log($"Try deserialize response to object of type '{typeof(TResponse)}'");
            var obj = JsonUtility.FromJson<TResponse>(json);
            DebugOnly.Log($"Success deserialize object '{obj}'");

            return obj;
        }

        public async Task<string> PutAsync(string uri, NameValueCollection form)
        {
            DebugOnly.Log($"Send PUT to '{uri}'");
            using (var request = UnityEngine.Networking.UnityWebRequest.Put(uri, form.ToString()))
            {
                SetAuthorizationIfNeeded(request);
                await request.SendWebRequest().WaitAsync();
                request.EnsureSuccessStatusCode();
                var response = request.downloadHandler.text;

                DebugOnly.Log($"Response from {uri} is '{response}'");
                return response;
            }
        }

        public async Task DeleteAsync(string uri)
        {
            DebugOnly.Log($"Send DELETE to '{uri}'");
            using (var request = UnityEngine.Networking.UnityWebRequest.Delete(uri))
            {
                SetAuthorizationIfNeeded(request);
                await request.SendWebRequest().WaitAsync();
                request.EnsureSuccessStatusCode();
                DebugOnly.Log($"Success response from {uri}");
            }
        }

        private void SetAuthorizationIfNeeded(UnityEngine.Networking.UnityWebRequest request)
        {
            if (!string.IsNullOrEmpty(_authorizationHeaderValue))
            {
                request.SetRequestHeader("Authorization", _authorizationHeaderValue);
            }
        }
    }
}
#endif