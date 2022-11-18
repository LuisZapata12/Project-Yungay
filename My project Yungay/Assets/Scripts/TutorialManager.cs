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

    
    private Transform cameraTransform;
    public float timer = 0f;
    public float maxTime = 0f;
    private Vector3 oldRotation = new Vector3(0, 0, 0);
    PlayerModel playerModel;
    public ItemObject backpack,paper;
    private Inventory inventory;
    private InventoryDisplay inventoryDisplay;
    private bool activateInventory = false;
    private GameObject backpackObjetc;
    private GameObject[] paperObject;
    private bool once = false;
    public GameObject box;
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
        if (CinematicTutorial.playTutorial)
        {
            if (!moveCamera)
            {
                CheckMoveCamera();
                MisionText.currentMision = 0;
            }
            else
            {
                if (!movePlayer)
                {
                    MisionText.currentMision = 1;
                    CheckMovement();
                }
                else
                {
                    if (!grabBackpack)
                    {
                        MisionText.currentMision = 2;
                        grabBackpack = CheckGrabItem(backpack);

                        if (!once)
                        {
                            backpackObjetc.AddComponent<Loot>().loot.Add(new Item(backpack, 1,0));
                            once = true;
                        }
                    }
                    else
                    {
                        if (!activateInventory)
                        {
                            inventory.RestItem(backpack, 1);
                            inventory.RemoveSlot();
                            inventoryDisplay.enabled = !inventoryDisplay.enabled;
                            activateInventory = true;
                        }

                        if (!openInventory)
                        {
                            MisionText.currentMision = 3;
                            if (Input.GetKeyDown(KeyCode.Tab))
                            {
                                openInventory = true; foreach (GameObject Papers in paperObject)
                                {
                                    Papers.AddComponent<Loot>().loot.Add(new Item(paper, 1,0));
                                }
                                
                            }
                        }
                        else
                        {
                            if (!grabPaper)
                            {
                                MisionText.currentMision = 4;
                                grabPaper = CheckGrabItem(paper);
                            }
                            else
                            {
                                if (!throwPaper)
                                {
                                    MisionText.currentMision = 5;

                                    if (Input.GetMouseButtonDown(1))
                                    {
                                        throwPaper = true;
                                    }

                                }
                                else
                                {
                                    if (!moveBox)
                                    {
                                        MisionText.currentMision = 6;
                                        box.GetComponent<Rigidbody>().isKinematic = false;
                                        CheckMoveBox();
                                    }
                                    else
                                    {
                                        MisionText.currentMision = 7;
                                    }
                                }

                            }
                        }
                    }
                }

            }
        }
        else
        {
            MisionText.currentMision = 8;
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
