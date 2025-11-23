using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
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

    [SerializeField] private GameObject promptUIGO;

    private bool playerIsClose = false;

    void Start()
    {
        if (transitionUIGO != null)
        {
            transitionUIGO.SetActive(false);
        }
        if (promptUIGO != null)
        {
            promptUIGO.SetActive(false);
        }
    }

    void Update()
    {
        if (playerIsClose && Input.GetKeyDown(KeyCode.E))
        {
            if (Player.Instance != null)
            {
                if (Player.Instance.JissawChard >= requiredChards)
                {
                    if (cloudAnimator != null && fadeOutClip != null)
                    {
                        StartCoroutine(PlayFadeAndLoad());
                    }
                }
                else
                {
                    Destroy(Player.Instance.gameObject);
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
        playerIsClose = false;
        if (promptUIGO != null) promptUIGO.SetActive(false);


        if (transitionUIGO != null)
            transitionUIGO.SetActive(true);

        yield return new WaitForSeconds(1.4f);

        SceneManager.LoadScene(nextSceneName);
    }

    private void HandlePromptDisplay()
    {
        if (promptUIGO == null) return;

        if (playerIsClose)
        {
            bool requirementMet = Player.Instance != null && Player.Instance.JissawChard >= requiredChards;

            promptUIGO.SetActive(true);

        }
        else
        {
            promptUIGO.SetActive(false);
        }
    }
}
