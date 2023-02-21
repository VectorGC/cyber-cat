using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TextMeshPro_TextShared greetingstext;
    public void Start()
    {
        greetingstext.text = ("Привет, <color=green>" + PlayerPrefs.GetString("name") + "</color>!");
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
