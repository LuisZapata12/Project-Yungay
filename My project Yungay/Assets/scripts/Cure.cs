using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cure : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Inventory inventory;
    public InventoryDisplay inventoryDisplay;
    public ItemObject bandageItem;
    public EquipmentHealing itemHealing;
    private float health;
    public float percentageCure;
    public float speed;
    public GameObject chargeBar;
    public float speedBar;
    private Image image;
    private Coroutine coroutine;

    private void Start()
    {
        inventoryDisplay = InventoryDisplay.instance;
        chargeBar.SetActive(false);
        image = chargeBar.transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        itemHealing = (EquipmentHealing)Hand.currentItem;

        if (itemHealing != null && playerHealth.mb.health < playerHealth.mb.maxHealth && Input.GetMouseButtonDown(0))
        {

            coroutine = StartCoroutine(FillBar());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                image.fillAmount = 0;
                chargeBar.SetActive(false);
            }
        }


    }
    public void Heal()
    {
        health = playerHealth.mb.maxHealth * (itemHealing.restoreHealthPercentage / 100);
        StartCoroutine(healing(playerHealth.mb.health + health));

        inventory.RestItem(Hand.currentItem, 1);
        inventory.RemoveSlot();
        inventoryDisplay.UpdateDisplay();
    }

    IEnumerator healing(float heal)
    {
        while (playerHealth.mb.health < heal)
        {
            playerHealth.mb.health += speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield break;

    }

    IEnumerator FillBar()
    {
        while (image.fillAmount < 1 )
        {
            image.fillAmount += speedBar * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        chargeBar.SetActive(false);
        image.fillAmount = 0;
        yield break;
    }
}