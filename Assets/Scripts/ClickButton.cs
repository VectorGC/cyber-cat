using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Text))]
public class ClickButton : Button
{
    AudioSource audio;
    Text text;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        text = GetComponent<Text>();
    }
    public void ClickSound()
    {
        GetComponent<AudioSource>().Play();
    }
    
}
