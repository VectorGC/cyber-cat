using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Authentication;
using TasksData.Requests;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using WebRequests;
using WebRequests.Extensions;

public class SendCodeToTestingRequest : IWebRequest, IUnityWebRequestHandler
{
    private readonly TokenSession _token;
    private readonly int _taskId;
    private readonly string _codeText;

    public NameValueCollection QueryParams => _token.ToQueryParam();

    public SendCodeToTestingRequest(TokenSession token, int taskId, string codeText)
    {
        _token = token;
        _taskId = taskId;
        _codeText = codeText;
    }

    public UnityWebRequest GetWebRequestHandler(string uri)
    {
        //var formData = new List<IMultipartFormSection>();

        // var formSection = new MultipartFormDataSection("token", _token);
        // formData.Add(formSection);
        //
        // var sectionTaskId = new MultipartFormDataSection("tasks", _taskId.ToString());
        // formData.Add(sectionTaskId);
        //
        // var data = System.Text.Encoding.UTF8.GetBytes(_codeText);
        // formData.Add(new MultipartFormFileSection("source_93", data, "test.c", "application/octet-stream"));
        //
        // return UnityWebRequest.Post("https://kee-reel.com/solution", formData);


        // =============================
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("task_id", _taskId.ToString()));
        formData.Add(new MultipartFormDataSection("source_text", _codeText));
        formData.Add(new MultipartFormDataSection("verbose", "false"));

        var url = new GetTasksRequest(_token).GetUri();
        // "https://kee-reel.com/cyber-cat/?" + _token.Token

        UnityWebRequest www = UnityWebRequest.Post(url, formData);
        return www;
        // =============================


        /*var t = www.SendWebRequest();
        while (!t.isDone)
        {
            Debug.Log($"uploadProgress {t.webRequest.uploadProgress}");
            Debug.Log($"progress {t.progress}");
            Debug.Log($"downloadProgress {t.webRequest.downloadProgress}");
            yield return null;
        }

        Debug.Log($"uploadProgress {t.webRequest.uploadProgress}");
        Debug.Log($"progress {t.progress}");
        Debug.Log($"downloadProgress {t.webRequest.downloadProgress}");
        
        //yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            PostReceived.Invoke(www.downloadHandler.text);
        */
    }
}

public class CodeEditor : MonoBehaviour
{
    [SerializeField] private TMP_InputField codeInputField;

    private int _openedTaskId;

    public static void OpenSolutionForTask(string taskId)
    {
        Debug.Log($"Opening code editor for task '{taskId}'");

        new GetTaskRequest(taskId)
            .SendRequest()
            .Subscribe(SetupCodeEditorForTask);
    }

    public static void SetupCodeEditorForTask(ITaskTicket task)
    {
        var codeEditorStartup = FindObjectOfType<CodeEditorStartup>();
        codeEditorStartup.SetupCodeEditorForTask(task);
    }

    public void SetupCodeEditor(ITaskTicket task)
    {
        _openedTaskId = task.Id;
    }

    public void SendCodeToTesting()
    {
        var codeText = GetCode();
        SendCodeToTesting(_openedTaskId, codeText);
    }

    private void SendCodeToTesting(int taskId)
    {
        var codeText = GetCode();
        SendCodeToTesting(taskId, codeText);
    }

    private static void SendCodeToTesting(int taskId, string codeText)
    {
        var token = TokenSession.FromPlayerPrefs();
        // new SendCodeToTestingRequest(token, taskId, codeText)
        //     .OnResponse(str => { Debug.Log(str); })
        //     .SendRequest();
    }

    private string GetCode()
    {
        return codeInputField.text;
    }
}