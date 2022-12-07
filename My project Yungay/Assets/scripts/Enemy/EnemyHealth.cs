using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float life;
    public List<Item> staticItems = new();
    public List<ItemObject> Randomitems = new();
    private Animator anim;
    public  float timer;
    public bool dead;
    private Loot enemyLoot;
    private int count = 0;

    public GameObject CanvaHealth;
    public Image healthBar;
    public GameObject blood;
    public float bloodTime;
    private float timer2;
    public float healthActual, healthMax;
    private GameObject player;
    public float maxDistance;
    void Start()
    {
        healthMax = life;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = life / healthMax;
        if (dead) 
        { 

            if (enemyLoot?true:false)
            {
                enemyLoot.Delete();
            }

            if (Vector3.Distance(transform.position,player.transform.position) > maxDistance)
            {
                Destroy(gameObject);
            }

        }

        if (blood.activeSelf)
        {
            timer2 += Time.deltaTime;
            if (timer2 >= bloodTime)
            {
                timer2 = 0f;
                blood.SetActive(false);
            }
        }
    }
    public void lifeE(float valor)
    {
        life-=valor;
        if (blood.activeSelf)
        {
            blood.SetActive(false);
            timer2 = 0f;
        }
        blood.SetActive(true);
        if (!dead)
        {
            //anim.Play("Enemy_Reaction");
        }
        
        if (life <= 0 && !dead)
        {
            dead = true;
            anim.Play("Dead");
            enemyLoot = gameObject.AddComponent<Loot>();
            RandomLoot();
            StaticItems();
            Debug.Log("Agregar arma");
            CheckItem();
            GetComponent<CapsuleCollider>().isTrigger = true;
            //int _ = LayerMask.NameToLayer("Ignore Player");
            //gameObject.layer = _;
            player = GameObject.Find("Player");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bala"))
        {
            lifeE(10);
        }

        if (other.CompareTag("Axe") || other.CompareTag("Knife") || other.CompareTag("Spear"))
        {
            if (other.CompareTag("Axe") && !dead)
            {
                anim.Play("Enemy_Reaction");
            }
            EquipmentMelee _ = Hand.currentItem as EquipmentMelee;
            if (_ != null)
            {
                lifeE(_.damage);
            }
        }
    }

    private void StaticItems()
    {
        for (int i = 0; i < staticItems.Count; i++)
        {
            enemyLoot.loot.Add(new Item(null,0,0));
            enemyLoot.loot[count].item = staticItems[i].item;
            enemyLoot.loot[count].amount = staticItems[i].amount;
            count++;
        }
    }

    private void RandomLoot()
    {
        count = 0;
        for (int i = 0; i <= (int)Random.Range(0,Randomitems.Count); i++)
        {
            enemyLoot.loot.Add(new Item(null,0,0));
            enemyLoot.loot[i].item = Randomitems[i];
            count++;
        }
        enemyLoot.RandomAmount();
    }

    private void CheckItem()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log(transform.GetChild(i));
            Loot _ = transform.GetChild(i).GetComponent<Loot>();
            if (_ != null)
            {
                Loot loot = transform.GetComponent<Loot>();
                loot.loot.Add(new Item(_.loot[0].item, _.loot[0].amount, _.loot[0].durability));
                _.gameObject.SetActive(false);
            }
        }
    }
}
