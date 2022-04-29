using Authentication;
using Cysharp.Threading.Tasks;
using UnityEngine;

public static class TaskProgressBoard
{
    public static async UniTask ShowTaskProgressBoard()
    {
        var token = TokenSession.FromPlayerPrefs();
        var tasksProgress = await TasksProgress.GetFromServer(token);

        OnTaskProgressReceived(tasksProgress);
    }

    private static void OnTaskProgressReceived(TasksProgress tasksProgress)
    {
        Time.timeScale = 0f;
        var modalPanel = UnityEngine.Object.FindObjectOfType<ModalPanel>();
        modalPanel.MessageBox(null, "Прогресс по задачам", tasksProgress.ToString(), () => { }, () => { }, () => { },
            () => { Time.timeScale = 1f; }, false, "Ok");
    }
}