using System;
using Legacy_do_not_use_it;
using TasksData;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class HackerVisionTaskFx : MonoBehaviourObserver<ITaskData>
{
    [SerializeField] private new ParticleSystem particleSystem;

    private bool? _isTaskSolved;

    protected void Start()
    {
        enabled = false;
        TryGetComponent(out particleSystem);
    }

    private void Update()
    {
        if (GameMode.Vision == VisionMode.HackVision)
        {
            if (_isTaskSolved is false)
            {
                if (!particleSystem.isPlaying) particleSystem.Play();
                return;
            }
        }

        particleSystem.Stop();
        particleSystem.Clear();
    }

    public override void OnNext(ITaskData value)
    {
        _isTaskSolved = value?.IsSolved;
        if (_isTaskSolved is false)
        {
            enabled = true;
        }
    }

    public override void OnCompleted() => enabled = false;

    public override void OnError(Exception error) => enabled = false;
}