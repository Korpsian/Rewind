﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public void LoadNewScene(string str)
    {
        SceneManager.LoadScene(str, LoadSceneMode.Single);
    }

}
