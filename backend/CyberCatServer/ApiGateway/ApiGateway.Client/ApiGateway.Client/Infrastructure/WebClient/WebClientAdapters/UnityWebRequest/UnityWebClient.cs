#if UNITY_WEBGL
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApiGateway.Client.V3.Infrastructure.WebClientAdapters.UnityWebRequest;
using fastJSON;
using UnityEngine;

namespace ApiGateway.Client.Infrastructure.WebClient.WebClientAdapters.UnityWebRequest
{
    internal class UnityWebClient : IInternalWebClientAdapter
    {
        private string _authorizationHeaderValue;
        private bool _debug;

        public UnityWebClient()
        {
            SetDebugMode();
        }

        public void Dispose()
        {
        }

        [Conditional("DEBUG")]
        private void SetDebugMode()
        {
            _debug = true;
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

        public async Task<TResponse> GetFromJsonAsync<TResponse>(string uri)
        {
            DebugOnly.Log($"Send GET to '{uri}'");
            using (var request = UnityEngine.Networking.UnityWebRequest.Get(uri))
            {
                SetAuthorizationIfNeeded(request);
                await request.SendWebRequest().WaitAsync();
                request.EnsureSuccessStatusCode();
                var response = request.downloadHandler.text;

                DebugOnly.Log($"Response from {uri} is '{response}'");
                DebugOnly.Log($"Try deserialize response to object of type '{typeof(TResponse)}'");
                var obj = JsonUtility.FromJson<TResponse>(response);
                DebugOnly.Log($"Success deserialize object '{obj}'");

                return obj;
            }
        }

        public async Task<TResponse> GetFromFastJsonPolymorphicAsync<TResponse>(string uri)
        {
            DebugOnly.Log($"Try deserialize response to object of type '{typeof(TResponse)}'");
            var response = await GetStringAsync(uri);
            TResponse obj;
            if (_debug)
            {
                try
                {
                    obj = JSON.ToObject<TResponse>(response);
                }
                catch (Exception e)
                {
                    throw new Exception($"Uri: {uri}, Json: {response}", e);
                }
            }
            else
            {
                obj = JSON.ToObject<TResponse>(response);
            }

            DebugOnly.Log($"Success deserialize object '{obj}'");
            return obj;
        }

        public async Task<string> PostAsync(string uri, Dictionary<string, string> form)
        {
            DebugOnly.Log($"Send POST to '{uri}'. Form: {PrintForm(form)}");
            using (var request = UnityEngine.Networking.UnityWebRequest.Post(uri, form))
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
            using (var request = UnityEngine.Networking.UnityWebRequest.Post(uri, string.Empty))
            {
                SetAuthorizationIfNeeded(request);
                await request.SendWebRequest().WaitAsync();
                request.EnsureSuccessStatusCode();
                var response = request.downloadHandler.text;

                DebugOnly.Log($"Response from {uri} is '{response}'");
                return response;
            }
        }

        public async Task<TResponse> PostAsJsonAsync<TResponse>(string uri, Dictionary<string, string> form)
        {
            var json = await PostAsync(uri, form);
            DebugOnly.Log($"Try deserialize response to object of type '{typeof(TResponse)}'");
            var obj = JsonUtility.FromJson<TResponse>(json);
            DebugOnly.Log($"Success deserialize object '{obj}'");

            return obj;
        }

        public async Task<TResponse> PostAsFastJsonPolymorphicAsync<TResponse>(string uri, Dictionary<string, string> form)
        {
            var response = await PostAsync(uri, form);
            DebugOnly.Log($"Try deserialize response to object of type '{typeof(TResponse)}'");
            var obj = JSON.ToObject<TResponse>(response);
            DebugOnly.Log($"Success deserialize object '{obj}'");

            return obj;
        }

        private void SetAuthorizationIfNeeded(UnityEngine.Networking.UnityWebRequest request)
        {
            if (!string.IsNullOrEmpty(_authorizationHeaderValue))
            {
                request.SetRequestHeader("Authorization", _authorizationHeaderValue);
            }
        }

        private string PrintForm(Dictionary<string, string> form)
        {
            var lines = form.Select(kvp => $"{kvp.Key}: {kvp.Value}");
            return string.Join(", ", lines);
        }
    }
}
#endif