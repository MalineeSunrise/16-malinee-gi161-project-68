using UnityEngine;

public abstract class Enemy : Character
{
    public int SanityHit { get; protected set; }

    public int DamageHit { get; protected set; }
    public abstract void Behavior();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
