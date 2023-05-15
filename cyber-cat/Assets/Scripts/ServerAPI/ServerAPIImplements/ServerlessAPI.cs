using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Models;
using Newtonsoft.Json;
using Proyecto26;
using ServerAPI.InternalDto;
using UnityEngine;

namespace ServerAPI.ServerAPIImplements
{
    internal class TaskFromFileDto : ITask
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
    }

    public class ServerlessAPI : IServerAPI
    {
        public UniTask<ITokenSession> Authenticate(string login, string password, IProgress<float> progress = null)
        {
            var token = new TokenSessionDto
            {
                AccessToken = "serverless"
            };

            return UniTask.FromResult<ITokenSession>(token);
        }

        public async UniTask<ITask> GetTask(string taskId, IProgress<float> progress = null)
        {
            var tasksJson = await ReadFileContentAsync("tasks.json");
            var tasks = JsonConvert.DeserializeObject<Dictionary<string, TaskFromFileDto>>(tasksJson);

            if (!tasks.TryGetValue(taskId, out var task))
            {
                throw new KeyNotFoundException($"Task '{taskId}'");
            }

            return task;
        }

        public UniTask<IPlayer> AuthorizePlayer(ITokenSession token, IProgress<float> progress = null)
        {
            var player = new PlayerDto
            {
                Name = "Player",
                Token = token
            };

            return UniTask.FromResult<IPlayer>(player);
        }

        private async UniTask<string> ReadFileContentAsync(string fileName)
        {
            var filePath = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
            if (filePath.Contains("://"))
            {
                var response = await RestClient.Get(filePath).ToUniTask();
                return response.Text;
            }

            return System.IO.File.ReadAllText(filePath);
        }
    }
}