using System.Collections.Generic;
using UnityEngine;

public class HackerVisionParticles : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _particleSystems;

    void Update()
    {
        SetEnabledOnUpdate(HackerVisionSingleton.Instance.Active);
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