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
    public float speedShoot, speedBack;

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
            AudioManager.Instance.PlaySFX("Hook_releaseobject");
            hookeableObject.SetParent(null);
            hookeableObject = null;
            isGrab = false;
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
                    AudioManager.Instance.PlaySFX("Hook_shoot");
                    isShot = true;
                    hitposition = hit.point;
                }
                
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Q) && isGrab == true)
        {
            AudioManager.Instance.PlaySFX("Hook_releasesurface");
            PickHook();
            isGrab = false;
            hookback = true;
            
            
        }

        if (isShot == true)
        {
            hookObject.transform.position = Vector3.MoveTowards(hookObject.transform.position, hitposition, speedShoot * Time.deltaTime);
            hookObject.transform.SetParent(null);
            this.GetComponent<BoxCollider>().enabled = true;
        }
        else if (isShot == false && isGrab == false)
        {
            PickHook();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isShot = false;
        if (other.gameObject.tag != "Hookeable")
        {
            if (hookback==false)
            {
                AudioManager.Instance.sfxSource.Stop();
                AudioManager.Instance.PlaySFX("Hook_hitchsurface");
                isGrab = true;
            }
            
        }
        else
        {
            AudioManager.Instance.sfxSource.Stop();
            AudioManager.Instance.PlaySFX("Hook_hitchobject");
            other.gameObject.transform.SetParent(this.transform);
            hookeableObject = other.gameObject.transform;
        }
        
        Debug.Log("colision");
    }

    private void PickHook()
    {
        this.transform.SetParent(hookParent.transform);
        this.transform.position = Vector3.MoveTowards(hookObject.transform.position, hookParent.transform.position, speedBack * Time.deltaTime);
        this.transform.localRotation = Quaternion.Euler(-90f, 0, 0);
        this.GetComponent<BoxCollider>().enabled = false;
    }
 
}
