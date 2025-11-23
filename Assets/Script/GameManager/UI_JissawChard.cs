using TMPro;
using UnityEngine;

public class UI_JissawChard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI chardText;

    private Player playerInstance;

    void Start()
    {
        if (chardText == null)
        {
            chardText = GetComponent<TextMeshProUGUI>();
        }

        playerInstance = Player.Instance;

        Player.OnChardCountChanged += UpdateChardDisplay;
    }

    void Update()
    {
        if (playerInstance != null)
        {
            if (playerInstance.Health <= 0)
            {
                gameObject.SetActive(false);

                enabled = false;
            }
        }
        else if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            enabled = false;
        }
    }

    private void UpdateChardDisplay(int newChardCount)
    {
        if (chardText != null)
        {
            chardText.text = newChardCount.ToString();
        }
    }


    void OnDestroy()
    {
        Player.OnChardCountChanged -= UpdateChardDisplay;
    }
}