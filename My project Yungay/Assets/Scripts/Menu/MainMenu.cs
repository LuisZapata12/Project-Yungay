using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject panelMainMenu;
    public GameObject panelOptions;
    private void Awake()
    {
        //panelMainMenu = GameObject.Find("Panel MainMenu");
        if(panelMainMenu == null)
        {

        }


        if (panelOptions == null)
        {

        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (panelMainMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (sceneName == "Credits")
            {
                panelMainMenu.SetActive(true);
            }
        }
    }


    public void ButtonStart()
    {
        panelMainMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Tutorial");
    }

    public void ButtonOptions()
    {
        panelMainMenu.SetActive(false);
        panelOptions.SetActive(true);
    }

    public void ButtonCredits()
    {
        panelMainMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credits");
    }

    public void ButtonExitGame()
    {
        Application.Quit();
    }
}
