using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHUD : MonoBehaviour
{
    private RectTransform rect;
    public List<RectTransform> slotsui = new();

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();    
    }

    // Update is called once per frame
    void Update()
    {
        rect.position = slotsui[Hand.slotIndex].position;
    }
}
