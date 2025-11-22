using UnityEngine;

public abstract class Items : MonoBehaviour
{
    [SerializeField]
    private int itemValue;

    protected int ItemValue { get { return itemValue; } set { itemValue = value; } }

    public abstract void Use(Player player);

    public void PickUp(Player player)
    {
        Use(player);
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
