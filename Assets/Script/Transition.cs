using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Transition : MonoBehaviour
{
    [Header("Transition Objects")]

    [SerializeField] private GameObject transitionUIGO;

    [SerializeField] private Animator cloudAnimator;

    [SerializeField] private AnimationClip fadeOutClip;

    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName;

    private bool playerIsClose = false;

    void Start()
    {
        if (transitionUIGO != null)
        {
            transitionUIGO.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            if (cloudAnimator != null && fadeOutClip != null)
            {
                StartCoroutine(PlayFadeAndLoad());
            }
        }
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
        // เปิด UI Panel Fullscreen
        if (transitionUIGO != null)
            transitionUIGO.SetActive(true);

        // รอเวลาที่ต้องการ (เหมือน Fade-Out)
        yield return new WaitForSeconds(1.4f); // ปรับตามความยาว Fade ที่ต้องการ

        // โหลด Scene ใหม่ทันที
        SceneManager.LoadScene(nextSceneName);
    }
}