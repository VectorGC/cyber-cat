using UnityEngine;
using UnityEngine.SceneManagement;

namespace MonoBehaviours
{
    public class SceneUnloader : MonoBehaviour
    {
        public void UnloadActiveScene()
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(currentScene).ViaLoadingScreen();
        }

        public void UnloadSceneByIndex(int index)
        {
            Time.timeScale = 1f;
            var sceneByIndex = SceneManager.GetSceneAt(index);
            SceneManager.UnloadSceneAsync(sceneByIndex).ViaLoadingScreen();
        }
    }
}