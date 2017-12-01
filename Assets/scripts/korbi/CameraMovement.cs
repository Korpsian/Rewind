using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraMovement : MonoBehaviour {

    float moveSpeedX = 0.05f;
    float moveSpeedY = 0.05f;
    float movementDelay = 1f;
    public GameObject Player;
    public float camSize = 9;


    // Update is called once per frame
    private void FixedUpdate()
    {

            //MoveCameraX();
            //MoveCameraY();
            FocusPlayer();
            SetCameraSize();

        
    }

    void MoveCameraX()
    {
        //Greift die x Position des players ab
        float posX = Player.transform.position.x;
        //Speichert die derzeitige position der Kamera
        Vector3 newPos = transform.position;
        var pRB = Player.GetComponent<Rigidbody2D>();

        //Bewege nach links
        if (Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Duck") < -0.01f)
        {
                newPos.x = posX - Screen.width / 58 * -pRB.velocity.x / 20;
                transform.position = Vector3.Lerp(transform.position, newPos, moveSpeedX);
        }

        //Bewegung nach rechts
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Duck") < -0.01f)
        {
            newPos.x = posX + Screen.width / 58 * pRB.velocity.x / 20;
            transform.position = Vector3.Lerp(transform.position, newPos, moveSpeedX);
        }

        if(pRB.velocity.x > -2 && pRB.velocity.x < 2 && Input.GetAxisRaw("Horizontal") == 0)
        {
            newPos.x = posX;
            transform.position = Vector3.Lerp(transform.position, newPos, moveSpeedX);

            //transform.position = newPos;
            //transform.position = Vector3.SmoothDamp(transform.position, newPos, ref(newPos), 0.5f);

        }

    }

    void MoveCameraY()
    {
        var pRB = Player.GetComponent<Rigidbody2D>();

        Vector3 newPos = transform.position;

        if( pRB.velocity.y > 1)
        {
            newPos.y = pRB.transform.position.y + Screen.height / 256*3;
            //newPos.x = newPos.x + Screen.width / 256 * (pRB.velocity.x / 256);

            transform.position = Vector3.Lerp(transform.position, newPos, moveSpeedY * pRB.velocity.y / 3);
        }

        if(pRB.velocity.y < 0)
        {
            newPos = Player.transform.position;
            newPos.y = newPos.y + Screen.height / 256;
            //newPos.x = newPos.x + Screen.width / 256 * (pRB.velocity.x / 256);
            newPos.z = transform.position.z;

            transform.position = Vector3.Lerp(transform.position, newPos, moveSpeedY * -pRB.velocity.y / 3);
        }

        if (pRB.velocity.y == 0)
        {
            newPos = Player.transform.position;
            newPos.y = newPos.y + Screen.height / 256*3;
            newPos.z = transform.position.z;

            transform.position = Vector3.Lerp(transform.position, newPos, moveSpeedY);
        }
    }

    void MoveCamYFix()
    {

    }

    //Je schneller, desto größer *badum zss*
    void SetCameraSize()
    {
        var pRB = Player.GetComponent<Rigidbody2D>();
        var cam = gameObject.GetComponent<Camera>();

        if(pRB.velocity.x > 0)
        {
            cam.orthographicSize = camSize + pRB.velocity.x / 10;
        } else if(pRB.velocity.x < 0)
        {
            cam.orthographicSize = camSize + -pRB.velocity.x / 10;
        } else
        {
            cam.orthographicSize = camSize;
        }
    }

    void FocusPlayer()
    {
        Vector3 pos = Player.transform.position;
        pos.y = pos.y +  Screen.height / 128;

        pos.z = transform.position.z;
        transform.position = pos;
    }

}
