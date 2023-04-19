using System;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText, comboText, accuracyText;
    private void UpdateText(int score, int combo, float accuracy)
    {
        scoreText.text = score.ToString();
        comboText.text = $"{combo}x";
        accuracyText.text = $"{Math.Round(accuracy, 2)}%";
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
