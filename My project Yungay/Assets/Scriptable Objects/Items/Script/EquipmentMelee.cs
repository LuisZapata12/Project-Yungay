using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment Melee Item", menuName = "Inventory System/Items/Equipment/Melee")]
public class EquipmentMelee : EquipmentItem
{
    public float force;
    public float damage;
    public AnimationClip animation;
    public AudioClip attackClip;
    public int durability;
    private void Awake()
    {
        equipmentType = EquipmentType.Melee;
    }
}
