using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicSOUI : MonoBehaviour
{
    public MusicSO musicSO;
    public TextMeshProUGUI title, length, grade;
    public Image stars, gradeCircle;
    public Color[] gradeColors;

    private void Start()
    {
        title.text = musicSO.songTitle;
        length.text = musicSO.songLength;
        string strGrade = PlayerPrefs.GetString(musicSO.songTitle, "~");
        Color color = gradeColors[0];
        switch (strGrade)
        {
            case "~":
                color = gradeColors[0];
                break;
            case "D":
                color = gradeColors[1];
                break;
            case "C":
                color = gradeColors[2];
                break;
            case "B":
                color = gradeColors[3];
                break;
            case "A":
                color = gradeColors[4];
                break;
            case "S":
                color = gradeColors[5];
                break;
        }

        grade.text = strGrade;
        grade.color = color;
        gradeCircle.color = color;
        stars.fillAmount = musicSO.songDifficulty / 5;
    }
    public void OnClick()
    {
        GameManager.Instance.currentSelectedMusic = musicSO;
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("LevelScene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
