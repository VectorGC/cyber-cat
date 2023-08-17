using JetBrains.Annotations;
using Panda;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseBehaviourTreeController : MonoBehaviour
{
    [Task]
    [UsedImplicitly]
    public void Modal(string header, string text, string buttonText)
    {
        if (ThisTask.status == Status.Ready)
        {
            ThisTask.data = SimpleModalWindow.Create()
                .SetHeader(header)
                .SetBody(text)
                .AddButton(buttonText)
                .Show();
            return;
        }

        if ((SimpleModalWindow) ThisTask.data == null)
        {
            ThisTask.Succeed();
        }
    }

    [Task]
    [UsedImplicitly]
    public bool IsActiveHackerMode()
    {
        return HackerVisionSingleton.Instance.Active;
    }

    [Task]
    [UsedImplicitly]
    public bool IsOpenCodeEditor()
    {
        return CodeEditor.IsOpen;
    }

    [Task]
    [UsedImplicitly]
    public void OpenMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}