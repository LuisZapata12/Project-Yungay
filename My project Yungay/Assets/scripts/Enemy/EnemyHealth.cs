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
            timer += Time.deltaTime;
            if (enemyLoot?true:false)
            {
                enemyLoot.Delete();
            }
            if (timer > 5f)
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
            CheckItem();
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
            if (other.CompareTag("Axe"))
            {
                anim.Play("Enemy_Reaction");
            }
            EquipmentMelee _ = Hand.currentItem as EquipmentMelee;
            lifeE(_.damage);
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
            Loot _ = transform.GetChild(i).GetComponent<Loot>();
            Loot loot = transform.GetComponent<Loot>();
            if (_? true:false)
            {
                loot.loot.Add(new Item(_.loot[0].item, _.loot[0].amount, _.loot[0].durability));
                _.gameObject.SetActive(false);
            }
        }
    }
}
