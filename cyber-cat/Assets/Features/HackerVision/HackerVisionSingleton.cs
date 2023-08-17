using System;
using UnityEngine;

public class HackerVisionSingleton : MonoBehaviour
{
    public static HackerVisionSingleton Instance
    {
        get
        {
            if (!_instance)
            {
                var gameObject = new GameObject("HackerVisionSingleton");
                _instance = gameObject.AddComponent<HackerVisionSingleton>();
                Debug.Log($"Instantiate {nameof(HackerVisionSingleton)}");
            }

            return _instance;
        }
    }

    private static HackerVisionSingleton _instance;

    public bool Active
    {
        get => _active;
        set
        {
            _active = value;
            if (_active)
            {
                Debug.Log("Hacker mode activate");
            }
            else
            {
                Debug.Log("Hacker mode deactivate");
            }

            _snapshotMode.SetFilterProperties(_active ? 1 : 0);
        }
    }

    private SnapshotMode _snapshotMode;
    private bool _active;

    public void Start()
    {
        if (Camera.main)
        {
            _snapshotMode = Camera.main.gameObject.AddComponent<SnapshotMode>();
            Debug.Log($"Add {nameof(SnapshotMode)} to main camera");
        }
        else
        {
            throw new ArgumentNullException("Main camera not found");
        }
    }

    public void Update()
    {
        var isHackModePressed = Input.GetKeyDown(KeyCode.Q);
        if (isHackModePressed)
        {
            Active = !Active;
        }
    }
}