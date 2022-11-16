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
    float time;


    public GameObject deathPanel;

    // Update is called once per frame
    void Update()
    {
        LifeUpdate();
        if (mb.armor > 0f)
        {
            mb.armor -= 1 * Time.deltaTime;
        }
    }
    public void Damage(float damage)
    {
        if (mb.health >= 0 && !ConsoleCheats.godMode)
        {
            if (mb.armor > 0f)
            {
                mb.health -= damage / 2;
                StartCoroutine(FeedBackDamage());
            }
            else
            {
                mb.health -= damage;
                StartCoroutine(FeedBackDamage());
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
