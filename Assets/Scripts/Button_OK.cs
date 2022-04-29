using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//������ ����� �� Main Camera
public class Button_OK : MonoBehaviour
{
    [SerializeField]
    InputField field;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnClick();
        }
    }
    
    public void OnClick()
    {
        AuthenticationSucceeded("123");
    }

    private void AuthenticationSucceeded(string token)
    {
        Debug.Log($"Authentication succeeded. Access granted for user email: {field.text}");
        SceneManager.LoadScene("MainMenu");
    }
}
