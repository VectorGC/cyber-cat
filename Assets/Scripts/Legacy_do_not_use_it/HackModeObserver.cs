using UnityEngine;

public class HackModeObserver : MonoBehaviour
{
    private void Update()
    {
        var isHackModePressed = Input.GetKeyDown(KeyCode.Q);
        if (isHackModePressed)
        {
            Debug.Log("Hack mode pressed");
            GameMode.HackMode = GameMode.HackMode == Mode.Default ? Mode.HackMode : Mode.Default;
        }
    }

    private void OnDestroy()
    {
        GameMode.HackMode = Mode.Default;
    }
}