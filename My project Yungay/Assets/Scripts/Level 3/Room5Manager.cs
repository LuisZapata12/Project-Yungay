using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room5Manager : MonoBehaviour
{
    public GameObject spawnEnemysRoom3;
    public GameObject audio;
    public TMP_Text textMesh;
    public float timer;
    public bool isEnd;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnd)
        {
            timer += Time.deltaTime;
            if (timer >= 3)
            {
                textMesh.text = "";
                Destroy(this.gameObject);
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            spawnEnemysRoom3.SetActive(true);
            audio.SetActive(true);
            textMesh.text = "Soldado: Cuidado, puede que este cerca";
            isEnd = true;
            
        }
        
    }
}
