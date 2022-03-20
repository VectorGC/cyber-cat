using UnityEngine;

namespace MonoBehaviours
{
    // Пока открыта сцена редактора?

    public enum PauseActiveMode
    {
        None = 0,
        ActiveOnStart = 1
    }
    
    public class PauseTimeScaler : MonoBehaviour
    {
        [SerializeField] private PauseActiveMode pauseActiveMode;

        void Start()
        {
            if (pauseActiveMode == PauseActiveMode.ActiveOnStart)
            {
                EnablePause();
            }
        }
        
        public void EnablePause()
        {
            Time.timeScale = 0;
        }

        public void DisablePause()
        {
            Time.timeScale = 1;
        }

        private void OnDestroy()
        {
            Time.timeScale = 1;
        }
    }
}