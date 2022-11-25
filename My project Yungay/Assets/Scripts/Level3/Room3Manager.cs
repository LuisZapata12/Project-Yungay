using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Room3Manager : MonoBehaviour
{
    public GameObject spawn;
    public GameObject obstaculos;
    public PlayableDirector Cm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn.transform.childCount<=0)
        {
            obstaculos.SetActive(false);
            Debug.Log("Inicia cinematica");
            Cm.Play();
        }
        
    }
}
