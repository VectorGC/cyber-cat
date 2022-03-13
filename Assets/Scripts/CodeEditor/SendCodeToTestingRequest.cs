using System;
using System.Collections.Specialized;
using Authentication;
using TasksData.Requests;
using UniRx;
using UnityEngine;
using WebRequests;
using WebRequests.Extensions;

public class SendCodeToTestingRequest : IWebRequest, ISendRequestHandler<string>
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

    public IObservable<string> SendRequest(IProgress<float> progress = null)
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


        //new WWWForm().AddField();

        // =============================
        //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        var formData = new WWWForm();

        formData.AddField("task_id", _taskId.ToString());
        formData.AddField("source_text", _codeText);
        formData.AddField("verbose", "false");

        //formData.Add(new MultipartFormDataSection("source_text", _codeText));
        //formData.Add(new MultipartFormDataSection("verbose", "false"));

        var url = new GetTasksRequest(_token).GetUri();
        // "https://kee-reel.com/cyber-cat/?" + _token.Token

        var y = ObservableWWW.Post(url.ToString(), formData);
        return y;
        //UnityWebRequest www = UnityWebRequest.Post(url, formData);
        //return www;
        // =============================

        /*
         *rmData.AddField("task_id"); .Add(new MultipartFormDataSection(, _taskId.ToString()));
        formData.Add(new MultipartFormDataSection("source_text", _codeText));
        formData.Add(new MultipartFormDataSection("verbose", "false"));
         * 
         */

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