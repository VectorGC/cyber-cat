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

        var message = TryGetLocalizedMessage(condition);
        if (!string.IsNullOrEmpty(message))
        {
            text.text = message;
        }
    }

    private string TryGetLocalizedMessage(string message)
    {
        switch (message)
        {
            case "RequestException: HTTP/1.1 401 Unauthorized":
                return "Не авторизован. Не правильный пароль";
            case "RequestException: HTTP/1.1 500 Internal Server Error":
                return "Внутренняя ошибка сервера. Обратитесь к организатору";
            case "RequestException: HTTP/1.1 404 Not Found":
                return "Пользователь не найден. Проверьте логин";
            case "RequestException: SSL CA certificate error":
                return "Ошибка сертификата. Обратитесь к организаторам";
            default:
                return string.Empty;
        }
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