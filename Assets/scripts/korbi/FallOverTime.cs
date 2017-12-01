using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOverTime : MonoBehaviour {

    public float delayTime = 0.5f;
    public float respawnTime = 5f;
    Vector3 pos;

    private void Start()
    {
        pos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            StartCoroutine(activateIsSimulated());
        }
    }

    IEnumerator activateIsSimulated()
    {
        yield return new WaitForSeconds(delayTime);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        StartCoroutine(respawnOverTime());
    }

    //Setze es nach einer gewissen zeit wieder an den anfang einfach... ich kann das sonst nicht anders handeln gerade...
    //Macht eigentlich so oder so mehr sinn :/
    IEnumerator respawnOverTime()
    {
        yield return new WaitForSeconds(respawnTime);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        transform.position = pos;

    }

}
