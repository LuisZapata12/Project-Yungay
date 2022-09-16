using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject item;
    public int amount;

    private void Update()
    {
        if (amount==0)
        {
            Destroy(gameObject);
        }
    }
}