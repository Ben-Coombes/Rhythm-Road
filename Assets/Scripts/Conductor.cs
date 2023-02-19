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
    // Start is called before the first frame update
    void Start()
    {
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

            GameObject obj = Instantiate(objectToSpawn);
            float speed = obj.GetComponent<MoveObstacle>().speed;

            float zPos = timeInMS / (1f / speed * 1000);
            obj.transform.position = new Vector3(laneXPos[laneNum], obj.transform.position.y, zPos);
        }
        reader.Close();

        FindObjectOfType<SoundManager>().Play("SoundTrack1");
        startSongPosition = (float)AudioSettings.dspTime;
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        songposition = (float)AudioSettings.dspTime - startSongPosition;
        text.text = songposition.ToString();
    }
}
