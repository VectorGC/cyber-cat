using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Authentication;

public class RestoreController : MonoBehaviour
{
    [SerializeField] private TMP_InputField loginTextField;
    [SerializeField] private TMP_InputField passwordTextField;
    [SerializeField] private ErrorMessageView errorText;

    [SerializeField] private UnityEvent onComplete;

    public async void Restore()
    {
        var login = loginTextField.text;
        var password = passwordTextField.text;

        if (!LoginIsOk())
        {
            string error = "Login is not OK!";
            errorText.OnError(new InputException(error));
            return;
        }
        if (!PasswordIsOK())
        {
            string error = "Password is not OK!";
            errorText.OnError(new InputException(error));
            return;
        }

        var tocken = await TokenSession.RestorePassword(login, password);
        if (tocken.Error != null)
        {
            errorText.OnError(new RequestTokenException(tocken.Error));
        }
        else
        {
            string pass = "¬ам на почту пришло сообщение с подтверждением!";
            errorText.OnError(new InputException(pass));
            errorText.SetGoodColor();
        }
        onComplete.Invoke();
    }

    private bool LoginIsOk()
    {
        return !string.IsNullOrWhiteSpace(loginTextField.text);
    }

    private bool PasswordIsOK()
    {
        return !string.IsNullOrWhiteSpace(passwordTextField.text);
    }
}
