using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
public abstract class DiaLogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public Button continueButton;

    protected string[] dialogue;
    protected int index = 0;
    public float wordSpeed = 0.03f;
    protected bool playerIsClose = false;
    private bool isTyping = false;

    void Start()
    {
        dialogue = GetDialogue();
        dialogueText.text = "";

        continueButton.onClick.AddListener(OnClickContinue);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(true);
                index = 0;                 // รีเซ็ตค่า
                dialogueText.text = "";     // เคลียร์หน้าจอ
                isTyping = false;
            }
        }
    }

    public void OnClickContinue()
    {
        if (!dialoguePanel.activeInHierarchy)
            return;

        // ถ้ากดตอนกำลังพิมพ์ → กระโดดแสดงข้อความทั้งหมด
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = dialogue[index];
            isTyping = false;
            return;
        }

        // ถ้าพิมพ์เสร็จแล้ว → ไปบรรทัดถัดไป
        if (!isTyping)
        {
            NextLine();
        }
    }

    void StartTyping()
    {
        dialogueText.text = "";
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        isTyping = true;

        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        isTyping = false;
    }

    void NextLine()
    {
        // ถ้าเพิ่งเปิด UI → เริ่มพิมพ์ประโยคแรก
        if (dialogueText.text == "" && index == 0)
        {
            StartTyping();
            return;
        }

        // ถ้ามีบทพูดถัดไป
        if (index < dialogue.Length - 1)
        {
            index++;
            StartTyping();
        }
        else
        {
            RemoveText();
        }
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        isTyping = false;
        dialoguePanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerIsClose = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            RemoveText();
        }
    }
    protected abstract string[] GetDialogue();

    //https://www.youtube.com/watch?v=1nFNOyCalzo
}



