using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Sacks : MonoBehaviour
{
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
            GameObject clone = Instantiate(lootPrefab, transform.position, Quaternion.identity);
            Loot loot = clone.GetComponent<Loot>();
            for (int i = 0; i < items.Count; i++)
            {
                loot.loot.Add(new Item(items[i], 0, 0));
            }
            loot.RandomAmount();

        }
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
