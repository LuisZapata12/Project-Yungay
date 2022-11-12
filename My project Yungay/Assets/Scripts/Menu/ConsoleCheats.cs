using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleCheats : MonoBehaviour
{
    public GameObject consoleCanvas;
    public GameObject ObjectScroll;
    public bool isOpen = false;
    public Transform spwanPos;
    // Start is called before the first frame update
    void Start()
    {
        consoleCanvas.SetActive(false);
        ObjectScroll.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5) && !isOpen)
        {
            consoleCanvas.SetActive(true);
            isOpen = true;
        }
        else if(Input.GetKeyDown(KeyCode.F5) && isOpen)
        {
            consoleCanvas.SetActive(false);
            isOpen = false;
        }
    }

    public void GodMode(bool tog)
    {
        if (tog)
        {
            Debug.Log("GodMode");
        }
        else
        {
            Debug.Log("NO GodMode");
        }
    }

    public void UnlimetdAmmo(bool tog)
    {
        if (tog)
        {
            Debug.Log("UnlimetdAmmo");
        }
        else
        {
            Debug.Log("No UnlimetdAmmo");
        }
        
    }

    public void NoClip(bool tog)
    {
        if (tog)
        {
            Debug.Log("NoClip");
        }
        else
        {
            Debug.Log("No NoClip");
        }
    }

    public void ShowSl(bool tog)
    {
        if (tog)
        {
            Debug.Log("ShowSl");
        }
        else
        {
            Debug.Log("No ShowSl");

        }
    }

    public void OpenCloseScroll(bool tog)
    {
        if (tog)
        {
            ObjectScroll.SetActive(true);
        }
        else
        {
            ObjectScroll.SetActive(false);
        }

    }


    public void SpawnObject(GameObject PrefabItem)
    {
        Debug.Log(PrefabItem.name);
        var clone = Instantiate(PrefabItem, null,spwanPos);

    }
}

