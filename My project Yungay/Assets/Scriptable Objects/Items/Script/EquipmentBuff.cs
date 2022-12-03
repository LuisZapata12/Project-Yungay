using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Buff Item", menuName = "Inventory System/Items/Equipment/Buff")]
public class EquipmentBuff : EquipmentItem
{
    public float armor;
    public AnimationClip animation;
    private void Awake()
    {
        equipmentType = EquipmentType.Healing;
    }
}
