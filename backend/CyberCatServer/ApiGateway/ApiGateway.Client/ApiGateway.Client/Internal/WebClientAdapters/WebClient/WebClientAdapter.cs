#if WEB_CLIENT
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace ApiGateway.Client.Internal.WebClientAdapters.WebClient
{
    internal class WebClientAdapter : IWebClient
    {
        private readonly System.Net.WebClient _client = new System.Net.WebClient();

        public WebClientAdapter()
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

        public async Task<string> PostAsync(string uri, Dictionary<string, string> form)
        {
            _client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

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
            var obj = DeserializeJson<TResponse>(response);

            return obj;
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
            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonString)))
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