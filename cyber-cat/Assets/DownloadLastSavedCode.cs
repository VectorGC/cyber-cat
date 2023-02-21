using Authentication;
using GameCodeEditor.Scripts;
using RestAPIWrapper;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class DownloadLastSavedCode : MonoBehaviour
{
    [SerializeField] private Button downloadLastSavedCodeButton;

    private void OnEnable()
    {
        downloadLastSavedCodeButton.onClick.AddListener(OnClickHandle);
    }

    private static async void OnClickHandle()
    {
        var taskId = CodeEditor.Task.Id;
        var token = TokenSession.FromPlayerPrefs();

        var lastSavedCode = await new GetLastSaveCodeRequest().GetLastSavedCode(token, taskId);

        var message = new SetTaskInEditor(CodeEditor.Task, lastSavedCode);
        MessageBroker.Default.Publish(message);
    }

    private void OnDisable()
    {
        downloadLastSavedCodeButton.onClick.RemoveListener(OnClickHandle);
    }
}