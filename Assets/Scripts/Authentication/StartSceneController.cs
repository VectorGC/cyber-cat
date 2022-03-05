using UnityEngine;
using UnityEngine.SceneManagement;
using Authentication;

public class StartSceneController : MonoBehaviour
{
    void Start()
    {
        //PlayerPrefs.DeleteKey("token");
        if(CheckForExistingPlayers())
        {
            SceneManager.LoadScene("AuthScene");
        }
        else
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public static bool CheckForExistingPlayers()
    {
        return string.IsNullOrEmpty(TokenSession.FromPlayerPrefs().Token);
    }
}
