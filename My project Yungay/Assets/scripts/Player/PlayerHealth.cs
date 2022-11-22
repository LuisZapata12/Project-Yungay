using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public PlayerModel mb;
    public Image lifeBar;
    public Text numberLife;
    public Image damageFeedback;
    public CanvasGroup lifeHud;
    public float timeDamageMax;
    float time;
    float timerDamage;
    public bool takeDamage;
    public bool takeHeal;
    


    public GameObject deathPanel;

    // Update is called once per frame
    void Update()
    {
        LifeUpdate();
        if (mb.armor > 0f)
        {
            mb.armor -= 1 * Time.deltaTime;
        }
        if (!takeHeal)
        {
            if (takeDamage)
            {
                timerDamage += 1 * Time.deltaTime;
                if (timerDamage >= timeDamageMax)
                {
                    takeDamage = false;
                }
                else
                {
                    lifeHud.alpha = 1f;
                }
            }
            else
            {
                lifeHud.alpha = 0f;
                timerDamage = 0;
                Debug.Log("a");
            }
        }
        if (!takeDamage)
        {
            if (takeHeal)
            {
                lifeHud.alpha = 1f;
            }
            else
            {
                lifeHud.alpha = 0f;
                Debug.Log("b");
            }
        }
    
        if(takeHeal && takeDamage)
        {
            lifeHud.alpha = 1f;
        }

    }
    public void Damage(float damage)
    {
        if (mb.health >= 0 && !ConsoleCheats.godMode)
        {
            if (mb.armor > 0f)
            {
                mb.health -= damage / 2;
                takeDamage = true;
                timerDamage = 0;
                StartCoroutine(FeedBackDamage());
            }
            else
            {
                mb.health -= damage;
                takeDamage = true; 
                timerDamage = 0;
                StartCoroutine(FeedBackDamage());
                // StartCoroutine(FeedBackDamage());
            }
        }
    }
    public void LifeUpdate()
    {
        if (mb.health >= 0)
        {
            lifeBar.fillAmount = mb.health / mb.maxHealth;
            //numberLife.text = mb.health.ToString();
        }
        if(mb.health<= 0)
        {
            mb.isDeath = true;
            DeathPanel();
            time += 1 * Time.deltaTime;
            if (time >= 1)
            {
                mb.checkpoint.GetComponent<DataChekpoint>().ReturnPoint();
                time = 0;
            }
            mb.state = PlayerModel.State.death;
            Debug.Log("Tiezo");
            
        }
    }

    public void DeathPanel()
    {
        deathPanel.SetActive(true);
        GameManager.ShowCursor();
    }
    public void RestarLevel()
    {
        mb.isDeath = false;
        
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        //GameManager.HideCursor();
        
    }

    IEnumerator FeedBackDamage()
    {
        for(float i = 0.5f; i >= 0; i -= Time.deltaTime)
            {
            // set color with i as alpha
            damageFeedback.color = new Color(1, 1, 1, i);
            yield return null;
        }
   
    }
}
