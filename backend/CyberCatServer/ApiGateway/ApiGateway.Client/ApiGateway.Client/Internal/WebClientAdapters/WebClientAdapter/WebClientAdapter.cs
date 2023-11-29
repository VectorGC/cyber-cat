#if WEB_CLIENT
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using ApiGateway.Client.V3.Infrastructure;
using fastJSON;

namespace ApiGateway.Client.Internal.WebClientAdapters.WebClientAdapter
{
    internal class WebClientAdapter : IWebClientAdapter
    {
        private readonly System.Net.WebClient _client = new System.Net.WebClient();
        private readonly bool _debug;

        public WebClientAdapter(bool debug)
        {
            _debug = debug;
            _client.Encoding = System.Text.Encoding.UTF8;
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public void AddAuthorizationHeader(string type, string value)
        {
            _client.Headers[HttpRequestHeader.Authorization] = $"{type} {value}";
        }

        public void RemoveAuthorizationHeader()
        {
            _client.Headers.Remove(HttpRequestHeader.Authorization);
        }

        public async Task<string> GetStringAsync(string uri)
        {
            var response = string.Empty;
            if (!_debug)
            {
                return await _client.DownloadStringTaskAsync(uri);
            }

            try
            {
                response = await _client.DownloadStringTaskAsync(uri);
                return response;
            }
            catch (WebException e)
            {
                if (string.IsNullOrEmpty(response))
                    throw new Exception($"Response is empty. Request: {uri}", e);
                else
                    throw new Exception($"Response: {response}. Request: {uri}", e);
            }
        }

        public async Task<TResponse> GetFromJsonAsync<TResponse>(string uri)
        {
            var response = string.Empty;
            if (!_debug)
            {
                response = await GetStringAsync(uri);
                return DeserializeJson<TResponse>(response);
            }

            try
            {
                response = await GetStringAsync(uri);
                return DeserializeJson<TResponse>(response);
            }
            catch (WebException e)
            {
                if (string.IsNullOrEmpty(response))
                    throw new Exception($"Response is empty. Request: {uri}", e);
                else
                    throw new Exception($"Response: {response}. Request: {uri}", e);
            }
        }

        public async Task<TResponse> GetFromFastJsonPolymorphicAsync<TResponse>(string uri)
        {
            var response = string.Empty;
            if (!_debug)
            {
                response = await GetStringAsync(uri);
                return JSON.ToObject<TResponse>(response);
            }

            try
            {
                response = await GetStringAsync(uri);
                return JSON.ToObject<TResponse>(response);
            }
            catch (WebException e)
            {
                if (string.IsNullOrEmpty(response))
                    throw new Exception($"Response is empty. Request: {uri}", e);
                else
                    throw new Exception($"Response: {response}. Request: {uri}", e);
            }
        }

        public async Task<string> PostAsync(string uri, Dictionary<string, string> form)
        {
            var response = string.Empty;
            if (!_debug)
            {
                _client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                var responseBytes = await _client.UploadValuesTaskAsync(uri, FormToData(form));
                response = System.Text.Encoding.UTF8.GetString(responseBytes);

                return response;
            }

            try
            {
                _client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                var responseBytes = await _client.UploadValuesTaskAsync(uri, FormToData(form));
                response = System.Text.Encoding.UTF8.GetString(responseBytes);

                return response;
            }
            catch (WebException e)
            {
                if (string.IsNullOrEmpty(response))
                    throw new WebException($"Response is empty. Request: {uri}", e, e.Status, e.Response);
                else
                    throw new WebException($"Response: {response}. Request: {uri}", e, e.Status, e.Response);
            }
        }

        public async Task<string> PostAsync(string uri)
        {
            var response = string.Empty;
            if (!_debug)
            {
                return await _client.UploadStringTaskAsync(uri, "");
            }

            try
            {
                response = await _client.UploadStringTaskAsync(uri, "");
                return response;
            }
            catch (WebException e)
            {
                if (string.IsNullOrEmpty(response))
                    throw new Exception($"Response is empty. Request: {uri}", e);
                else
                    throw new Exception($"Response: {response}. Request: {uri}", e);
            }
        }


        public async Task<TResponse> PostAsJsonAsync<TResponse>(string uri, Dictionary<string, string> form)
        {
            var response = string.Empty;
            if (!_debug)
            {
                response = await PostAsync(uri, form);
                return DeserializeJson<TResponse>(response);
            }

            try
            {
                response = await PostAsync(uri, form);
                return DeserializeJson<TResponse>(response);
            }
            catch (WebException e)
            {
                if (string.IsNullOrEmpty(response))
                    throw new Exception($"Response is empty. Request: {uri}", e);
                else
                    throw new Exception($"Response: {response}. Request: {uri}", e);
            }
        }

        public async Task<TResponse> PostAsFastJsonPolymorphicAsync<TResponse>(string uri, Dictionary<string, string> form)
        {
            var response = string.Empty;
            if (!_debug)
            {
                response = await PostAsync(uri, form);
                return JSON.ToObject<TResponse>(response);
            }

            try
            {
                response = await PostAsync(uri, form);
                return JSON.ToObject<TResponse>(response);
            }
            catch (WebException e)
            {
                if (string.IsNullOrEmpty(response))
                    throw new Exception($"Response is empty. Request: {uri}", e);
                else
                    throw new Exception($"Response: {response}. Request: {uri}", e);
            }
        }

        private static string SerializeToJson<TValue>(TValue value)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(TValue));
                serializer.WriteObject(stream, value);
                stream.Position = 0;
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static T DeserializeJson<T>(string jsonString)
        {
            using (var stream = new MemoryStream(System.Text.Encoding.Unicode.GetBytes(jsonString)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T) serializer.ReadObject(stream);
            }
        }

        private static NameValueCollection FormToData(Dictionary<string, string> form)
        {
            var collection = new NameValueCollection();
            foreach (var kvp in form)
            {
                collection.Add(kvp.Key, kvp.Value);
            }

            return collection;
        }
    }
}
#endif