using UnityEngine;

public class HPPotion : Items
{
    private Player playerInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerInRange = other.GetComponent<Player>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Player>() == playerInRange)
            playerInRange = null;
    }

    public override void Use(Player player)
    {
        if (player)
        {
            player.Heal(ItemValue);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange != null && Input.GetKeyDown(KeyCode.F))
        {
            Use(playerInRange);
            Destroy(gameObject);
        }
    }
}
