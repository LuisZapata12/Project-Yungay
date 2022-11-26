using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room8Manager : MonoBehaviour
{
    public GameObject sacks;
    public GameObject obstacle1;
    public GameObject obstacle2;
    public TMP_Text textMesh;
    public GameObject enemys;
    public bool isShaking;
    public float timer;
    public CameraShake cameraShake;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isShaking == true)
        {
            textMesh.text = "Kathya: Tengo un mal presentimiento";
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                textMesh.text = "";
                isShaking = false;
                timer = 0;
            }
        }

        if (enemys.transform.childCount <=0)
        {
            if (i == 0)
            {
                StartCoroutine(cameraShake.Shake());
                i++;
            }
            isShaking = true;

        }
        if (sacks.transform.childCount <= 0)
        {
            obstacle1.GetComponent<Window>().enabled = true;
            obstacle2.GetComponent<Window>().enabled = true;
            textMesh.text = "Kathya: esto deberia ayudarme a continuar";
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                textMesh.text = "";
                Destroy(this.gameObject);
            }
        }
    }
}
