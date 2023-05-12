using Patch3rdParty.ConfigurationUI.LoadingScreen;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace MonoBehaviours
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneField scene;
        [SerializeField] private LoadSceneMode loadSceneMode;

        public void LoadSceneAsync() => scene.LoadAsyncViaLoadingScreen(loadSceneMode);
    }
}