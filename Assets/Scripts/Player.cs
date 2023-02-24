using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int lanePos;
    public float laneThickness = 3;
    Rigidbody rb;

    public TextMeshProUGUI text;

    [Header("Ground Check")]
    private bool isGrounded;
    public LayerMask whatIsGround;
    public float playerHeight;
    public float jumpHeight = 15;

    [Header("Note Check")]
    public bool isInNote;
    public Note note;
    public List<float> noteHits;

    // Start is called before the first frame update
    void Start()
    {
        isInNote = false;
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
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (lanePos > 0)
            {
                MoveLeft();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (lanePos < 2)
            {
                MoveRight();
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            Crouch(false);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Crouch(true);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();
        }
    }

    public void HitNote()
    {
        if(isInNote && note != null)
        {
            float noteTime = Conductor.Instance.startSongPosition + note.time;
            float keyHitTime = (float)AudioSettings.dspTime;
            string lastSource = "HitSound";
            noteHits.Add(Math.Abs(keyHitTime - noteTime));
            UpdateText();
            if(lastSource == "HitSound")
            {
                FindObjectOfType<SoundManager>().Play("HitSound2");
                lastSource = "HitSound2";
            } else
            {
                FindObjectOfType<SoundManager>().Play("HitSound");
                lastSource = "HitSound";
            }
            Destroy(note.gameObject);
            Debug.Log($"Hit Time: {keyHitTime} Note Time: {noteTime} Difference: {keyHitTime - noteTime}");
        }
    }

    private void UpdateText()
    {
        float total = 0;
        foreach(float hitTime in noteHits)
        {
            total += hitTime;
        }
        total /= noteHits.Count;

        text.text = Math.Round(total * 1000, 0).ToString();
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

    public void KillPlayer()
    {
        GameManager.Instance.RestartGame();
        Destroy(this.gameObject);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other + "hit");
        if (other.gameObject.tag == "Obstacle")
        {
            KillPlayer();
        }

        if (other.gameObject.tag == "Note")
        {
            isInNote = true;
            note = other.gameObject.GetComponent<Note>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Note")
        {
            isInNote = false;
        }
    }
}
