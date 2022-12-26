using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Manager : MonoBehaviour
{
    public bool start;
    public static bool isTravel = false;
    public ItemObject axe, pistol;   
    private Inventory inventory;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLv1());
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        MisionManager();
    }

    private void MisionManager()
    {
        //switch (count)
        //{
        //    case 0:
        //        MisionText.currentMision = 0;
        //        if (inventory.CheckItem(axe))
        //        {
        //            count = 1;
        //        }
        //        break;

        //    case 1:
        //        if (Lever.endTravel)
        //        {
        //            MisionText.currentMision = 1;
        //            if (inventory.CheckItem(axe))
        //            {
        //                count = 2;
        //            }
        //        }
        //        else
        //        {
        //            MisionText.currentMision = 3;
        //        }
        //        break;

        //    case 2:
        //        MisionText.currentMision = 2;
        //        if (inventory.CheckItem(pistol))
        //        {
        //            count = 3;
        //        }
        //        break;

        //    case 3:
        //        MisionText.currentMision = 4;
        //        break;

        //    default:
        //        break;
        //}
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
