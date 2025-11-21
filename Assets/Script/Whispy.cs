using UnityEngine;

public class Whispy : Enemy
{

    [SerializeField] Vector2 velocity;
    public Transform[] MovePoint;

    public override void Behavior()
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

    private void FixedUpdate()
    {
        Behavior();
    }

    public void Flip()
    {
        velocity.x *= -1; 
                          
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public override bool Checksanity()
    {
        throw new System.NotImplementedException();
    }

    public void IntializeWhispy()
    {
        base.Intialize(70, 70);
    }

    void Start()
    {
        IntializeWhispy();
        SanityHit = 5;


        velocity = new Vector2(-1.0f, 0.0f);
    }

    void Update()
    {
        
    }
}
