using System;
using UnityEngine;
using Zenject;

public class HackerVision : IInitializable, ITickable
{
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

    public void Initialize()
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

    public void Tick()
    {
        var isHackModePressed = Input.GetKeyDown(KeyCode.Q);
        if (isHackModePressed)
        {
            Active = !Active;
        }
    }
}