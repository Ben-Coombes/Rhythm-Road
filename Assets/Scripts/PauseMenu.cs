using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [SerializeField] private GameObject pauseMenu, settingsMenu, pauseOverlay;
    [SerializeField] private Slider musicSlider, sfxSlider;


    //Finds the pause panel
    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseOverlay.SetActive(false);
        musicSlider.value = PlayerPrefs.GetFloat("music", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfx", 0.5f);
    }

    void Update()
    {
        //Checks if the game is paused
        if (Input.GetKeyDown(KeyCode.Escape))
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
        pauseOverlay.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    public void Retry()
    {
        GameIsPaused = false;
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void Pause()
    {
        AudioListener.pause = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
        pauseMenu.SetActive(true);
        pauseOverlay.SetActive(true);
    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
        SceneManager.LoadScene(0);
    }

    public void OnMusicVolumeChanged()
    {
        PlayerPrefs.SetFloat("music", musicSlider.value);
        FindObjectOfType<SoundManager>().GetAudioSource("SoundTrack1").volume = musicSlider.value;
    }

    public void OnSFXVolumeChanged()
    {
        PlayerPrefs.SetFloat("sfx", sfxSlider.value);
        FindObjectOfType<SoundManager>().GetAudioSource("HitSound").volume = sfxSlider.value;
        FindObjectOfType<SoundManager>().GetAudioSource("HitSound2").volume = sfxSlider.value;
    }
}
