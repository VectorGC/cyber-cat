using Legacy_do_not_use_it;
using UnityEngine;

public class HackModeObserver : MonoBehaviour
{
    private void Update()
    {
        var isHackModePressed = Input.GetKeyDown(KeyCode.Q);
        if (isHackModePressed)
        {
            Debug.Log("Hack mode pressed");
            GameMode.Vision = GameMode.Vision == VisionMode.Default ? VisionMode.HackVision : VisionMode.Default;
        }
    }

    private void OnDestroy()
    {
        GameMode.Vision = VisionMode.Default;
    }
}