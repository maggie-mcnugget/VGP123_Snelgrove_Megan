using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;

    private bool isPaused = false;

    void Start()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
        
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You have quit the game!");
    }
}
