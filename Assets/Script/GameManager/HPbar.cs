using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    [SerializeField] private Slider sliderHP;
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

        if (targetCharacter != null && sliderHP != null)
        {
            float t = targetCharacter.CalculateHealth();
            sliderHP.value = t;
        }
    }

    // Reference https://youtu.be/__jkoSmVSMM?si=ZCH9beCw31o9KdsD
}