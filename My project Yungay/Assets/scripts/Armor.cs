using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armor : MonoBehaviour
{
    public PlayerModel playerHealth;
    public Inventory inventory;
    public InventoryDisplay inventoryDisplay;
    public EquipmentBuff armorItem;
    private bool hasArmor;
    public float timeArmor;
    public float timer;
    public float maxTime;
    private bool charge;
    public GameObject chargeBar;
    private Image image;
    public float speedBar;
    private Coroutine coroutine;

    private void Start()
    {
        inventoryDisplay = InventoryDisplay.instance;
        chargeBar.SetActive(false);
        image = chargeBar.transform.GetChild(0).GetComponent<Image>();
    }
    private void Update()
    {
        //if (hasArmor && Input.GetMouseButtonDown(0))
        //{
        //    charge = true;
        //}
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    if (timer < maxTime)
        //    {
        //        timer = 0;
        //        charge = false;
        //    }
        //}

        //if (timer >= maxTime)
        //{
        //    timer = 0;
        //    charge = false;
        //    Doped();
        //}

        //if (charge)
        //{
        //    timer += Time.deltaTime;
        //}

        armorItem = Hand.currentItem as EquipmentBuff;

        if (armorItem && Input.GetMouseButtonDown(0))
        {
            coroutine = StartCoroutine(FillBar());
        }
        else if(coroutine != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                StopCoroutine(coroutine);
                image.fillAmount = 0;
                chargeBar.SetActive(false);
            }
        }
    }
    public void Doped(float value)
    {
        playerHealth.armor = value;
        inventory.RestItem(armorItem, 1);
        inventory.RemoveSlot();
        inventoryDisplay.UpdateDisplay();
    }

    IEnumerator FillBar()
    {
        while (image.fillAmount < 1)
        {
            chargeBar.SetActive(true);
            image.fillAmount += speedBar * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Doped(armorItem.armor);
        chargeBar.SetActive(false);
        image.fillAmount = 0;
    }
}
