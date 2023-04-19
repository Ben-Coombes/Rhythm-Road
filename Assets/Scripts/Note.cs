using TMPro;
using UnityEngine;

public class Note : MonoBehaviour
{
    public float speed = 20f;
    public float time;
    Vector3 endPositon;
    public TextMeshProUGUI text;
    public Color[] colors;
    private Animation anim;
    private BoxCollider boxCollider;
    // Update is called once per frame

    private void Start()
    {
        endPositon = new Vector3(transform.position.x, transform.position.y, 0);
        anim = GetComponent<Animation>();
        boxCollider = GetComponent<BoxCollider>();
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

    public void Hit(int score)
    {
        text.gameObject.SetActive(true);
        text.text = score.ToString();
        if (score == 300)
        {
            text.color = colors[0];
        }
        else if (score == 100)
        {
            text.color = colors[1];
        }
        else
        {
            text.color = colors[2];
        }
        anim.Play("NoteHit");
        boxCollider.enabled = false;
    }

    public void HitAnimFinished()
    {
        Destroy(gameObject);
    }


}
