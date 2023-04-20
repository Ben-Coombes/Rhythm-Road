using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; set; }

    public int perfectHits, goodHits, badHits, misses;
    public float accuracy;
    public Grade grade;
    int overallDifficulty;
    public int score;
    int currentCombo;
    public int highestCombo;

    private void Awake()
    {

        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        perfectHits = 0;
        goodHits = 0;
        badHits = 0;
        score = 0;
        currentCombo = 0;
        highestCombo = 0;
        accuracy = 0;
        overallDifficulty = 8;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnNoteHit(float hitWindow, Note note)
    {
        FindObjectOfType<SoundManager>().PlayOneShot("HitSound");
        int hitValue = CalculateHitValue(hitWindow);

        int comboMultiplier = 0;
        if (currentCombo - 1 > 0)
            comboMultiplier = currentCombo - 1;

        score += hitValue * (1 + comboMultiplier);

        currentCombo++;
        if (currentCombo > highestCombo)
            highestCombo = currentCombo;

        CalculateAccuracy();

        Events.onScoreChanged.Invoke(score, currentCombo, accuracy);
        note.Hit(hitValue);
    }

    private int CalculateHitValue(float hitWindow)
    {
        Debug.Log(hitWindow);
        int hitValue = 0;

        if (hitWindow < (80 - 6 * overallDifficulty))
        {
            hitValue = 300;
            perfectHits++;
        }
        else if (hitWindow < (140 - 8 * overallDifficulty))
        {
            hitValue = 100;
            goodHits++;
        }
        else
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
        CalculateAccuracy();
        Events.onScoreChanged.Invoke(score, currentCombo, accuracy);
    }

    private void CalculateAccuracy()
    {
        accuracy = (float)(300 * perfectHits + 100 * goodHits + 50 * badHits) / (300 * (perfectHits + goodHits + badHits + misses));
        accuracy *= 100;
    }

    private void CalculateGrade()
    {
        int totalNotes = perfectHits + goodHits + badHits + misses;
        float percentagePerfect = ((float)perfectHits / totalNotes) * 100;
        float percentageBad = ((float)badHits / totalNotes) * 100;
        Debug.Log("Percentage Perfect: " + percentagePerfect.ToString());
        if (misses == 0 && percentagePerfect >= 90 && percentageBad <= 1)
        {
            grade = Grade.S;
        }
        else if (percentagePerfect >= 80 && misses == 0 || percentagePerfect >= 90)
        {
            grade = Grade.A;
        }
        else if (percentagePerfect >= 70 && misses == 0 || percentagePerfect >= 80)
        {
            grade = Grade.B;
        }
        else if (percentagePerfect >= 60)
        {
            grade = Grade.C;
        }
        else
        {
            grade = Grade.D;
        }
        Grade currentGrade;
        if (Enum.TryParse(PlayerPrefs.GetString(GameManager.Instance.currentSelectedMusic.songTitle), out currentGrade))
        {
            if ((int)currentGrade < (int)grade)
            {
                PlayerPrefs.SetString(GameManager.Instance.currentSelectedMusic.songTitle, grade.ToString());
            }
        }
    }
    private void OnEnable()
    {
        Events.onGameOverTrigger.AddListener(CalculateGrade);
        Events.onNoteHit.AddListener(OnNoteHit);
        Events.onNoteMiss.AddListener(OnNoteMiss);
    }

    private void OnDisable()
    {
        Events.onGameOverTrigger.RemoveListener(CalculateGrade);
        Events.onNoteHit.RemoveListener(OnNoteHit);
        Events.onNoteMiss.RemoveListener(OnNoteMiss);
    }
}

public enum Grade
{
    _ = 0,
    D = 1,
    C = 2,
    B = 3,
    A = 4,
    S = 5,
}
