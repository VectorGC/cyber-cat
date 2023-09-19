using UnityEngine.EventSystems;

public abstract class CodeConsoleView : UIBehaviour
{
    public abstract void Log(string message);
    public abstract void LogError(string message);
    public abstract void LogSuccess(string message);
}