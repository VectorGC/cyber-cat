using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ApiGateway.Client;
using Proyecto26;
using UnityEngine;

namespace ServerAPI
{
    public class UnityRestClientProxy : IRestClient
    {
        public void Dispose()
        {
        }

        public async Task<string> PostAsync(string uri, Dictionary<string, string> form, IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = uri,
                SimpleForm = form,
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = Debug.isDebugBuild
            };

            var response = await RestClient.Post(request).ToUniTask();
            return response.Text;
        }

        public async Task<string> GetStringAsync(string uri, IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = uri,
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = Debug.isDebugBuild,
            };

            var response = await RestClient.Get(request).ToUniTask();
            return response.Text;
        }

        public async Task<T> GetFromJsonAsync<T>(string uri, IProgress<float> progress = null)
        {
            var request = new RequestHelper
            {
                Uri = uri,
                ProgressCallback = value => progress?.Report(value),
                EnableDebug = Debug.isDebugBuild,
            };

            var responseObject = await RestClient.Get<T>(request).ToUniTask();
            return responseObject;
        }

        public void AddAuthorizationHeader(string type, string value)
        {
            RestClient.DefaultRequestHeaders["Authorization"] = $"{type} {value}";
        }

        public void RemoveAuthorizationHeader()
        {
            RestClient.DefaultRequestHeaders.Remove("Authorization");
        }
    }
}