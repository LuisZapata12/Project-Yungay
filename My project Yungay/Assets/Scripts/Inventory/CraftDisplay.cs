using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftDisplay : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Inventory inventory;
    public InventoryDisplay inventoryDisplay;
    public CraftRecipes recipe;
    private Image image, imageChild;
    private bool canCraft, isClick;
    private Coroutine coroutine;
    public float chargeSpeed;
    private void Awake()
    {
        image = GetComponent<Image>();
        image.color = new Color32(255, 255, 255, 100);
        imageChild = transform.GetChild(0).GetComponent<Image>();
        imageChild.fillAmount = 0f;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (canCraft && Input.GetMouseButtonDown(0))
        {
            coroutine = StartCoroutine(ChargeImage(chargeSpeed));
            isClick = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(coroutine);
            imageChild.fillAmount = 0f;
            isClick = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canCraft && !isClick)
        {
            image.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            image.color = new Color32(255, 255, 255, 100);
        }
    }

    //public void ClickImage()
    //{
    //    if (canCraft)
    //    {
    //        coroutine = StartCoroutine(ChargeImage());
    //        isClick = true;
    //    }
    //}

    //public void UpClick()
    //{
    //    if (coroutine != null)
    //    {
    //        StopCoroutine(coroutine);
    //    }
    //    imageChild.fillAmount = 0f;
    //    isClick = false;
    //}

    public void CheckIsCraftable()
    {
        if (inventory.CheckItems(recipe))
        {
            if (inventory.CheckItem(recipe.result))
            {
                if (inventory.slots[inventory.GetItemIndex(recipe.result)].amount < inventory.slots[inventory.GetItemIndex(recipe.result)].item.maxStack)
                {
                    canCraft = true;
                }
                else
                {
                    canCraft = false;
                }
                
            }
            else
            {
                canCraft = true;
            }
        }
        else
        {
            canCraft = false;
        }
    }

    public IEnumerator ChargeImage(float speed)
    {
        while (imageChild.fillAmount < 1f)
        {
            imageChild.fillAmount += 0.1f * speed;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        imageChild.fillAmount = 1f;
        inventory.CraftItem(recipe);
        inventory.UpdateInventory();
        inventoryDisplay.UpdateCraftDisplay();
        CheckIsCraftable();
    }
}
