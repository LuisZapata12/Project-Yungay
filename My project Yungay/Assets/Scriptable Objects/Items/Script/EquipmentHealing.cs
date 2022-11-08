using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Healing Item", menuName = "Inventory System/Items/Equipment/Healing")]
public class EquipmentHealing : EquipmentItem
{
    public float restoreHealthPercentage;
    public AnimationClip animation;
    private void Awake()
    {
        equipmentType = EquipmentType.Healing;
    }
}
