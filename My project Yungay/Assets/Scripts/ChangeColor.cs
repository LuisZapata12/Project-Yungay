using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private Color colorDefault;
    public Color newColor;
    private Renderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        colorDefault = renderer.material.color;
    }

    private void OnMouseOver()
    {
        renderer.material.color = newColor;
    }

    private void OnMouseExit()
    {
        renderer.material.color = colorDefault;
    }
}
