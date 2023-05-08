using System.Collections.Generic;
using Authentication;
using CodeEditorModels.ProgLanguages;
using RestAPIWrapper;
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

        TokenSession token = PlayerPrefsInfo.GetToken();

        var checkingResult = await RestAPI.Instance.SendCodeToChecking(token.Token, task.Id, code, ProgLanguages[language]);

        CodeConsole.WriteLine(checkingResult);
    }
}