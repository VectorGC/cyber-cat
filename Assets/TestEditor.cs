using System;
using Authentication;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;
using RestAPIWrapper;
using UniRx;
using UnityEngine;

public static class TaskChecker
{
    public static async UniTask<CodeCheckingResult> CheckCodeAsync(int taskId, string code)
    {
        var checkingResult = await CheckCodeStringAsync(taskId, code);
        return JsonConvert.DeserializeObject<CodeCheckingResult>(checkingResult);
    }

    private static async UniTask<string> CheckCodeStringAsync(int taskId, string code)
    {
        var token = TokenSession.FromPlayerPrefs();
        return await RestAPI.SendCodeToChecking(token, taskId, code);
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

public class CodeCheckingResult : ICodeConsoleMessage
{
    [CanBeNull]
    [JsonProperty("error")]
    public CodeCheckingError Error { get; set; }

    public bool IsOk => Error == null;

    public CodeConsoleMessage GetConsoleMessage()
    {
        if (IsOk)
        {
            return new CodeCheckingSuccess().GetConsoleMessage();
        }
        
        return Error?.GetConsoleMessage() ?? new CodeCheckingNone().GetConsoleMessage();
    }
}