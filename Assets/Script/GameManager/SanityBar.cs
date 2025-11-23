using UnityEngine;
using UnityEngine.UI;

public class SanityBar : MonoBehaviour
{
    [SerializeField] private Slider sliderSanity;
    private Character targetCharacter;

    void Awake()
    {
        FindTarget();
    }

    private void FindTarget()
    {
        targetCharacter = GetComponentInParent<Character>();

        if (targetCharacter == null && Player.Instance != null)
        {
            if (GetComponentInParent<Player>() == Player.Instance)
            {
                targetCharacter = Player.Instance;
            }
        }

        if (targetCharacter == null)
        {
            
        }
    }

    void Update()
    {
        if (targetCharacter == null)
        {
            FindTarget();
        }

        if (targetCharacter != null && sliderSanity != null)
        {
            float t = targetCharacter.CalculateSanity();
            sliderSanity.value = t;
        }
    }
}