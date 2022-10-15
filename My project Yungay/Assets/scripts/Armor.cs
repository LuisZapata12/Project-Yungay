using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    public PlayerModel playerHealth;
    public Inventory inventory;
    public ItemObject armorItem;
    private bool hasArmor;
    public float timeArmor;
    private void FixedUpdate()
    {
        hasArmor = inventory.CheckItem(armorItem);
        if (hasArmor && Input.GetMouseButtonDown(0))
        {
            Doped();
        }
    }
    public void Doped()
    {
        playerHealth.armor = timeArmor;
    }
}