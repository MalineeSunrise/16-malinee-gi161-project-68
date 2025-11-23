using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadUI : MonoBehaviour
{
    public GameObject DeadMenu;

    public bool isDead;

    void Start()
    {
        if (DeadMenu != null && DeadMenu.transform.parent != null)
        {
            DeadMenu.transform.parent.gameObject.SetActive(true);
        }

        DeadMenu.SetActive(false);
    }

    private void DestroyPlayerInstance()
    {
        if (Player.Instance != null)
        {
            Destroy(Player.Instance.gameObject);
        }
    }

    private void DelayedDestroyPlayer()
    {
        if (Player.Instance != null)
        {
            Destroy(Player.Instance.gameObject);
        }
    }

    public void ShowGameOver()
    {
        DeadMenu.SetActive(true);

        Time.timeScale = 0f;

        Invoke("DelayedDestroyPlayer", 0.01f);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f; 

        SceneManager.LoadScene("Bedroom1");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("_MainMenu");
    }
}
