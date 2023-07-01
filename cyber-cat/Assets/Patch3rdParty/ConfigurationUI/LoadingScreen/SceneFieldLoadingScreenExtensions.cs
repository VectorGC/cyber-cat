using System;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Patch3rdParty.ConfigurationUI.LoadingScreen
{
    public static class SceneFieldLoadingScreenExtensions
    {
        public static IDisposable LoadAsync(this SceneField scene, LoadSceneMode loadSceneMode = default) =>
            SceneManager.LoadSceneAsync(scene.SceneName, loadSceneMode).AsAsyncOperationObservable().Subscribe();

        public static IDisposable LoadAsyncViaLoadingScreen(this SceneField scene, LoadSceneMode loadSceneMode = default) =>
            SceneManager.LoadSceneAsync(scene.SceneName, loadSceneMode).ViaLoadingScreen();

        public static IObservable<AsyncOperation> ViaLoadingScreen(this SceneField scene, LoadSceneMode loadSceneMode = default) =>
            SceneManager.LoadSceneAsync(scene.SceneName, loadSceneMode).ViaLoadingScreenObservable();
    }
}