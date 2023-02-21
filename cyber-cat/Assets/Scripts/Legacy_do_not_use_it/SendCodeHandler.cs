using System.Collections.Generic;
using Authentication;
using CodeEditorModels.ProgLanguages;
using RequestAPI.Proxy;
using TaskChecker;
using UnityEngine;

public class SendCodeHandler : MonoBehaviour
{
    private static readonly Dictionary<ProgLanguage, string> ProgLanguages = new Dictionary<ProgLanguage, string>()
    {
        [ProgLanguage.Cpp] = "cpp",
        [ProgLanguage.Python] = "py",
        [ProgLanguage.Pascal] = "pas"
    };
    
    public async void SendCodeToChecking()
    {
        var task = CodeEditor.Task;
        var code = CodeEditor.Code;
        var language = CodeEditor.Language;

        var token = TokenSession.FromPlayerPrefs();

        var checkingResult = await RequestAPIProxy.CheckCodeAsync(task, token, code, ProgLanguages[language]);

        //var checkingResult = await task.CheckCodeAsync(token, code, ProgLanguages[language]);
        CodeConsole.WriteLine(checkingResult);
    }
}