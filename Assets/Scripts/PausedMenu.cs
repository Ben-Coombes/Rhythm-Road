using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausedMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] private GameObject pauseMenu, settingsMenu;
   

    //Finds the pause panel
    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    void Update()
    {
        //Checks if the game is paused
        if (Input.GetKeyDown(KeyCode.P))
            if (GameIsPaused)
                Resume();
            else
                Pause();
    }

    public void Resume()
    {
        AudioListener.pause = false;
        //FindObjectOfType<SoundManager>().Play("SoundTrack1");
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void StartOver()
    {
        GameIsPaused = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);      
       
    }

    public void Pause()
    {
        AudioListener.pause = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseMenu.SetActive(true);
    }

    public void settingMenu()
    {        
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
