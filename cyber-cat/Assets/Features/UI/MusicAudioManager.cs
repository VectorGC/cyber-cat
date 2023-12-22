using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MusicAudioManager : MonoInstaller, IInitializable, IDisposable
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _mainMenuMusic;
    [SerializeField] private AudioClip _gameMusic;
    [SerializeField] private AudioClip _codeEditorMusic;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<MusicAudioManager>().FromInstance(this).AsSingle();
    }

    public void Initialize()
    {
        var scene = SceneManager.GetActiveScene();
        OnActiveSceneChanged(scene, scene);

        SceneManager.activeSceneChanged += OnActiveSceneChanged;
    }

    public void Dispose()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
    }

    private void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        switch (newScene.name)
        {
            case "MainMenu":
                _audioSource.clip = _mainMenuMusic;
                _audioSource.Play();
                break;
            case "Tutorial":
            case "Game":
                StopAllCoroutines();
                if (oldScene.name == "CodeEditor")
                    StartCoroutine(SwitchSound(3f, _gameMusic));
                else
                    StartCoroutine(SwitchSound(20f, _gameMusic));
                break;
            case "CodeEditor":
                StopAllCoroutines();
                StartCoroutine(SwitchSound(3f, _codeEditorMusic));
                break;
        }
    }

    private IEnumerator SwitchSound(float time, AudioClip audioClip)
    {
        if (_audioSource.isPlaying)
        {
            // Wait before fade
            if (time > 6f)
                yield return new WaitForSeconds(time - 5);

            // Fade sound
            for (var i = 0.1f; i < time; i += 0.2f)
            {
                yield return new WaitForSeconds(0.2f);
                var volume = Mathf.Lerp(1, 0, i / time);
                _audioSource.volume = volume;
            }

            // Wait if needed
            if (time > 10f)
                yield return new WaitForSeconds(3f);
        }

        // Start new sound
        _audioSource.clip = audioClip;
        _audioSource.Play();
        _audioSource.volume = 1;
    }
}