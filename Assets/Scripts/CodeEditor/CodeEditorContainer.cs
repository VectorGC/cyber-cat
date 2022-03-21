using UnityEngine;

public class CodeEditorContainer : MonoBehaviour
{
    [Header("Task Description")] 
    [SerializeField] private TextField taskName;
    [SerializeField] private TextField taskDescription;

    public void SetTaskDescription(ITaskTicket taskTicket)
    {
        taskName.SetText(taskTicket.Name);
        taskDescription.SetText(taskTicket.Description);
    }
}