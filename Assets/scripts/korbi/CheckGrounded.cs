using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrounded : MonoBehaviour {

    public CharController player;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            player.SetGrounded(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.SetGrounded(false);

    }

}
