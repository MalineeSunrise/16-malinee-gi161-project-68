using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character, IShootable
{
    [field: SerializeField] public GameObject Bullet { get; set; }
    [field: SerializeField] public Transform ShootPoint { get; set; }
    [field: SerializeField] public GameObject Bullet2 { get; set; }
    [field: SerializeField] public Transform ShootPoint2 { get; set; }

    public float ReloadTime { get; set; }
    public float WaitTime { get; set; }

    [SerializeField] private int jissawChard = 0;
    public static Action<int> OnChardCountChanged;

    private bool isLosingHealth = false;
    private float healthTimer = 0f;

    public int JissawChard { get { return jissawChard; } set { jissawChard = value; } }

    public static Player Instance { get; private set; }

    public void AddChard(int value)
    {
        JissawChard += value;
        OnChardCountChanged?.Invoke(JissawChard);
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
        if (Sanity <= 0)
        {
            if (!isLosingHealth)
            {
                isLosingHealth = true;
                healthTimer = 0f;
            }

            healthTimer += Time.fixedDeltaTime;
            if (healthTimer >= 5f)
            {
                Health -= 10;
                healthTimer = 0f;

                if (Health <= 0)
                {
                    Health = 0;
                }
            }

            return true;
        }
        else
        {
            isLosingHealth = false;
            healthTimer = 0f;
            return false;
        }
    }

    public void IntializePlayer()
    {
        base.Intialize(100, 100);
    }

    public void OnHitWith(Enemy enemy)
    {
        TakeDamage(enemy.DamageHit, enemy.SanityHit);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            OnHitWith(enemy);

            Vector2 pushDirection = (transform.position - enemy.transform.position).normalized;
            rb.AddForce(pushDirection * 10f, ForceMode2D.Impulse);
        }
    }

    public void FixedUpdate()
    {
        WaitTime += Time.fixedDeltaTime;
        Checksanity();
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

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnPointGO = GameObject.FindWithTag("SpawnPoint");

        if (spawnPointGO != null)
        {
            this.transform.position = spawnPointGO.transform.position;
        }
        else
        {
            Debug.LogWarning("SpawnPoint not found in scene: " + scene.name + ". Player will use the last position.");
        }
    }

    void Start()
    {
        IntializePlayer();
        ReloadTime = 1.0f;
        WaitTime = 0.0f;
    }

    void Update()
    {
        Shoot();
    }
}
