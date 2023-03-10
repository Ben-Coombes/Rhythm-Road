using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;

public class Conductor : MonoBehaviour
{
    public static Conductor Instance { get; private set; }
    float songposition;
    public float startSongPosition;
    public float offset = 35;
    public List<ObstacleData> obstacles;
    public List<float> laneXPos;
    public TextMeshProUGUI text;
    public List<float> noteTimes;
    float pitch;
    float noteStartTime;
    int noteCounter = 0;
    // Start is called before the first frame update
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
        }

    }
    void Start()
    {
        float speed;
        GameObject obj;
        float zPos;
        noteTimes = new List<float>();
        var file = Resources.Load<TextAsset>("Rainbow Road/rainbowroad");

        string[] lines = file.text.Split(
        new string[] { "\r\n", "\r", "\n" },
    StringSplitOptions.None
);


        foreach (string str in lines)
        {
            if (str.Length > 1)
            {
                string[] line = str.Split(",");

                float timeInMS = float.Parse(line[0]) + offset;
                GameObject objectToSpawn = null;
                int laneNum = int.Parse(line[2]);

                foreach (ObstacleData obstacle in obstacles)
                {
                    if (obstacle.key == line[1])
                    {
                        objectToSpawn = obstacle.obj;
                    }
                }
                if (line[1] == "N")
                {
                    noteTimes.Add(timeInMS / 1000);
                    obj = Instantiate(objectToSpawn);
                    speed = obj.GetComponent<Note>().speed;
                    obj.GetComponent<Note>().time = timeInMS / 1000;
                    zPos = timeInMS / (1f / speed * 1000);
                    obj.transform.position = new Vector3(laneXPos[laneNum], obj.transform.position.y, zPos);
                }
                else
                {
                    obj = Instantiate(objectToSpawn);
                    speed = obj.GetComponent<MoveObstacle>().speed;
                    zPos = timeInMS / (1f / speed * 1000);
                    obj.transform.position = new Vector3(laneXPos[laneNum], obj.transform.position.y, zPos);
                }
            }
        }

        FindObjectOfType<SoundManager>().Play("SoundTrack1");
        pitch = FindObjectOfType<SoundManager>().GetAudioSource("SoundTrack1").pitch;
        startSongPosition = (float)AudioSettings.dspTime;
        noteStartTime = (float)AudioSettings.dspTime + noteTimes[noteCounter];
        FindObjectOfType<SoundManager>().PlayScheduled("HitSound", noteStartTime);
        noteCounter++;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        songposition = ((float)AudioSettings.dspTime - startSongPosition) * pitch - offset;
        text.text = $"{songposition + offset}";
        //ScheduleHitSounds();
    }

    void ScheduleHitSounds()
    {
        if(AudioSettings.dspTime > noteStartTime)
        {
            noteStartTime = startSongPosition + noteTimes[noteCounter];
            if(noteCounter%2 == 0)
            {
                FindObjectOfType<SoundManager>().PlayScheduled("HitSound", noteStartTime);
                

            } else
            {
                FindObjectOfType<SoundManager>().PlayScheduled("HitSound2", noteStartTime);
            }
            
            noteCounter++;
        }
    }
}
