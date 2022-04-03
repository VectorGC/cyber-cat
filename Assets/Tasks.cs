using System;
using Authentication;
using Cysharp.Threading.Tasks;
using RestAPIWrapper;

public class Tasks
{
    public static async UniTask<ITaskTicket> GetTask(string taskId, IProgress<float> progress = null)
    {
        var token = TokenSession.FromPlayerPrefs();
        return await RestAPI.GetTask(token, taskId, progress);
    }
}