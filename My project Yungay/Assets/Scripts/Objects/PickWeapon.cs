using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWeapon : MonoBehaviour
{
    Rigidbody rdbd;
    private EquipmentMelee item;
    private Loot loot;

    private void Start()
    {
        rdbd = GetComponent<Rigidbody>();
        loot = GetComponent<Loot>();
        item = loot.loot[0].item as EquipmentMelee;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            rdbd.isKinematic = true;
            loot.loot[0].durability--;
            if (loot.loot[0].durability <= 0)
            {
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            rdbd.isKinematic = true;
            loot.loot[0].durability--;
            collision.gameObject.GetComponent<EnemyHealth>().lifeE(item.damage);
            if (loot.loot[0].durability <= 0)
            {
                Destroy(gameObject);
            }
            transform.SetParent(collision.transform);            
        }
    }
}
