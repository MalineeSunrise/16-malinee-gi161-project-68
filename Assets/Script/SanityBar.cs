using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class SanityBar : MonoBehaviour
{
    [SerializeField] private Slider sliderSanity;

    [SerializeField] private Character target;
    [SerializeField] private Character _characterTarget;

    void Start()
    {
        if (target == null)
        {
            target = GetComponent<Character>();

            if (target == null)
                target = GetComponentInParent<Character>();
        }
    }
    void Update()
    {
        if (_characterTarget != null && sliderSanity != null)
        {
            float t = _characterTarget.CalculateHealth();


            sliderSanity.value = t;
        }
    }
}
