using System.Collections;
using System.Collections.Generic;
using TasksData.Requests;
using TMPro;
using UniRx;
using UnityEngine;

public class CodeEditorController : MonoBehaviour
{
    [SerializeField] private TMP_InputField codeInputField;

    private int _openedTaskId;

    public static void OpenEditorForTask(string taskId)
    {
        new GetTaskRequest(taskId)
            .SendRequest()
            .Subscribe(OpenEditorForTask);
    }

    public static void OpenEditorForTask(ITaskTicket task)
    {
        Debug.Log($"Opening code editor for task '{task.Id}'");
        Scene.OpenScene("Code_editor_Blue",
            () =>
            {
                var codeEditorStartup = FindObjectOfType<CodeEditorStartup>();
                codeEditorStartup.SetupCodeEditorForTask(task);
            });
    }

    public static int GetOpenedTaskId() => Instance._openedTaskId;
    public static string GetCode() => Instance.codeInputField.text;

    public void SetupCodeEditor(ITaskTicket task)
    {
        _openedTaskId = task.Id;
    }

    private static CodeEditorController Instance => FindObjectOfType<CodeEditorController>();
}