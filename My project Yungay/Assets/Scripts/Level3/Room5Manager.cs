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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            spawnEnemysRoom3.SetActive(true);
            audio.SetActive(true);
            textMesh.text = "Cuidado, puede que este cerca";

            timer += Time.deltaTime;
            if (timer >= 3)
            {
                textMesh.text = "";
                Destroy(this.gameObject);
            }
        }
        
    }
}
