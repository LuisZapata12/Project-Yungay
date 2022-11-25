using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvent : MonoBehaviour
{
    public GameObject spawn;
    public GameObject obstaculos;
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
        }
    }
}
