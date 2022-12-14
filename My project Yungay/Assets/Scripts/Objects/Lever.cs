using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lever : MonoBehaviour
{
    public int leverID;
    public int id;
    public Animator anim;
    public Animator fade;

    public ItemObject axe;
    public Inventory inventory;
    public InventoryDisplay inventoryDisplay;
    public DataChekpoint dataChekpoint;
    public static bool endTravel = false;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        inventoryDisplay = GameObject.Find("Canvas").GetComponent<InventoryDisplay>();
        EventManager.current.useLeverEvent += StartCinematic;

    }
    private void Update()
    {
        if (!dataChekpoint)
        {
            dataChekpoint = GameObject.Find("Checkpoint").GetComponent<DataChekpoint>();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            inventory.UpdateInventory();
            inventory.CheckItem(axe);
            if (inventory.CheckItem(axe) && Input.GetKeyDown(KeyCode.E))
            {
                inventory.RestItem(axe, 1);
                inventory.RemoveSlot();
                inventoryDisplay.UpdateDisplay();

                dataChekpoint.CheckInventory();
                EventManager.current.StartUseLeverEvent(leverID);
                anim.SetBool("isUse", true);
            }
           
        }
    }


    private void StartCinematic(int id)
    {
        if (id == this.id)
        {
            StartCoroutine(Fading());
        }

    }

    private void OnDisable()
    {
        EventManager.current.useLeverEvent -= StartCinematic;
    }


    IEnumerator Fading()
    {
        fade.SetBool("isFade", true);
        yield return new WaitForSeconds(3f);
        fade.SetBool("isFade", false);
        endTravel = true;
        SceneManager.LoadScene("Nivel1Prototipo");

    }

    public void TravelSound()
    {
        AudioManager.Instance.PlaySFX("Car");
    }
}
