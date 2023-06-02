using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Utils
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

        public UniTask LoadSceneAsync(LoadSceneMode loadSceneMode)
        {
            return SceneManager.LoadSceneAsync(m_SceneName, loadSceneMode).ToUniTask();
        }
    }
}