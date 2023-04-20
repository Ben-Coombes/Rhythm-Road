using UnityEngine;

public class MenuController : MonoBehaviour
{
    private AudioSource musicSource;
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        GameManager.Instance.ChangeState(GameState.Menu);
        musicSource.volume = PlayerPrefs.GetFloat("music", 0.5f);
        musicSource.clip = GameManager.Instance.currentSelectedMusic.songAudio;
        musicSource.Play();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
