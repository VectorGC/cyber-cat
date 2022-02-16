using UnityEngine;
using UnityEngine.SceneManagement;

namespace MonoBehaviours
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneField scene;

        public void LoadScene() => SceneManager.LoadScene(scene);
    }
}