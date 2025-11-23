using TMPro;
using UnityEngine;

public class UI_JissawChard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI chardText;

    void Start()
    {
        if (chardText == null)
        {
            chardText = GetComponent<TextMeshProUGUI>();
        }

        Player.OnChardCountChanged += UpdateChardDisplay;
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