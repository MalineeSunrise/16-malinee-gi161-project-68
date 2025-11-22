using UnityEngine;

public class JissawChard : Items
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

    void Update()
    {
        if (playerInRange != null && Input.GetKeyDown(KeyCode.F))
        {
            Use(playerInRange);
            Destroy(gameObject);
        }
    }

    public override void Use(Player player)
    {
        
        if (player)
        {
            player.AddChard(ItemValue);
        }
    }
}
