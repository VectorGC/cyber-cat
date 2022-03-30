using System;
using Authentication;
using Cysharp.Threading.Tasks;
using RestAPIWrapper;
using UniRx;
using UnityEngine;

public class TaskChecker
{
    public static async UniTask<string> CheckCodeAsync(int taskId, string code)
    {
        var token = TokenSession.FromPlayerPrefs();
        return await RestAPI.SendCodeToTesting(token, taskId, code);
    }
}

public class Tasks
{
    public static async UniTask<ITaskTicket> GetTask(string taskId)
    {
        var token = TokenSession.FromPlayerPrefs();
        return await RestAPI.GetTask(token, taskId);
    }
}

public class TestEditor : MonoBehaviour
{
    async void Start()
    {
        var taskId = "10";

        var task = await RestAPI.GetTask(TokenSession.FromPlayerPrefs(), taskId);

        CodeEditor.Task = task;
        var message = new SetTaskDescriptionMessage(task);
        MessageBroker.Default.Publish(message);
    }

    public async void SendCodeToTesting()
    {
        var taskId = CodeEditor.Task.Id;
        var code = CodeEditor.Code;

        var checkingResult = await TaskChecker.CheckCodeAsync(taskId, code);
        CodeConsole.WriteLine(checkingResult);
    }
}