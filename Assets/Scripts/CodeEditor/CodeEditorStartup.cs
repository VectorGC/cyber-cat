using UnityEngine;

public class CodeEditorStartup : MonoBehaviour
{
    [SerializeField] private CodeEditor codeEditor;
    [SerializeField] private CodeEditorContainer container;

    public void SetupCodeEditorForTask(ITaskTicket task)
    {
        codeEditor.SetupCodeEditor(task);
        container.SetTaskDescription(task);
    }
}