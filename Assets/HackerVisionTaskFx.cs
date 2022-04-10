using System;
using Legacy_do_not_use_it;
using TasksData;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class HackerVisionTaskFx : TaskUnitView
{
    [SerializeField] private new ParticleSystem particleSystem;

    protected override void Start()
    {
        base.Start();
        TryGetComponent(out particleSystem);
    }

    protected override void UpdateTaskData(ITaskData taskData)
    {
    }

    private void Update()
    {
        if (GameMode.Vision != VisionMode.HackVision)
        {
            if (!particleSystem.isPlaying)
            {
                return;
            }

            particleSystem.Stop();
            particleSystem.Clear();

            return;
        }

        if (!IsTaskSolved)
        {
            if (!particleSystem.isPlaying) particleSystem.Play();
            return;
        }

        particleSystem.Stop();
        particleSystem.Clear();
    }

    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }
}