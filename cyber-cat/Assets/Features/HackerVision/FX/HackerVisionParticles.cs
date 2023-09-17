using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HackerVisionParticles : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _particleSystems;

    private HackerVision _hackerVision;

    [Inject]
    private void Construct(HackerVision hackerVision)
    {
        _hackerVision = hackerVision;
    }

    void Update()
    {
        SetEnabledOnUpdate(_hackerVision.Active);
    }

    private void SetEnabledOnUpdate(bool isEnabled)
    {
        foreach (var particles in _particleSystems)
        {
            if (isEnabled && !particles.isPlaying)
            {
                particles.Play();
            }

            if (!isEnabled && particles.isPlaying)
            {
                particles.Stop();
                particles.Clear();
            }
        }
    }
}