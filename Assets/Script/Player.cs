using UnityEngine;

public class Player : Character, IShootable
{
    [field: SerializeField] public GameObject Bullet { get; set; }

    [field: SerializeField] public Transform ShootPoint { get; set; }

    [field: SerializeField] public GameObject Bullet2 { get; set; }

    [field: SerializeField] public Transform ShootPoint2 { get; set; }

    public float ReloadTime { get; set; }

    public float WaitTime { get; set; }

    [SerializeField] private int jissawChard = 0;
    public int JissawChard { get { return jissawChard; } set { jissawChard = value; } }

    public void AddChard(int value)
    {
        JissawChard += value;
    }

    public void Heal(int value)
    {
        Health += value;
    }

    public void addSanity(int value)
    {
        Sanity += value;
    }

    public override bool Checksanity()
    {
        throw new System.NotImplementedException();
    }

    public void IntializePlayer()
    {
        base.Intialize(100, 100);
    }

    public void OnHitWith(Enemy enemy)
    {
        LoseSanity(enemy.SanityHit);
        TakeDamage(enemy.DamageHit);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            OnHitWith(enemy);

            Vector2 pushDirection = (transform.position - enemy.transform.position).normalized;

            rb.AddForce(pushDirection * 30f, ForceMode2D.Impulse);


        }
    }

    public void FixedUpdate()
    {
        WaitTime += Time.fixedDeltaTime;
    }

    public void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && WaitTime >= ReloadTime)
        {
            var bullet = Instantiate(Bullet, ShootPoint.position, Quaternion.identity);
            LightMagic light = bullet.GetComponent<LightMagic>();
            if (light != null)
                light.InitWeapon(0, this, 10);

            WaitTime = 0.0f;
        }

        if (Input.GetButtonDown("Fire2") && WaitTime >= ReloadTime)
        {
            var bullet = Instantiate(Bullet2, ShootPoint2.position, Quaternion.identity);
            MagicDamage magic = bullet.GetComponent<MagicDamage>();
            if (magic != null)
                magic.InitWeapon(0, this, 10);

            WaitTime = 0.0f;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IntializePlayer();

        ReloadTime = 1.0f;
        WaitTime = 0.0f; 
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }
}
