using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        songTitleText.text = GameManager.Instance.currentSelectedMusic.songTitle.ToString();
        scoreText.text = ScoreManager.Instance.score.ToString();
        accuracyText.text = ScoreManager.Instance.accuracy.ToString("#.##") + "%";
        _300text.text = ScoreManager.Instance.perfectHits.ToString();
        _100text.text = ScoreManager.Instance.goodHits.ToString();
        _50text.text = ScoreManager.Instance.badHits.ToString();
        missText.text = ScoreManager.Instance.misses.ToString();
        letterGradeText.text = ScoreManager.Instance.grade.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
