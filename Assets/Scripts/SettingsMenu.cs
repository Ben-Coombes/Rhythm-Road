using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider musicSlider, sfxSlider;
    [SerializeField] private AudioSource musicSource;
    public void OnMusicVolumeChanged()
    {
        musicSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("music", musicSlider.value);
    }

    public void OnSFXVolumeChanged()
    {
        PlayerPrefs.SetFloat("sfx", sfxSlider.value);
    }

    private void OnEnable()
    {
        musicSlider.value = PlayerPrefs.GetFloat("music", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("sfx", 0.5f);
    }

}
