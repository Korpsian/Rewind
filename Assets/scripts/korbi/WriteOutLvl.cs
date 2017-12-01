using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WriteOutLvl : MonoBehaviour {


    [HideInInspector] public WriteOutLvl instance;
    public Text bgT;
    Text t;
    public float writeDelay = 0.2f;
    

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        t = gameObject.GetComponent<Text>(); 
	}

    public IEnumerator Write(string str)
    {
        //Resetten der texte
        t.text = "";
        bgT.text = "";

        foreach(char c in str)
        {
            t.text = t.text + c;
            bgT.text = bgT.text + c;
            yield return new WaitForSeconds(writeDelay);
        }

    }

}

