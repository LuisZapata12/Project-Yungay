using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeTravel : MonoBehaviour
{
    public ItemObject axe;
    public Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.CheckItem(axe))
        {

        }
    }
}
