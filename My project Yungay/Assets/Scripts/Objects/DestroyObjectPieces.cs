using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectPieces : MonoBehaviour
{
    private MeshCollider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.GetComponent<MeshCollider>();
        if(collider != null)
        {
            StartCoroutine(Pieces());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 10f);
    }

    IEnumerator Pieces()
    {
        
        yield return new WaitForSeconds(4f);
        this.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(0.25f);
        collider.isTrigger = true;
    }
}
