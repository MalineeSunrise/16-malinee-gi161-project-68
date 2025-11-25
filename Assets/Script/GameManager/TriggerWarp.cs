using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TriggerWarp : MonoBehaviour
{
    [Header("Transition Objects")]
    [SerializeField] private GameObject transitionUIGO;
    [SerializeField] private Animator cloudAnimator;
    [SerializeField] private AnimationClip fadeOutClip;

    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName;

    [Header("Chard Requirement")]
    [SerializeField] private int requiredChards = 8;

    [Header("UI Prompt")]
    [SerializeField] private GameObject promptUIGO;

    private bool playerIsClose = false;
    private bool isWarping = false;
    private bool triggerUsed = false;

    void Start()
    {
        if (transitionUIGO != null)
            transitionUIGO.SetActive(false);

        if (promptUIGO != null)
            promptUIGO.SetActive(false);
    }

    void Update()
    {
        if (playerIsClose && Input.GetKeyDown(KeyCode.E) && !isWarping && !triggerUsed)
        {
            if (Player.Instance != null)
            {
                if (Player.Instance.JissawChard >= requiredChards)
                {
                    isWarping = true;
                    triggerUsed = true;

                    // ปิด Trigger เพื่อไม่ให้กดซ้ำ
                    DisableTrigger();

                    // เรียก Coroutine เพื่อจัดลำดับ fade → destroy → warp
                    StartCoroutine(FadeAnimateDestroyAndWarp());
                }
                else
                {
                    DeadUI deadUIManager = FindObjectOfType<DeadUI>();
                    if (deadUIManager != null)
                        deadUIManager.ShowGameOver();

                    Player.Instance.enabled = false;
                }
            }
        }

        HandlePromptDisplay();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerIsClose = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerIsClose = false;
    }

    private IEnumerator FadeAnimateDestroyAndWarp()
    {
        if (transitionUIGO != null)
            transitionUIGO.SetActive(true);

        if (cloudAnimator != null && fadeOutClip != null)
        {
            cloudAnimator.Play(fadeOutClip.name, 0, 0f);
        }

        yield return new WaitForSeconds(1.4f);

        if (Player.Instance != null)
        {
            // ปิด UI ของ Player ถ้ามี
            foreach (Transform child in Player.Instance.transform)
            {
                if (child.CompareTag("Player")) // แก้เป็น Player
                {
                    child.gameObject.SetActive(false);
                }
            }

            Player.Instance.gameObject.SetActive(false);
            Player.Instance.PrepareForWarp();
        }

        SceneManager.LoadScene(nextSceneName);
    }

    private void HandlePromptDisplay()
    {
        if (promptUIGO == null || isWarping) return;

        promptUIGO.SetActive(playerIsClose);
    }

    private void DisableTrigger()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }
}
