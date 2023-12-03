using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using fastJSON;

#if WEB_CLIENT

namespace ApiGateway.Client.V3.Infrastructure.WebClientAdapters.WebClientAdapter
{
    internal class InternalWebClientAdapter : IInternalWebClientAdapter
    {
        private readonly System.Net.WebClient _client = new System.Net.WebClient();
        private bool _debug;

        public InternalWebClientAdapter()
        {
            _client.Encoding = System.Text.Encoding.UTF8;
            SetDebugMode();
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        [Conditional("DEBUG")]
        private void SetDebugMode()
        {
            _debug = true;
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
            return await _client.DownloadStringTaskAsync(uri);
        }

        public async Task<TResponse> GetFromJsonAsync<TResponse>(string uri)
        {
            var response = await GetStringAsync(uri);
            return DeserializeJson<TResponse>(response);
        }

        public async Task<TResponse> GetFromFastJsonPolymorphicAsync<TResponse>(string uri)
        {
            var response = await GetStringAsync(uri);
            return DeserializeFastJson<TResponse>(uri, response);
        }

        public async Task<string> PostAsync(string uri, Dictionary<string, string> form)
        {
            var responseBytes = await _client.UploadValuesTaskAsync(uri, FormToData(form));
            var response = System.Text.Encoding.UTF8.GetString(responseBytes);

            return response;
        }

        public async Task<string> PostAsync(string uri)
        {
            return await _client.UploadStringTaskAsync(uri, "");
        }

        public async Task<TResponse> PostAsJsonAsync<TResponse>(string uri, Dictionary<string, string> form)
        {
            var response = await PostAsync(uri, form);
            return DeserializeJson<TResponse>(response);
        }

        public async Task<TResponse> PostAsFastJsonPolymorphicAsync<TResponse>(string uri, Dictionary<string, string> form)
        {
            var response = await PostAsync(uri, form);
            return DeserializeFastJson<TResponse>(uri, response);
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

        private T DeserializeFastJson<T>(string uri, string jsonString)
        {
            if (!_debug)
                return JSON.ToObject<T>(jsonString);

            try
            {
                return JSON.ToObject<T>(jsonString);
            }
            catch (Exception e)
            {
                throw new Exception($"Uri: {uri}, Json: {jsonString}", e);
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