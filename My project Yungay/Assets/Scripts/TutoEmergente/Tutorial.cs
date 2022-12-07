using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Tutorial : MonoBehaviour
{
    public static Tutorial instance;

    public GameObject Panel;
    public GameObject [] tutorialImag;
    public string [] tutorialText;
    public TMP_Text tmpText;

    
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
        Panel.SetActive(true);
        tutorialImag[x].SetActive(true);
        tmpText.text = tutorialText[x];
        GameManager.inPause = true;
        
    }

    public void SkipTutoImage(int x)
    {
        Panel.SetActive(false);
        tutorialImag[x].SetActive(false);
        tmpText.text = null;
        GameManager.inPause = false;
    }
}
