using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float CurrentHealth { get; private set;}

    [SerializeField] private float MaxHp = 100f;

    private float sanity;
    public float Sanity
    {
        get { return sanity; }
        set { sanity = (value < 0) ? 0 : value; }
    }

    [SerializeField] public float maxSanity;

    private float hearlth;
    public float Health
    {
        get { return hearlth; }
        set { hearlth = (value < 0) ? 0 : value; }
    }

    protected Animator anim;
    protected Rigidbody2D rb;

    public void Intialize(int startHealth, int sanity)
    {
        Health = startHealth;
        Sanity = sanity;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    public void TakeDamage(int damage)
    {
        Health -= damage;

        IsDead();

        if (CurrentHealth < 0)
            CurrentHealth = 0;

        CurrentHealth = Health;
    }

    public void TakeDamage(int damage, int sanity)
    {
        Health -= damage;
        Sanity -= sanity;
    }

    public void LoseSanity(int amount)
    {
        Sanity -= amount;
    }

    public bool IsDead()
    {
        if (Health <= 0)
        {
            Destroy(this.gameObject);
            return true;
        }
        else { return false; }
    }

    public float CalculateHealth()
    {
        return Health / MaxHp;
    }

    public float CalculateSanity()
    {
        return Sanity /maxSanity;
    }

    public abstract bool Checksanity();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
