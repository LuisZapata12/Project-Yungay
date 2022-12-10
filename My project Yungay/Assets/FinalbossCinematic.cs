using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FinalbossCinematic : MonoBehaviour
{
    public PlayableDirector Cm;
    private float time;
    public bool activeTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTime)
        {
            time += Time.deltaTime;
            if (time >= 11f)
            {
                GameManager.inPause = false;
                Cm.Stop();
            }
        }
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.inPause = true;
            Cm.Play();
            Destroy(this.gameObject, 12f);


        }
        
    }
}
