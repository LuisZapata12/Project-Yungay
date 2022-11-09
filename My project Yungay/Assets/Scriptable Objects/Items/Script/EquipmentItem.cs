using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Melee,
    Range,
    Healing
}
//[CreateAssetMenu(fileName = "New Equipment Item", menuName = "Inventory System/Items/Equipment")]
public class EquipmentItem : ItemObject
{
    public string itemName;
    public Mesh itemMesh;
    public Material itemMaterial;
    public EquipmentType equipmentType;
    public AnimationClip idleClip;
    private void Awake()
    {
        type = ItemType.Equipment;
    }
}
