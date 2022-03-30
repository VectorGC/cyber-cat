using System;
using Authentication;
using Cysharp.Threading.Tasks;
using RestAPIWrapper;
using UniRx;
using UnityEngine;

public class TestEditor : MonoBehaviour
{
    private static bool isInited;

    async void Start()
    {
        if (isInited)
        {
            return;
        }

        isInited = true;

        var taskId = "10";

        var task = await RestAPI.GetTask(TokenSession.FromPlayerPrefs(), taskId);
        await CodeEditor.OpenSolution(task);
    }

    public async void SendCodeToTesting()
    {
        // Человек берет тест
        // Отправляет
        // Получает ответ и показывает

        var taskId = CodeEditor.Task.Id;
        var code = CodeEditor.Code;

        var token = TokenSession.FromPlayerPrefs();

        var codeTestingResult = await RestAPI.SendCodeToTesting(token, taskId, code);
        CodeConsole.WriteDelayedLine(codeTestingResult, 0.05f);
    }
}