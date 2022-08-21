using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject pause;


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        MusicClass.instance.startFadingOut = true;
    }

    public void ShowPauseMenu()
    {
        pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pause.SetActive(false);
        if(!GameManagement.instance.isPaused)
            Time.timeScale = 1f;
    }

    
}
