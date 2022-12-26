using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private Color colorDefault;
    public Color newColor;
    private Renderer rdr;
    
    // Start is called before the first frame update
    void Start()
    {
        rdr = GetComponent<Renderer>();
        colorDefault = rdr.material.color;
    }

    private void OnMouseOver()
    {
        rdr.material.color = newColor;
    }

    private void OnMouseExit()
    {
        rdr.material.color = colorDefault;
    }
}
