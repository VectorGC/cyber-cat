using System;
using Cysharp.Threading.Tasks;
using Proyecto26;
using UnityEngine;

public class RestAPI_V1 : IRestAPI
{
    private static string Url = Debug.isDebugBuild
        ? "https://server.cyber-cat.pro"
        : "http://localhost:5000";

    private string GetToken()
    {
        return PlayerPrefs.GetString("token");
    }

    public async UniTask<string> GetTasks(IProgress<float> progress = null)
    {
        var request = new RequestHelper
        {
            Uri = Url + "/tasks/flat",
            Params =
            {
                ["token"] = GetToken()
            },
            ProgressCallback = value => progress?.Report(value),
            EnableDebug = Debug.isDebugBuild
        };

        var response = await RestClient.Get(request).ToUniTask();
        return response.Text;
    }
}