using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class FinalbossCinematic : MonoBehaviour
{
    public PlayableDirector Cm;
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
            Cm.Play();
            Destroy(this.gameObject, 12f);
        }
        
    }
}
