using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject boxFullPieces;
    public GameObject lootPrefab;
    public List<ItemObject> items = new List<ItemObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroy()
    {
        Instantiate(boxFullPieces, transform.position, Quaternion.identity);

        if (items.Count != 0)
        {
            GameObject clone = Instantiate(lootPrefab, transform.position, Quaternion.identity);
            Loot loot = clone.GetComponent<Loot>();
            for (int i = 0; i < items.Count; i++)
            {
                loot.loot.Add(new Item(items[i], 0,0));
            }
            loot.RandomAmount();
            
        }
        Destroy(this.gameObject);
    }

    public void DestroyByOthers()
    {
        Instantiate(boxFullPieces, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Axe"))
        {
            Destroy();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Axe"))
        {
            Destroy();
        }
    }
}
