using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
public abstract class DiaLogue : MonoBehaviour
{
    protected string[] dialogue;

    [Header("Dialogue Settings")]
    public float wordSpeed = 0.03f; 

    protected bool playerIsClose = false;

    void Start()
    {
        dialogue = GetDialogue();

        if (UIDL_manager.Instance != null)
        {
            UIDL_manager.Instance.wordSpeed = wordSpeed;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (UIDL_manager.Instance != null)
            {
                UIDL_manager.Instance.StartDialogue(dialogue);
            }
        }
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
            if (UIDL_manager.Instance != null)
            {
                UIDL_manager.Instance.RemoveText();
            }
        }
    }

    protected abstract string[] GetDialogue();

    // Reference https://www.youtube.com/watch?v=1nFNOyCalzo
}



