using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Window : MonoBehaviour
{
    public int windowId;
    public GameObject windowPieces;
    public int hits;


    public TMP_Text textKathyaVoice;
    public string dialogo;
    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.brokeWindowEvent += BreakWindow;
        EventManager.current.kathyaVoiceEvent += KathyaVoice;
    }

    // Update is called once per frame
    void Update()
    {
        if (hits <= 0)
        {
            EventManager.current.StartBreakingWindowEvent(windowId);
        }
    }

    private void BreakWindow(int id)
    {
        if (id == windowId)
        {
            Instantiate(windowPieces, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        

    }
    private void KathyaVoice(int id)
    {
        StartCoroutine(showText());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Axe") || other.CompareTag("Spear"))
        {
            hits -= 1;
            
            
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag != "Player")
    //    {
    //        BreakWindow(windowId);
    //    }


    //}

    private void OnDestroy()
    {
        EventManager.current.brokeWindowEvent -= BreakWindow;
    }

    IEnumerator showText()
    {
        textKathyaVoice.text = dialogo;
        yield return new WaitForSeconds(2f);
        textKathyaVoice.text = "";
    }
}
