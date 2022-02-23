using System.Collections.Generic;
using UnityEngine;

public static class GlobalMap
{
    public static void OpenTaskList(IReadOnlyCollection<ITaskTicket> taskTickets)
    {
        Scene.OpenScene("GlobalMapNew", () =>
        {
            var taskNodesController = BaseTaskNodesController.GetTaskNodesController();
            taskNodesController.CreateTaskList(taskTickets);
        });
    }
}

public abstract class BaseTaskNodesController : MonoBehaviour
{
    public abstract void OpenTask(ITaskTicket taskTicket);
    public abstract void CreateTaskList(IReadOnlyCollection<ITaskTicket> taskTickets);

    public static BaseTaskNodesController GetTaskNodesController() => FindObjectOfType<BaseTaskNodesController>();
}

public class TaskNodesController : BaseTaskNodesController
{
    [SerializeField] private Transform content;

    [SerializeField] private TaskNodeView taskNodeViewPrefab;

    public override void OpenTask(ITaskTicket taskTicket)
    {
        CustomCodeEditor.OpenEditorForTask(taskTicket);
    }

    public override void CreateTaskList(IReadOnlyCollection<ITaskTicket> taskTickets)
    {
        foreach (var taskTicket in taskTickets)
        {
            var taskNodeView = Instantiate(taskNodeViewPrefab, content);
            taskNodeView.SetupView(taskTicket);
        }
    }
}