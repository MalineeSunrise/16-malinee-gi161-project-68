using UnityEngine;
using System.Collections.Generic;

public class Whispy : Enemy
{
    [SerializeField] Vector2 velocity;
    public Transform[] MovePoint;

    [Header("Whispy Loot Setup")]
    [SerializeField] private GameObject guaranteedDropItem;

    [SerializeField] private GameObject randomItem1;
    [SerializeField, Range(0, 100)] private float randomItem1Chance;

    [SerializeField] private GameObject randomItem2;
    [SerializeField, Range(0, 100)] private float randomItem2Chance;

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
        DamageHit = 5;

        velocity = new Vector2(-4.0f, 0.0f);

        guaranteedLoot.Clear();
        lootTable.Clear();

        if (guaranteedDropItem != null)
        {
            guaranteedLoot.Add(guaranteedDropItem);
        }

        if (randomItem1 != null)
        {
            lootTable.Add(new LootItem { itemPrefabs = randomItem1, dropChance = randomItem1Chance });
        }

        if (randomItem2 != null)
        {
            lootTable.Add(new LootItem { itemPrefabs = randomItem2, dropChance = randomItem2Chance });
        }
    }
}