using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lanePos;
    public float laneThickness = 3;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lanePos = 1;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    public void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (lanePos > 0)
            {
                MoveLeft();
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (lanePos < 2)
            {
                MoveRight();
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            Crouch(false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Crouch(true);
        }
    }

    public void MoveLeft()
    {
        lanePos--;
        transform.position = new Vector3(transform.position.x - laneThickness, transform.position.y, transform.position.z);
    }

    public void MoveRight()
    {
        lanePos++;
        transform.position = new Vector3(transform.position.x + laneThickness, transform.position.y, transform.position.z);
    }

    public void Crouch(bool isKeyDown)
    {
        if (isKeyDown)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y / 2, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2, transform.localScale.z);
        }
    }
}
