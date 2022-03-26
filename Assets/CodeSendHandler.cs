using System;
using Authentication;
using Newtonsoft.Json;
using UniRx;
using UnityEngine;
using WebRequests.Requesters;

public interface ICodeTestingResult
{
    string Message { get; }
}

public class CodeTestingResult : ICodeTestingResult
{
    public string Message { get; set; }
}

public class ErrorCodeTestingResult : ICodeTestingResult
{
    [JsonProperty("error")] 
    public string Message { get; set; }
}

public class CodeSendHandler : MonoBehaviour
{
    public void SendCodeToTesting()
    {
        var taskId = CodeEditorController.GetOpenedTaskId();
        var code = CodeEditorController.GetCode();
        var request = new SendCodeToTestingRequest(TokenSession.FromPlayerPrefs(), taskId, code);

        request
            .SendRequest()
            .ResponseHandle()
            .Do(OnResponse)
            .DoOnError(OnError)
            .Subscribe();
    }

    private static void OnResponse(string codeTestingResultString)
    {
        var codeTestingResult = JsonConvert.DeserializeObject<CodeTestingResult>(codeTestingResultString);
        MessageBroker.Default.Publish(codeTestingResult);
    }

    private static void OnError(Exception exception)
    {
        MessageBroker.Default.Publish<ICodeTestingResult>(new ErrorCodeTestingResult
        {
            Message = exception.Message
        });
    }
}