using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LogMessageView : UIBehaviour
{
    [SerializeField] private TMP_Text text;

    protected override void Start()
    {
        text.text = string.Empty;
        Application.logMessageReceived += OnLogMessageReceived;
    }

    protected override void OnDestroy()
    {
        Application.logMessageReceived -= OnLogMessageReceived;
    }

    private void OnLogMessageReceived(string condition, string stacktrace, LogType type)
    {
        switch (type)
        {
            case LogType.Error:
            case LogType.Exception:
                SetBadColor();
                break;
            default:
                SetGoodColor();
                break;
        }

        text.text = condition;
    }

    private void SetGoodColor()
    {
        text.color = Color.green;
    }

    private void SetBadColor()
    {
        text.color = Color.red;
    }
}