using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    private int lanePos;
    private float laneThickness = 3;
    Rigidbody rb;
    public float laneChangeSpeed;

    [Header("Ground Check")]
    public bool isGrounded;
    public LayerMask whatIsGround;
    public float playerHeight;
    public float jumpHeight = 15;
    public bool canSlide = true;
    public bool canMove = true;

    [Header("Note Check")]
    public bool isInNote;
    public Note note;
    public List<float> noteHits;

    public Animator anim;

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
        anim.SetBool("isGrounded", isGrounded);
        CheckInput();
    }

    public void CheckInput()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (lanePos > 0 && canMove)
            {
                MoveLeft();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (lanePos < 2 && canMove)
            {
                MoveRight();
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Slide();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            Jump();
        }
    }

    public void HitNote(CallbackContext ctx)
    {

        if (isInNote && note.isHit == false && ctx.performed)
        {
            float noteTime = Conductor.Instance.startSongPosition + note.time;
            float keyHitTime = (float)AudioSettings.dspTime;
            Events.onNoteHit.Invoke(Math.Abs(keyHitTime - noteTime) * 1000, note);
        }
    }
    public void MoveLeft()
    {
        lanePos--;
        StartCoroutine(ChangeLane(new Vector3(transform.position.x - laneThickness, transform.position.y, transform.position.z)));
    }

    public void MoveRight()
    {
        lanePos++;
        StartCoroutine(ChangeLane(new Vector3(transform.position.x + laneThickness, transform.position.y, transform.position.z)));
    }

    private IEnumerator ChangeLane(Vector3 target)
    {
        while (transform.position.x - target.x > 0.1f)
        {
            canMove = false;
            transform.position = new Vector3(Mathf.Lerp(transform.position.x, target.x, laneChangeSpeed * Time.deltaTime), transform.position.y, transform.position.z);
            yield return new WaitForFixedUpdate();
        }
        canMove = true;
        transform.position = target;
        yield return null;
    }
    public void Slide()
    {
        canSlide = false;
        anim.SetBool("isSliding", true);
        float length = anim.GetCurrentAnimatorClipInfo(0).Length;
        StartCoroutine(StartSlide(length));
    }

    private IEnumerator StartSlide(float length)
    {
        GetComponent<CapsuleCollider>().height = 1;
        GetComponent<CapsuleCollider>().center = new Vector3(0, -0.5f, 0);
        yield return new WaitForSeconds(length);
        anim.SetBool("isSliding", false);
        canSlide = true;
        GetComponent<CapsuleCollider>().height = 2;
        GetComponent<CapsuleCollider>().center = new Vector3(0, 0, 0);
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
