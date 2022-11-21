using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class VoiceTextKatya : MonoBehaviour
{
    public string [] dialogo;
    public TMP_Text textKathyaVoice;
    public int voiceid;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.current.kathyaVoiceEvent += KathyaVoice;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void KathyaVoice(int id)
    {
        if (id ==voiceid)
        {
            StartCoroutine(showText());
        }
        
    }

    IEnumerator showText()
    {
        textKathyaVoice.text = dialogo[voiceid];
        yield return new WaitForSeconds(2f);
        textKathyaVoice.text = "";
    }
}
