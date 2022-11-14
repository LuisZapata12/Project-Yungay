using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simularcaminar : MonoBehaviour
{
    public GameObject a;
    public AudioSource audio;
    public bool caminar;


    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            audio.Play();
           //a=AudioManager.Instance.SelecSfx(a, 0, 1,false,1);
        }
        else if(Input.GetKeyUp(KeyCode.W))
        {
           audio.Pause();
        }
    }
}
