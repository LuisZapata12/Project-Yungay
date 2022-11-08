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
    private bool hasBandage;
    private float health;
    public float percentageCure;
    public float speed;
    public float timer;
    public float maxTime;
    private bool charge;
    public GameObject chargeBar;
    public float speedBar;
    public Image image;
    private Coroutine coroutine;

    private void Start()
    {
        inventoryDisplay = InventoryDisplay.instance;
        chargeBar.SetActive(false);
        image = chargeBar.transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        if (inventory.CheckItem(bandageItem) && playerHealth.mb.health < playerHealth.mb.maxHealth && Input.GetMouseButtonDown(0)) 
        {
            charge = true;
            coroutine = StartCoroutine(FillBar());
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(timer < maxTime)
            {
                timer = 0;
                charge = false;
            }

            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                image.fillAmount = 0;
                chargeBar.SetActive(false);
            }

        }

        if (Input.GetMouseButtonDown(0))
        {
            coroutine = StartCoroutine(FillBar());
        }

        if (Input.GetMouseButtonUp(0) && coroutine != null)
        {
            StopCoroutine(coroutine);
            image.fillAmount = 0;
            chargeBar.SetActive(false);

        }

        if (timer >= maxTime)
        {
            timer = 0;
            charge = false;
            Heal();
        }

        if (charge)
        {
            timer += Time.deltaTime;
        }
    }
    public void Heal()
    {
        health = playerHealth.mb.maxHealth * (percentageCure/100);
        StartCoroutine(healing(playerHealth.mb.health + health));

        //playerHealth.mb.health += health;
        inventory.RestItem(bandageItem, 1);
        inventory.RemoveSlot();
        inventoryDisplay.UpdateDisplay();
    }

    IEnumerator healing(float heal)
    {
        while(playerHealth.mb.health < heal)
        {
            playerHealth.mb.health += speed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield break;

    }

    IEnumerator FillBar()
    {
        while(image.fillAmount < 1)
        {
            chargeBar.SetActive(true);
            image.fillAmount += speedBar * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        chargeBar.SetActive(false);
        image.fillAmount = 0;
        yield break;
    }
}
