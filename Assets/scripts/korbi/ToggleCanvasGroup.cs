using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleCanvasGroup : MonoBehaviour {

    public CanvasGroup group;
    public float startwert = 0;

    private void Start()
    {
        group.alpha = startwert;
    }

    public void ToggleCGRoup()
    {
        if(group.alpha == 1)
        {
            group.alpha = 0;
        }
        else if(group.alpha == 0)
        {
            group.alpha = 1;
        }
    }

}
