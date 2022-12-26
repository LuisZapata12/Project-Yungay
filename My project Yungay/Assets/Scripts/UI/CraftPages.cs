using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPages : MonoBehaviour
{
    private int indexPage = 0;
    public List<GameObject> pages = new();
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Page(int page)
    {
        indexPage = page;

        for (int i = 0; i < pages.Count; i++)
        {
            if (i == indexPage)
            {
                pages[i].SetActive(true);
            }
            else
            {
                pages[i].SetActive(false);
            }
        }
    }
}
