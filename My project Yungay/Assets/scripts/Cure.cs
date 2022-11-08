using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        inventoryDisplay = InventoryDisplay.instance;
    }

    private void Update()
    {
        if (inventory.CheckItem(bandageItem) && playerHealth.mb.health < playerHealth.mb.maxHealth && Input.GetMouseButtonDown(0)) 
        {
            charge = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            if(timer < maxTime)
            {
                timer = 0;
                playerHealth.mb.state = PlayerModel.State.idle;
                charge = false;
            }
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
            playerHealth.mb.state = PlayerModel.State.healing;
        }
    }
    public void Heal()
    {
        health = playerHealth.mb.maxHealth * (percentageCure/100);
        StartCoroutine(healing(playerHealth.mb.health + health));

        playerHealth.mb.state = PlayerModel.State.idle;
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
}
