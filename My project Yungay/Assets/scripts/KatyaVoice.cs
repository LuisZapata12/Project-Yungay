using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class KatyaVoice : MonoBehaviour
{
    public GameObject PanelD;
    [SerializeField, TextArea(3, 8)]
    public string text;
    public TMP_Text dialogueText;
    private Collider m_Collider;
    public float m_distance;
    RaycastHit m_hit;
    void Start()
    {
        m_Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.BoxCast(m_Collider.bounds.center, transform.localScale, transform.forward, out m_hit, transform.rotation, m_distance))
        {
            if (m_hit.transform.tag == "Player")
            {
                Debug.Log("hola");
                PanelD.SetActive(true);
                dialogueText.text = ""+ text;
            }
            //else
            //{
            //    PanelD.SetActive(false);
            //}

        }
    }
    //public void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        PanelD.SetActive(true);
    //        dialogueText.text = ""+ text;
    //    }
    //}
    //public void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        PanelD.SetActive(false);

    //    }

    //}}
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.forward * m_distance, transform.localScale);
    }

}
