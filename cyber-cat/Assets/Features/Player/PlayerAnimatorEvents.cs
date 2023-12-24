using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorEvents : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _clips;

    public void Footstep1()
    {
        var rndIndex = Random.Range(0, _clips.Count);
        var clip = _clips[rndIndex];
        _audioSource.PlayOneShot(clip);
    }

    public void Footstep2()
    {
        var rndIndex = Random.Range(0, _clips.Count);
        var clip = _clips[rndIndex];
        _audioSource.PlayOneShot(clip);
    }
}