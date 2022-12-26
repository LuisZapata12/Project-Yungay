using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    private GameObject dialoguePanel;
    private GameObject dialogueTextGame;
    private GameObject pressButtonDialogue;
    private GameObject pressInitDialogue;
    private TMP_Text dialogueText;
    public static bool pressInit;
    [SerializeField, TextArea(3, 8)] private string[] dialogueLines;
    [Range(0, 1f)] [Min(0)] [Tooltip("Tiempo de tipeo de letras del dialogo")]
    [SerializeField] private float typingTime;
    [HideInInspector]
    public bool isPlayerInrange;
    private bool didDialogueStart;
    private bool me;
    private int lineIndex;
    private PlayerModel playerModel;
    private GameObject player;

    // Update is called once per frame
    private void Update()
    {
        if (dialoguePanel == null)
        {
            dialoguePanel = GameObject.FindGameObjectWithTag("PanelDialogue");
            dialogueTextGame = dialoguePanel.transform.GetChild(0).gameObject;
            pressButtonDialogue = dialoguePanel.transform.GetChild(1).gameObject;
            pressInitDialogue = dialoguePanel.transform.GetChild(2).gameObject;
            dialogueText = dialogueTextGame.GetComponent<TMP_Text>();
        }

        if(playerModel == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerModel = player.GetComponent<PlayerModel>();
        }
        if (me)
        {
            if (isPlayerInrange)
            {
                if (pressInit == false)
                {
                    pressInitDialogue.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!didDialogueStart)
                    {
                        StartDialogue();
                        pressInitDialogue.SetActive(false);
                    }
                    else if (dialogueText.text == dialogueLines[lineIndex])
                    {
                        NextDialogueLine();
                    }
                    else
                    {
                        StopAllCoroutines();
                        dialogueText.text = dialogueLines[lineIndex];
                    }
                }
            }
            else
            {
                pressInit = false;
                pressInitDialogue.SetActive(false);
            }
        }
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if(lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            pressInitDialogue.SetActive(false);
            didDialogueStart = false;
            pressInit = false;
            dialogueTextGame.SetActive(false);
            pressButtonDialogue.SetActive(false);
            pressInitDialogue.SetActive(true);
            // dialoguePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void StartDialogue()
    {
        pressInit = true;
        pressInitDialogue.SetActive(false);
        didDialogueStart = true;
        dialogueTextGame.SetActive(true);
        pressButtonDialogue.SetActive(true);
        //dialoguePanel.SetActive(true);
        lineIndex = 0;
        StartCoroutine(ShowLine());
        Time.timeScale = 0;
    }

    IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach(char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //isPlayerInrange = true;
            playerModel.lookMe = true;
            me = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInrange = false;
            playerModel.lookMe = false;
            me = false;
        }
    }
}
