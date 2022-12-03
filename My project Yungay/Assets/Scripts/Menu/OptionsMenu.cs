using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{

    public GameObject panelMainMenu;
    public GameObject panelOptions;
    public GameObject panelPause;
    public GameObject pauseMenu;

    [Header ("Buttons")]
    public GameObject audioSection;
    public GameObject videoSection;
    public GameObject controlSection;
    // Start is called before the first frame update
    void Start()
    {
        //panelMainMenu = GameObject.Find("Panel MainMenu");
        if (panelMainMenu == null)
        {

        }


        if (panelOptions == null)
        {

        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AudioButton()
    {
        audioSection.SetActive(true);
        videoSection.SetActive(false);
        controlSection.SetActive(false);
    }

    public void VideoButton()
    {
        audioSection.SetActive(false);
        videoSection.SetActive(true);
        controlSection.SetActive(false);
    }
    public void ControlButton()
    {
        audioSection.SetActive(false);
        videoSection.SetActive(false);
        controlSection.SetActive(true);
    }
    public void ButtonBack()
    {
        if(GameManager.actualScene == "Menu")
        {
            panelOptions.SetActive(false);
            panelMainMenu.SetActive(true);
        }
        else
        {
            panelOptions.SetActive(false);
            panelPause.SetActive(true);
        }
    }
}
