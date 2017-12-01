using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Ded Plain");
        if(col.gameObject.tag == "Player")
        {
            RewindTrigger.instance.ResetCurrentLvl();
        }
    }

}
