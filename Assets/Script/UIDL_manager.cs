using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIDL_manager : MonoBehaviour
{
    public static UIDL_manager Instance;

    [Header("UI References")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button continueButton;

    private string[] currentDialogue;
    private int index;

    public float wordSpeed = 0.03f;

    private bool isTyping = false;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        dialoguePanel.SetActive(false);

        continueButton.onClick.AddListener(OnClickContinue);
    }

    public void StartDialogue(string[] dialogueLines)
    {
        if (dialoguePanel.activeInHierarchy) return;

        currentDialogue = dialogueLines;
        index = 0;
        dialogueText.text = "";
        dialoguePanel.SetActive(true);

        StartTyping();
    }

    public void OnClickContinue()
    {
        if (!dialoguePanel.activeInHierarchy) return;

        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentDialogue[index];
            isTyping = false;
        }
        else
        {
            NextLine();
        }
    }

    private void NextLine()
    {
        if (index < currentDialogue.Length - 1)
        {
            index++;
            StartTyping();
        }
        else
        {
            RemoveText(); 
        }
    }

    private void StartTyping()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        isTyping = true;

        if (currentDialogue != null && index < currentDialogue.Length)
        {
            foreach (char letter in currentDialogue[index].ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(wordSpeed);
            }
        }

        isTyping = false;
    }

    public void RemoveText()
    {
        StopAllCoroutines();
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }
}
