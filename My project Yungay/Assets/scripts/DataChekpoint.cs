using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataChekpoint : MonoBehaviour
{
    public List<DataEnemys> dataEnemys = new List<DataEnemys>();
    public GameObject[] enemigos;
    public List<Player> players = new List<Player>();
    public List<InventorySlot> inventories = new List<InventorySlot>();
    public GameObject player;
    public InventoryDisplay inventoryDisplay;

    private void Start()
    {
        inventoryDisplay = InventoryDisplay.instance;
    }

    public void Check()
    {
        dataEnemys.Clear();
        players.Clear();
        inventories.Clear();
        enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        foreach (GameObject enemysSingle in enemigos)
        {
            Debug.Log(enemysSingle.name);
            dataEnemys.Add(new DataEnemys(enemysSingle.GetComponent<Transform>().position, enemysSingle.GetComponent<EnemyHealth>().life));
        }
        players.Add(new Player(player.GetComponent<Transform>().position, player.GetComponent<PlayerModel>().health));
        for(int i = 0; i < player.GetComponent<Inventory>().slots.Count; i++)
        {
            inventories.Add(new InventorySlot(player.GetComponent<Inventory>().slots[i].item, player.GetComponent<Inventory>().slots[i].amount));
        }
    }

    public void ReturnPoint()
    {
        for (int i = 0; i < enemigos.LongLength; i++)
        {
            enemigos[i].transform.position = dataEnemys[i].position;
            enemigos[i].GetComponent<EnemyHealth>().life = dataEnemys[i].health;
        }
        player.transform.position = players[0].position;
        player.GetComponent<PlayerModel>().health = players[0].health;
        for (int i = 0; i < inventories.Count; i++)
        {
            player.GetComponent<Inventory>().slots[i].item = inventories[i].item;
            player.GetComponent<Inventory>().slots[i].amount = inventories[i].amount;
        }
        inventoryDisplay.UpdateDisplay();
    }

    [System.Serializable]

    public class DataEnemys
    {
        public Vector3 position;
        public float health;

        public DataEnemys(Vector3 pos, float health)
        {
            this.position = pos;
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

        public InventorySlot(ItemObject item, int amount)
        {
            this.item = item;
            this.amount = amount;
        }
    }
}
