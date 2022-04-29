using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackModeObserver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var isHackModePressed = Input.GetKeyDown(KeyCode.Q);
        if (isHackModePressed)
        {
            Debug.Log("Hack mode pressed");
            GameMode.HackMode = GameMode.HackMode == Mode.Default ? Mode.HackMode : Mode.Default;
        }
    }
}
