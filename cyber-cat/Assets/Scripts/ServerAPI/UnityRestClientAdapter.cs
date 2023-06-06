using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ApiGateway.Client;
using Newtonsoft.Json;
using Proyecto26;
using UnityEngine;

namespace ServerAPI
{
    public class UnityRestClientAdapter : IRestClient
    {
        public void Dispose()
        {
        }

        public async Task<string> PostAsync(string uri, Dictionary<string, string> form)
        {
            var request = new RequestHelper
            {
                Uri = uri,
                SimpleForm = form,
                EnableDebug = Debug.isDebugBuild
            };

            var response = await RestClient.Post(request).ToUniTask();
            return response.Text;
        }

        public async Task<string> GetStringAsync(string uri)
        {
            var request = new RequestHelper
            {
                Uri = uri,
                EnableDebug = Debug.isDebugBuild,
            };

            var response = await RestClient.Get(request).ToUniTask();
            return response.Text;
        }

        public async Task<T> GetFromJsonAsync<T>(string uri)
        {
            var request = new RequestHelper
            {
                Uri = uri,
                EnableDebug = Debug.isDebugBuild,
            };

            var responseObject = await RestClient.Get<T>(request).ToUniTask();
            return responseObject;
        }

        public void AddAuthorizationHeader(string type, string value)
        {
            var t = HttpRequestHeader.Authorization.ToString();
            RestClient.DefaultRequestHeaders[t] = $"{type} {value}";
        }

        public void RemoveAuthorizationHeader()
        {
            var t = HttpRequestHeader.Authorization.ToString();
            RestClient.DefaultRequestHeaders.Remove(t);
        }

        public async Task PostStringAsync(string uri, string value)
        {
            var json = JsonConvert.SerializeObject(value);
            var request = new RequestHelper
            {
                Uri = uri,
                BodyString = json,
                EnableDebug = Debug.isDebugBuild,
            };

            await RestClient.Post(request).ToUniTask();
        }

        public async Task PostAsJsonAsync<TValue>(string uri, TValue value)
        {
            var json = JsonConvert.SerializeObject(value);
            var request = new RequestHelper
            {
                Uri = uri,
                BodyString = json,
                EnableDebug = Debug.isDebugBuild,
            };

            await RestClient.Post(request).ToUniTask();
        }

        public async Task<TResponse> PostAsJsonAsync<TResponse>(string uri, string value)
        {
            var request = new RequestHelper
            {
                Uri = uri,
                BodyString = value,
                EnableDebug = Debug.isDebugBuild,
            };

            return await RestClient.Post<TResponse>(request).ToUniTask();
        }

        public async Task DeleteAsync(string uri)
        {
            var request = new RequestHelper
            {
                Uri = uri,
                EnableDebug = Debug.isDebugBuild,
            };

            await RestClient.Delete(request).ToUniTask();
        }
    }
}