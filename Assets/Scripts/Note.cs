using UnityEngine;

public class Note : MonoBehaviour
{
    public float speed = 20f;
    public float time;
    Vector3 endPositon;
    // Update is called once per frame

    private void Start()
    {
        endPositon = new Vector3(transform.position.x, transform.position.y, 0);
    }
    void FixedUpdate()
    {
        if (GameManager.Instance.currentState == GameState.Playing)
            Move();
        //Debug.Log(100/speed) // get time till player
    }

    public void Move()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed * Time.fixedDeltaTime);

        // transform.position = Vector3.Lerp(transform.position, endPositon, Conductor.Instance.songposition - time);
    }


}
