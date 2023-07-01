using System;
using Cysharp.Threading.Tasks;
using Proyecto26;
using UnityEngine;

public static class RestAPI_v1
{
    private static string Url = Debug.isDebugBuild
        ? "https://server.cyber-cat.pro"
        : "http://localhost:5000";

    private static string GetToken()
    {
        return PlayerPrefs.GetString("token");
    }

    public static async UniTask<string> GetTasks(IProgress<float> progress = null)
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