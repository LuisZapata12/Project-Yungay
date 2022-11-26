using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room1Manager : MonoBehaviour
{
    public Inventory inventoryPlayer;
    public bool hasSubmachine;
    public ItemObject submachineItem;
    public GameObject SpawnEnemys;
    public GameObject Cajas;
    public TMP_Text textMesh;
    public float timer;
    //private string textdialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Cajas.transform.childCount <=0)
        {
            textMesh.text = "mmm, puedo usar el gancho para alcanzar eso";
        }
        hasSubmachine = inventoryPlayer.CheckItem(submachineItem);
        if (hasSubmachine)
        {
            SpawnEnemys.SetActive(true);
            textMesh.text = "Que ah sido ese rui... ¡Intrusos!";

            timer += Time.deltaTime;
            if (timer>= 3)
            {
                textMesh.text = "";
                Destroy(this.gameObject);
            }
        }
    }
}
