using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sacks : MonoBehaviour
{
    public GameObject boxFullPieces;
    public GameObject lootPrefab;
    public List<ItemObject> items = new List<ItemObject>();
    private Inventory inventory;
    public TMP_Text  text;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Destroy()
    {
        if (items.Count != 0)
        {
            //GameObject clone = Instantiate(lootPrefab, transform.position, Quaternion.identity);
            //Loot loot = clone.GetComponent<Loot>();

            for (int i = 0; i < items.Count; i++)
            {
                GameObject clone = Instantiate(items[i].prefab, transform.position, Quaternion.identity);
                Loot loot = clone.GetComponent<Loot>();
                loot.RandomAmount();
            }

        }
        //Instantiate(boxFullPieces, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void DestroyByOthers()
    {
        //Instantiate(boxFullPieces, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knife"))
        {
            Destroy();
            int index = inventory.GetItemIndex(Hand.currentItem);
            inventory.slots[index].durability -= 1;
            inventory.RemoveSlotDurability();
        }
        if (!other.CompareTag("Player"))
        {
           
            StartCoroutine(showText());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            Destroy();
            Loot loot = collision.gameObject.GetComponent<Loot>();
            loot.loot[0].durability--;
        }
        if (!collision.gameObject.CompareTag("Player"))
        {
          
            StartCoroutine(showText());
        }
    }

    IEnumerator showText()
    {
        text.text = "Necesito un cuchillo";
        yield return new WaitForSeconds(2f);
        text.text = "";
    }
}
