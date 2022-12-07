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
    private Inventory inventory;
    public float rayDistance;
    private bool canShot = true;
    [SerializeField]
    private float distance;
    [SerializeField]
    private LayerMask hook;
    private float scope;
    private bool offLimits;

    public Transform hookeableObject;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = CalculateDistance();

        if (!GameManager.inPause)
        {
            if (this.transform.position == hookParent.transform.position)
            {
                hookback = false;
                canShot = true;
                offLimits = false;
            }
            ShotStart();

            if (hookeableObject != null && isShot == true)
            {
                PickHook();
            }

            if (distance >=rayDistance + 1f)
            {
                offLimits = true;
            }

            if (offLimits == true)
            {
                PickHook();
                isGrab = false;
            }

            if (this.transform.position == hookParent.transform.position && this.transform.parent != null && hookeableObject != null)
            {
                PickHook();
                Loot loot = hookeableObject.GetComponent<Loot>();
                ItemObject item = loot.loot[0].item;

                if (item.type == ItemType.Equipment)
                {
                    EquipmentItem equipment = (EquipmentItem)item;
                    if (equipment.equipmentType == EquipmentType.Melee || equipment.equipmentType == EquipmentType.Range)
                    {
                        if (inventory.InventorySpace())
                        {
                            inventory.AddItem(null, item, loot.loot[0].amount, loot.loot[0].durability);
                            AudioManager.Instance.PlaySFX("Hook_releaseobject");
                            Destroy(hookeableObject.gameObject);
                            isGrab = false;
                        }
                        else
                        {
                            Instantiate(Hand.currentItem.prefab, transform.position, Quaternion.identity);
                            int a = inventory.CheckAmount(Hand.currentItem);
                            inventory.RestItem(Hand.currentItem, a);
                            inventory.RemoveSlot();
                            inventory.AddItem(null, item, loot.loot[0].amount, loot.loot[0].durability);
                            AudioManager.Instance.PlaySFX("Hook_releaseobject");
                            Destroy(hookeableObject.gameObject);
                            isGrab = false;
                        }
                    }
                }
                else
                {
                    AudioManager.Instance.PlaySFX("Hook_releaseobject");
                    hookeableObject.SetParent(null);
                    hookeableObject = null;
                    isGrab = false;
                }

            }
        }
    }

    private void ShotStart()
    {

        if (Input.GetKeyDown(KeyCode.Q) && isGrab == false && isShot == false && canShot)
        {
            if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out RaycastHit hit, rayDistance, hook, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.tag != "Hook")
                {
                    //AudioManager.Instance.PlaySFX("Hook_shoot");
                    Debug.Log(hit.collider.name);
                    AudioManager.Instance.PlaySFX("Hook_shootrope");
                   isShot = true;
                    hitposition = hit.point;
                }
                
            }

            canShot = false;
        }

        if (Input.GetKeyDown(KeyCode.Q) && isGrab == true)
        {
            AudioManager.Instance.PlaySFX("Hook_releasesurface");
            AudioManager.Instance.PlaySFX("Hook_retraction");
            
            PickHook();
            isGrab = false;
            hookback = true;
            
            
        }

        if (isShot == true)
        {
            hookObject.transform.position = Vector3.MoveTowards(hookObject.transform.position, hitposition, speedShoot * Time.deltaTime);
            hookObject.transform.SetParent(null);
            if(Vector3.Distance(hookObject.transform.position, hitposition) < distance)
            {
                this.GetComponent<BoxCollider>().enabled = true;
            }
            
        }
        else if (isShot == false && isGrab == false)
        {
            PickHook();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isShot = false;
        if (other.gameObject.tag != "Hookeable" && other.gameObject.tag != "Box" && other.gameObject.tag != "Axe" && other.gameObject.tag != "Knife" && other.gameObject.tag != "Spear")
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
        
    }

    private void PickHook()
    {
        this.transform.SetParent(hookParent.transform);
        this.transform.position = Vector3.MoveTowards(hookObject.transform.position, hookParent.transform.position, speedBack * Time.deltaTime);
        this.transform.localRotation = Quaternion.Euler(-90f, 0, 0);
        this.GetComponent<BoxCollider>().enabled = false;
        if (this.transform.position == hookParent.transform.position && hookback==true)
        {
            AudioManager.Instance.sfxSource.Stop();
        }
    }

    private float CalculateDistance()
    {
        return Vector3.Distance(transform.position, hookParent.transform.position);
    }


}
