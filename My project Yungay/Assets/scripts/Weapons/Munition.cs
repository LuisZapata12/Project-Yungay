using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Munition : MonoBehaviour
{
    public ItemObject pistol;
    public ItemObject submachine;
    public int nails;
    public int bullets;
    public int chargerNails;
    public int chargerBullets;
    public TMP_Text textNails;
    public TMP_Text textBullets;
    //[HideInInspector]
    public bool thereBullets, thereNails;
    public bool hasItemNails, hasItemBullets;
    public Inventory inventory;
    public ItemObject nailsItem;
    public ItemObject bulletsItem;
    public InventoryDisplay inventoryDisplay;
    private Hand hand;

    private void Start()
    {
        hand = GetComponent<Hand>();
    }

    private void FixedUpdate()
    {
        if (Hand.canAim)
        {
            //textNails.text = chargerNails.ToString();
            //CheckAmmo();
            //hasItemBullets = inventory.CheckItem(bulletsItem);
            //hasItemNails = inventory.CheckItem(nailsItem);
            //InventoryAmmo();
        }
    }

    public void InventoryAmmo()
    {
        if (hasItemNails)
        {
            int i = inventory.GetItemIndex(nailsItem);
            nails = inventory.CheckAmount(inventory.slots[i].item);
        }

        if (hasItemBullets)
        {
            int i = inventory.GetItemIndex(bulletsItem);
            bullets = inventory.CheckAmount(inventory.slots[i].item);
        }

    }

    #region basura
    //public void RechargeAmmo(ItemObject itemObject)
    //{
    //    EquipmentRange _ = (EquipmentRange)itemObject;
    //    if (itemObject == pistol)
    //    {
    //        if (rangeWeapons.ammoPistol == true && bullets >= 1)
    //        {

    //            if (bullets > _.chargerBulletsMax && chargerBullets < _.chargerBulletsMax)
    //            {
    //                for (int i = chargerBullets; i < _.chargerBulletsMax; i++)
    //                {
    //                    chargerBullets++;
    //                    inventory.RestItem(bulletsItem, 1);
    //                    inventoryDisplay.UpdateDisplay();
    //                }
    //            }
    //            else
    //            {
    //                for (int i = bullets; i > 0; i--)
    //                {
    //                    if (chargerBullets < _.chargerBulletsMax)
    //                    {
    //                        chargerBullets++;
    //                        inventory.RestItem(bulletsItem, 1);
    //                        inventoryDisplay.UpdateDisplay();
    //                    }

    //                }
    //            }
    //        }
    //        else if (rangeWeapons.ammoPistol == false && nails >= 1)
    //        {
    //            if (nails > _.chargerNailsMax && chargerNails < _.chargerNailsMax)
    //            {
    //                for (int i = chargerNails; i < _.chargerNailsMax; i++)
    //                {
    //                    chargerNails++;
    //                    inventory.RestItem(nailsItem, 1);
    //                    inventoryDisplay.UpdateDisplay();
    //                }
    //            }
    //            else
    //            {
    //                for (int i = nails; i > 0; i--)
    //                {
    //                    if (chargerNails < _.chargerNailsMax)
    //                    {
    //                        chargerNails++;
    //                        inventory.RestItem(nailsItem, 1);
    //                        inventoryDisplay.UpdateDisplay();
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    if (itemObject == submachine)
    //    {
    //        if (rangeWeapons.ammoPistol == true && bullets >= 1)
    //        {
    //            if (bullets > _.chargerBulletsMax && chargerBullets < _.chargerBulletsMax)
    //            {
    //                for (int i = chargerBullets; i < _.chargerBulletsMax; i++)
    //                {
    //                    chargerBullets++;
    //                    inventory.RestItem(bulletsItem, 1);
    //                    inventoryDisplay.UpdateDisplay();

    //                }
    //            }
    //            else
    //            {
    //                for (int i = bullets; i > 0; i--)
    //                {
    //                    if (chargerBullets < _.chargerBulletsMax)
    //                    {
    //                        chargerBullets++;
    //                        inventory.RestItem(bulletsItem, 1);
    //                        inventoryDisplay.UpdateDisplay();
    //                    }

    //                }
    //            }
    //        }
    //    }

    //    //switch (itemObject)
    //    //{
    //    //    case pistol:
    //    //        if (pistol.ammo == true && bullets >= 1)
    //    //        {

    //    //            if (bullets > pistol.pistol.chargerBulletsMax && chargerBullets < pistol.pistol.chargerBulletsMax)
    //    //            {
    //    //                for (int i = chargerBullets; i < pistol.pistol.chargerBulletsMax; i++)
    //    //                {
    //    //                    chargerBullets++;
    //    //                    inventory.RestItem(bulletsItem, 1);
    //    //                    inventoryDisplay.UpdateDisplay();
    //    //                }
    //    //            }
    //    //            else
    //    //            {
    //    //                for (int i = bullets; i > 0; i--)
    //    //                {
    //    //                    if (chargerBullets < pistol.pistol.chargerBulletsMax)
    //    //                    {
    //    //                        chargerBullets++;
    //    //                        inventory.RestItem(bulletsItem, 1);
    //    //                        inventoryDisplay.UpdateDisplay();
    //    //                    }

    //    //                }
    //    //            }
    //    //        }
    //    //        else if (pistol.ammo == false && nails >= 1)
    //    //        {
    //    //            if (nails > pistol.pistol.chargerNailsMax && chargerNails < pistol.pistol.chargerNailsMax)
    //    //            {
    //    //                for (int i = chargerNails; i < pistol.pistol.chargerNailsMax; i++)
    //    //                {
    //    //                    chargerNails++;
    //    //                    inventory.RestItem(nailsItem, 1);
    //    //                    inventoryDisplay.UpdateDisplay();
    //    //                }
    //    //            }
    //    //            else
    //    //            {
    //    //                for (int i = nails; i > 0; i--)
    //    //                {
    //    //                    if (chargerNails < pistol.pistol.chargerNailsMax)
    //    //                    {
    //    //                        chargerNails++;
    //    //                        inventory.RestItem(nailsItem, 1);
    //    //                        inventoryDisplay.UpdateDisplay();
    //    //                    }
    //    //                }
    //    //            }
    //    //        }
    //    //        break;
    //    //    case 2:
    //    //        if (submachine.ammo == true && bullets >= 1)
    //    //        {
    //    //            if (bullets > submachine.submachine.chargerBulletsMax && chargerBullets < submachine.submachine.chargerBulletsMax)
    //    //            {
    //    //                for (int i = chargerBullets; i < submachine.submachine.chargerBulletsMax; i++)
    //    //                {
    //    //                    chargerBullets++;
    //    //                    inventory.RestItem(bulletsItem, 1);
    //    //                    inventoryDisplay.UpdateDisplay();

    //    //                }
    //    //            }
    //    //            else
    //    //            {
    //    //                for (int i = bullets; i > 0; i--)
    //    //                {
    //    //                    if (chargerBullets < submachine.submachine.chargerBulletsMax)
    //    //                    {
    //    //                        chargerBullets++;
    //    //                        inventory.RestItem(bulletsItem, 1);
    //    //                        inventoryDisplay.UpdateDisplay();
    //    //                    }

    //    //                }
    //    //            }
    //    //        }
    //    //        break;
    //    //}
    //}
    #endregion
    public void ReloadMunition(ItemObject item)
    {/*
        MunitonSlot result = item.munitions.Find();
        if(item.munitions[result].munition== hand.currentMunition)
        {
            int maxCharge = item.munitions[result].charge;
            if (inventory.CheckAmount(hand.currentMunition) < maxCharge)
            {
                for (int m = inventory.CheckAmount(hand.currentMunition); m > 0; m--)
                {
                    inventory.RestItem(item, 1);
                    inventory.RemoveSlot();
                }
            }

        }*/
        if (inventory.CheckItem(item))
        {
            for (int i = 0; i < hand.weaponSlots.Count; i++)
            {
                if (hand.weaponSlots[i].weapon == Hand.currentItem)
                {
                    for (int j = 0; j < hand.weaponSlots[i].munitions.Count; j++)
                    {
                        if (hand.weaponSlots[i].munitions[j].munition == item)
                        {
                            EquipmentRange _ = (EquipmentRange)Hand.currentItem;
                            int maxCharge = 0;
                            for (int k = 0; k < _.munitions.Count; k++)
                            {
                                if (_.munitions[k].munition == item)
                                {
                                    maxCharge = _.munitions[k].charge;
                                    break;
                                }
                            }
                            /*
                            if (inventory.CheckAmount(hand.currentMunition) < maxCharge)
                            {
                                for (int m = inventory.CheckAmount(hand.currentMunition); m > 0; m--)
                                {
                                    hand.weaponSlots[i].munitions[j].charge++;
                                    inventory.RestItem(item, 1);
                                    inventory.RemoveSlot();
                                }
                            }
                            else
                            {
                                if (inventory.slots[inventory.GetItemIndex(item)].amount > maxCharge)
                                {
                                    int amount = maxCharge - hand.weaponSlots[i].munitions[j].charge;
                                    hand.weaponSlots[i].munitions[j].charge = maxCharge;
                                    inventory.RestItem(item, amount);
                                    inventory.RemoveSlot();
                                }
                                else
                                {
                                    int amount = 0;

                                    for (int l = hand.weaponSlots[i].munitions[j].charge; l < maxCharge; l++)
                                    {
                                        hand.weaponSlots[i].munitions[j].charge++;
                                        amount++;
                                        inventory.RestItem(item, 1);
                                        inventory.RemoveSlot();
                                    }
                                }
                            }
                            */
                            if (inventory.CheckAmount(hand.currentMunition) - (maxCharge - hand.weaponSlots[i].munitions[j].charge) > 0)
                            {
                                inventory.RestItem(item, maxCharge - hand.weaponSlots[i].munitions[j].charge);
                                hand.weaponSlots[i].munitions[j].charge = maxCharge;
                                Debug.Log(hand.GetCharge(hand.currentMunition));
                            }
                            else
                            {
                                hand.weaponSlots[i].munitions[j].charge += inventory.CheckAmount(hand.currentMunition);
                                inventory.RestItem(item, inventory.CheckAmount(hand.currentMunition));
                            }
                                inventoryDisplay.UpdateDisplay();
                                break;
                        }
                    }
                    break;
                }
            }
           
        }
    }

    public bool CheckAmmo(ItemObject item)
    {
        bool haveMunition = false;

        for (int i = 0; i < hand.weaponSlots.Count; i++)
        {
            if (hand.weaponSlots[i].weapon == Hand.currentItem)
            {
                for (int j = 0; j < hand.weaponSlots[i].munitions.Count; j++)
                {
                    if (hand.weaponSlots[i].munitions[j].munition == item)
                    {
                        if (hand.weaponSlots[i].munitions[j].charge > 0)
                        {
                            haveMunition = true; Debug.Log(haveMunition);
                            break;
                        }
                    }
                }

                break;
            }
        }


        return haveMunition;

    }

    public void RestMunition(ItemObject item)
    {
        for (int i = 0; i < hand.weaponSlots.Count; i++)
        {
            if (hand.weaponSlots[i].weapon == Hand.currentItem)
            {
                for (int j = 0; j < hand.weaponSlots[i].munitions.Count; j++)
                {
                    if (hand.weaponSlots[i].munitions[j].munition == item)
                    {
                        hand.weaponSlots[i].munitions[j].charge--;
                        break;
                    }
                }

                break;
            }
        }
    }
}