using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharController : MonoBehaviour {

    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sp;
    //Es darf nur einen trigger geben
    RewindTrigger rw;

    public CapsuleCollider2D cap1;
    public CapsuleCollider2D cap2;

    public int moveSpeed = 800;
    public int jumpPower = 400;
    public int angularDrag = 200;

    public int maxSpeedHoriz = 800;
    public int maxSpeedVertic = 800;
    public int stopSpeed = 50;

    bool grounded = false;
    [Space]
    public float PlaybackSpeed = 0.2f;
    public float LerpSpeed = 1f;

    float[] xVelocity = new float [1];
    bool AddXVelocity = false;

    Vector3 startPoint;
    Vector3 [] recording = new Vector3 [0];
    bool allowMoving = true;
    bool allowHorizMoving = true;
    bool allowCheckVelocity = true;
    bool allowRecording = false;
    //Sagt nach wie vielen fixed updates eine neue position gespeichert werden soll
    public int recordDelay = 10;
    int recordDelayCount = 0;

	// Use this for initialization
	void Start () {
        rw = RewindTrigger.instance;
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        sp = gameObject.GetComponent<SpriteRenderer>();
        //Speichere den Startpunkt
        startPoint = transform.position;
	}

    private void FixedUpdate()
    {
        SetVelocityY();
        if (allowMoving)
        {
            if(allowHorizMoving)
            {
                Movement();
            }
            JumpMovement();
            DuckMovement();
            ResetButton();
        }
        if(allowCheckVelocity)
        {
            CheckVelocityX();
        }
        if(allowRecording)
        {
            RecordMovement();
        }
    }

    public void SetGrounded(bool grounded)
    {
        this.grounded = grounded;
        anim.SetBool("grounded", grounded);
    }

    void ResetButton()
    {
        if(Input.GetButtonDown("Reset"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    //Nicht Ente.... sondern DUCKEN
    void DuckMovement()
    {
        if(Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Duck") < -0.01f && grounded)
        {
            anim.SetBool("ducking", true);
            allowHorizMoving = false;
            cap1.enabled = false;
            cap2.enabled = true;

        }
        else if(Input.GetAxisRaw("Vertical") >= 0 || Input.GetAxisRaw("Duck") >= 0 || !grounded)
        {
            anim.SetBool("ducking", false);
            allowHorizMoving = true;
            cap1.enabled = true;
            cap2.enabled = false;
        }

    //Ändere Animation bool zu duck
    //Ändere 2D collider 
    //Ändere Sprite
    }

    void JumpMovement()
    {
        if (Input.GetButton("Jump"))
        {
            if (grounded)
            {
                rb.AddForce(Vector3.up * jumpPower * Time.deltaTime, ForceMode2D.Impulse);
            }
        }
        else if(!Input.GetButton("Jump"))
        {
            if(!grounded)
            {
                rb.AddForce(Vector3.down * angularDrag * Time.deltaTime);
            }
            else
            {
            }
        }

        if(!grounded)
        {
            AddXVelocity = true;
            xVelocity[xVelocity.Length - 1] = rb.velocity.x;
            System.Array.Resize(ref (xVelocity), xVelocity.Length + 1);
        } else
        {
            if(AddXVelocity)
            {
                AddXVelocity = false;
                rb.velocity = new Vector2( xVelocity[xVelocity.Length - 2], rb.velocity.y);
                System.Array.Resize(ref (xVelocity), 1);
            }
        }

    }

    //Bewegungsverhalten vom Player
    void Movement()
    {
        //Bewegung rechts
        if(Input.GetAxisRaw("Horizontal") > 0 && rb.velocity.x < maxSpeedHoriz && Input.GetButton("Run") && grounded)
        {
            sp.flipX = false;
            anim.SetInteger("walking", 2);
            rb.AddForce(Vector3.right * moveSpeed * 1.2f * Time.deltaTime, ForceMode2D.Impulse);
        }

        else if(Input.GetAxisRaw("Horizontal") > 0 && rb.velocity.x < maxSpeedHoriz)
        {
            sp.flipX = false;
            anim.SetInteger("walking", 1);
            rb.AddForce(Vector3.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }

        if(Input.GetAxisRaw("Horizontal") < 0 && rb.velocity.x > -maxSpeedHoriz && Input.GetButton("Run") && grounded)
        {
            sp.flipX = true;
            anim.SetInteger("walking", 2);
            rb.AddForce(Vector3.left * moveSpeed * 1.2f * Time.deltaTime, ForceMode2D.Impulse);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && rb.velocity.x > -maxSpeedHoriz)
        {
            sp.flipX = true;
            anim.SetInteger("walking", 1);
            rb.AddForce(Vector3.left * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }

        //Gibt eine gegen kraft wenn kein button gedrückt wird und stoppt aufnahme derweil 
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            anim.SetInteger("walking", 0);

            //Falls der Player am boden ist, soll ein wiederstand entstehen, ansonsten nicht
            if (grounded)
            {
                if (0 < rb.velocity.x)
                {
                    //rb.AddForce(Vector3.left * -rb.velocity.x);
                    rb.AddForce(Vector3.left * rb.velocity.x*3);
                }
                else if (0 > rb.velocity.x)
                {
                    rb.AddForce(Vector3.right * -rb.velocity.x*3);
                    //rb.velocity = new Vector2(-stopSpeed, rb.velocity.y);
                }
            }
        }
    }

    //Übeprrüfe die Y Velocity um die animation anzupassen
    void SetVelocityY()
    {
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    //Dient zur überprüfung ob der RigidBody sich bewegt, ist das der fall wird aufgezeichnet 
    void CheckVelocityX()
    {
            if (rb.velocity.x > 0 || rb.velocity.x < 0 || rb.velocity.y > 0 || rb.velocity.y < 0)
            {
                allowRecording = true;
            }
            else
            {
                allowRecording = false;
            }
    }

    void RecordMovement()
    {
            if(recordDelayCount >= recordDelay)
            {
                recordDelayCount = 0;
                System.Array.Resize(ref (recording), recording.Length + 1);
                recording[recording.Length - 1] = transform.position;
            } else
            {
                recordDelayCount++;
            }
    }

    //Gibt das movement rückwährts aus
    public void PlayMovement()
    {
        allowCheckVelocity = false;
        allowMoving = false;
        allowRecording = false;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;

        StartCoroutine(PlayMov());
    }

    IEnumerator PlayMov()
    {

        cap1.enabled = false;
        cap2.enabled = false;

        for (int i = recording.Length - 2; i > 0; i--)
        {
            //transform.position = recording[i];
            transform.position = Vector3.Lerp(transform.position, recording[i], LerpSpeed);
            yield return new WaitForSeconds(PlaybackSpeed);
        }

        System.Array.Resize(ref (recording), 0);
        allowMoving = true;
        rb.isKinematic = false;
        allowCheckVelocity = true;

        cap1.enabled = true;
        cap2.enabled = true;

        rw.EndRewindTrigger();
    }

    //Setzt den Recording Array wieder auf 0, wir nur getätig bei tot etc.
    void ResetRecordingArray()
    {
        System.Array.Resize(ref (recording), 0);
    }

    //Setzte den Player 
    public void ResetToStart()
    {
        transform.position = startPoint;
        ResetRecordingArray();
    }
}
