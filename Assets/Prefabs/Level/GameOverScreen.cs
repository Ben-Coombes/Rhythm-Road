using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI songTitleText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI accuracyText;
    public TextMeshProUGUI _300text;
    public TextMeshProUGUI _100text;
    public TextMeshProUGUI _50text;
    public TextMeshProUGUI missText;
    public TextMeshProUGUI letterGradeText;
    public TextMeshProUGUI highestComboText;
    private string grade;

    public Color[] gradeColors;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        Color color = gradeColors[0];
        grade = ScoreManager.Instance.grade.ToString();
        songTitleText.text = GameManager.Instance.currentSelectedMusic.songTitle.ToString();
        scoreText.text = ScoreManager.Instance.score.ToString();
        accuracyText.text = ScoreManager.Instance.accuracy.ToString("#.##") + "%";
        _300text.text = ScoreManager.Instance.perfectHits.ToString();
        _100text.text = ScoreManager.Instance.goodHits.ToString();
        _50text.text = ScoreManager.Instance.badHits.ToString();
        missText.text = ScoreManager.Instance.misses.ToString();
        letterGradeText.text = ScoreManager.Instance.grade.ToString();
        highestComboText.text = ScoreManager.Instance.highestCombo.ToString() + "x";

        switch (grade)
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
        letterGradeText.color = color;

    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
