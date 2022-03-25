using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public struct SetupTaskDescriptionMessage
{
    public ITaskTicket TaskTicket { get; }

    public SetupTaskDescriptionMessage(ITaskTicket taskTicket)
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
        MessageBroker.Default.Receive<SetupTaskDescriptionMessage>().Subscribe(SetupTaskDescription);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Test"))
        {
            var task = new SimpleTaskTicket(0, 0, "Name of Task", "Description of Task");
            MessageBroker.Default.Publish(new SetupTaskDescriptionMessage(task));
        }
    }

    private void SetupTaskDescription(SetupTaskDescriptionMessage message)
    {
        goalTask.SetText(message.TaskTicket.Name);
        descriptionTask.SetText(message.TaskTicket.Description);
    }
}