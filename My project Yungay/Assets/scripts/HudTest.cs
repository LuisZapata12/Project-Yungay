using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudTest : MonoBehaviour
{
    public List<TMP_Text> texto = new ();
    public bool testing;
    public float timer;
    public float maxTimer;
    private PlayerModel player;
    private Inventory inventory;
    public RawImage image;
    public List<Sprite> sprite = new();
    public GameObject prefabText;
    public Transform viewPort;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerModel>();
    }

    private void Update()
    {
        CleanTexts(); 
        State();
    }
    public void TextHud(ItemObject itemObject, int cantidad)
    {
        GameObject Text = Instantiate(prefabText, viewPort);
        Text.GetComponent<TMP_Text>(). text = "Recogido: " + itemObject.name + " (X" + cantidad.ToString() + ")";

        //testing = false;
        //for (int i = 0; i < texto.Count; i++)
        //{
        //    if(texto[i].text == "")
        //    {
        //        if (cantidad != 0)
        //        {
        //            texto[i].text = "Recogido: " + itemObject.name + " (X" + cantidad.ToString() + ")";
        //        }
        //        else
        //        {
        //            texto[i].text =  itemObject.name + " (maxStack)";
        //        }

        //        break;
        //    }
        //}
        //testing = true;
    }

    public void CleanTexts()
    {
        if (testing)
        {
            timer += Time.deltaTime;
        }

        if (timer >= maxTimer)
        {
            timer = 0f;
            for (int i = 0; i < texto.Count; i++)
            {
                if (texto[i].text != "")
                {
                    texto[i].text = "";
                }

            }
            testing = false;
        }
    }

    private void State()
    {
        switch (player.state)
        {
            case PlayerModel.State.idle:
                image.texture = sprite[0].texture;
                break;
            case PlayerModel.State.walk:
                image.texture = sprite[1].texture;
                break;
            case PlayerModel.State.run:
                image.texture = sprite[2].texture;
                break;
            case PlayerModel.State.crounching:
                image.texture = sprite[3].texture;
                break;
            case PlayerModel.State.jumping:
                image.texture = sprite[4].texture;
                break;
            case PlayerModel.State.healing:
                image.texture = sprite[5].texture;
                break;

        }
    }

}
