using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public struct SetTaskDescriptionMessage
{
    public ITaskTicket TaskTicket { get; }

    public SetTaskDescriptionMessage(ITaskTicket taskTicket)
    {
        TaskTicket = taskTicket;
    }
}

public class TaskDescription : UIBehaviour
{
    [SerializeField] private TextField goalTask;
    [SerializeField] private TextField descriptionTask;

    protected override void Awake()
    {
        MessageBroker.Default.Receive<SetTaskDescriptionMessage>().Subscribe(SetTaskDescription);
    }

    private void SetTaskDescription(SetTaskDescriptionMessage message)
    {
        var task = message.TaskTicket;
        
        goalTask.SetText(task.Name);
        descriptionTask.SetText(task.Description);
    }
}