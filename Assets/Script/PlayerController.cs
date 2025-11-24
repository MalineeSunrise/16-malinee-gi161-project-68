using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float jumpForce;

    private bool isGrounded = true;

    private Rigidbody2D rb;
    private Animator anim;
    private float moveInput;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isGrounded = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (moveInput > 0 && !facingRight)
            Flip();
        else if (moveInput < 0 && facingRight)
            Flip();

        UpdateAnimation();
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * walkSpeed, rb.linearVelocity.y);
    }

    void UpdateAnimation()
    {
        float speed = Mathf.Abs(moveInput);

        anim.SetFloat("Speed", speed);

        anim.SetFloat("VerticalSpeed", rb.linearVelocity.y);

        anim.SetBool("IsGrounded", isGrounded);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}