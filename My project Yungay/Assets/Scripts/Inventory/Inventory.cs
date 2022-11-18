using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int maxSlots;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public InventoryDisplay inventoryDisplay;
    public HudTest hudTest;

    private void Awake()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            slots.Add(new InventorySlot(null, 0,0));
        }
        hudTest = GameObject.Find("Canvas").GetComponent<HudTest>();
    }

    public void AddItem(Item item, ItemObject itemObject, int amount, int durability)
    {
        bool hasItem = CheckItem(itemObject);

        if (hasItem)
        {
            int index = GetItemIndex(itemObject);

            if (slots[index].amount + amount < slots[index].item.maxStack)
            {
                slots[index].AddAmount(amount);
                hudTest.TextHud(itemObject, amount);
                if (item != null)
                {
                    item.amount = 0;
                }
                

            }
            else
            {
                if (item != null)
                {
                    item.amount = (slots[index].amount + amount) - slots[index].item.maxStack;
                }
                slots[index].amount = slots[index].item.maxStack;
                hudTest.TextHud(itemObject, slots[index].item.maxStack - slots[index].amount);
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
                        slots[i].durability = durability;
                        break;
                    }

                }

                if (item != null)
                {
                    item.amount = 0;
                }

                hudTest.TextHud(itemObject, amount);
            }
            else
            {
                Debug.Log("No hay espacio en el inventario");
            }
        }

        inventoryDisplay.UpdateDisplay();

    }

    public bool InventorySpace()
    {
        bool hasSpace = false;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                hasSpace = true;
                break;
            }
        }
        return hasSpace;
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

        if (recipe.result.type == ItemType.Equipment)
        {
            EquipmentItem _ = (EquipmentItem)recipe.result;
            if (_.equipmentType == EquipmentType.Melee)
            {
                EquipmentMelee item = (EquipmentMelee)recipe.result;
                AddItem(null, recipe.result, recipe.amount, item.durability);
                Debug.Log(item.durability);
            }
            else
            {
                AddItem(null, recipe.result, recipe.amount, 0);
            }
        }
        else
        {
            AddItem(null, recipe.result, recipe.amount, 0);
        }
    }
    public void RemoveSlot()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].amount == 0)
            {
                slots[i].item = null;
                slots[i].amount = 0;
                slots[i].durability = 0;
            }
        }
        inventoryDisplay.UpdateDisplay();
    }

    public void RemoveSlotDurability()
    {
        for (int i = 0; i < slots.Count; i++)
        {
                EquipmentItem _ = slots[i].item as EquipmentItem;
                
                if (_!=null && _.equipmentType == EquipmentType.Melee)
                {
                    if (slots[i].durability <= 0)
                    {
                        slots[i].item = null;
                        slots[i].amount = 0;
                        slots[i].durability = 0;
                    }
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

    public bool CheckItems(CraftRecipes recipe)
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

        return hasMaterials;
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
    public void ReturnItem(ItemObject item, int value)
    {
        bool hasItem = CheckItem(item);
        if (hasItem)
        {
            int index = GetItemIndex(item);
            slots[index].AddAmount(value);
        }
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
    public int durability;

    public InventorySlot(ItemObject item, int amount, int durability)
    {
        this.item = item;
        this.amount = amount;
        this.durability = durability;
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