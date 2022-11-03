using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Noise : MonoBehaviour
{
    private float timer;
    private Light lightComponent;
    public float maxSeconds;
    [SerializeField] private UnityEvent<float> randomNoise; 
    
    
    // Start is called before the first frame update
    void Start()
    {
        lightComponent = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        randomNoise.Invoke(Random.value * maxSeconds);
        //lightComponent.intensity = Random.Range(0f, 2.5f);
    }
}
