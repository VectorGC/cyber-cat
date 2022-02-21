using UnityEngine;

namespace MonoBehaviours.PropertyFields
{
    [System.Serializable]
    public class SceneField
    {
        [SerializeField] private Object m_SceneAsset;
        [SerializeField] private string m_SceneName = "";

        public string SceneName => m_SceneName;

        // makes it work with the existing Unity methods (LoadLevel/LoadScene)
        public static implicit operator string(SceneField sceneField)
        {
            return sceneField.SceneName;
        }
    }
}