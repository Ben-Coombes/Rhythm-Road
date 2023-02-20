using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class Conductor : MonoBehaviour
{
    float songposition;
    float startSongPosition;
    public List<ObstacleData> obstacles;
    public List<float> laneXPos;
    public TextMeshProUGUI text;
    public List<float> noteTimes;
    float noteStartTime;
    int noteCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        noteTimes = new List<float>();
        string path = Application.dataPath + "/Level Data/level test.csv";

        StreamReader reader = new StreamReader(path);


        while (!reader.EndOfStream)
        {

            string[] line = reader.ReadLine().Split(",");

            float timeInMS = float.Parse(line[0]);
            GameObject objectToSpawn = null;
            int laneNum = int.Parse(line[2]);

            foreach (ObstacleData obstacle in obstacles)
            {
                if (obstacle.key == line[1])
                {
                    objectToSpawn = obstacle.obj;
                }
            }
            noteTimes.Add(timeInMS / 1000);
            GameObject obj = Instantiate(objectToSpawn);
            float speed = obj.GetComponent<MoveObstacle>().speed;

            float zPos = timeInMS / (1f / speed * 1000);
            obj.transform.position = new Vector3(laneXPos[laneNum], obj.transform.position.y, zPos);
        }
        reader.Close();

        FindObjectOfType<SoundManager>().Play("SoundTrack1");
        startSongPosition = (float)AudioSettings.dspTime;
        noteStartTime = (float)AudioSettings.dspTime + noteTimes[noteCounter];
        FindObjectOfType<SoundManager>().PlayScheduled("HitSound", noteStartTime);
        noteCounter++;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        songposition = (float)AudioSettings.dspTime - startSongPosition;
        text.text = songposition.ToString();
        ScheduleHitSounds();
    }

    void ScheduleHitSounds()
    {
        if(AudioSettings.dspTime > noteStartTime)
        {
            noteStartTime = startSongPosition + noteTimes[noteCounter];
            FindObjectOfType<SoundManager>().PlayScheduled("HitSound", noteStartTime);
            noteCounter++;
        }
        //
        //
    }
}
