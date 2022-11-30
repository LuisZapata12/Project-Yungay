using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Sprite slotSprite;

    public InventorySlot slot = new InventorySlot(null,0,0);

    private void Start()
    {
        slotSprite = GetComponent<Image>().sprite;
    }
}
