using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float CurrentHealth { get; private set;}

    [SerializeField] public float MaxHp = 100f;

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

    public virtual void TakeDamage(int damage)
    {
        Health -= damage;

        IsDead();

        CurrentHealth = Health;
    }

    public void TakeDamage(int damage, int sanity)
    {
        Health -= damage;
        Sanity -= sanity;

        CurrentHealth = Health;

        IsDead();
    }

    public void LoseSanity(int amount)
    {
        Sanity -= amount;
    }

    public virtual bool IsDead()
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

    public virtual bool Checksanity()
    {
        if (Sanity <= 0)
        {
            return true;
        }
        return false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
