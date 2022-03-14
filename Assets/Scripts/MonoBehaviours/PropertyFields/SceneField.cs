using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace MonoBehaviours.PropertyFields
{
    [Serializable]
    public class SceneField
    {
        [SerializeField] [UsedImplicitly] private Object m_SceneAsset;
        [SerializeField] private string m_SceneName = "";

        public string SceneName => m_SceneName;

        // makes it work with the existing Unity methods (LoadLevel/LoadScene)
        public static implicit operator string(SceneField sceneField)
        {
            return sceneField.SceneName;
        }
        
        public IDisposable LoadAsync() =>
            SceneManager.LoadSceneAsync(m_SceneName).AsAsyncOperationObservable().Subscribe();

        public IDisposable LoadAsyncViaLoadingScreen() =>
            SceneManager.LoadSceneAsync(m_SceneName).ViaLoadingScreenObservable().Subscribe();

        public IObservable<AsyncOperation> ViaLoadingScreen() =>
            SceneManager.LoadSceneAsync(m_SceneName).ViaLoadingScreenObservable();
    }
}