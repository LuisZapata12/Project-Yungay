using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Healing,
    Material,
    Equipment,
    Default
}
public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public Sprite itemSprite;
    public ItemType type;
    public int maxStack;
    [TextArea(15,20)]
    public string description;
    public string pickSoundName;
}
