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
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            rdbd.isKinematic = true;
            transform.SetParent(collision.transform);
            collision.gameObject.GetComponent<EnemyHealth>().lifeE(item.damage);
        }
    }
}
