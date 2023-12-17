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
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();
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
            _headers["Authorization"] = $"{type} {value}";
        }

        public void RemoveAuthorizationHeader()
        {
            RemoveHeader("Authorization");
        }

        public void AddHeader(string header, string value)
        {
            _headers[header] = value;
            DebugOnly.Log($"Add header '{header}' - '{value}'");
        }

        public void RemoveHeader(string header)
        {
            _headers.Remove(header);
            DebugOnly.Log($"Remove header '{header}'");
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
            foreach (var header in _headers)
            {
                request.SetRequestHeader(header.Key, header.Value);
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