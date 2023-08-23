using JetBrains.Annotations;
using Panda;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PandaBehaviour))]
public class BaseBehaviourTreeController : MonoBehaviour
{
    [Task]
    [UsedImplicitly]
    public void Modal(string text)
    {
        if (ThisTask.status == Status.Ready)
        {
            ThisTask.data = SimpleModalWindow.Create()
                .SetBody(text)
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
    public void Modal(string header, string text)
    {
        if (ThisTask.status == Status.Ready)
        {
            ThisTask.data = SimpleModalWindow.Create()
                .SetHeader(header)
                .SetBody(text)
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
    public bool IfActiveHackerVision()
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

    [Task]
    [UsedImplicitly]
    public void Hint(string text)
    {
        var hud = FindObjectOfType<HUDController>();
        hud.HintText = text;

        ThisTask.Succeed();
    }
}