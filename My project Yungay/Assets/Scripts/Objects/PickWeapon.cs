using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWeapon : MonoBehaviour
{
    Rigidbody rdbd;
    private EquipmentMelee item;
    private Loot loot;
    public bool onSurface;

    private void Start()
    {
        onSurface = false;
        rdbd = GetComponent<Rigidbody>();
        loot = GetComponent<Loot>();
        item = loot.loot[0].item as EquipmentMelee;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            rdbd.isKinematic = true;
            onSurface = true;
            //loot.loot[0].durability--;
            //if (loot.loot[0].durability <= 0)
            //{
            //    Destroy(gameObject);
            //}
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            onSurface = true;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!onSurface)
            {
                rdbd.isKinematic = true;
                loot.loot[0].durability -= 7;
                if (loot.loot[0].durability <= 0)
                {
                    AudioManager.Instance.PlaySFX("Broke");
                    Destroy(gameObject);
                }
                else
                {
                    transform.SetParent(collision.transform);
                }
                EnemyHealth _ = collision.gameObject.GetComponent<EnemyHealth>();
                if (gameObject.tag == "Axe" || gameObject.tag == "Spear")
                {
                    _.lifeE(100);
                }
                else if (gameObject.tag == "Knife")
                {
                    _.lifeE(75);
                }
            }
        }
    }
}
