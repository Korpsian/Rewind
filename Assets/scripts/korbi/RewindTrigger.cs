using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RewindTrigger : MonoBehaviour {

    public GameObject Player;
    public GameObject IUText;
    public GameObject[] Lvl;
    GameObject currentLvl;
    PolygonCollider2D pol;
    public string nextLvlName;

    public static RewindTrigger instance;

    int lvlCounter = 0;

    private void Awake()
    {
        instance = this;

        foreach (GameObject g in Lvl)
        {
            g.SetActive(false);
        }
        Lvl[0].SetActive(true);
    }

    private void Start()
    {
        pol = gameObject.GetComponent<PolygonCollider2D>();
        //Setze den Ersten namen des "Lvl's" als startname
        StartCoroutine(IUText.GetComponent<WriteOutLvl>().Write(Lvl[0].name));
        //Nehe das derzeitige Lvl und speichere es 
        currentLvl = Lvl[lvlCounter];
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {            
            if(lvlCounter < Lvl.Length -1)
            {
                pol.enabled = false;
                //Aktiviere das Movement
                Player.GetComponent<CharController>().PlayMovement();
                lvlCounter++;
                //Speichere das derzeitige lvl unverändert in den zwischenspeicher
                currentLvl = Lvl[lvlCounter];
            } else
            {
                //Wenn lvl Counter das ende erreicht hat, wird kein Rewind mehr gemacht, neues Lvl wird geladen bzw. End sequenz wie auch immer
                Debug.Log("LVL End, play ending scene?");
                SceneManager.LoadScene(nextLvlName, LoadSceneMode.Single);

            }

        }
    }

    public void EndRewindTrigger()
    {
        pol.enabled = true;
        Lvl[lvlCounter - 1].SetActive(false);
        Lvl[lvlCounter].SetActive(true);
        StartCoroutine(IUText.GetComponent<WriteOutLvl>().Write(Lvl[lvlCounter].name));
    }

    public void ResetCurrentLvl()
    {
        Player.GetComponent<CharController>().PlayMovement();

    }

}
