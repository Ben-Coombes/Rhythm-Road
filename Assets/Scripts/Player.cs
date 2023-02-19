using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int lanePos;
    public float laneThickness = 3;
    Rigidbody rb;

    [Header("Ground Check")]
    private bool isGrounded;
    public LayerMask whatIsGround;
    public float playerHeight;
    public float jumpHeight = 15;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lanePos = 1;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
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

    public void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        
    }

    

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other + "hit");
        if (other.gameObject.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
