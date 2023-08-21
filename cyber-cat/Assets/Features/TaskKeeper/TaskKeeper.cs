using UnityEngine;

public class TaskKeeper : MonoBehaviour
{
    [SerializeField] private TaskType _task;
    public TaskType Task => _task;
}