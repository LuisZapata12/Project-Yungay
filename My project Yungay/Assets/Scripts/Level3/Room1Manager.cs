using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Manager : MonoBehaviour
{
    public Inventory inventoryPlayer;
    public bool hasSubmachine;
    public ItemObject submachineItem;
    public GameObject SpawnEnemys;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hasSubmachine = inventoryPlayer.CheckItem(submachineItem);
        if (hasSubmachine)
        {
            SpawnEnemys.SetActive(true);
        }
    }
}
