using UnityEngine;
using System.Collections.Generic;

public class Whispy : Enemy
{
    [SerializeField] Vector2 velocity;
    public Transform[] MovePoint;

    private void FixedUpdate()
    {
        Behavior();
    }

    public override void Behavior()
    {
        if (rb != null)
        {
            rb.MovePosition(rb.position + velocity * Time.deltaTime);

            if (velocity.x < 0 && rb.position.x <= MovePoint[0].position.x)
            {
                Flip();
            }

            if (velocity.x > 0 && rb.position.x >= MovePoint[1].position.x)
            {
                Flip();
            }
        }
    }

    public void Flip()
    {
        velocity.x *= -1;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void IntializeWhispy()
    {
        base.Intialize(70, 70);
    }

    void Start()
    {
        IntializeWhispy();

        SanityHit = 5;
        DamageHit = 10;

        velocity = new Vector2(-4.0f, 0.0f);
    }
}