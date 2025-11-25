using UnityEngine;

public class Gloob : Enemy
{
    public float jumpRange = 3f;
    [SerializeField] private float attackRange = 6.0f;
    public float speed = 4.0f;

    public float jumpForce = 15f;
    public bool canJump = true;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("Gloob requires a SpriteRenderer component!");
        }
    }
    void Start()
    {
        IntializeGloob();
        SanityHit = 10;

        Behavior();
    }

    public void IntializeGloob()
    {
        base.Intialize(30, 30);
    }

    [System.Obsolete]
    public override void Behavior()
    {
        Player playerTarget = Player.Instance;

        if (playerTarget == null) return;

        Vector2 directionToPlayer = playerTarget.transform.position - transform.position;
        float distanceMagnitude = directionToPlayer.magnitude;

        if (directionToPlayer.x < 0)
        {
            if (spriteRenderer != null) spriteRenderer.flipX = true;
            else transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (directionToPlayer.x > 0)
        {
            if (spriteRenderer != null) spriteRenderer.flipX = false;
            else transform.localScale = new Vector3(1, 1, 1);
        }

        if (distanceMagnitude <= jumpRange)
        {
            JumpAttack(directionToPlayer.normalized);
        }

        else if (distanceMagnitude <= attackRange)
        {
            transform.position = Vector2.MoveTowards(
                this.transform.position,
                playerTarget.transform.position,
                speed * Time.deltaTime
            );
        }
    }

    [System.Obsolete]
    public void JumpAttack(Vector2 direction)
    {
        Rigidbody2D currentRb = GetComponent<Rigidbody2D>();

        if (currentRb != null && canJump)
        {
            currentRb.velocity = new Vector2(currentRb.velocity.x, 0f);
            Vector2 launchVelocity = new Vector2(direction.x * jumpForce, jumpForce * 0.5f);

            currentRb.AddForce(launchVelocity, ForceMode2D.Impulse);

            canJump = false;
            Invoke(nameof(ResetJump), 2.0f);

            if (currentRb != null && canJump)
            {
                canJump = false;
                Invoke(nameof(ResetJump), 2.0f);
            }
        }
    }

    private void ResetJump()
    {
        canJump = true;
    }

    void Update()
    {
        Behavior();
    }
}