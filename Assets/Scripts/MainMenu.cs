using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text greetingstext;
    public void Start()
    {
        greetingstext.text = ("������, " + /*��� ������ �� ��?*/ "PlayerName" + "!");
    }
    public void Run()
    {
        SceneManager.LoadScene("GlobalMap");
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
