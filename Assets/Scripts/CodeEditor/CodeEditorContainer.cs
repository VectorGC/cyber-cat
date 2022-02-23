using TMPro;
using UnityEngine;

public class CodeEditorContainer : MonoBehaviour
{
    public TMP_Text Title;
    public TMP_InputField CodeInputField;

    [Header("Task Description")] public TextField TaskName;
    public TextField TaskDescription;

    public void SetTaskDescription(ITaskTicket taskTicket)
    {
        TaskName.SetContent(taskTicket.Name, "Задание");
        TaskDescription.SetContent(taskTicket.Description, "Цель");
    }
}