using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public List<Item> loot = new List<Item>();
    public bool isRandom = false;
    private void Start()
    {
        if (isRandom)
        {
            RandomAmount();
        }       
    }

    public void RandomAmount()
    {
        for (int i = 0; i < loot.Count; i++)
        {
            loot[i].amount = (int)Random.Range(1f, 5f);
        }
    }

    private void Update()
    {

    }

    public void Delete()
    {
        int count = 0;
        for (int i = 0; i < loot.Count; i++)
        {
            if (loot[i].amount == 0)
            {
                count++;
            }
        }

        if (count == loot.Count)
        {
            Destroy(GetComponent<Loot>());
        }
    }

    public void DeleteObject()
    {
        int count = 0;
        for (int i = 0; i < loot.Count; i++)
        {
            if (loot[i].amount == 0)
            {
                count++;
            }
        }

        if (count == loot.Count)
        {
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class Item
{
    public ItemObject item;
    public int amount;
    public Item(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}
