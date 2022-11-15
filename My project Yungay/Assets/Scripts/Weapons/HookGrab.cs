using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookGrab : MonoBehaviour
{
    public Camera playerCamera;
    [SerializeField] private GameObject hookObject;
    public GameObject hookParent;
    public bool isShot;
    public bool isGrab;
    public bool hookback;
    Vector3 hitposition;

    public float rayDistance;

    public Transform hookeableObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position == hookParent.transform.position)
        {
            hookback = false;
        }
        ShotStart();

        if (hookeableObject != null)
        {
            PickHook();
        }

        if (this.transform.position == hookParent.transform.position && this.transform.parent != null && hookeableObject!= null)
        {
            hookeableObject.SetParent(null);
        }   
        
    }

    private void ShotStart()
    {

        if (Input.GetKeyDown(KeyCode.Q) && isGrab == false && isShot == false)
        {
            if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, rayDistance))
            {
                if (hit.transform.tag != "Hook")
                {
                    isShot = true;
                    hitposition = hit.point;
                }
                
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Q) && isGrab == true)
        {
            PickHook();
            isGrab = false;
            hookback = true;
            
        }

        if (isShot == true)
        {
            hookObject.transform.position = Vector3.MoveTowards(hookObject.transform.position, hitposition, 30f * Time.deltaTime);
            hookObject.transform.SetParent(null);
        }
        else if (isShot == false && isGrab == false)
        {
            PickHook();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Hookeable")
        {
            if (hookback==false)
            {
                isGrab = true;
            }
            
        }
        else
        {
            other.gameObject.transform.SetParent(this.transform);
            hookeableObject = other.gameObject.transform;
        }
        isShot = false;
        Debug.Log("colision");
    }

    private void PickHook()
    {
        this.transform.SetParent(hookParent.transform);
        this.transform.position = Vector3.MoveTowards(hookObject.transform.position, hookParent.transform.position, 50f * Time.deltaTime);
        this.transform.localRotation = Quaternion.Euler(-90f, 0, 0);
    }
 
}
