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

    void Start()
    {
        if (transitionUIGO != null)
            transitionUIGO.SetActive(false);

        if (promptUIGO != null)
            promptUIGO.SetActive(false);
    }

    [System.Obsolete]
    void Update()
    {
        if (playerIsClose && Input.GetKeyDown(KeyCode.E) && !isWarping)
        {
            if (Player.Instance != null)
            {
                if (Player.Instance.JissawChard >= requiredChards)
                {
                    if (cloudAnimator != null && fadeOutClip != null)
                    {
                        isWarping = true;
                        Player.Instance.PrepareForWarp();
                        StartCoroutine(PlayFadeAndLoad());
                    }
                }
                else
                {
                    DeadUI deadUIManager = FindObjectOfType<DeadUI>();
                    if (deadUIManager != null)
                        deadUIManager.ShowGameOver();

                    Player.Instance.enabled = false;

                    var cam = FindObjectOfType<CameraFollow>();
                    cam?.StopFollow();
                }

            }
        }

        HandlePromptDisplay();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }

    private IEnumerator PlayFadeAndLoad()
    {
        if (transitionUIGO != null)
            transitionUIGO.SetActive(true);

        if (cloudAnimator != null && fadeOutClip != null)
        {
            cloudAnimator.Play(fadeOutClip.name);
            yield return new WaitForSeconds(fadeOutClip.length);
        }
        else
        {
            yield return new WaitForSeconds(1.4f);
        }

        if (Player.Instance != null)
        {
            Player.Instance.PrepareForWarp();
        }

        SceneManager.LoadScene(nextSceneName);
    }


    private void HandlePromptDisplay()
    {
        if (promptUIGO == null || isWarping) return;

        if (playerIsClose)
        {
            promptUIGO.SetActive(true);
        }
        else
        {
            promptUIGO.SetActive(false);
        }
    }
}
