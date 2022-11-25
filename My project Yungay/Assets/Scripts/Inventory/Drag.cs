using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private Vector3 pos;
    [HideInInspector]public GameObject prefabItem;
    public InventoryDisplay inventoryDisplay;
    public Inventory inventory;
    private GameObject ghost;
    public GameObject inventoryPanel;
    public List<RaycastResult> results = new List<RaycastResult>();
    public Transform spawPos;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GetComponent<Slot>().slot.item != null)
        {
            ghost = Instantiate(eventData.pointerDrag.gameObject, rectTransform.position, Quaternion.identity);
            Destroy(ghost.GetComponent<Drag>());
            Destroy(ghost.GetComponent<Slot>());
            eventData.pointerDrag.GetComponent<Image>().color = new Color32(255, 255, 255, 100);
            ghost.transform.SetParent(inventoryPanel.transform,true);
            ghost.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            prefabItem = GetItemObject().prefab;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GetComponent<Image>().sprite != null)
        {
            if (GetComponent<Slot>().slot.item != null)
            {
                ghost.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EventSystem.current.RaycastAll(eventData, results);

        Slot slot = null;

        foreach (var result in results)
        {
            slot = result.gameObject.GetComponent<Slot>();
            if (slot != null)
            {
                ChangeSlots(slot, GetComponent<Slot>());
                break;
            }
        }

        if (slot == null)
        {
            GameObject clone = Instantiate(prefabItem, spawPos.position, spawPos.rotation);
            Slot _ = GetComponent<Slot>();
            clone.GetComponent<Loot>().loot[0].amount = _.slot.amount;
            clone.GetComponent<Loot>().loot[0].durability = _.slot.amount;
            _.slot.item = null;
            _.slot.amount = 0;
            inventory.UpdateInventory();
            inventoryDisplay.UpdateDisplay();
        }

        Destroy(ghost);
        eventData.pointerDrag.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    private ItemObject GetItemObject()
    {
        ItemObject item = null;
        for (int i = 0; i < inventoryDisplay.slotsUI.Count; i++)
        {
            if (inventoryDisplay.slotsUI[i] == gameObject)
            {             
                for (int j = 0; j < inventory.slots.Count; j++)
                {
                    if (j == i)
                    {
                        item = inventory.slots[j].item;
                        break;
                    }
                }

                break;
            }
        }
        return item;
    }


    public void OnDrop(PointerEventData eventData)
    {
        
    }

    private void ChangeSlots(Slot slot1, Slot slot2)
    {
        ItemObject item2 = slot2.slot.item;
        int amount2 = slot2.slot.amount;
        int durability2 = slot2.slot.durability;

        if (slot1.slot != null)
        {
            if (slot1.slot.item != null)
            {
                ItemObject item1 = slot1.slot.item;
                int amount1 = slot1.slot.amount;
                int durability = slot1.slot.durability;
                slot2.slot.item = item1;
                slot2.slot.amount = amount1;
                slot2.slot.durability = durability; 
                slot1.slot.item = item2;
                slot1.slot.amount = amount2;
                slot1.slot.durability = durability2;
            }
            else
            {
                slot2.slot.item = null;
                slot2.slot.amount = 0;
                slot2.slot.durability = 0;
                slot1.slot = new InventorySlot(item2,amount2,durability2);
            }
        }

        inventory.UpdateInventory();
        inventoryDisplay.UpdateDisplay();
    }
}
