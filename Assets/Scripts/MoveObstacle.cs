using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    public float speed = 10f;
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        //Debug.Log(100/(speed * Time.fixedDeltaTime)); // = 500
    }

    public void Move()
    {
        transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - speed * Time.fixedDeltaTime);
    }
}
