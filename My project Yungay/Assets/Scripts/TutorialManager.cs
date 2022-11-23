using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
 
    public bool moveCamera = false;
    public bool movePlayer = false;
    public bool grabBackpack = false;
    public bool openInventory = false;
    public bool grabPaper = false;
    public bool throwPaper = false;
    public bool moveBox = false;

    
    public  Transform cameraTransform;
    public float timer = 0f;
    public float maxTime = 0f;
    private Vector3 oldRotation = new Vector3(0, 0, 0);
    PlayerModel playerModel;
    public ItemObject backpack,paper,axe;
    private Inventory inventory;
    private InventoryDisplay inventoryDisplay;
    private bool activateInventory = false;
    private GameObject backpackObjetc;
    private GameObject[] paperObject;
    private bool once = false;
    public GameObject box;

    public int index = 0;
    private void Start()
    {
        cameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
        GameObject _ = GameObject.FindGameObjectWithTag("Player");
        playerModel = _.GetComponent<PlayerModel>();
        inventory = _.GetComponent<Inventory>();
        inventoryDisplay = GameObject.Find("Canvas").GetComponent<InventoryDisplay>();
        inventoryDisplay.enabled = !inventoryDisplay.enabled;
        paperObject = GameObject.FindGameObjectsWithTag("Paper");
        backpackObjetc = GameObject.Find("Backpack");
        AudioScene();

    }
    private void Update()
    {
        MisionText.currentMision = index;
        switch (index)
        {
            case 0:
                CheckMoveCamera();
                if (moveCamera)
                {
                    index = 1;
                }
                break;
            case 1:
                CheckMovement();
                if (movePlayer)
                {
                    index = 2;
                }
                break;
            case 2:
                if (!once)
                {
                    backpackObjetc.AddComponent<Loot>().loot.Add(new Item(backpack, 1, 0));
                    once = true;
                }
                grabBackpack = CheckGrabItem(backpack);
                if (grabBackpack)
                {
                    inventory.RestItem(backpack, 1);
                    inventory.RemoveSlot();
                    inventoryDisplay.enabled = !inventoryDisplay.enabled;
                    index = 3;
                }
                break;
            case 3:
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    foreach (GameObject Papers in paperObject)
                    {
                        Papers.AddComponent<Loot>().loot.Add(new Item(paper, 1, 0));
                    }
                    index = 4;
                }
                break;
            case 4:
                grabPaper = CheckGrabItem(paper);
                if (grabPaper)
                {
                    index = 5;
                }
                break;
            case 5:
                if (Input.GetMouseButtonDown(1))
                {
                    index = 6;
                }
                break;
            case 6:
                if (inventory.CheckItem(axe))
                {
                    index = 7;
                }
                break;
            case 7:
                break;
            default:
                break;
        }
    }

    private void CheckMoveCamera()
    {
        if (cameraTransform.rotation.eulerAngles != oldRotation)
        {
            timer += Time.deltaTime;
            oldRotation = cameraTransform.transform.rotation.eulerAngles;
        }

        if (timer > maxTime)
        {
            moveCamera = true;
            timer = 0f;
        }
    }

    private void CheckMovement()
    {
        if (playerModel.actualSpeed > 0.1f)
        {
            timer += Time.deltaTime;
        }

        if (timer > maxTime)
        {
            movePlayer = true;
            timer = 0f;
        }

    }

    private bool CheckGrabItem(ItemObject item)
    {
        bool grabItem = inventory.CheckItem(item);
        return grabItem;
    }

    private void CheckMoveBox()
    {
        if (PlayerPickUp.isPushing)
        {
            moveBox = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            moveBox = true;
        }
    }

    private void AudioScene()

    { 
        AudioManager.Instance.PlayMusic("Tutorial");
    }
}
