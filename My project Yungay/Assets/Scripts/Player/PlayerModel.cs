using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerModel : MonoBehaviour
{
    public enum State
    {
        cinematica,death, idle,move, walk, run,jumping,healing,crounching
    }

    [Header("State")]
    public State state = State.idle;
    public delegate void OnState();
    public Dictionary<State, OnState> states;

    [Header("Life")]
  
    public float health;
    public float maxHealth;
    public bool isDeath;
    [HideInInspector]
    public float armor;

    [Header("Jump")]

    public bool isJump;

    [Header("Movement")]
    public float actualSpeed;
    public float speedWalk;
    public float speedRun;
    public float speedCrouch;
    public float jumpForce;
    public float jumpCooldown;

    [Header("Crouch")]
    public bool isCrouching = false;
    public CapsuleCollider cap;

    public float standHeight;
    public float standY;
    public float crouchHeight;
    public float CrouchY;

    [Header("Stamina Parameters")]
    public int staMax;
    public float staActual;
    public bool isRunning = false;

    [Header("Stamina Regen Parameters")]
    public float staminaDrain = 0.5f;
    public float staRegen = 0.3f;


    [Header("Optional Functions")]
    public bool canCrouch;
    public bool canJump;
    public bool canRun;
    public bool canUpLedge;

    [Header("Checkpoint")]
    [SerializeField]
    private GameObject checkpoint;
    [SerializeField]
    public DataChekpoint dataChekpoint;

    public Rigidbody rb;

    public GameObject sourceSound;

    public static Transform playerTransform;

    public static PlayerModel instance;

    [HideInInspector]
    public bool lookMe;
    public Camera cam;
    [HideInInspector]
    public GameObject _;
    public LayerMask npc;
    public float timer;
    private bool a;

    private void Start()
    {
        timer = 0;
        a = true;
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        checkpoint = GameObject.FindGameObjectWithTag("Checkpoint");
        if (!dataChekpoint)
        {
            dataChekpoint = checkpoint.GetComponent<DataChekpoint>();
        }
        
    }
    private void Awake()
    {
        cap = GetComponent<CapsuleCollider>();
        rb = GetComponent<Rigidbody>();
        playerTransform = GetComponent<Transform>();
        state = State.idle;
    }

    private void Update()
    {
        if (ConsoleCheats.noClip)
        {
            Noclip();
        }
        if (a)
        {
            if (dataChekpoint.inventories != null)
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
                if (timer >= 2f)
                {
                    dataChekpoint.Inventory();
                    a = false;
                }

            }
            else
            {
                timer += Time.deltaTime;
                Debug.Log(timer);
                if (timer >= 2f)
                {
                    dataChekpoint.Inventory();
                    a = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Save"))
        {
            dataChekpoint.Check(); 
        }
    }

    private void Noclip()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * 10 * Time.deltaTime, Camera.main.transform);
        }
    }

}
