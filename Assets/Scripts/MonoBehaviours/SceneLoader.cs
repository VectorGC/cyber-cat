using MonoBehaviours.PropertyFields;
using UnityEngine;

namespace MonoBehaviours
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneField scene;

        public void LoadSceneAsync() => scene.LoadAsync();
        public void LoadSceneSync() => scene.LoadAsync();
    }
}