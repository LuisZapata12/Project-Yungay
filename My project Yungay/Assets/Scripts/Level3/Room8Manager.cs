using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room8Manager : MonoBehaviour
{
    public GameObject sacks;
    public GameObject obstacle1;
    public GameObject obstacle2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sacks.transform.childCount <= 0)
        {
            obstacle1.GetComponent<Window>().enabled = true;
            obstacle2.GetComponent<Window>().enabled = true;
        }
    }
}
