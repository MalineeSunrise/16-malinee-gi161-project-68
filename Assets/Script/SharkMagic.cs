using UnityEngine;
using System;

public class SharkMagic : Weapon
{
    [SerializeField] private float speed = 5.0f;

    private Transform target;

    [SerializeField] private float maxLifeTime = 2.0f;
    private float currentLifeTime = 0.0f;

    public override void Move()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 directionToTarget = (target.position - transform.position).normalized;

        Vector2 deltaPosition = directionToTarget * speed * Time.deltaTime;

        transform.position = (Vector2)transform.position + deltaPosition;
    }

    public override void OnHitWith(Character character)
    {
        if (character is Player)
        {
            character.TakeDamage(this.damage, this.sanity);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        sanity = 10;
        damage = 20;
    }

    void Update()
    {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= maxLifeTime)
        {
            Destroy(gameObject);
            return;
        }

        if (target == null)
        {
            if (Player.Instance != null)
            {
                target = Player.Instance.transform;
            }

            if (target == null)
            {
                return;
            }
        }

        Move();
    }
}