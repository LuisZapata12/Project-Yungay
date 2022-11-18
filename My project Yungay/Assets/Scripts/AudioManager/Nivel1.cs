using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AudioScene()
    {
        AudioManager.Instance.musicSource.Stop();
        AudioManager.Instance.PlayMusic("Nivel1-ica");
        
    }
}
