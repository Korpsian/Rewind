using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOverButton : MonoBehaviour {

    public GameObject ActivateObj;

    private void OnMouseEnter()
    {
        ActivateObj.SetActive(true);
    }

    private void OnMouseExit()
    {
        ActivateObj.SetActive(false);
    }

}
