using System;
using TMPro;
using UnityEngine;

public class CodeEditorContainer : MonoBehaviour
{
    public TMP_Text Title;
    public TMP_InputField CodeInputField;

    [Header("Task Description")] 
    public TextField TaskName;
    public TextField TaskDescription;

    private void Start()
    {
        CodeInputField.text = "int main()";

        // "#include <stdio.h>\nint main(){return 0;}"
        
        var t = new TestTask()
        {
            Name = "Test Name",
            Description = "Desc Test"
        };

        SetTaskDescription(t);
    }
    
    class TestTask : ITaskTicket
    {
        public int Id { get; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public void SetTaskDescription(ITaskTicket taskTicket)
    {
        TaskName.SetContent(taskTicket.Name, "Task Name");
        TaskDescription.SetContent(taskTicket.Description, "The Goal");
    }
}
