using UnityEngine;


public class EscapeMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;

    public GameObject escapeMenuUI;

    private void Start()
    {
        escapeMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        escapeMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        IsGamePaused = false;
    }

    private void Pause()
    {
        escapeMenuUI.SetActive(true);
       // Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void ExitGame()
    {
        Debug.Log("The game was exited.");
        //Time.timeScale = 1f;

        /*
         * Enter the name of the scene you want to go to after exiting the game.
         */

        //SceneManager.LoadScene(" WRITE SCENE NAME HERE !!! ");
    }
}
