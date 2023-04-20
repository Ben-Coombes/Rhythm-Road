using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText, comboText, accuracyText;
    public Image timeImage;
    private void UpdateText(int score, int combo, float accuracy)
    {
        scoreText.text = score.ToString();
        comboText.text = $"{combo}x";
        accuracyText.text = $"{Math.Round(accuracy, 2)}%";
    }

    private void Update()
    {
        DateTime songLength = DateTime.ParseExact(GameManager.Instance.currentSelectedMusic.songLength, "m:ss", CultureInfo.InvariantCulture);
        float songTime = songLength.Minute * 60 + songLength.Second;
        Debug.Log("Song Length " + songTime + "Song Position " + Conductor.Instance.songposition);
        timeImage.fillAmount = Conductor.Instance.songposition / songTime;
        accuracyText.text = Conductor.Instance.songposition.ToString();
    }
    public void OnGameOverTrigger()
    {
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Events.onScoreChanged.AddListener(UpdateText);
        Events.onGameOverTrigger.AddListener(OnGameOverTrigger);
    }

    private void OnDisable()
    {
        Events.onScoreChanged.RemoveListener(UpdateText);
        Events.onGameOverTrigger.RemoveListener(OnGameOverTrigger);
    }
}
