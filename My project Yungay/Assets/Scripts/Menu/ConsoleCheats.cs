using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleCheats : MonoBehaviour
{
    public GameObject consoleCanvas;
    public GameObject ObjectScroll;
    bool isOpen = false;
    public Transform spwanPos;
    public static bool godMode, unlimitedAmmo, noClip, showSl;
    Inventory inventory;
    public ItemObject nails, bullets;
    // Start is called before the first frame update
    void Start()
    {
        consoleCanvas.SetActive(false);
        ObjectScroll.SetActive(false);
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5) && !isOpen)
        {
            consoleCanvas.SetActive(true);
            isOpen = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(Input.GetKeyDown(KeyCode.F5) && isOpen)
        {
            consoleCanvas.SetActive(false);
            isOpen = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void GodMode(bool tog)
    {
        if (tog)
        {
            godMode = true;
        }
        else
        {
            godMode = false;
        }
    }

    public void UnlimitedAmmo(bool tog)
    {
        if (tog)
        {
            unlimitedAmmo = true;
            Debug.Log(unlimitedAmmo);
            inventory.AddItem(null, nails, 50, 0);
            inventory.AddItem(null, bullets, 50, 0);

        }
        else
        {
            unlimitedAmmo = false;
            Debug.Log(unlimitedAmmo);
        }
        
    }

    public void NoClip(bool tog)
    {
        if (tog)
        {
            noClip = true;
        }
        else
        {
            noClip = false;
        }
    }

    public void ShowSl(bool tog)
    {
        if (tog)
        {
            showSl = true;
        }
        else
        {
            showSl = false;
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
        Instantiate(PrefabItem, spwanPos.position, Quaternion.identity);
    }
}

