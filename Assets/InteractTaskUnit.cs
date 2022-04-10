using Legacy_do_not_use_it;
using TasksData;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractTaskUnit : TaskUnitView
{
    private async void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        var isHackModePressed = Input.GetKey(KeyCode.F);
        if (isHackModePressed && GameMode.Vision == VisionMode.HackVision)
        {
            await CodeEditor.OpenSolution(taskUnit);
            
            // TODO: Use message broker here.
            taskUnit.CallTaskChanged();
        }
    }

    protected override void UpdateTaskData(ITaskData taskData)
    {
        if (!IsTaskSolved)
        {
            return;
        }

        TryGetComponent<Collider>(out var collider);
        collider.enabled = false;
    }
}
