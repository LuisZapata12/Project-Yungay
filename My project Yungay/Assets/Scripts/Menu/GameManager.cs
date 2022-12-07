using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public GameObject panelPause;
    public GameObject panelOptions;
    private GameObject player;
    private PlayerModel model;
    [SerializeField] private string sceneName;
    public static bool inPause = false;
    public bool inPase = false;
    public static string actualScene;
    


    public bool pause;
    //public static string actualScene;
    // Start is called before the first frame update
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        Hand.isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {

        inPase = inPause;
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        actualScene = sceneName;
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            model = player.GetComponent<PlayerModel>();
        }


        if (Input.GetKeyDown(KeyCode.Escape) && !InventoryDisplay.isOpen && model.state != PlayerModel.State.death && !Dialogue.pressInit)
        {
            if (sceneName != "Menu" && inPause == false)
            {
                ShowCursor();

                Time.timeScale = 0f;
                panelPause.SetActive(true);
                inPause = true;
            }
            else if (sceneName != "Menu" && inPause && !panelOptions.activeSelf)
            {
                Time.timeScale = 1f;
                panelPause.SetActive(false);
                inPause = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else if (sceneName != "Menu" && inPause && panelOptions.activeSelf)
            {
                panelOptions.SetActive(false);
                panelPause.SetActive(true);
            }

            if (sceneName == "Menu")
            {
                ShowCursor();
            }

        }

        if (Input.GetKeyDown(KeyCode.Space) && inPause == true)
        {
            Tutorial.instance.SkipTutoImage();
        }

        if (InventoryDisplay.isOpen)
        {
            Time.timeScale = 0f;
            inPause = true;
        }

        pause = inPause;
    }


    public static void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public static void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void InPause()
    {
        if (inPause == true)
        {
            Time.timeScale = 0f;
        }

        else
        {
            Time.timeScale = 1f;
        }
    }


}
