using UnityEngine;

[CreateAssetMenu(fileName = "Music", menuName = "ScriptableObjects/Music", order = 1)]
public class MusicSO : ScriptableObject
{
    public string songTitle;
    public string songLength;
    [Range(0f, 5f)]
    public float songDifficulty;
    public AudioClip songAudio;
    [Tooltip("FolderName/csvName")]
    public string songCSVFilePath;
    public float offset;

    public void SetLevelSong()
    {
        GameManager.Instance.currentSelectedMusic = this;
    }
}
