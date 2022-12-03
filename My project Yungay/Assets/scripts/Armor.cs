using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Armor : MonoBehaviour
{
    public PlayerModel playerHealth;
    public Inventory inventory;
    public InventoryDisplay inventoryDisplay;
    public ItemObject armorItem;
    private bool hasArmor;
    public float timeArmor;
    public float timer;
    public float maxTime;
    private bool charge;
    public GameObject chargeBar;
    private Image image;
    public float speedBar;

    private void Start()
    {
        inventoryDisplay = InventoryDisplay.instance;
    }
    private void Update()
    {
        hasArmor = inventory.CheckItem(armorItem);
        if (hasArmor && Input.GetMouseButtonDown(0))
        {
            charge = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (timer < maxTime)
            {
                timer = 0;
                charge = false;
            }
        }

        if (timer >= maxTime)
        {
            timer = 0;
            charge = false;
            Doped();
        }

        if (charge)
        {
            timer += Time.deltaTime;
        }
    }
    public void Doped()
    {
        playerHealth.armor = timeArmor;
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
        chargeBar.SetActive(false);
        image.fillAmount = 0;
    }
}
