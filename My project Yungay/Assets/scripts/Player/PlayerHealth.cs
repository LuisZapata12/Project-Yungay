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
            }
            else
            {
                mb.health -= damage;
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
}
