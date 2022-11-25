using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room5Manager : MonoBehaviour
{
    public GameObject SpawnEnemysRoom3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpawnEnemysRoom3.SetActive(true);
        }
        
    }
}
