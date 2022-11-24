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
    public bool xd;
    public float range;
    public Transform player;
    public bool inRange;

    
    void Start()
    {
        m_Collider = GetComponent<Collider>();
  
    }

    // Update is called once per frame
    void Update()
    {

        if (calculateDistante() < range)
        {
            inRange = true;
            PanelD.SetActive(true);
            dialogueText.text = "" + text;
        }

        if (calculateDistante() > range && inRange)
        {
            inRange = false;
            PanelD.SetActive(false);
        }




        //distance = CalculateDistance();
        //if (distance < 5f)
        //{
        //    Debug.Log("activo");
        //    //PanelD.SetActive(true);
        //    //dialogueText.text = "" + text;
        //}

        //else
        //{
        //    Debug.Log("inactivo");
        //    //PanelD.SetActive(false);
        //}
        //if (xd)
        //{
        //    PanelD.SetActive(true);
        //    dialogueText.text = "" + text;
        //}
        //else
        //{
        //    PanelD.SetActive(false);
        //}
        //if (Physics.SphereCast(transform.position, transform.lossyScale.x / 2, transform.right, out m_hit,  m_distance))
        //{
        //    if (m_hit.transform.tag == "Player")
        //    {
        //        PanelD.SetActive(true);
        //        dialogueText.text = "" + text;

        //    }
            



        //}
        //else

        //{
        //    PanelD.SetActive(false);
        //    //xd = false;
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
    //}

    //private float CalculateDistance()
    //{
    //    return Vector3.Distance(transform.position, target.transform.position);
    //}

    private float calculateDistante()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        return distance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
