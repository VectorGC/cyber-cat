using Authentication;
using UniRx;
using UnityEngine;

public class CodeSendHandler : MonoBehaviour
{
    public void SendCodeToTesting()
    {
        var taskId = CodeEditorController.GetOpenedTaskId();
        var code = CodeEditorController.GetCode();
        var t = new SendCodeToTestingRequest(TokenSession.FromPlayerPrefs(), taskId, code);

        t.SendRequest().Subscribe(
            x => Debug.LogError(x));
    }
}