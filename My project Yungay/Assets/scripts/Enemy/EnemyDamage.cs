using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public enum Enemy
    {
        melee, shooting
    }
    public Enemy enemyType;
    public PlayerHealth
        player;
    public float damage;
    
   
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.Damage(damage);

        }
    }


}
