using UnityEngine;

public class SendCodeHandler : MonoBehaviour
{
    public async void SendCodeToChecking()
    {
        var task = 10;//CodeEditor.Task;
        var code = CodeEditor.Code;
        var language = CodeEditor.Language;

        var checkingResult = await TaskChecker.CheckCodeAsync(task, code, language);
        CodeConsole.WriteLine(checkingResult);
    }
}
