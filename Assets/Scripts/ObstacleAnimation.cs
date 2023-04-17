using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAnimation : MonoBehaviour
{
    public float rotationSpeed;
    public Vector3 rotationDirection;

    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(40f, 80f);
        rotationDirection = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationDirection * Time.deltaTime * rotationSpeed);

    }
}
