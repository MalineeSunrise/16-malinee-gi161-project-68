using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class HPbar : MonoBehaviour
{
    [SerializeField] private Slider sliderHP;

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
        if (_characterTarget != null && sliderHP != null)
        {
            float t = _characterTarget.CalculateHealth();


            sliderHP.value = t;
        }
    }
}
