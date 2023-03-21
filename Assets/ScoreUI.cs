using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText, comboText;
    private void UpdateText(int score, int combo)
    {
        scoreText.text = score.ToString();
        comboText.text = $"{combo}x";
    }

    private void OnEnable()
    {
        Events.onScoreChanged.AddListener(UpdateText);
    }

    private void OnDisable()
    {
        Events.onScoreChanged.RemoveListener(UpdateText);
    }
}
