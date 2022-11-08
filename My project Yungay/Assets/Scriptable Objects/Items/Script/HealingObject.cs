using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Item", menuName = "Inventory System/Items/Healing")]
public class HealingObject : ItemObject
{
    public float restoreHealthPercentage;
    private void Awake()
    {
        type = ItemType.Healing;
    }
}
