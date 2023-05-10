using MonoBehaviours.PropertyFields;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MonoBehaviours
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneField scene;
        [SerializeField] private LoadSceneMode loadSceneMode;

        public void LoadSceneAsync() => scene.LoadAsyncViaLoadingScreen(loadSceneMode);
    }
}