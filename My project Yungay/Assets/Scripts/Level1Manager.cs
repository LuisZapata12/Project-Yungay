using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public bool start;
    public static bool isTravel = false;
    public ItemObject axe;
    private Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLv1());
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
         if (checkItem())
         {
             MisionText.currentMision = 0;

            if (Lever.endTravel)
            {
                 MisionText.currentMision = 2;
            }
         }
         else
         {
            MisionText.currentMision = 1;
         }

        

    }

    IEnumerator StartLv1()
    {
        yield return new WaitForSeconds(2f);

        start = true;

    }

    private bool checkItem()
    {
        bool craftItem = false;

        if (inventory.CheckItem(axe))
        {
            craftItem = true;
        }

        return craftItem;
    }
}
