using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenu;

    [Header("State")]
    public bool isPause;

    void Start()
    {
        // ป้องกัน null
        if (pauseMenu != null)
            pauseMenu.SetActive(false);
        else
            Debug.LogWarning("PauseMenu is not assigned in PauseGame script!");

        Time.timeScale = 1f;
        isPause = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape Key Pressed!");

            if (isPause)
            {
                ResumeGame();
            }
            else
            {
                PauseGameAction();
            }
        }
    }

    public void PauseGameAction()
    {
        if (pauseMenu != null)
            pauseMenu.SetActive(true);
        else
            Debug.LogWarning("PauseMenu is not assigned! Cannot open pause menu.");

        Time.timeScale = 0f;
        isPause = true;
    }

    public void ResumeGame()
    {
        Debug.Log("--- ResumeGame is Executed ---");

        if (pauseMenu != null)
            pauseMenu.SetActive(false);
        else
            Debug.LogWarning("PauseMenu is not assigned! Cannot close pause menu.");

        Time.timeScale = 1f;
        isPause = false;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("_MainMenu");

        if (Player.Instance != null)
            Destroy(Player.Instance.gameObject);
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    // Reference https://www.youtube.com/watch?v=9dYDBomQpBQ
}
