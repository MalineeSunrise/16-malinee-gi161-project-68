using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("การเคลื่อนที่")]
    public float walkSpeed = 5f;
    public float jumpForce = 10f;

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
        // รับ Input
        moveInput = Input.GetAxisRaw("Horizontal");

        // กระโดด
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // พลิกตัว
        if (moveInput > 0 && !facingRight)
            Flip();
        else if (moveInput < 0 && facingRight)
            Flip();

        // อัพเดท Animation
        UpdateAnimation();
    }

    void FixedUpdate()
    {
        // เคลื่อนที่
        rb.velocity = new Vector2(moveInput * walkSpeed, rb.velocity.y);
    }

    void UpdateAnimation()
    {
        float speed = Mathf.Abs(moveInput); // ถ้า moveInput เป็น 0, speed จะเป็น 0
        anim.SetFloat("Speed", speed); // ส่งค่า Speed = 0 ไปที่ Animator
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}