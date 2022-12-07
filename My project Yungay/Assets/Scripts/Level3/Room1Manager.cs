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
    private float timer1;
    private float timer2;
    public float time;

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
            textMesh.text = "Kathya: mmm, puedo usar el gancho para alcanzar eso";
        }
        hasSubmachine = inventoryPlayer.CheckItem(submachineItem);
        if (hasSubmachine)
        {
            timer1 += Time.deltaTime;
            if (timer1 >= time)
            {
                SpawnEnemys.SetActive(true);
                textMesh.text = "Soldado: Que ah sido ese rui... ¡Intrusos!";
                timer1 = 0f;
            }

            

            timer2 += Time.deltaTime;
            if (timer2>= 3)
            {
                textMesh.text = "";
                Destroy(this.gameObject);
            }
        }
    }
}
