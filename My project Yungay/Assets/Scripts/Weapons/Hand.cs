using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Hand : MonoBehaviour
{
    private Inventory inventory;
    private InventoryDisplay inventoryDisplay;
    public static ItemObject currentItem = null;
    public ItemObject currentMunition = null;
    public List<ItemObject> itemsMunition = new();
    public List<RangeWeaponSlot> weaponSlots = new();
    private int munitionIndex = 0;
    private int slotIndex = 0;
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private Animator anim;
    public static bool isAttacking;
    private bool canAttack = false;
    public float force;
    public static bool canAim = false;
    private Munition muni;
    public Image munitionImage;
    public TMP_Text ammotext;
    private AudioSource audioSource;
    public Sprite defaultCursor, weaponsCursor, aimCursor;
    public static Image imageCursor;

    public ItemObject itemxd;
    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        inventoryDisplay = InventoryDisplay.instance;//GameObject.FindGameObjectWithTag("UI").GetComponent<InventoryDisplay>();
        anim = GetComponent<Animator>();
        currentMunition = itemsMunition[0];
        muni = GetComponent<Munition>();
        audioSource = GetComponent<AudioSource>();
        imageCursor = GameObject.Find("Cursor").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        itemxd = currentItem;
        ChangeItem();
        ChangeMesh();
        if (canAttack)
        {
            UseItem();
            Throw();
            ChangeMunition();

            if (Input.GetKeyDown(KeyCode.R))
            {
                muni.ReloadMunition(currentMunition);
            }
        }
        UpdateText();
    }

    private void ChangeItem()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            slotIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            slotIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            slotIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            slotIndex = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            slotIndex = 4;
        }

        canAim = false;
        canAttack = false;

        if (currentItem? true : false)
        {
            var _ = currentItem as EquipmentItem;

            if (_?true:false)
            {
                if (_.equipmentType == EquipmentType.Range)
                {
                    if (!canAim)
                    {
                        imageCursor.sprite = weaponsCursor;
                    }

                    canAim = true;
                    
                }
                else
                {
                    canAim = false;
                    imageCursor.sprite = defaultCursor;
                }

                if (_.equipmentType == EquipmentType.Melee)
                {
                    canAttack = true;
                }
                else
                {
                    canAttack = false;
                }
            }
        }
    }

    private void UpdateText()
    {
        if (canAim)
        {
            munitionImage.gameObject.SetActive(true);
            ammotext.gameObject.SetActive(true);
            munitionImage.sprite = currentMunition.itemSprite;
            ammotext.text = GetCharge(currentMunition).ToString() + " / " + inventory.CheckAmount(currentMunition);
        }
        else
        {
            munitionImage.gameObject.SetActive(false);
            ammotext.gameObject.SetActive(false);
        }
    }

    private int GetCharge(ItemObject item)
    {
        int charge = 0;
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            if (weaponSlots[i].weapon == currentItem)
            {
                for (int j = 0; j < weaponSlots[i].munitions.Count; j++)
                {
                    if (weaponSlots[i].munitions[j].munition == item)
                    {
                        charge = weaponSlots[i].munitions[j].charge;
                        break;
                    }
                }
                break;
            }
        }
        return charge;
    }
    private void ChangeMunition()
    {
        if (canAim)
        {
            EquipmentItem _ = (EquipmentItem)currentItem;
            if (_.name == "Pistol")
            {
                if (Input.GetKeyDown(KeyCode.T))
                {
                    munitionIndex++;
                    if (munitionIndex > itemsMunition.Count-1)
                    {
                        munitionIndex = 0;
                    }
                    currentMunition = itemsMunition[munitionIndex];
                }
            }
            else if(_.name == "Submachine")
            {
                currentMunition = itemsMunition[1];
            }
        }
    }

    private void ChangeMesh()
    {
        currentItem = inventory.slots[slotIndex].item;
        if (currentItem ? true:false)
        {
            if (currentItem.type == ItemType.Equipment)
            {
                EquipmentItem _ = (EquipmentItem)currentItem;
                meshFilter.sharedMesh = _.itemMesh;
                meshRenderer.sharedMaterial = _.itemMaterial;
                canAttack = true;
                gameObject.tag = _.itemName;
                anim.Play(_.idleClip.name);
            }
            else
            {
                meshFilter.sharedMesh = null;
                meshRenderer.sharedMaterial = null;
                canAttack = false;
                anim.Play("idle");
            }
        }
        else
        {
            meshFilter.sharedMesh = null;
            meshRenderer.sharedMaterial = null;
        }

    }

    private void UseItem()
    {
        EquipmentItem _ = currentItem as EquipmentItem;

        if (_?true:false)
        {
            if (_.equipmentType == EquipmentType.Range)
            {
                canAim = true;
            }
            else
            {
                canAim = false;
            }

            if (Input.GetMouseButtonDown(0) && !GameManager.inPause)
            {
                if (_.equipmentType == EquipmentType.Melee)
                {
                    EquipmentMelee melee = (EquipmentMelee)currentItem;
                    if (melee.animation != null)
                    {
                        melee.durability -= 1;
                        anim.Play(melee.animation.name);
                        isAttacking = true;
                    }
                }
            }
        }

    }

    private void Throw()
    {
        if (Input.GetMouseButton(1) && !GameManager.inPause)
        {
            EquipmentItem _ = currentItem as EquipmentItem;

            if (_?true:false)
            {
                if (_.equipmentType == EquipmentType.Melee)
                {
                    GameObject clone = Instantiate(currentItem.prefab, transform.position, transform.rotation);
                    clone.GetComponent<Loot>().loot[0].amount = inventory.slots[slotIndex].amount;
                    clone.GetComponent<Rigidbody>().isKinematic = false;
                    clone.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * force, ForceMode.Impulse);
                    inventory.slots[slotIndex].item = null;
                    inventory.slots[slotIndex].amount = 0;
                    inventory.UpdateInventory();
                    inventoryDisplay.UpdateDisplay();
                    canAttack = false;
                    EquipmentMelee melee = _ as EquipmentMelee;
                    AudioManager.Instance.PlaySFX("Throw");
                }
            }
        }
    }

    public void AddCollider()
    {
        gameObject.AddComponent<BoxCollider>();
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void RemoveCollider()
    {
        Destroy(GetComponent<BoxCollider>());
    }

    public void PlaySound()
    {
        EquipmentMelee melee = (EquipmentMelee)currentItem;
        AudioManager.Instance.PlaySFX(melee.attackClip.name);
    }

    public int GetMunitionIndex(ItemObject item)
    {
        int index = 0;
        for (int i = 0; i < weaponSlots.Count; i++)
        {
            if (weaponSlots[i].weapon == item)
            {
                index = i;
                break;
            }
        }
        return index;
    }
}

[System.Serializable]
public class RangeWeaponSlot
{
    public ItemObject weapon;
    public List<MunitonSlot> munitions = new();

    public RangeWeaponSlot(ItemObject weapon, List<MunitonSlot> munitions)
    {
        this.weapon = weapon;
        this.munitions = munitions;
    }
}

[System.Serializable]
public class MunitonSlot
{
    public ItemObject munition;
    public int charge;

    public MunitonSlot(ItemObject munition,int charge)
    {
        this.munition = munition;
        this.charge = charge;
    }
}
