using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace ApiGateway.Client
{
    public class WebRestClient : IRestClient
    {
        private readonly WebClient _client = new WebClient();

        public WebRestClient()
        {
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

        public async Task<string> PostAsync(string uri, Dictionary<string, string> form)
        {
            _client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

            var formData = new System.Collections.Specialized.NameValueCollection();
            foreach (var kvp in form)
            {
                formData.Add(kvp.Key, kvp.Value);
            }

            var responseBytes = await _client.UploadValuesTaskAsync(uri, formData);
            var response = System.Text.Encoding.UTF8.GetString(responseBytes);

            return response;
        }

        public async Task<string> GetStringAsync(string uri)
        {
            return await _client.DownloadStringTaskAsync(uri);
        }

        public async Task<T> GetFromJsonAsync<T>(string uri)
        {
            var response = await GetStringAsync(uri);
            var obj = DeserializeJson<T>(response);

            return obj;
        }

        public async Task PostStringAsync(string uri, string value)
        {
            _client.Headers[HttpRequestHeader.ContentType] = "application/json";

            var json = SerializeToJson(value);
            await _client.UploadStringTaskAsync(uri, WebRequestMethods.Http.Post, json);
        }

        public async Task PostAsJsonAsync<TValue>(string uri, TValue value)
        {
            _client.Headers[HttpRequestHeader.ContentType] = "application/json";

            var json = SerializeToJson(value);
            await _client.UploadStringTaskAsync(uri, WebRequestMethods.Http.Post, json);
        }

        public async Task<TResponse> PostAsJsonAsync<TResponse>(string uri, string value)
        {
            _client.Headers[HttpRequestHeader.ContentType] = "application/json";

            var json = SerializeToJson(value);
            var response = await _client.UploadStringTaskAsync(uri, WebRequestMethods.Http.Post, json);

            var obj = DeserializeJson<TResponse>(response);
            return obj;
        }

        public async Task DeleteAsync(string uri)
        {
            await _client.UploadStringTaskAsync(uri, "DELETE", string.Empty);
        }

        public async Task DeleteAsync(string uri, string value)
        {
            _client.Headers[HttpRequestHeader.ContentType] = "application/json";

            var json = SerializeToJson(value);
            await _client.UploadStringTaskAsync(uri, "DELETE", json);
        }

        public static string SerializeToJson<TValue>(TValue value)
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

        public static T DeserializeJson<T>(string jsonString)
        {
            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonString)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T) serializer.ReadObject(stream);
            }
        }
    }
}