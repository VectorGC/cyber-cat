using UnityEngine;

public class IntroCartoonPlayer : MonoBehaviour
{
    public async void Play()
    {
        await IntroCartoon.Play();
    }
}
