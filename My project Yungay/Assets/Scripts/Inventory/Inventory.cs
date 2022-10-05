using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSlots;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public InventoryDisplay inventoryDisplay;
    public ItemObject currentItem;

    private void Awake()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new InventorySlot(null,0));
        }
    }

    public void AddItem(Item item, ItemObject itemObject, int amount)
    {
        bool hasItem = CheckItem(itemObject);

        if (hasItem)
        {
            int index = GetItemIndex(itemObject);

            if (slots[index].amount + amount < slots[index].item.maxStack)
            {
                slots[index].AddAmount(amount);
                item.amount = 0;
            }
            else
            {
                item.amount = (slots[index].amount + amount) - slots[index].item.maxStack;
                slots[index].amount = slots[index].item.maxStack;
            }
        }
        else
        {
            if (slots.Count <= maxSlots)
            {
                for (int i = 0; i < slots.Count; i++)
                {
                    if (slots[i].item == null)
                    {
                        slots[i].item = itemObject;
                        slots[i].amount = amount;
                        break;
                    }

                }

                if (item != null)
                {
                    item.amount = 0;
                }
            }
            else
            {
                Debug.Log("No hay espacio en el inventario");
            }
        }

        inventoryDisplay.UpdateDisplay();

    }

    public void CraftItem(CraftRecipes recipe)
    {
        bool hasMaterials = false;
        int materialCount = recipe.materials.Count;
        int count = 0;
        for (int i = 0; i < recipe.materials.Count; i++)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.item == recipe.materials[i].item)
                {
                    if (slot.amount >= recipe.materials[i].amount)
                    {
                        count++;
                    }
                }
            }
        }

        if (count == materialCount)
        {
            hasMaterials = true;
        }
        else
        {
            Debug.Log("No tienes los materiales suficientes");
        }

        if (hasMaterials)
        {
            bool hasItem = CheckItem(recipe.result);
            if (hasItem)
            {
                int index = GetItemIndex(recipe.result);
                if (slots[index].amount + recipe.amount < recipe.result.maxStack)
                {
                    Craft(recipe, count);
                }
            }
            else
            {
                Craft(recipe, count);
            }
        }
    }

    private void Craft(CraftRecipes recipe, int count)
    {
        for (int i = 0; i < recipe.materials.Count; i++)
        {
            foreach (InventorySlot slot in slots)
            {
                if (slot.item == recipe.materials[i].item)
                {
                    slot.amount -= recipe.materials[i].amount;
                }
            }
        }

        for (int i = 0; i < count; i++)
        {
            RemoveSlot();
        }
        AddItem(null, recipe.result, recipe.amount);
    }
    private void RemoveSlot()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].amount == 0)
            {
                slots[i].item = null;
                slots[i].amount = 0;
            }
        }
        inventoryDisplay.UpdateDisplay();
    }

    public bool CheckItem(ItemObject item)
    {
        bool hasItem = false;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] != null)
            {
                if (slots[i].item == item)
                {
                    hasItem = true;
                    break;
                }
                else
                {
                    hasItem = false;
                }

            }
        }

        return hasItem;
    }

    public int CheckAmount(ItemObject item)
    {
        int amount = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] != null)
            {
                if (slots[i].item == item)
                {
                    amount = slots[i].amount;
                    break;
                }
            }
        }

        return amount;
    }

    public int GetItemIndex(ItemObject item)
    {
        int index = 0;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] != null)
            {
                if (slots[i].item == item)
                {
                    index = i;
                    break;
                }
            }
        }

        return index;
    }
    public void RestItem(ItemObject item, int value)
    {
        bool hasItem = CheckItem(item);
        if (hasItem)
        {
            int index = GetItemIndex(item);
            slots[index].RestAmount(value);
        }
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlot slot = inventoryDisplay.slotsUI[i].GetComponent<Slot>().slot;
            if (slot != null)
            {
                if (slot.item != null)
                {
                    slots[i].item = slot.item;
                    slots[i].amount = slot.amount;
                }
                else
                {
                    slots[i].item = null;
                    slots[i].amount = 0;
                }
            }
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amount;

    public InventorySlot(ItemObject item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

    public void RestAmount(int value)
    {
        amount -= value;
    }
}