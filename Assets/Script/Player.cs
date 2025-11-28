using System;
using System.Collections;
using Unity.VisualScripting;
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

    private Vector2 warpTargetPosition;
    private bool isWarping = false;

    public int JissawChard { get { return jissawChard; } set { jissawChard = value; } }

    public static Player Instance { get; private set; }

    private DeadUI deadUIManager;


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
                Instance.TakeDamage(10);
                healthTimer = 0f;

                if (Health <= 0)
                {
                    Health = 0;
                    IsDead();
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
        IsDead(); // เรียก IsDead() ทุกครั้งที่โดนโจมตี
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

    public override bool IsDead()
    {
        if (Health <= 0)
        {
            Health = 0;
            Destroy(gameObject);

            if (deadUIManager != null)
            {
                Destroy(gameObject);
                deadUIManager.ShowGameOver();
                this.enabled = false;
            }
            return true;
        }
        return false;
    }

    [Obsolete]
    void Start()
    {
        IntializePlayer();
        ReloadTime = 1.0f;
        WaitTime = 0.0f;

        deadUIManager = FindObjectOfType<DeadUI>();
        if (deadUIManager == null)
        {
            Debug.LogError("DeadUI Manager not found...");
        }
    }

    void Update()
    {
        Shoot();
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void PrepareForWarp()
    {
        isWarping = true;
        this.enabled = false;
    }

    private IEnumerator SetPositionNextFrame(Vector3 targetPosition)
    {
        yield return null;

        this.transform.position = targetPosition;

        isWarping = false;

        this.enabled = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded called.");

        if (isWarping)
        {
            GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");

            if (spawnPoint != null)
            {
                StartCoroutine(SetPositionNextFrame(spawnPoint.transform.position));
            }
        }
    }

    public void ResetPlayer()
    {
        Health = MaxHp;
        Sanity = maxSanity;
        this.enabled = true;

        GameObject spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        if (spawnPoint != null)
            transform.position = spawnPoint.transform.position;
    }
}