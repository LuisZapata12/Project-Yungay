using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;

    public GameObject [] tutorialImag;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

    }

    public void ShowTutoImage(int x)
    {
        tutorialImag[x].enabled = true;
    }
}
