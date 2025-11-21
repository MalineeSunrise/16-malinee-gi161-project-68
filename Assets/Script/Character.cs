using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public float CurrentHealth { get; private set;}

    [SerializeField] private float MaxHp = 100f;
    public float Sanity { get; private set; }
    public float maxSanity { get; private set; } = 100f;


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

        Debug.Log($"{this.name} is initialize Health Health : {this.Health} and {this.Sanity}");


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

    public void LoseSanity(int sanity)
    {
        Sanity -= sanity;
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
