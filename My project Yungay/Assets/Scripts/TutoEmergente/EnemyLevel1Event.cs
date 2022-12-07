using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLevel1Event : MonoBehaviour
{
    public EnemyHealth enemyHealth;
    // Start is called before the first frame update
    void Awake()
    {
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth.dead)
        {
            Tutorial.instance.ShowTutoImage(0);
        }
    }
}
