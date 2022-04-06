using UnityEngine;

public class SendCodeHandler : MonoBehaviour
{
    public async void SendCodeToChecking()
    {
        var task = CodeEditor.Task.Id;
        var code = CodeEditor.Code;
        var language = CodeEditor.Language;

        var checkingResult = await TaskChecker.CheckCodeAsync(task, code, language);
        CodeConsole.WriteLine(checkingResult);
    }
}
