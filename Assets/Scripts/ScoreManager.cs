using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int perfectHits, goodHits, badHits, misses;
    int overallDifficulty = 4;
    int score;
    int currentCombo;
    int highestCombo;
    // Start is called before the first frame update
    void Start()
    {
        perfectHits = 0;
        goodHits = 0;
        badHits = 0;
        score = 0;
        currentCombo = 0;
        highestCombo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnNoteHit(float hitWindow, Note note)
    {
        int hitValue = CalculateHitValue(hitWindow);

        int comboMultiplier = 0;
        if (currentCombo - 1 > 0)
            comboMultiplier = currentCombo - 1;

        score += hitValue * (1 + comboMultiplier);

        currentCombo++;
        if (currentCombo > highestCombo)
            highestCombo = currentCombo;

        Destroy(note.gameObject);

        Events.onScoreChanged.Invoke(score, currentCombo);
    }

    private int CalculateHitValue(float hitWindow)
    {
        int hitValue = 0;

        if(hitWindow < (80 - 6 * overallDifficulty))
        {
            hitValue = 300;
            perfectHits++;
        } else if(hitWindow < (140 - 8 * overallDifficulty))
        {
            hitValue = 100;
            goodHits++;
        } else
        {
            hitValue = 50;
            badHits++;
        }

        return hitValue;
    }
    private void OnNoteMiss()
    {
        currentCombo = 0;
        misses++;
        Events.onScoreChanged.Invoke(score, currentCombo);
    }
    private void OnEnable()
    {
        Events.onNoteHit.AddListener(OnNoteHit);
        Events.onNoteMiss.AddListener(OnNoteMiss);
    }

    private void OnDisable()
    {
        Events.onNoteHit.RemoveListener(OnNoteHit);
        Events.onNoteMiss.RemoveListener(OnNoteMiss);
    }
}
