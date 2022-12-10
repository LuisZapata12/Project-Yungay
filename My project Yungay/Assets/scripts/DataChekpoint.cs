using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataChekpoint : MonoBehaviour
{
    public List<DataEnemys> dataEnemys = new List<DataEnemys>();
    public GameObject[] enemigos;
    public GameObject[] check;
    public GameObject[] check1;
    public List<Checking> lastCheck;
    public List<Player> players = new List<Player>();
    public List<InventorySlot> inventories = new List<InventorySlot>();
    public GameObject player;
    public InventoryDisplay inventoryDisplay;
    private GameObject canvas;

    private void Start()
    {
        inventoryDisplay = InventoryDisplay.instance;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if(!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if (!inventoryDisplay)
        {
            canvas = GameObject.FindGameObjectWithTag("UI");
            inventoryDisplay = canvas.GetComponent<InventoryDisplay>();
        }
    }

    public void Check()
    {
        //dataEnemys.Clear();
        //players.Clear();
        //inventories.Clear();
        //enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        //player = GameObject.FindGameObjectWithTag("Player");
        Clean();
       // CheckEnemys();
        CheckInventory();
        CheckPosition(); 
        LastCheckPoint();
        //foreach (GameObject enemysSingle in enemigos)
        //{
        //    dataEnemys.Add(new DataEnemys(enemysSingle.GetComponent<EnemyHealth>().life)); Debug.Log(enemigos[i].name);
        //}
        //players.Add(new Player(player.GetComponent<Transform>().position, player.GetComponent<PlayerModel>().health));
        //for(int i = 0; i < player.GetComponent<Inventory>().slots.Count; i++)
        //{
        //    inventories.Add(new InventorySlot(player.GetComponent<Inventory>().slots[i].item, player.GetComponent<Inventory>().slots[i].amount, player.GetComponent<Inventory>().slots[i].durability));
        //}
    }
    public void CheckEnemys()
    {
        enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemysSingle in enemigos)
        {
            dataEnemys.Add(new DataEnemys( enemysSingle.GetComponent<EnemyHealth>().life));
        }
    }
    public void CheckInventory()
    {
        inventories.Clear(); 
        for (int i = 0; i < player.GetComponent<Inventory>().slots.Count; i++)
        {
            inventories.Add(new InventorySlot(player.GetComponent<Inventory>().slots[i].item, player.GetComponent<Inventory>().slots[i].amount, player.GetComponent<Inventory>().slots[i].durability));
        }
        players.Clear();
    }
    public void CheckPosition()
    {
        players.Clear(); 
        player = GameObject.FindGameObjectWithTag("Player");
        players.Add(new Player(player.GetComponent<Transform>().position, player.GetComponent<PlayerModel>().health));
    }


    public void CheckPoint() // start
    {
        check = GameObject.FindGameObjectsWithTag("Save");
    }
    public void LastCheckPoint() // Collider
    {
        lastCheck.Clear();
        check1 = GameObject.FindGameObjectsWithTag("Save");
        foreach (GameObject lastCheckSingle in check)
        {
           lastCheck.Add(new Checking(lastCheckSingle.gameObject,lastCheckSingle.GetComponent<Collider>().enabled));
        }
    }
    public void ReturnPoint()
    {
        //for(int i = 0; i < check.Length; i++)
        //{
        //    if (check[i] != lastCheck[i])
        //    {
        //        Destroy(check[i].gameObject);
        //    }
        //}
        //for (int i = 0; i < enemigos.LongLength; i++)
        //{
        //    //enemigos[i].transform.position = dataEnemys[i].position;
        //    enemigos[i].GetComponent<EnemyHealth>().life = dataEnemys[i].health;
        //}
        player.transform.position = players[0].position;
        player.GetComponent<PlayerModel>().health = players[0].health;
        for (int i = 0; i < inventories.Count; i++)
        {
            player.GetComponent<Inventory>().slots[i].item = inventories[i].item;
            player.GetComponent<Inventory>().slots[i].amount = inventories[i].amount;
            player.GetComponent<Inventory>().slots[i].durability = inventories[i].durability;
        }
        inventoryDisplay.UpdateDisplay();
    }
    public void ReturnInventory()
    {
        for (int i = 0; i < inventories.Count; i++)
        {
            player.GetComponent<Inventory>().slots[i].item = inventories[i].item;
            player.GetComponent<Inventory>().slots[i].amount = inventories[i].amount;
            player.GetComponent<Inventory>().slots[i].durability = inventories[i].durability;
        }
        inventoryDisplay.UpdateDisplay();

    }
    public void ReturnCheck()
    {
        for (int i = 0; i < check.Length; i++)
        {
            check[i].GetComponent<Collider>().enabled = lastCheck[i].collider;
        }
    }
    public void ReturnPosition()
    {
        player.transform.position = players[0].position;
        player.GetComponent<PlayerModel>().health = players[0].health;
    }

    public void Clean()
    {
        players.Clear(); inventories.Clear(); dataEnemys.Clear();
    }


    [System.Serializable]

    public class DataEnemys
    {
       // public Vector3 position;
        public float health;

        public DataEnemys( float health)
        {
            //this.position = pos;
            this.health = health;
        }
    }
    [System.Serializable]
    public class Player
    {
        public Vector3 position;
        public float health;

        public Player(Vector3 pos, float health)
        {
            this.position = pos;
            this.health = health;
        }
    }
    [System.Serializable]
    public class InventorySlot
    {
        public ItemObject item;
        public int amount;
        public int durability;

        public InventorySlot(ItemObject item, int amount, int durability)
        {
            this.item = item;
            this.amount = amount;
            this.durability = durability;
        }
    }
    [System.Serializable]
    public class Checking
    {
        public GameObject checkPoint;
        public bool collider;

        public Checking(GameObject checkPoint,bool collider)
        {
            this.checkPoint = checkPoint;
            this.collider = collider;
        }
    }
}
