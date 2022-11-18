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
     private bool pressInit;
    [SerializeField,TextArea(3,8)]private string[] dialogueLines;
    [Range(0,1f)] [Min(0)][Tooltip("Tiempo de tipeo de letras del dialogo")]
    [SerializeField] private float typingTime;
    private bool isPlayerInrange;
    private bool didDialogueStart;
    private int lineIndex;

    // Update is called once per frame
    void Update()
    {
        if(dialoguePanel == null)
        {
            dialoguePanel = GameObject.FindGameObjectWithTag("PanelDialogue");
            dialogueTextGame = dialoguePanel.transform.GetChild(0).gameObject;
            pressButtonDialogue = dialoguePanel.transform.GetChild(1).gameObject;
            pressInitDialogue = dialoguePanel.transform.GetChild(2).gameObject;
            dialogueText = dialogueTextGame.GetComponent<TMP_Text>();
        }
        if(isPlayerInrange && Input.GetKeyDown(KeyCode.X))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
                pressInitDialogue.SetActive(false);
            }
            else if(dialogueText.text == dialogueLines[lineIndex])
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
            dialogueTextGame.SetActive(false);
            pressButtonDialogue.SetActive(false);
            pressInitDialogue.SetActive(true);
            // dialoguePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    private void StartDialogue()
    {
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
            isPlayerInrange = true;
            if (pressInit == false)
            {
                pressInitDialogue.SetActive(true);
                pressInit = true;
            }
            Debug.Log("Hola");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInrange = true;
            pressInit = false;
            pressInitDialogue.SetActive(false);
            Debug.Log("Hola");
        }
    }
}
