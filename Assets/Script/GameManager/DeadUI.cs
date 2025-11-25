using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadUI : MonoBehaviour
{
    public static DeadUI Instance { get; private set; }
    public GameObject DeadMenu;

    private Player player;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        if (DeadMenu != null)
            DeadMenu.SetActive(false);

        player = Player.Instance;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = Player.Instance;

        if (DeadMenu != null)
            DeadMenu.SetActive(false);
    }

    public void ShowGameOver()
    {
        if (DeadMenu != null)
            DeadMenu.SetActive(true);

        Time.timeScale = 0f;

        if (player != null)
            player.enabled = false;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1f;
        StartCoroutine(ResetAfterSceneLoad());
        SceneManager.LoadScene("Bedroom1");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("_MainMenu");
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private IEnumerator ResetAfterSceneLoad()
    {
        yield return new WaitUntil(() => Player.Instance != null);

        player = Player.Instance;

        if (player != null)
            player.ResetPlayer();
    }
}
