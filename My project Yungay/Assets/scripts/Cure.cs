using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Cure : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Inventory inventory;
    public InventoryDisplay inventoryDisplay;
    private float health;
    public float speed;
    public EquipmentHealing itemHealing;
    public GameObject chargeBar;
    public Image feed;
    public float speedBar;
    private Image image;
    private bool a;
    private bool b;
    private Coroutine coroutine;

    private void Start()
    {
        inventoryDisplay = InventoryDisplay.instance;
        chargeBar.SetActive(false);
        image = chargeBar.transform.GetChild(0).GetComponent<Image>();
        feed.gameObject.SetActive(false);
    }

    private void Update()
    {
        itemHealing = Hand.currentItem as EquipmentHealing;

        if (itemHealing && playerHealth.mb.health < playerHealth.mb.maxHealth && Input.GetMouseButtonDown(0) && !b)
        {
            coroutine = StartCoroutine(FillBar());
        }
        else if (coroutine!=null)
        {
            if (Input.GetMouseButtonUp(0) && !Dialogue.pressInit)
            {
                StopCoroutine(coroutine);
                image.fillAmount = 0;
                chargeBar.SetActive(false);
            }
            if (Input.GetMouseButtonUp(0) && a == false)
            {
             feed.gameObject.SetActive(false);
             playerHealth.takeHeal = false;
            }
        }
    }
    public void Heal()
    {
        StartCoroutine(healing(playerHealth.mb.health + health));

        inventory.RestItem(Hand.currentItem, 1);
        inventory.RemoveSlot();
        inventoryDisplay.UpdateDisplay();
        
    }

    IEnumerator healing(float heal)
    {
        if(heal > playerHealth.mb.maxHealth)
        {
            heal =  playerHealth.mb.maxHealth;
        }
        //while (playerHealth.mb.health < heal)
        //{
        //    playerHealth.mb.health += speed * Time.deltaTime;
        //    yield return new WaitForEndOfFrame();
        //    a = true;
        //    b = true;
        //}
        playerHealth.mb.health = heal;
        feed.gameObject.SetActive(false);
        a = false;
        b = false;
        playerHealth.takeHeal = false;
        yield return 0;
    }

    IEnumerator FillBar()
    {
        health = playerHealth.mb.maxHealth * (itemHealing.restoreHealthPercentage / 100);
        feed.fillAmount =(playerHealth.mb.health + health)/playerHealth.mb.maxHealth;
        feed.gameObject.SetActive(true);
        while (image.fillAmount < 1 )
        {
            playerHealth.takeHeal = true;
            chargeBar.SetActive(true);
            image.fillAmount += speedBar * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        chargeBar.SetActive(false);
        Heal();
        image.fillAmount = 0;
    }
}