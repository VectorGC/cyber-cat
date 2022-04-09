using TaskCodeCheckModels;
using UnityEngine;

public class SendCodeHandler : MonoBehaviour
{
    public async void SendCodeToChecking()
    {
        var task = CodeEditor.Task;
        var code = CodeEditor.Code;
        var language = CodeEditor.Language;

        var checkingResult = await task.CheckCodeAsync(code, language);
        CodeConsole.WriteLine(checkingResult);
    }
}