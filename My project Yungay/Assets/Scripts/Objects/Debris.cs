using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(Physics.gravity);
        Destroy(this.gameObject, 4f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           playerHealth.Damage(5f);
           Destroy(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
