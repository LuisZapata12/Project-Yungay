using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class KatyaVoice : MonoBehaviour
{
    public GameObject PanelD;
    [SerializeField, TextArea(3, 8)]
    public string text;
    public TMP_Text dialogueText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PanelD.SetActive(true);
            dialogueText.text = ""+ text;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PanelD.SetActive(false);
            
        }
       
    }

}
