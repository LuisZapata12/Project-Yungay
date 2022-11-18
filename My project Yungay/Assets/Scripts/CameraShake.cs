using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float magnitud;
    [SerializeField] private float duration;
    private Vector3 original;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Shake());
        }
    }
    public IEnumerator Shake()
    {
        original = transform.position;
        float elapse = 0f;

        while(elapse<duration)
        {
            float x = Random.Range(-0.5f, 0.5f) * magnitud;
            float y = Random.Range(-0.1f,0.1f) * magnitud;

            transform.position = new Vector3(original.x - x,original.y - y, original.z);
            elapse += 1 * Time.deltaTime;
            yield return null;
        }
        transform.position = original;
    }
}
