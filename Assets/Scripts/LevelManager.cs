using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float secondsBetweenSpawn;
    public float elapsedTime = 0.0f;
    public int currentWave;
    public int maxWaves;
    public List<ObstacleData> obstacles;
    public List<List<GameObject>> obstaclesToSpawn;

    [Header("Lane Positions")]
    public List<Vector3> lanePositions;
    // Start is called before the first frame update
    void Start()
    {
        currentWave = 0;

        obstaclesToSpawn = new();

        string path = Application.dataPath + "/Level Data/level test.csv";

        StreamReader reader = new StreamReader(path);
        

        while(!reader.EndOfStream)
        {
            string[] line = reader.ReadLine().Split(",");
            //Debug.Log(line);
            List<GameObject> tempObjs = new();
            for (int i = 0; i < line.Length; i++)
            {
                if(line[i] == "_")
                {
                    tempObjs.Add(null);
                } else
                {
                    foreach (ObstacleData obstacle in obstacles)
                    {
                        if (obstacle.key == line[i])
                        {
                            tempObjs.Add(obstacle.obj);
                        }
                    }
                }
            }
            obstaclesToSpawn.Add(tempObjs);
        }
        reader.Close();
        maxWaves = obstacles.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWave < maxWaves)
        {
            elapsedTime += Time.deltaTime;

            if (elapsedTime > secondsBetweenSpawn)
            {
                elapsedTime = 0;
                SpawnObstacles();

            }
        }
    }

    private void SpawnObstacles()
    {
        for(int i = 0; i < obstaclesToSpawn[currentWave].Count; i++)
        {
            for (int j = 0; j < lanePositions.Count; j++)
            {
                if(obstaclesToSpawn[currentWave][j] != null)
                {
                    GameObject obj = Instantiate(obstaclesToSpawn[currentWave][j]);
                    obj.transform.position = new Vector3(lanePositions[j].x, obj.transform.position.y, lanePositions[j].z);
                }
            }
        }
        currentWave++;
    }
}
